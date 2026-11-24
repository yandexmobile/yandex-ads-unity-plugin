using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.Callbacks;
using UnityEngine;

namespace YandexAdsEditor
{
    [InitializeOnLoad]
    public sealed class YandexMobileAdsEdmPostprocessor : IPreprocessBuildWithReport
    {
        private const string RestoreKey = "YandexMobileAds.RestoreEdmSpm";
        private const string RestoreEmptyPodfileKey = "YandexMobileAds.RestoreEdmEmptyPodfile";
        private static bool usesSwiftPackages;

        static YandexMobileAdsEdmPostprocessor()
        {
            // Failed builds do not invoke post-build callbacks. SessionState also survives domain reloads.
            EditorApplication.update += RestoreAfterBuild;
        }

        public int callbackOrder { get { return 0; } }

        private static Type ResolverType()
        {
            return AppDomain.CurrentDomain.GetAssemblies().Select(assembly => assembly.GetType("Google.IOSResolver"))
                .FirstOrDefault(type => type != null);
        }

        private static PropertyInfo SpmSetting(Type resolver)
        {
            return resolver == null ? null : resolver.GetProperty("SwiftPackageManagerEnabled", BindingFlags.Public | BindingFlags.Static);
        }

        private static bool ResolverFlag(Type resolver, string name)
        {
            var property = resolver.GetProperty(name, BindingFlags.Public | BindingFlags.Static);
            return property != null && property.CanRead && property.PropertyType == typeof(bool) && (bool)property.GetValue(null, null);
        }

        public void OnPreprocessBuild(BuildReport report)
        {
            if (report.summary.platform != BuildTarget.iOS)
                return;
            usesSwiftPackages = false;
            var resolver = ResolverType();
            if (resolver == null)
                throw new BuildFailedException("Install External Dependency Manager for Unity (EDM4U) to resolve Yandex Mobile Ads iOS dependencies.");
            if (!ResolverFlag(resolver, "Enabled"))
                throw new BuildFailedException("EDM4U iOS Resolver is unavailable. Install Unity iOS Build Support and a compatible EDM4U before exporting.");
            if (!ResolverFlag(resolver, "CocoapodsIntegrationEnabled"))
                throw new BuildFailedException("Enable CocoaPods Integration (Project or Workspace) in Assets > External Dependency Manager > iOS Resolver > Settings. EDM4U requires it to refresh both Pods and SPM dependencies.");
            var setting = SpmSetting(resolver);
            var supported = false;
#if UNITY_IOS && UNITY_2021_3_OR_NEWER
            var product = resolver.Assembly.GetType("Google.SwiftPackage");
            var hasProjectApi = SupportsSpmProjectApi(typeof(UnityEditor.iOS.Xcode.PBXProject));
            supported = hasProjectApi && setting != null && setting.CanRead && setting.CanWrite && product != null &&
                product.GetProperty("TargetName") != null && product.GetProperty("TargetsMain") != null &&
                product.GetProperty("TargetsFramework") != null && product.GetProperty("ReplacesPod") != null;
#endif
            var enabled = setting != null && setting.CanRead && (bool)setting.GetValue(null, null);
            var files = ImportedDependencyFiles();
            try
            {
                usesSwiftPackages = YandexMobileAdsIosDependencyPolicy.UseSwiftPackages(files, supported, enabled);
            }
            catch (Exception exception)
            {
                throw new BuildFailedException("Yandex Mobile Ads iOS dependency selection failed: " + exception.Message);
            }
            // EDM otherwise runs CocoaPods even when SPM replaced every pod.
            var emptyPodfileSetting = resolver.GetProperty("AllowEmptyPodfileGeneration", BindingFlags.Public | BindingFlags.Static);
            if (usesSwiftPackages && emptyPodfileSetting != null && emptyPodfileSetting.CanRead &&
                emptyPodfileSetting.CanWrite && emptyPodfileSetting.PropertyType == typeof(bool) &&
                (bool)emptyPodfileSetting.GetValue(null, null))
            {
                SessionState.SetBool(RestoreEmptyPodfileKey, true);
                emptyPodfileSetting.SetValue(null, false, null);
            }
            if (!usesSwiftPackages && enabled)
            {
                if (!setting.CanWrite)
                    throw new BuildFailedException("Disable Swift Package Manager Integration in EDM4U iOS Resolver settings for this CocoaPods export.");
                SessionState.SetBool(RestoreKey, true);
                setting.SetValue(null, false, null);
                Debug.Log("Yandex Mobile Ads: using CocoaPods for this export because SPM is unavailable or an installed pod has no SPM replacement.");
            }
        }

