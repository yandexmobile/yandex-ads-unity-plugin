/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Android (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using UnityEngine;
using YandexMobileAds.Base;

namespace YandexMobileAds.Platforms.Android
{
    internal class AdInfoFactory
    {
        public static AdInfo CreateAdInfo(AndroidJavaObject nativeAdInfo)
        {
            string AdUnitId = NativeApi.GetAdUnitId(nativeAdInfo);
            AdSize AdSize = null;

            AndroidJavaObject adSizeObject = NativeApi.GetAdSize(nativeAdInfo);

            if (adSizeObject != null)
            {

                AdSize = AdSizeUtils.CreateAdSize(adSizeObject);
            }

            return new AdInfo(AdUnitId, AdSize);
        }

        private static class NativeApi
        {

            public static string GetAdUnitId(AndroidJavaObject adInfoJavaObject)
            {
                return adInfoJavaObject.Call<string>("getAdUnitId");
            }

            public static AndroidJavaObject GetAdSize(AndroidJavaObject adInfoJavaObject)
            {
                return adInfoJavaObject.Call<AndroidJavaObject>("getAdSize");
            }
        }
    }
}
