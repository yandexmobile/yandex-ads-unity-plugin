using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;

namespace YandexAdsEditor
{
    public class AdaptersInfoWindow : EditorWindow
    {
        private AdapterInfo[] adapters = new AdapterInfo[0];
        private const string GitHubApiUrl = "https://api.github.com/repos/yandexmobile/yandex-ads-unity-plugin/contents/mobileads-mediation";
        private const string BaseRawUrl = "https://raw.githubusercontent.com/yandexmobile/yandex-ads-unity-plugin/master/mobileads-mediation/";
        private string latestSdkVersion = "Unknown";
        private bool isLoading = true;

        [MenuItem("YandexAds/Adapters Info")]
        public static void ShowWindow()
        {
            var window = GetWindow<AdaptersInfoWindow>("Adapters Info");
            window.minSize = new Vector2(800, 400);
            window.maxSize = new Vector2(800, 400);
        }

        private void OnEnable()
        {
            _ = FetchAndLoadAdaptersAsync();
        }

        private async Task FetchAndLoadAdaptersAsync()
        {
            isLoading = true;
            Repaint();
            latestSdkVersion = await SdkUpdater.GetLatestSdkVersionFromChangelogAsync();
            adapters = await AdapterDataLoader.LoadAdapterInfo();
            isLoading = false;
            Repaint();
        }

        private void OnGUI()
        {
            string currentVersion = SdkVersionReader.GetSdkVersion();
            GUIStyle boldStyle = new GUIStyle(GUI.skin.label)
            {
                fontStyle = FontStyle.Bold,
                fontSize = 12,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = Color.white },
                padding = new RectOffset(15, 15, 10, 10)
            };

            if (isLoading)
            {
                GUILayout.Label("Loading...");
                return;
            }

            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical(GUILayout.Width(300));
            GUILayout.Label("SDK Version: " + currentVersion);
            if (latestSdkVersion == currentVersion && latestSdkVersion != "Unknown")
            {
                GUILayout.Label("You have the latest version!", EditorStyles.label);
            }
            else
            {
                GUILayout.Label(latestSdkVersion == "Unknown" ? "(Last SDK version is not avaliable. Please check your internet connection!)" : "(Latest version: " + latestSdkVersion + ")", EditorStyles.label);
            }
            GUILayout.EndVertical();

            GUILayout.BeginVertical(GUILayout.Width(150));
            GUILayout.Space(10);
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Adapter Name", GUILayout.Width(300));
            GUILayout.Label("Android Version", GUILayout.Width(150));
            GUILayout.Label("iOS Version", GUILayout.Width(150));
            GUILayout.Label("", GUILayout.Width(100));
            GUILayout.EndHorizontal();

            if (adapters.Length == 0)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("N/A", GUILayout.Width(300));
                GUILayout.Label("N/A", GUILayout.Width(150));
                GUILayout.Label("N/A", GUILayout.Width(150));
                GUILayout.EndHorizontal();

                GUILayout.Space(20);
                GUILayout.Label("Please use the Unity Package Manager to manage dependencies.", boldStyle);
                return;
            }

            for (int i = 0; i < adapters.Length; i++)
            {
                var adapter = adapters[i];
                GUILayout.BeginHorizontal();
                GUILayout.Label(adapter.Name, GUILayout.Width(300));
                GUILayout.Label(adapter.AndroidVersion, GUILayout.Width(150));
                GUILayout.Label(adapter.IOSVersion, GUILayout.Width(150));

                if (adapter.Name == AdapterDataLoader.GoogleAdapterName || adapter.Name == AdapterDataLoader.MediationAdapterName)
                {
                    if (adapter.IsConnected)
                    {
                        if (GUILayout.Button("Setting", GUILayout.Width(100)))
                        {
                            OpenAdapterSettings(adapter);
                        }
                    }
                }
                else
                {
                    GUILayout.Space(100);
                }

                GUILayout.EndHorizontal();
            }

            GUILayout.Space(20);
            GUILayout.Label("Please use the Unity Package Manager to manage dependencies.", boldStyle);
        }

        private void OpenAdapterSettings(AdapterInfo adapter)
        {
            GoogleSettingsWindow.ShowWindow(adapter);
        }
    }
}