        private static System.Collections.Generic.IEnumerable<string> ImportedDependencyFiles()
        {
            return AssetDatabase.GetAllAssetPaths()
                .Where(path => path.EndsWith("Dependencies.xml", StringComparison.Ordinal) &&
                    path.Contains("/Editor/"))
                .Select(DependencyFilePath).Where(path => path != null && File.Exists(path)).Distinct();
        }

        private static string DependencyFilePath(string assetPath)
        {
            if (!assetPath.StartsWith("Packages/", StringComparison.Ordinal))
                return assetPath;
            // Resolve only imported assets; Samples~ and Documentation~ are not dependencies.
            var package = UnityEditor.PackageManager.PackageInfo.FindForAssetPath(assetPath);
            return package == null ? null : Path.Combine(package.resolvedPath,
                assetPath.Substring(package.assetPath.Length + 1));
        }

        internal static bool SupportsSpmProjectApi(Type projectType)
        {
            var versionArguments = new[] { typeof(string), typeof(string) };
            return new[] { "AddRemotePackageReferenceAtVersion", "AddRemotePackageReferenceAtVersionUpToNextMinor",
                "AddRemotePackageReferenceAtVersionUpToNextMajor" }.All(name => projectType.GetMethod(name, versionArguments) != null) &&
                projectType.GetMethod("AddRemotePackageFrameworkToProject", new[] { typeof(string), typeof(string), typeof(string), typeof(bool) }) != null;
        }

        // EDM attaches SPM products at order 35 using Unity's target lookup APIs.
        [PostProcessBuild(34)]
        public static void ValidateSpmTargets(BuildTarget target, string path)
        {
#if UNITY_IOS && UNITY_2021_3_OR_NEWER
            if (target != BuildTarget.iOS || !usesSwiftPackages)
                return;
            var project = new UnityEditor.iOS.Xcode.PBXProject();
            project.ReadFromFile(UnityEditor.iOS.Xcode.PBXProject.GetPBXProjectPath(path));
            RequireEdmTargets(project);
#endif
        }

#if UNITY_IOS && UNITY_2021_3_OR_NEWER
        internal static void RequireEdmTargets(UnityEditor.iOS.Xcode.PBXProject project)
        {
            if (string.IsNullOrEmpty(project.GetUnityFrameworkTargetGuid()) || string.IsNullOrEmpty(project.GetUnityMainTargetGuid()))
                throw new BuildFailedException("EDM4U cannot identify both Unity targets in the exported Xcode project. " +
                    "Export to a clean directory and run custom target-renaming callbacks after the Yandex postprocessor " +
                    "(callback order greater than 1000), so EDM can attach the SPM dependencies first.");
        }
#endif

        private static void RestoreAfterBuild()
        {
            if (!BuildPipeline.isBuildingPlayer)
                RestoreDependencyManagerSettings();
        }

        public static void RestoreDependencyManagerSettings()
        {
            if (!SessionState.GetBool(RestoreKey, false) && !SessionState.GetBool(RestoreEmptyPodfileKey, false))
                return;
            var resolver = ResolverType();
            RestoreEnabledPreference(SpmSetting(resolver), RestoreKey);
            RestoreEnabledPreference(resolver == null ? null : resolver.GetProperty(
                "AllowEmptyPodfileGeneration", BindingFlags.Public | BindingFlags.Static), RestoreEmptyPodfileKey);
        }

        private static void RestoreEnabledPreference(PropertyInfo setting, string key)
        {
            if (!SessionState.GetBool(key, false) || setting == null || !setting.CanWrite)
                return;
            setting.SetValue(null, true, null);
            SessionState.EraseBool(key);
        }

