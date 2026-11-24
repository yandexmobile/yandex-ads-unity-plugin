#if UNITY_IOS
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

namespace YandexAdsEditor
{
    public static class AppLovinMobileAdsPostprocessor
    {
        [PostProcessBuild(1000)]
        public static void OnPostprocessBuild(BuildTarget target, string path)
        {
            if (target != BuildTarget.iOS)
                return;

            var projectPath = PBXProject.GetPBXProjectPath(path);
            var project = new PBXProject();
            project.ReadFromFile(projectPath);
            var main = project.GetUnityMainTargetGuid();
            if (string.IsNullOrEmpty(main))
                throw new BuildFailedException("Cannot find the Unity application target for the Yandex AppLovin MAX adapter.");

            // MAX discovers the static adapter by class name; retain its Objective-C implementation.
            project.UpdateBuildProperty(main, "OTHER_LDFLAGS", new[] { "$(inherited)", "-ObjC" }, new string[0]);
            project.WriteToFile(projectPath);
        }
    }
}
#endif
