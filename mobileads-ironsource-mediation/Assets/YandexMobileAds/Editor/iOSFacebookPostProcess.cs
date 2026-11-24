using UnityEditor;
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;
using UnityEditor.Callbacks;
using System.IO;

public class PostProcessBuildFacebook
{
    // EDM4U installs CocoaPods at order 50; framework embedding must run afterward.
    private const int AfterCocoaPodsInstallation = 100;

    [PostProcessBuild(AfterCocoaPodsInstallation)]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string pathToBuiltProject)
    {
        if (buildTarget == BuildTarget.iOS)
        {
            var podfile = Path.Combine(pathToBuiltProject, "Podfile");
            if (!File.Exists(podfile) || !System.Text.RegularExpressions.Regex.IsMatch(File.ReadAllText(podfile),
                @"^\s*pod\s+['""](?:(?:Yandex|YX)MobileAds(?:AdQuality|Mediation|AdMobAdapters)?|[^'""]*(?:Yandex|YX)MobileAdsAdapters)(?:/[^'""]*)?['""]",
                System.Text.RegularExpressions.RegexOptions.Multiline)) return;
            string frameworkName = "FBAudienceNetwork.xcframework";
            string frameworkPath = Path.Combine("Pods", "FBAudienceNetwork", "Dynamic", frameworkName);

            string projPath = PBXProject.GetPBXProjectPath(pathToBuiltProject);
            PBXProject proj = new PBXProject();
            proj.ReadFromString(File.ReadAllText(projPath));

            string unityFrameworkTargetGuid = proj.GetUnityFrameworkTargetGuid();
            string mainTargetGuid = proj.GetUnityMainTargetGuid();

            string fileGuid = proj.AddFile(frameworkPath, "Frameworks/" + frameworkName, PBXSourceTree.Source);
            proj.AddFileToBuildSection(unityFrameworkTargetGuid, proj.GetFrameworksBuildPhaseByTarget(unityFrameworkTargetGuid), fileGuid);

            proj.AddCopyFilesBuildPhase(mainTargetGuid, "Embed Frameworks", "", "10");
            PBXProjectExtensions.AddFileToEmbedFrameworks(proj, mainTargetGuid, fileGuid);

            File.WriteAllText(projPath, proj.WriteToString());
        }
    }
}
