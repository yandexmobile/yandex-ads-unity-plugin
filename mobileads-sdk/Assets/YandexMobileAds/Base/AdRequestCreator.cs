using System.Collections.Generic;

namespace YandexMobileAds.Base
{
    public class AdRequestCreator
    {
        private const string PluginTypeParameter = "plugin_type";
        private const string PluginVersionParameter = "plugin_version";

        private const string PluginType = "unity";

        public AdRequest CreateAdRequest(AdRequest adRequest)
        {
            if (adRequest == null)
            {
                return null;
            }

            var parameters = adRequest.Parameters != null
                ? new Dictionary<string, string>(adRequest.Parameters)
                : new Dictionary<string, string>();
            parameters[PluginTypeParameter] = PluginType;
            parameters[PluginVersionParameter] = MobileAdsPackageInfo.PackageVersion;

            return new AdRequest(
                adRequest.AdUnitId,
                targeting: adRequest.Targeting,
                adTheme: adRequest.AdTheme,
                parameters: parameters);
        }
    }
}
