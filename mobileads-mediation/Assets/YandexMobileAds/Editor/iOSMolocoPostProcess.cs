using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;

public class PostProcessBuildMoloco
{
    // EDM4U writes the Podfile at order 40 and installs Pods at order 50.
    private const int BeforeCocoaPodsInstallation = 45;
    private const string HookMarker = "# Yandex Mobile Ads: embed Moloco after CocoaPods resolves and integrates dependencies.";

    [PostProcessBuild(BeforeCocoaPodsInstallation)]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string pathToBuiltProject)
    {
        if (buildTarget != BuildTarget.iOS) return;
        var podfile = Path.Combine(pathToBuiltProject, "Podfile");
        if (!File.Exists(podfile)) return;
        var contents = File.ReadAllText(podfile);
        if (contents.Contains(HookMarker)) return;

        // Wrap the hook invocation to preserve a publisher's existing post_integrate block.
        // CocoaPods also executes this when pod install is run outside Unity.
        File.AppendAllText(podfile, @"
# Yandex Mobile Ads: embed Moloco after CocoaPods resolves and integrates dependencies.
yandex_moloco_post_integrate = method(:post_integrate!)
define_singleton_method(:post_integrate!) do |installer|
  yandex_moloco_post_integrate.call(installer)
  has_moloco = installer.pod_targets.any? { |target| target.pod_name == 'MolocoSDKiOS' }
  installer.aggregate_targets.map(&:user_project).compact.uniq.each do |project|
    app = project.targets.find { |target| target.name == 'Unity-iPhone' }
    next unless app
    framework_path = 'Pods/MolocoSDKiOS/MolocoSDK.xcframework'
    reference = project.files.find { |file| file.path == framework_path }
    unless has_moloco
      if reference
        reference.build_files.each(&:remove_from_project)
        reference.remove_from_project
        project.save
      end
      next
    end
    reference ||= project.frameworks_group.new_file(framework_path, 'SOURCE_ROOT')
    unity = project.targets.find { |target| target.name == 'UnityFramework' }
    if unity && !unity.frameworks_build_phase.files_references.include?(reference)
      unity.frameworks_build_phase.add_file_reference(reference)
    end
    phase = app.copy_files_build_phases.find { |item| item.name == 'Embed Frameworks' }
    phase ||= app.new_copy_files_build_phase('Embed Frameworks')
    phase.dst_subfolder_spec = '10'
    unless phase.files_references.include?(reference)
      build_file = phase.add_file_reference(reference)
      build_file.settings = { 'ATTRIBUTES' => ['CodeSignOnCopy', 'RemoveHeadersOnCopy'] }
    end
    project.save
  end
  true
end
");
    }
}
