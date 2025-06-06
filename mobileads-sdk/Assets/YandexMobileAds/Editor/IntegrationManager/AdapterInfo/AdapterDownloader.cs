using System.IO;
using System.Net;
using UnityEditor;
using UnityEngine;

namespace YandexAdsEditor
{
    public static class AdapterDownloader
    {
        private static readonly string BaseGitHubUrl = "https://raw.githubusercontent.com/yandexmobile/yandex-ads-unity-plugin/master/mobileads-mediation/";

        public static void DownloadAndImport(string adapterName, string version)
        {
            try
            {
                string lowerCaseName = adapterName.ToLower();
                string url = $"{BaseGitHubUrl}{lowerCaseName}/mobileads-{lowerCaseName}-mediation-{version}.unitypackage";

                string downloadPath = Path.Combine(Application.dataPath, $"{lowerCaseName}-{version}.unitypackage");

                Debug.Log($"Starting download of adapter '{adapterName}' from {url}.");

                using (var webClient = new WebClient())
                {
                    webClient.DownloadFile(url, downloadPath);
                }

                AssetDatabase.ImportPackage(downloadPath, true);
                Debug.Log($"Adapter '{adapterName}' downloaded and imported successfully.");

            }
            catch (WebException webEx)
            {
                if (webEx.Response is HttpWebResponse response && response.StatusCode == HttpStatusCode.NotFound)
                {
                    Debug.LogWarning($"Adapter '{adapterName}' is not supported for this version and will be removed.");

                    string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets/YandexMobileAds/Editor");

                    string adapterXmlName = $"Mobileads{adapterName}MediationDependencies.xml";
                    string adapterXmlPath = Path.Combine(directoryPath, adapterXmlName);

                    if (File.Exists(adapterXmlPath))
                    {
                        File.Delete(adapterXmlPath);
                        Debug.Log($"Removed adapter file: {adapterXmlPath}");
                        AssetDatabase.Refresh();
                    }
                    else
                    {
                        Debug.LogWarning($"Adapter XML file not found for '{adapterName}' at {adapterXmlPath}. Possibly it was never properly connected or already removed.");
                    }

                }
                else
                {
                    Debug.LogError($"Failed to download adapter '{adapterName}'. Please check your internet connection. Error: {webEx.Message}");
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"An error occurred while processing adapter '{adapterName}'. Please try again. Error: {ex.Message}");
            }
        }
    }
}
