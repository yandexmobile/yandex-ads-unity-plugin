using UnityEditor;
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;
using UnityEditor.Callbacks;
using System.IO;
using UnityEngine;

public class PostProcessBuildBigoAds
{
    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string pathToBuiltProject)
    {
        if (buildTarget == BuildTarget.iOS)
        {
            string frameworkName = "BigoADS.xcframework";
            string frameworkPath = Path.Combine("Pods", "BigoADS", "BigoADS", frameworkName);

            string projPath = PBXProject.GetPBXProjectPath(pathToBuiltProject);
            PBXProject proj = new PBXProject();
            proj.ReadFromString(File.ReadAllText(projPath));

            string unityFrameworkTargetGuid = proj.GetUnityFrameworkTargetGuid();
            string mainTargetGuid = proj.GetUnityMainTargetGuid(); 

            string fileGuid = proj.AddFile(frameworkPath, "Frameworks/" + frameworkName, PBXSourceTree.Source);
            proj.AddFileToBuildSection(unityFrameworkTargetGuid, proj.GetFrameworksBuildPhaseByTarget(unityFrameworkTargetGuid), fileGuid);

            if (EditorUserBuildSettings.development == false || PlayerSettings.iOS.sdkVersion != iOSSdkVersion.SimulatorSDK)
            {
                string embedPhase = proj.AddCopyFilesBuildPhase(mainTargetGuid, "Embed Frameworks", "", "10");
                PBXProjectExtensions.AddFileToEmbedFrameworks(proj, mainTargetGuid, fileGuid);
            }
            
            File.WriteAllText(projPath, proj.WriteToString());
        }
    }
}