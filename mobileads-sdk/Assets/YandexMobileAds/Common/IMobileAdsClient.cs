/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */


namespace YandexMobileAds.Common
{
    internal interface IMobileAdsClient
    {
        /// <summary>
        /// Set a value indicating whether user from GDPR region allowed to collect personal data
        /// which is used for analytics and ad targeting.
        /// If the value is set to false personal data will not be collected.
        /// </summary>
        /// <param name="consent"><c>true</c> if user provided consent to collect personal data, otherwise <c>false</c>.</param>
        void SetUserConsent(bool consent);

        /// <summary>
        /// Enables location usage for ad loading.
        /// Location permission is still required to be granted additionally to the consent.
        /// </summary>
        /// <param name="consent"><c>true</c> if user provided consent to use location for ads loading, otherwise <c>false</c>.</param>
        void SetLocationConsent(bool consent);

        /// <summary>
        /// Set a value indicating whether user is a child or undefined age.
        /// If the value is set to true personal data will not be collected.
        /// </summary>
        /// <param name="ageRestrictedUser"><c>true</c> if user falls under COPPA restrictions, otherwise <c>false</c>.</param>
        void SetAgeRestrictedUser(bool ageRestrictedUser);

        /// <summary>
        /// Shows Debug Panel.
        /// </summary>
        void ShowDebugPanel();
    }
}
