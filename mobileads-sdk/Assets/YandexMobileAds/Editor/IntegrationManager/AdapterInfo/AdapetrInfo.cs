namespace YandexAdsEditor
{
    public class AdapterInfo
    {
        public string Name { get; set; }
        public string AndroidVersion { get; set; }
        public string IOSVersion { get; set; }
        public bool IsConnected { get; set; }

        public AdapterInfo(string name, string androidVersion, string iosVersion, bool isConnected)
        {
            Name = name;
            AndroidVersion = androidVersion;
            IOSVersion = iosVersion;
            IsConnected = isConnected;
        }
    }
}
