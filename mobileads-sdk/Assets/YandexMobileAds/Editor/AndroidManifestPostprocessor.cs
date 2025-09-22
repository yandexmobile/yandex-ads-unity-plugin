#if UNITY_EDITOR
using System.IO;
using System.Text;
using System.Xml;
using UnityEditor.Android;

public class PostprocessConfigureAndroidManifest : IPostGenerateGradleAndroidProject
{
    public void OnPostGenerateGradleAndroidProject(string basePath)
    {
        var androidManifest = new AndroidManifest(GetManifestPath(basePath));
        androidManifest.SetHardwareAccel();
        androidManifest.Save();
    }

    public int callbackOrder { get { return 1; } }

    private string _manifestFilePath;

    private string GetManifestPath(string basePath)
    {
        if (string.IsNullOrEmpty(_manifestFilePath))
        {
            var pathBuilder = new StringBuilder(basePath);
            pathBuilder.Append(Path.DirectorySeparatorChar).Append("src");
            pathBuilder.Append(Path.DirectorySeparatorChar).Append("main");
            pathBuilder.Append(Path.DirectorySeparatorChar).Append("AndroidManifest.xml");
            _manifestFilePath = pathBuilder.ToString();
        }
        return _manifestFilePath;
    }
}

internal class AndroidXmlDocument : XmlDocument
{
    private string m_Path;
    protected XmlNamespaceManager nsMgr;
    public readonly string AndroidXmlNamespace = "http://schemas.android.com/apk/res/android";
    public AndroidXmlDocument(string path)
    {
        m_Path = path;
        using (var reader = new XmlTextReader(m_Path))
        {
            reader.Read();
            Load(reader);
        }
        nsMgr = new XmlNamespaceManager(NameTable);
        nsMgr.AddNamespace("android", AndroidXmlNamespace);
    }

    public string Save()
    {
        using (var writer = new XmlTextWriter(m_Path, new UTF8Encoding(false)))
        {
            writer.Formatting = Formatting.Indented;
            Save(writer);
        }
        return m_Path;
    }
}

internal class AndroidManifest : AndroidXmlDocument
{
    public AndroidManifest(string path) : base(path) { }

    private XmlAttribute CreateAndroidAttribute(string key, string value)
    {
        XmlAttribute attr = CreateAttribute("android", key, AndroidXmlNamespace);
        attr.Value = value;
        return attr;
    }

    internal XmlNode GetActivityWithLaunchIntent()
    {
        return SelectSingleNode("/manifest/application/activity[intent-filter/action/@android:name='android.intent.action.MAIN' and " +
                "intent-filter/category/@android:name='android.intent.category.LAUNCHER']", nsMgr);
    }

    internal void SetHardwareAccel()
    {
        GetActivityWithLaunchIntent().Attributes.Append(CreateAndroidAttribute("hardwareAccelerated", "true"));
    }
}
#endif
