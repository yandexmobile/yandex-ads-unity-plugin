/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using UnityEngine;
using YandexMobileAds.Common;

namespace YandexMobileAds.Platforms.Android
{
    public class MobileAdsClient : AndroidJavaProxy, IMobileAdsClient
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

        private readonly AndroidJavaClass _mobileAdsClass;

        private MobileAdsClient() : base(Utils.MobileAdsClassName)
        {
            this._mobileAdsClass = new AndroidJavaClass(Utils.MobileAdsClassName);
        }

        public void SetUserConsent(bool consent)
        {
            this._mobileAdsClass.CallStatic("setUserConsent", consent);
        }

        public void SetLocationConsent(bool consent)
        {
            this._mobileAdsClass.CallStatic("setLocationConsent", consent);
        }

        public void SetAgeRestrictedUser(bool ageRestrictedUser)
        {
            this._mobileAdsClass.CallStatic("setAgeRestrictedUser", ageRestrictedUser);
        }

        public void ShowDebugPanel()
        {   
            AndroidJavaObject activity = Utils.GetCurrentActivity(); 
            this._mobileAdsClass.CallStatic("showDebugPanel", activity);
        }
    }
}
