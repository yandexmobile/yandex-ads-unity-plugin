using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor.PackageManager;
using UnityEditor;
using UnityEditor.PackageManager.Requests;

namespace YandexAdsEditor
{
    public static class AdapterDataLoader
    {
        public const string GoogleAdapterName = "google";
        public const string MediationAdapterName = "mobile ads mediation";

        public static async Task<AdapterInfo[]> LoadAdapterInfo()
        {

            string[] xmlFiles = await FindFilesInAllUnityPackagesAsync();

            if (xmlFiles.Length == 0)
            {
                xmlFiles = FindFilesInLocalPackages();
            }

            if (xmlFiles.Length == 0)
            {
                Debug.LogWarning("Required directory for adapter data not found. Please ensure the plugin is correctly installed.");
            }

            return CheckConnectedAdapters(xmlFiles)
                     .Select(kvp => new AdapterInfo(
                         name: kvp.Key,
                         androidVersion: kvp.Value.AndroidVersion,
                         iosVersion: kvp.Value.IOSVersion,
                         isConnected: true))
                     .ToArray();
        }
        private static string[] FindFilesInLocalPackages()
        {
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets/YandexMobileAds/Editor");

            if (Directory.Exists(directoryPath))
            {
                return Directory.GetFiles(directoryPath, "*MediationDependencies.xml").ToArray(); ;
            }
            return new string[0];
        }


     private static async Task<string[]> FindFilesInAllUnityPackagesAsync(string mask = "*MediationDependencies.xml")
    {
        var allPackages = await GetAllInstalledPackagesAsync();

        var result = new List<string>();
        foreach (var package in allPackages)
        {
            var assetsPath = package.resolvedPath;
            if (Directory.Exists(assetsPath))
                result.AddRange(Directory.GetFiles(assetsPath, mask, SearchOption.AllDirectories));
        }
        return result.ToArray();
    }

          private static Task<UnityEditor.PackageManager.PackageInfo[]> GetAllInstalledPackagesAsync()
    {
        var tcs = new TaskCompletionSource<UnityEditor.PackageManager.PackageInfo[]>();
        ListRequest request = Client.List();

       void Progress()
        {
            if (request.IsCompleted)
            {
                EditorApplication.update -= Progress;
                if (request.Status == StatusCode.Success)
                    tcs.SetResult(request.Result.ToArray());
                else
                    tcs.SetException(new System.Exception(request.Error?.message));
            }
        }
        EditorApplication.update += Progress;
        return tcs.Task;
    }


        private static Dictionary<string, (string AndroidVersion, string IOSVersion)> CheckConnectedAdapters(string[] xmlFiles)
        {

            Dictionary<string, (string AndroidVersion, string IOSVersion)> connectedAdapters = new Dictionary<string, (string, string)>();

            foreach (string xmlFile in xmlFiles)
            {

                if (Path.GetFileName(xmlFile).Equals("YandexMobileadsDependencies.xml"))
                {
                    continue;
                }

                if (Path.GetFileName(xmlFile).Equals("YandexMobileadsMediationDependencies.xml"))
                {
                    return CheckMobileAdsMediationVersions(xmlFile);
                }
                try
                {

                    var doc = XDocument.Load(xmlFile);

                    var androidPackage = doc.Descendants("androidPackage").FirstOrDefault();
                    string androidVersion = androidPackage?.Attribute("spec")?.Value.Split(':').LastOrDefault() ?? "—";

                    var iosPod = doc.Descendants("iosPod").FirstOrDefault();
                    string iosVersion = iosPod?.Attribute("version")?.Value ?? "—";

                    string adapterName = Path.GetFileNameWithoutExtension(xmlFile)
                        .Replace("YandexMobileadsDependencies", "")
                        .Replace("Mobileads", "")
                        .Replace("MediationDependencies", "")
                        .ToLower();

                    connectedAdapters[adapterName] = (androidVersion, iosVersion);
                }
                catch
                {
                    Debug.LogWarning($"An error occurred while processing adapter data. Please check the XML file: {Path.GetFileName(xmlFile)}.");
                }
            }

            return connectedAdapters;
        }

        private static Dictionary<string, (string AndroidVersion, string IOSVersion)> CheckMobileAdsMediationVersions(string xmlFile)
        {

            Dictionary<string, (string AndroidVersion, string IOSVersion)> connectedAdapters = new Dictionary<string, (string, string)>();
            try
            {

                var doc = XDocument.Load(xmlFile);

                var androidPackage = doc.Descendants("androidPackage")
                .FirstOrDefault(p => p.Attribute("spec")?.Value.StartsWith("com.yandex.android:mobileads-mediation") == true);
                string androidVersion = androidPackage?.Attribute("spec")?.Value.Split(':').LastOrDefault() ?? "—";

                var iosPod = doc.Descendants("iosPod")
                .FirstOrDefault(p => p.Attribute("name")?.Value.StartsWith("YandexMobileAdsMediation") == true);
                string iosVersion = iosPod?.Attribute("version")?.Value ?? "—";

                connectedAdapters[MediationAdapterName] = (androidVersion, iosVersion);
            }
            catch
            {
                Debug.LogWarning($"An error occurred while processing adapter data. Please check the XML file: {Path.GetFileName(xmlFile)}.");
            }

            return connectedAdapters;
        }
    }
}
