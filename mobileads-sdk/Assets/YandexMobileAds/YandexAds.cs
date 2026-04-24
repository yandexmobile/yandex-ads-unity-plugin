/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;
using YandexMobileAds.Common;
using YandexMobileAds.Platforms;

namespace YandexMobileAds
{
    /// <summary>
    /// A class allows you to set general SDK settings.
    /// </summary>
    public static class YandexAds
    {
        /// <summary>
        /// Sets a value that indicates whether a user from the GDPR region permits the collection of personal data that will be used for analytics and ad targeting.
        /// User data will not be collected until data collection is permitted. If the user once permitted or prohibited data collection,
        /// this value must be passed each time the app is launched.
        /// </summary>
        /// <param name="consent">Permits or prohibits data collection. By default, data is not collected.</param>
        public static void SetUserConsent(bool consent)
        {
            IMobileAdsClient mobileAds = YandexMobileAdsClientFactory.CreateMobileAdsClient();
            mobileAds.SetUserConsent(consent);
        }

        /// <summary>
        /// Enables or disables location tracking for ad loading. This option is disabled by default.
        /// </summary>
        /// <param name="enabled">Enables or disables location tracking.</param>
        public static void SetLocationTracking(bool enabled)
        {
            IMobileAdsClient mobileAds = YandexMobileAdsClientFactory.CreateMobileAdsClient();
            mobileAds.SetLocationTracking(enabled);
        }

        /// <summary>
        /// Sets whether the user is age-restricted. If true, personal data will not be collected.
        /// </summary>
        /// <param name="ageRestricted">Restrict or allow collecting personal data.</param>
        public static void SetAgeRestricted(bool ageRestricted)
        {
            IMobileAdsClient mobileAds = YandexMobileAdsClientFactory.CreateMobileAdsClient();
            mobileAds.SetAgeRestricted(ageRestricted);
        }

        /// <summary>
        /// Shows Debug Panel. Available only for Android
        /// </summary>
        public static void ShowDebugPanel()
        {
            IMobileAdsClient mobileAds = YandexMobileAdsClientFactory.CreateMobileAdsClient();
            mobileAds.ShowDebugPanel();
        }
    }
}
