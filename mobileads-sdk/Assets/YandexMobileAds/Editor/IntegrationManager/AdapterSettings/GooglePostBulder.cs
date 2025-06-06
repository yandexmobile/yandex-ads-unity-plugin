using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.IO;
using System.Xml;

public static class GooglePostProcessBuild
{
    private const string XML_ADMOB_PATH = "Assets/Editor/YandexAds/admob_app_ids.xml";

    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget target, string buildPath)
    {
        if (target == BuildTarget.iOS)
        {
            string appKey = "";
            if (File.Exists(XML_ADMOB_PATH))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(XML_ADMOB_PATH);
                XmlElement root = doc.DocumentElement;
                if (root != null)
                {
                    XmlNode iosNode = root.SelectSingleNode("iOS");
                    if (iosNode != null)
                    {
                        appKey = iosNode.InnerText.Trim();
                    }
                }
            }

            Debug.Log($"Retrieved iOS key: {appKey}");

            if (!string.IsNullOrEmpty(appKey))
            {
                string plistPath = Path.Combine(buildPath, "Info.plist");
                PlistDocument plist = new PlistDocument();
                plist.ReadFromFile(plistPath);
                PlistElementDict rootDict = plist.root;

                rootDict.SetString("GADApplicationIdentifier", appKey);

                File.WriteAllText(plistPath, plist.WriteToString());
                Debug.Log("GADApplicationIdentifier successfully added to Info.plist");
            }
            else
            {
                Debug.LogWarning("GADApplicationIdentifier was not set before build.");
            }
        }
    }
}
