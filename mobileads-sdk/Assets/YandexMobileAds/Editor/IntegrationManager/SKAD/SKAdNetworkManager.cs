using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using System.Xml;
using System.Linq;

namespace YandexAdsEditor
{
    public class SKAdNetworkEditorWindow : EditorWindow
    {
        private string xmlInput = "";
        private List<string> skadIds = new List<string>();
        private Vector2 scrollPos = Vector2.zero;

        private const string XMLFilePath = "Assets/Editor/YandexAds/skad_ids.xml";

        [MenuItem("YandexAds/Edit SKAdNetwork IDs")]
        public static void ShowWindow()
        {
            GetWindow<SKAdNetworkEditorWindow>("Edit SKAdNetwork IDs");
        }

        private void OnGUI()
        {
            GUILayout.Label("Import SKAdNetwork IDs from XML", EditorStyles.boldLabel);
            xmlInput = EditorGUILayout.TextArea(xmlInput, GUILayout.Height(300));

            GUILayout.Space(10);
            if (GUILayout.Button("Import from XML"))
            {
                ImportFromXML(xmlInput);
            }

            GUILayout.Space(10);
            if (GUILayout.Button("Save SKAdNetwork IDs"))
            {
                SaveSkadIds();
            }

            GUILayout.Space(20);
            DrawClickableLink();

            GUILayout.Space(10);
            GUILayout.Label("Current SKAdNetwork IDs:", EditorStyles.boldLabel);

            GUILayout.BeginVertical("box");
            scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.Width(position.width - 40), GUILayout.Height(300));

            if (skadIds.Count > 0)
            {
                for (int i = 0; i < skadIds.Count; i++)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(skadIds[i], GUILayout.ExpandWidth(true));
                    if (GUILayout.Button("Remove", GUILayout.Width(60)))
                    {
                        if (EditorUtility.DisplayDialog("Confirm Removal", $"Are you sure you want to remove '{skadIds[i]}'?", "Yes", "No"))
                        {
                            skadIds.RemoveAt(i);
                            break;
                        }
                    }
                    GUILayout.EndHorizontal();
                }
            }
            else
            {
                GUILayout.Label("No SKAdNetwork IDs imported yet.");
            }

            GUILayout.EndScrollView();
            GUILayout.EndVertical();

