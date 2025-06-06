using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;

namespace YandexAdsEditor
{
    public class GoogleSettingsWindow : EditorWindow
    {
        private AdapterInfo adapter;
        private string iOSAppKey = "";
        private string androidAppKey = "";

        private GUIStyle linkStyle;
        private static readonly Regex AdMobAppIdRegex = new Regex(@"^ca-app-pub-\d+~\d+$");

        private static string XmlAdmobPath
        {
            get { return Path.Combine(Application.dataPath, "Editor/YandexAds/admob_app_ids.xml"); }
        }

        private static string XmlAdmobDirectory
        {
            get { return Path.GetDirectoryName(XmlAdmobPath); }
        }

        [MenuItem("Window/Yandex Ads/Adapter Settings")]
        public static void ShowWindowStatic()
        {
            ShowWindow(null);
        }

        public static void ShowWindow(AdapterInfo adapter)
        {
            var window = GetWindow<GoogleSettingsWindow>("Adapter Settings");
            window.adapter = adapter;
            window.Show();
        }

        private void OnEnable()
        {
            LoadAppIDsFromXML();
        }

        private void OnGUI()
        {
            if (linkStyle == null)
            {
                linkStyle = new GUIStyle(EditorStyles.label)
                {
                    normal = { textColor = new Color(0.2f, 0.6f, 1.0f) },
                    hover = { textColor = new Color(0.2f, 0.6f, 1.0f) },
                    fontStyle = FontStyle.Bold
                };
            }

            GUILayout.Label("Google AdMob App IDs:", EditorStyles.boldLabel);
            GUILayout.Space(5);

            GUILayout.BeginHorizontal();
            GUILayout.Label("iOS App ID:", GUILayout.Width(100));
            iOSAppKey = GUILayout.TextField(iOSAppKey, GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();

            GUILayout.Space(5);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Android App ID:", GUILayout.Width(100));
            androidAppKey = GUILayout.TextField(androidAppKey, GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();

            GUILayout.Space(10);
            DrawLink("How to get your App IDs", "https://support.google.com/admob/answer/7356431");
            GUILayout.Space(10);

            if (GUILayout.Button("Save"))
            {
                var result = SaveKeysAndGetResult(iOSAppKey, androidAppKey);
                ShowResultMessage(result);
            }
        }

        private enum SaveResultState
        {
            NoKeysNoError,
            OnlyiOSNoError,
            OnlyAndroidNoError,
            BothNoError,
            BothWithErrors,
            OnlyiOSWithErrors,
            OnlyAndroidWithErrors,
            NoKeysWithErrors
        }

        private struct SaveResult
        {
            public bool SavediOS;
            public bool SavedAndroid;
            public string ErrorMessage;
        }

        private SaveResult SaveKeysAndGetResult(string iosKey, string androidKey)
        {
            bool iOSValid = string.IsNullOrEmpty(iosKey) || IsAppKeyValid(iosKey);
            bool androidValid = string.IsNullOrEmpty(androidKey) || IsAppKeyValid(androidKey);

            string errorMessage = "";
            if (!string.IsNullOrEmpty(iosKey) && !iOSValid)
                errorMessage += "- The iOS App Key is invalid. It should match the format: ca-app-pub-XXXXXXXXXXXXXXXX~XXXXXXXXXXX\n";
            if (!string.IsNullOrEmpty(androidKey) && !androidValid)
                errorMessage += "- The Android App Key is invalid. It should match the format: ca-app-pub-XXXXXXXXXXXXXXXX~XXXXXXXXXXX\n";

            bool savediOS = false;
            bool savedAndroid = false;

            if (iOSValid && !string.IsNullOrEmpty(iosKey))
            {
                iOSAppKey = iosKey;
                Debug.Log("iOS App ID set for iOS build process.");
                savediOS = true;
            }

            if (androidValid && !string.IsNullOrEmpty(androidKey))
            {
                androidAppKey = androidKey;
                Debug.Log("Android App ID processed for Android manifest.");
                savedAndroid = true;
            }

            SaveAppIDsToXML();

            return new SaveResult { SavediOS = savediOS, SavedAndroid = savedAndroid, ErrorMessage = errorMessage };
        }

        private void ShowResultMessage(SaveResult result)
        {
            var state = DetermineState(result);
            bool success = false;
            switch (state)
            {
                case SaveResultState.BothNoError:
                    EditorUtility.DisplayDialog("Success", "App ID(s) have been saved successfully.\n- iOS and Android App IDs have been saved.", "OK");
                    success = true;
                    break;
                case SaveResultState.OnlyiOSNoError:
                    EditorUtility.DisplayDialog("Success", "App ID(s) have been saved successfully.\n- iOS App ID has been saved.", "OK");
                    success = true;
                    break;
                case SaveResultState.OnlyAndroidNoError:
                    EditorUtility.DisplayDialog("Success", "App ID(s) have been saved successfully.\n- Android App ID has been saved.", "OK");
                    success = true;
                    break;
                case SaveResultState.NoKeysNoError:
                    EditorUtility.DisplayDialog("Success", "App ID(s) have been saved successfully.\n- No App ID was entered to save.", "OK");
                    success = true;
                    break;
                case SaveResultState.BothWithErrors:
                    EditorUtility.DisplayDialog("Invalid App Key(s)", "Both keys have been saved, but there were errors:\n" + result.ErrorMessage, "OK");
                    break;
                case SaveResultState.OnlyiOSWithErrors:
                    EditorUtility.DisplayDialog("Invalid App Key(s)", "iOS key has been saved, but there were errors:\n" + result.ErrorMessage, "OK");
                    break;
                case SaveResultState.OnlyAndroidWithErrors:
                    EditorUtility.DisplayDialog("Invalid App Key(s)", "Android key has been saved, but there were ошибки:\n" + result.ErrorMessage, "OK");
                    break;
                case SaveResultState.NoKeysWithErrors:
                    EditorUtility.DisplayDialog("Invalid App Key(s)", "No keys were saved due to the following errors:\n" + result.ErrorMessage, "OK");
                    break;
            }

            if (success)
            {
                Close();
            }
        }

        private SaveResultState DetermineState(SaveResult result)
        {
            bool hasError = !string.IsNullOrEmpty(result.ErrorMessage);
            bool noKeys = !result.SavediOS && !result.SavedAndroid;
            bool bothKeys = result.SavediOS && result.SavedAndroid;
            bool onlyiOS = result.SavediOS && !result.SavedAndroid;
            bool onlyAndroid = !result.SavediOS && result.SavedAndroid;

            if (!hasError && bothKeys) return SaveResultState.BothNoError;
            if (!hasError && onlyiOS) return SaveResultState.OnlyiOSNoError;
            if (!hasError && onlyAndroid) return SaveResultState.OnlyAndroidNoError;
            if (!hasError && noKeys) return SaveResultState.NoKeysNoError;

            if (hasError && bothKeys) return SaveResultState.BothWithErrors;
            if (hasError && onlyiOS) return SaveResultState.OnlyiOSWithErrors;
            if (hasError && onlyAndroid) return SaveResultState.OnlyAndroidWithErrors;
            return SaveResultState.NoKeysWithErrors;
        }

        private void DrawLink(string label, string url)
        {
            Rect rect = GUILayoutUtility.GetRect(new GUIContent(label), linkStyle);
            EditorGUIUtility.AddCursorRect(rect, MouseCursor.Link);
            if (GUI.Button(rect, label, linkStyle))
            {
                Application.OpenURL(url);
            }
        }

        private bool IsAppKeyValid(string key)
        {
            return AdMobAppIdRegex.IsMatch(key);
        }

        private void LoadAppIDsFromXML()
        {
            string fullPath = XmlAdmobPath;
            if (File.Exists(fullPath))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fullPath);
                XmlElement root = doc.DocumentElement;
                if (root != null)
                {
                    XmlNode iosNode = root.SelectSingleNode("iOS");
                    if (iosNode != null)
                    {
                        iOSAppKey = iosNode.InnerText.Trim();
                    }
                    XmlNode androidNode = root.SelectSingleNode("Android");
                    if (androidNode != null)
                    {
                        androidAppKey = androidNode.InnerText.Trim();
                    }
                }
            }
        }

        private void SaveAppIDsToXML()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("AdMobAppIDs");
            doc.AppendChild(root);

            XmlElement iosElement = doc.CreateElement("iOS");
            iosElement.InnerText = iOSAppKey;
            root.AppendChild(iosElement);

            XmlElement androidElement = doc.CreateElement("Android");
            androidElement.InnerText = androidAppKey;
            root.AppendChild(androidElement);

            string directory = XmlAdmobDirectory;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string fullPath = XmlAdmobPath;
            doc.Save(fullPath);
            Debug.Log($"App IDs saved to XML at: {fullPath}");
        }
    }
}
