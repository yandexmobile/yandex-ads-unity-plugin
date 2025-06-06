using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace YandexAdsEditor
{
    public static class SdkUpdater
    {
        public static async Task<string> GetLatestSdkVersionFromChangelogAsync()
        {
            const string changelogUrl = "https://raw.githubusercontent.com/yandexmobile/yandex-ads-unity-plugin/master/CHANGELOG.md";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Unity Editor");

                try
                {
                    HttpResponseMessage response = await client.GetAsync(changelogUrl);

                    if (!response.IsSuccessStatusCode)
                    {
                        Debug.LogError($"Error fetching CHANGELOG.md: {response.StatusCode} - {response.ReasonPhrase}");
                        return "Unknown";
                    }

                    string changelogContent = await response.Content.ReadAsStringAsync();
                    return ParseLatestVersionFromChangelog(changelogContent);
                }
                catch (HttpRequestException httpEx)
                {
                    Debug.LogError($"HTTP request error: {httpEx.Message}");
                    return "Unknown";
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"Error processing CHANGELOG.md: {ex.Message}");
                    return "Unknown";
                }
            }
        }

        private static string ParseLatestVersionFromChangelog(string changelogContent)
        {
            string versionPattern = @"Version (\d+\.\d+\.\d+)";
            var match = System.Text.RegularExpressions.Regex.Match(changelogContent, versionPattern);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            Debug.LogError("Failed to parse the latest SDK version from CHANGELOG.md");
            return "Unknown";
        }


        private static async Task<string> GetLatestVersionFileAsync(string apiUrl, string filePrefix)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Unity Editor");

                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (!response.IsSuccessStatusCode)
                    {
                        Debug.LogError($"Error querying GitHub API: {response.StatusCode} - {response.ReasonPhrase}");
                        return null;
                    }

                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    return ParseLatestVersionFile(jsonResponse, filePrefix);
                }
                catch (HttpRequestException httpEx)
                {
                    Debug.LogError($"HTTP request error: {httpEx.Message}");
                    return null;
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"Error processing GitHub API response: {ex.Message}");
                    return null;
                }
            }
        }

        private static string ParseLatestVersionFile(string jsonResponse, string filePrefix)
        {
            jsonResponse = "{ \"responses\": " + jsonResponse + " }";

            var container = JsonUtility.FromJson<GitHubResponseContainer>(jsonResponse);

            string latestVersionFile = null;
            foreach (var item in container.responses)
            {
                if (item.name.StartsWith(filePrefix) && item.name.EndsWith(".unitypackage"))
                {
                    latestVersionFile = item.name;
                }
            }

            if (latestVersionFile == null)
            {
                Debug.LogError($"No valid package found for prefix '{filePrefix}' in the GitHub response.");
            }

            return latestVersionFile;
        }

        private static async Task<bool> DownloadFileAsync(string url, string downloadPath)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Unity Editor");

                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (!response.IsSuccessStatusCode)
                    {
                        Debug.LogError($"Error downloading file: {response.StatusCode} - {response.ReasonPhrase}");
                        return false;
                    }

                    byte[] fileData = await response.Content.ReadAsByteArrayAsync();
                    File.WriteAllBytes(downloadPath, fileData);

                    Debug.Log($"File downloaded to {downloadPath}");
                    return true;
                }
                catch (HttpRequestException httpEx)
                {
                    Debug.LogError($"HTTP request error: {httpEx.Message}");
                    return false;
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"Error downloading file: {ex.Message}");
                    return false;
                }
            }
        }

        [System.Serializable]
        private class GitHubResponse
        {
            public string name;
            public string type;
        }

        [System.Serializable]
        private class GitHubResponseContainer
        {
            public GitHubResponse[] responses;
        }
    }
}
