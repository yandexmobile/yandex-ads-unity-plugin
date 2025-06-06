using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System.IO;
using System.Xml;

public class AndroidManifestPreprocessor : IPreprocessBuildWithReport
{
    public int callbackOrder => 0;

    public void OnPreprocessBuild(BuildReport report)
    {
        if (report.summary.platform != BuildTarget.Android)
        {
            return;
        }

        string appId = LoadAppIdFromXml();
        if (string.IsNullOrEmpty(appId))
        {
            Debug.LogWarning("No Android App ID set in XML, skipping AndroidManifest modification.");
            return;
        }

        string manifestPath = Path.Combine(Application.dataPath, "Plugins/Android/AndroidManifest.xml");
        if (!File.Exists(manifestPath))
        {
            CreateBaseManifest(manifestPath);
            Debug.Log("Created new base AndroidManifest.xml in Plugins/Android");
        }

        UpdateAndroidManifest(manifestPath, appId);

        Debug.Log($"AndroidManifest.xml updated with App ID: {appId}");
    }

    private string LoadAppIdFromXml()
    {
        string xmlPath = Path.Combine(Application.dataPath, "Editor/YandexAds/admob_app_ids.xml");
        if (!File.Exists(xmlPath))
            return null;

        XmlDocument doc = new XmlDocument();
        doc.Load(xmlPath);
        var root = doc.DocumentElement;
        if (root == null)
            return null;

        var androidNode = root.SelectSingleNode("Android");
        if (androidNode != null)
            return androidNode.InnerText.Trim();

        return null;
    }

    private void CreateBaseManifest(string path)
    {
        string baseManifest =
@"<?xml version=""1.0"" encoding=""utf-8""?>
<manifest xmlns:android=""http://schemas.android.com/apk/res/android""
    xmlns:tools=""http://schemas.android.com/tools""
    package=""com.unity3d.player"">

    <application>
        <activity android:name=""com.unity3d.player.UnityPlayerActivity""
                  android:theme=""@style/UnityThemeSelector"">
            <intent-filter>
                <action android:name=""android.intent.action.MAIN"" />
                <category android:name=""android.intent.category.LAUNCHER"" />
            </intent-filter>
            <meta-data android:name=""unityplayer.UnityActivity"" android:value=""true"" />
        </activity>
    </application>
</manifest>";
        File.WriteAllText(path, baseManifest);
    }

    private void UpdateAndroidManifest(string path, string appId)
    {
        const string ANDROID_NAMESPACE = "http://schemas.android.com/apk/res/android";
        const string META_NAME = "com.google.android.gms.ads.APPLICATION_ID";

        XmlDocument doc = new XmlDocument();
        doc.Load(path);

        var nsMgr = new XmlNamespaceManager(doc.NameTable);
        nsMgr.AddNamespace("android", ANDROID_NAMESPACE);

        XmlNode applicationNode = doc.SelectSingleNode("/manifest/application");
        if (applicationNode == null)
        {
            XmlNode manifestNode = doc.SelectSingleNode("/manifest");
            applicationNode = doc.CreateElement("application");
            manifestNode.AppendChild(applicationNode);
        }

 
        string xpath = $"meta-data[@android:name='{META_NAME}']";
        XmlNode metaNode = applicationNode.SelectSingleNode(xpath, nsMgr);

        if (metaNode == null)
        {

            XmlElement metaElem = doc.CreateElement("meta-data");
            metaElem.SetAttribute("name", ANDROID_NAMESPACE, META_NAME);
            metaElem.SetAttribute("value", ANDROID_NAMESPACE, appId);
            applicationNode.AppendChild(metaElem);
        }
        else
        {
            (metaNode as XmlElement)?.SetAttribute("value", ANDROID_NAMESPACE, appId);
        }

        doc.Save(path);
    }
}
