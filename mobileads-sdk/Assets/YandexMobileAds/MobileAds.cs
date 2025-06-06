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
    public static class MobileAds
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
        /// The SDK automatically collects location data if the user allowed the app to track the location. This option is disabled by default.
        /// </summary>
        /// <param name="consent">Enables or disables collecting location data.</param>
        public static void SetLocationConsent(bool consent)
        {
            IMobileAdsClient mobileAds = YandexMobileAdsClientFactory.CreateMobileAdsClient();
            mobileAds.SetLocationConsent(consent);
        }

        /// <summary>
        /// The SDK automatically collects personal data if the user didn't restrict them. By default restriction is disabled
        /// </summary>
        /// <param name="ageRestrictedUser">Restrict or allow collecting personal data.</param>
        public static void SetAgeRestrictedUser(bool ageRestrictedUser)
        {
            IMobileAdsClient mobileAds = YandexMobileAdsClientFactory.CreateMobileAdsClient();
            mobileAds.SetAgeRestrictedUser(ageRestrictedUser);
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
