/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Android (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System.Collections.Generic;
using UnityEngine;
using YandexMobileAds.Base;

namespace YandexMobileAds.Platforms.Android
{
    internal class AdInfoFactory
    {
        public static AdInfo CreateAdInfo(AndroidJavaObject nativeAdInfo)
        {
            string adUnitId = NativeApi.GetAdUnitId(nativeAdInfo);
            string extraData = NativeApi.GetExtraData(nativeAdInfo);
            string partnerText = NativeApi.GetPartnerText(nativeAdInfo);
            List<Creative> creatives = NativeApi.GetCreatives(nativeAdInfo);

            return new AdInfo(adUnitId, extraData, partnerText, creatives);
        }

        private static class NativeApi
        {
            public static string GetAdUnitId(AndroidJavaObject adInfo)
            {
                return adInfo.Call<string>("getAdUnitId");
            }

            public static string GetExtraData(AndroidJavaObject adInfo)
            {
                return adInfo.Call<string>("getExtraData");
            }

            public static string GetPartnerText(AndroidJavaObject adInfo)
            {
                return adInfo.Call<string>("getPartnerText");
            }

            public static List<Creative> GetCreatives(AndroidJavaObject adInfo)
            {
                AndroidJavaObject javaList = adInfo.Call<AndroidJavaObject>("getCreatives");
                if (javaList == null)
                {
                    return new List<Creative>();
                }

                int count = javaList.Call<int>("size");
                List<Creative> creatives = new List<Creative>(count);
                for (int i = 0; i < count; i++)
                {
                    AndroidJavaObject javaCreative = javaList.Call<AndroidJavaObject>("get", i);
                    string creativeId = javaCreative.Call<string>("getCreativeId");
                    string campaignId = javaCreative.Call<string>("getCampaignId");
                    string placeId = javaCreative.Call<string>("getPlaceId");
                    creatives.Add(new Creative(creativeId, campaignId, placeId));
                }
                return creatives;
            }
        }
    }
}
