using YandexMobileAds.Base;

namespace YandexAdsEditor
{
    public static class SdkVersionReader
    {
        public static string GetSdkVersion()
        {
          
            return MobileAdsPackageInfo.PackageVersion;
        }
    }
}