        public static bool UsesYandexPods(string buildPath)
        {
            var podfile = Path.Combine(buildPath, "Podfile");
            if (!File.Exists(podfile))
                return false;
            return Regex.IsMatch(File.ReadAllText(podfile),
                @"^\s*pod\s+['""](?:(?:Yandex|YX)MobileAds(?:AdQuality|Mediation|AdMobAdapters)?|[^'""]*(?:Yandex|YX)MobileAdsAdapters)(?:/[^'""]*)?['""]", RegexOptions.Multiline);
        }

        // EDM generates the Podfile at 40 and installs Pods at 50. Dependencies added
        // through AddPod or other late callbacks were not visible in the XML preflight.
        [PostProcessBuild(45)]
        public static void ValidateGeneratedPodfile(BuildTarget target, string path)
        {
            if (target != BuildTarget.iOS || !usesSwiftPackages)
                return;
            var podfile = Path.Combine(path, "Podfile");
            if (File.Exists(podfile) && Regex.IsMatch(File.ReadAllText(podfile),
                @"^[ \t]*pod(?:[ \t]+|\()", RegexOptions.Multiline))
                throw new BuildFailedException("EDM4U generated CocoaPods dependencies after SPM was selected. " +
                    "A plugin may register pods programmatically or during export. Disable Swift Package Manager Integration " +
                    "in Assets > External Dependency Manager > iOS Resolver > Settings and export to a clean directory " +
                    "to resolve all dependencies through CocoaPods.");
        }

#if UNITY_IOS && UNITY_2021_3_OR_NEWER
        private static void CopySwiftPackageResources(UnityEditor.iOS.Xcode.PBXProject project, string buildPath)
        {
            // Unity loads UnityFramework dynamically, so Xcode does not propagate its SPM
            // resource bundles to the app. Discover them after the dependencies are built,
            // without linking static SDK products into the application a second time.
            const string scriptName = "yandex_mobileads_copy_spm_resources.sh";
            var assetPath = AssetDatabase.GetAllAssetPaths().SingleOrDefault(asset =>
                asset.EndsWith("/YandexMobileAds/Editor/" + scriptName, StringComparison.Ordinal));
            var sourcePath = assetPath == null ? null : DependencyFilePath(assetPath);
            if (sourcePath == null || !File.Exists(sourcePath))
                throw new BuildFailedException("Yandex Mobile Ads resource copy script is missing. Reimport the complete SDK package.");
            File.Copy(sourcePath, Path.Combine(buildPath, scriptName), true);
            const string script = "/bin/sh \"${PROJECT_DIR}/yandex_mobileads_copy_spm_resources.sh\"";
            var main = project.GetUnityMainTargetGuid();
            project.AddShellScriptBuildPhase(main, "[Yandex Mobile Ads] Copy Swift Package Resources", "/bin/sh", script);
            // The bundle names are determined by the resolved packages at Xcode build time.
            project.SetBuildProperty(main, "ENABLE_USER_SCRIPT_SANDBOXING", "NO");
        }

#endif

        [PostProcessBuild(1000)]
        public static void OnPostprocessBuild(BuildTarget target, string path)
        {
            if (target != BuildTarget.iOS)
                return;
            try
            {
#if UNITY_IOS && UNITY_2021_3_OR_NEWER
                if (!usesSwiftPackages || UsesYandexPods(path))
                    return;
                var projectPath = UnityEditor.iOS.Xcode.PBXProject.GetPBXProjectPath(path);
                var project = new UnityEditor.iOS.Xcode.PBXProject();
                project.ReadFromFile(projectPath);
                RequireEdmTargets(project);
                CopySwiftPackageResources(project, path);
                var framework = project.GetUnityFrameworkTargetGuid();
                var main = project.GetUnityMainTargetGuid();
                project.UpdateBuildProperty(framework, "OTHER_LDFLAGS", new[] { "-ObjC" }, new string[0]);
                project.SetBuildProperty(framework, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "NO");
                project.SetBuildProperty(main, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "YES");
                project.WriteToFile(projectPath);
#endif
            }
            finally { RestoreDependencyManagerSettings(); }
        }
    }
}
