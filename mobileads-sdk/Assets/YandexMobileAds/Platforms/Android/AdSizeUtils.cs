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
    internal class AdSizeUtils
    {
        public static AdSize CreateAdSize(AndroidJavaObject adSizeJavaObject)
        {
            int Width = NativeApi.GetWidth(adSizeJavaObject);
            int Height = NativeApi.GetHeight(adSizeJavaObject);
            return new AdSize(Width, Height);
        }

        private static class NativeApi
        {
            public static int GetWidth(AndroidJavaObject adSizeJavaObject)
            {
                return adSizeJavaObject.Call<int>("getWidth");
            }

            public static int GetHeight(AndroidJavaObject adSizeJavaObject)
            {
                return adSizeJavaObject.Call<int>("getHeight");
            }
        }
    }
}
