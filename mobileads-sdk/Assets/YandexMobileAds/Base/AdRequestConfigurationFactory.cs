using System.Collections.Generic;

namespace YandexMobileAds.Base
{
    public class AdRequestConfigurationFactory
    {
        private const string PluginTypeParameter = "plugin_type";
        private const string PluginVersionParameter = "plugin_version";

        private const string PluginType = "unity";

        public AdRequestConfiguration CreateAdRequestConfiguration(AdRequestConfiguration adRequestConfiguration)
        {
            if (adRequestConfiguration == null)
            {
                return null;
            }

            var parameters = adRequestConfiguration.Parameters ?? new Dictionary<string, string>();
            parameters.Add(PluginTypeParameter, PluginType);
            parameters.Add(PluginVersionParameter, MobileAdsPackageInfo.PackageVersion);

            return new AdRequestConfiguration.Builder(adRequestConfiguration.AdUnitId)
                .WithAdRequestConfiguration(adRequestConfiguration)
                .WithParameters(parameters)
                .Build();
        }
    }
}
