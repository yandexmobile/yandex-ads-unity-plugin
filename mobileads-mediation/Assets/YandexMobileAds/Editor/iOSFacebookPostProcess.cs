using UnityEditor;
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;
using UnityEditor.Callbacks;
using System.IO;

public class PostProcessBuildFacebook
{
    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string pathToBuiltProject)
    {
        if (buildTarget == BuildTarget.iOS)
        {
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
