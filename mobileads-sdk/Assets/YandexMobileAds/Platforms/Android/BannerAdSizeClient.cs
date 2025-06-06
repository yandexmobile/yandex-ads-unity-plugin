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
using YandexMobileAds.Common;

namespace YandexMobileAds.Platforms.Android
{
    internal class BannerAdSizeClient : IBannerAdSizeClient
    {
        private AndroidJavaObject _bannerAdSizeObject;

        internal BannerAdSizeClient(AndroidJavaObject bannerAdSize)
        {
            _bannerAdSizeObject = bannerAdSize;
        }

        public int GetHeight()
        {
            return NativeApi.GetHeightInPixels(_bannerAdSizeObject, Utils.GetCurrentActivity());
        }

        public int GetWidth()
        {
            return NativeApi.GetWidthInPixels(_bannerAdSizeObject, Utils.GetCurrentActivity());
        }

        internal AndroidJavaObject GetBannerAdSizeAndroidJavaObject()
        {
            return _bannerAdSizeObject;
        }

        private static class NativeApi
        {
            public static int GetHeightInPixels(AndroidJavaObject bannerAdSize, AndroidJavaObject context)
            {
                return bannerAdSize.Call<int>(methodName: "getHeightInPixels", context);
            }

            public static int GetWidthInPixels(AndroidJavaObject bannerAdSize, AndroidJavaObject context)
            {
                return bannerAdSize.Call<int>(methodName: "getWidthInPixels", context);

            }
        }
    }
}