            GUILayout.Space(10);
            if (skadIds.Count > 0)
            {
                if (GUILayout.Button("Clear All SKAdNetwork IDs"))
                {
                    ClearSkadIds();
                }
            }
        }

        private void DrawClickableLink()
        {
            GUILayout.BeginHorizontal();
            GUIStyle linkStyle = new GUIStyle(GUI.skin.label);
            linkStyle.normal.textColor = new Color(0.2f, 0.6f, 1f);
            linkStyle.hover.textColor = new Color(0.1f, 0.4f, 0.8f);
            linkStyle.fontStyle = FontStyle.Bold;
            linkStyle.alignment = TextAnchor.MiddleLeft;
            linkStyle.richText = false;
            linkStyle.wordWrap = true;

            string linkText = "Actual list of SKAdNetworks can be found here";
            GUILayout.Label(linkText, linkStyle);

            Rect lastRect = GUILayoutUtility.GetLastRect();

            if (Event.current.type == EventType.MouseDown && lastRect.Contains(Event.current.mousePosition))
            {
                string url = "https://ads.yandex.com/helpcenter/ru/dev/ios/quick-start#skad";
                EditorUtility.OpenWithDefaultApp(url);
                Event.current.Use();
            }
            GUILayout.EndHorizontal();
        }

        private void SaveSkadIds()
        {
            int countBefore = skadIds.Count;

            skadIds = skadIds.Distinct().ToList();

            int duplicatesRemoved = countBefore - skadIds.Count;

            SaveIDsToXML();

            string message = "SKAdNetwork IDs saved successfully!";
            if (duplicatesRemoved > 0)
                message += $"\nRemoved {duplicatesRemoved} duplicates.";

            EditorUtility.DisplayDialog("Success", message, "OK");
            Debug.Log(message);
        }

        private void OnEnable()
        {
            LoadSkadIds();
        }

        private void LoadSkadIds()
        {
            skadIds = new List<string>();
            if (File.Exists(XMLFilePath))
            {
                skadIds = LoadIDsFromXML();
                // При желании можно и тут чистить дубли, чтобы в редакторе они не отображались
                skadIds = skadIds.Distinct().ToList();
            }
        }

        private void ClearSkadIds()
        {
            if (EditorUtility.DisplayDialog("Confirm", "Are you sure you want to clear all SKAdNetwork IDs?", "Yes", "No"))
            {
                skadIds.Clear();
                if (File.Exists(XMLFilePath))
                {
                    File.Delete(XMLFilePath);
                }
                EditorUtility.DisplayDialog("Cleared", "All SKAdNetwork IDs have been cleared.", "OK");
                Debug.Log("All SKAdNetwork IDs cleared.");
            }
        }

        private void ImportFromXML(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                EditorUtility.DisplayDialog("Error", "XML input is empty.", "OK");
                return;
            }

            List<string> importedIds = new List<string>();
            int duplicateCount = 0;
            int invalidCount = 0;

            try
            {
                string wrappedXml = "<root>" + xml + "</root>";
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(wrappedXml);

                XmlNodeList dictNodes = doc.GetElementsByTagName("dict");
                Debug.Log($"Number of <dict> nodes found: {dictNodes.Count}");

                foreach (XmlNode dict in dictNodes)
                {
                    XmlNode keyNode = null;
                    XmlNode stringNode = null;

                    for (int i = 0; i < dict.ChildNodes.Count; i++)
                    {
                        if (dict.ChildNodes[i].Name == "key" && dict.ChildNodes[i].InnerText == "SKAdNetworkIdentifier")
                        {
                            keyNode = dict.ChildNodes[i];
                            if (i + 1 < dict.ChildNodes.Count && dict.ChildNodes[i + 1].Name == "string")
                            {
                                stringNode = dict.ChildNodes[i + 1];
                            }
                            break;
                        }
                    }

                    if (keyNode != null && stringNode != null)
                    {
                        string skadId = stringNode.InnerText.Trim();
                        Debug.Log($"Found SKAdNetworkIdentifier: {skadId}");

                        if (IsValidSKAdNetworkID(skadId))
                        {
                            if (!skadIds.Contains(skadId))
                            {
                                importedIds.Add(skadId);
                            }
                            else
                            {
                                duplicateCount++;
                                Debug.LogWarning($"Duplicate SKAdNetworkIdentifier skipped: {skadId}");
                            }
                        }
                        else
                        {
                            invalidCount++;
                            Debug.LogWarning($"Invalid SKAdNetworkIdentifier skipped: {skadId}");
                        }
                    }
                }

                if (importedIds.Count > 0 || duplicateCount > 0 || invalidCount > 0)
                {
                    skadIds.AddRange(importedIds);
                    int countBeforeDistinct = skadIds.Count;
                    skadIds = skadIds.Distinct().ToList();
                    int removedByDistinct = countBeforeDistinct - skadIds.Count;

                    string message = $"{importedIds.Count} SKAdNetwork IDs imported successfully.";
                    if (duplicateCount > 0)
                        message += $"\n{duplicateCount} duplicate SKAdNetwork IDs were skipped (at the import stage).";
                    if (invalidCount > 0)
                        message += $"\n{invalidCount} invalid SKAdNetwork IDs were skipped.";

                    if (removedByDistinct > 0)
                        message += $"\nAdditionally removed {removedByDistinct} duplicates on Distinct() pass.";

                    EditorUtility.DisplayDialog("Import Result", message, "OK");
                    Debug.Log(message);
                }
                else
                {
                    EditorUtility.DisplayDialog("Import", "No SKAdNetwork IDs found to import.", "OK");
                    Debug.LogWarning("No SKAdNetwork IDs found to import.");
                }
            }
            catch (XmlException ex)
            {
                EditorUtility.DisplayDialog("XML Error", $"Failed to parse XML: {ex.Message}", "OK");
                Debug.LogError($"Failed to parse XML: {ex.Message}");
            }
            catch (System.Exception ex)
            {
                EditorUtility.DisplayDialog("Error", $"An error occurred: {ex.Message}", "OK");
                Debug.LogError($"An error occurred during import: {ex.Message}");
            }
        }

        private bool IsValidSKAdNetworkID(string id)
        {
            return id.EndsWith(".skadnetwork");
        }

        private void SaveIDsToXML()
        {
            skadIds = skadIds.Distinct().ToList();

            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("SKAdNetworkIDs");
            doc.AppendChild(root);

            foreach (var id in skadIds)
            {
                XmlElement idElement = doc.CreateElement("id");
                idElement.InnerText = id;
                root.AppendChild(idElement);
            }

            string directory = Path.GetDirectoryName(XMLFilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            doc.Save(XMLFilePath);
        }

        private List<string> LoadIDsFromXML()
        {
            List<string> ids = new List<string>();
            XmlDocument doc = new XmlDocument();
            doc.Load(XMLFilePath);
            XmlNodeList idNodes = doc.GetElementsByTagName("id");
            foreach (XmlNode node in idNodes)
            {
                if (!string.IsNullOrEmpty(node.InnerText))
                    ids.Add(node.InnerText.Trim());
            }
            return ids;
        }
    }
}
