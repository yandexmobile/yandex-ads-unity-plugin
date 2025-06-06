using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEditor;
using System.Xml;

namespace YandexAdsEditor
{
    public class PostBuildProcessor
    {
        private const string XMLFilePath = "Assets/Editor/YandexAds/skad_ids.xml";

        [PostProcessBuild]
        public static void OnPostProcessBuild(BuildTarget target, string pathToBuildProject)
        {
            if (target == BuildTarget.iOS)
            {
                ModifyPlist(pathToBuildProject);
            }
        }

        private static void ModifyPlist(string pathToBuildProject)
        {
            string plistPath = Path.Combine(pathToBuildProject, "Info.plist");
            if (!File.Exists(plistPath))
            {
                Debug.LogError("Info.plist not found at: " + plistPath);
                return;
            }

            PlistDocument plist = new PlistDocument();
            plist.ReadFromFile(plistPath);

            if (File.Exists(XMLFilePath))
            {
                List<string> skadIds = new List<string>();
                XmlDocument doc = new XmlDocument();
                doc.Load(XMLFilePath);
                XmlNodeList idNodes = doc.GetElementsByTagName("id");
                foreach (XmlNode node in idNodes)
                {
                    if (!string.IsNullOrEmpty(node.InnerText))
                        skadIds.Add(node.InnerText.Trim());
                }

                if (skadIds.Count > 0)
                {
                    PlistElementArray skAdNetworkItems;
                    if (plist.root.values.ContainsKey("SKAdNetworkItems"))
                    {
                        skAdNetworkItems = plist.root["SKAdNetworkItems"].AsArray();
                    }
                    else
                    {
                        skAdNetworkItems = plist.root.CreateArray("SKAdNetworkItems");
                    }

                    foreach (string skadId in skadIds)
                    {
                        bool exists = false;
                        foreach (PlistElement elem in skAdNetworkItems.values)
                        {
                            PlistElementDict dict = elem.AsDict();
                            if (dict != null && dict.values.ContainsKey("SKAdNetworkIdentifier"))
                            {
                                string existingId = dict["SKAdNetworkIdentifier"].AsString();
                                if (existingId == skadId)
                                {
                                    exists = true;
                                    break;
                                }
                            }
                        }

                        if (!exists)
                        {
                            PlistElementDict newDict = skAdNetworkItems.AddDict();
                            newDict.SetString("SKAdNetworkIdentifier", skadId);
                            Debug.Log($"Added SKAdNetworkIdentifier: {skadId}");
                        }
                    }

                    File.WriteAllText(plistPath, plist.WriteToString());
                    Debug.Log("SKAdNetwork IDs added to Info.plist");
                }
            }
            else
            {
                Debug.LogWarning("XML file with SKAdNetwork IDs not found at: " + XMLFilePath);
            }
        }
    }
}
