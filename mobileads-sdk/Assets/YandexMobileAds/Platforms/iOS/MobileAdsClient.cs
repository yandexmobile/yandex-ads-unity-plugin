/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2024 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using YandexMobileAds.Common;

namespace YandexMobileAds.Platforms.iOS
{
    #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE

    public class MobileAdsClient : IMobileAdsClient
    {
        private static MobileAdsClient _instance;

        private static readonly object _lockObject = new object();

        public static MobileAdsClient GetInstance()
        {
            if (_instance == null)
            {
                lock (_lockObject)
                {
                    if (_instance == null)
                        _instance = new MobileAdsClient();
                }
            }
            return _instance;
        }

        private MobileAdsClient() { }

        public void SetUserConsent(bool consent)
        {
            MobileAdsBridge.YMAUnitySetUserConsent(consent);
        }

        public void SetLocationConsent(bool consent)
        {
            MobileAdsBridge.YMAUnitySetLocationConsent(consent);
        }

        public void SetAgeRestrictedUser(bool ageRestrictedUser)
        {
            MobileAdsBridge.YMAUnitySetAgeRestrictedUser(ageRestrictedUser);
        }

        public void ShowDebugPanel()
        {   
            MobileAdsBridge.YMAUnityShowDebugPanel();
        }
    }

    #endif
}
