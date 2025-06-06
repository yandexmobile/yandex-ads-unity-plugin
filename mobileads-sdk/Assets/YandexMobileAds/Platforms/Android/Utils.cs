/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Android (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using UnityEngine;
using System.Collections.Generic;
using YandexMobileAds.Base;
using System;

namespace YandexMobileAds.Platforms.Android
{
    internal class Utils
    {
        public const string AdRequestBuilderClassName = "com.yandex.mobile.ads.common.AdRequest$Builder";
        public const string AdRequestConfigurationBuilderClassName = "com.yandex.mobile.ads.common.AdRequestConfiguration$Builder";

        public const string BannerViewClassName = "com.yandex.mobile.ads.unity.wrapper.banner.BannerAdWrapper";

        public const string InterstitialClassName =
            "com.yandex.mobile.ads.unity.wrapper.interstitial.InterstitialAdWrapper";

        public const string RewardedAdClassName =
            "com.yandex.mobile.ads.unity.wrapper.rewarded.RewardedAdWrapper";

        public const string LocationClassName = "android.location.Location";

        public const string MobileAdsClassName = "com.yandex.mobile.ads.common.MobileAds";

        public const string UnityBannerAdListenerClassName =
            "com.yandex.mobile.ads.unity.wrapper.banner.UnityBannerAdListener";

        public const string UnityInterstitialAdListenerClassName =
            "com.yandex.mobile.ads.unity.wrapper.interstitial.UnityInterstitialAdListener";

        public const string UnityRewardedAdListenerClassName =
            "com.yandex.mobile.ads.unity.wrapper.rewarded.UnityRewardedAdListener";

        public const string UnityActivityClassName = "com.unity3d.player.UnityPlayer";

        public const string AdThemeClassName = "com.yandex.mobile.ads.common.AdTheme";

        public static AndroidJavaObject GetAdRequestJavaObject(AdRequest adRequest)
        {
            if (adRequest == null)
            {
                return null;
            }

            AndroidJavaObject adRequestBuilder = new AndroidJavaObject(AdRequestBuilderClassName);

            if (adRequest.ContextQuery != null)
            {
                adRequestBuilder.Call<AndroidJavaObject>(
                    "setContextQuery",
                    adRequest.ContextQuery
                );
            }

            if (adRequest.ContextTags != null)
            {
                adRequestBuilder.Call<AndroidJavaObject>("setContextTags",
                    stringListToJavaStringArrayList(adRequest.ContextTags));
            }

            if (adRequest.Location != null)
            {
                adRequestBuilder.Call<AndroidJavaObject>("setLocation",
                    locationToJavaLocation(adRequest.Location));
            }

            Dictionary<string, string> parameters = adRequest.Parameters;
            if (parameters != null)
            {
                adRequestBuilder.Call<AndroidJavaObject>("setParameters",
                    dictionaryToJavaHashMap(parameters));
            }

            if (adRequest.Age != null)
            {
                adRequestBuilder.Call<AndroidJavaObject>("setAge",
                    adRequest.Age);
            }

            if (adRequest.Gender != null)
            {
                adRequestBuilder.Call<AndroidJavaObject>("setGender",
                    adRequest.Gender);
            }

            if (adRequest.AdTheme != AdTheme.None)
            {
                AndroidJavaClass adThemeEnum = new AndroidJavaClass(AdThemeClassName);
                AndroidJavaObject enumEntry;

                if (adRequest.AdTheme == AdTheme.Light)
                {
                    enumEntry = adThemeEnum.GetStatic<AndroidJavaObject>("LIGHT");

                }
                else
                {
                    enumEntry = adThemeEnum.GetStatic<AndroidJavaObject>("DARK");
                }

                adRequestBuilder.Call<AndroidJavaObject>("setPreferredTheme", enumEntry);
            }

            return adRequestBuilder.Call<AndroidJavaObject>("build");
        }

        public static AndroidJavaObject GetAdRequestConfigurationJavaObject(AdRequestConfiguration adRequestConfiguration)
        {
            if (adRequestConfiguration == null)
            {
                return null;
            }

            if (adRequestConfiguration.AdUnitId == null)
            {
                return null;
            }

            AndroidJavaObject adRequestConfigurationBuilder = new AndroidJavaObject(
                AdRequestConfigurationBuilderClassName,
                adRequestConfiguration.AdUnitId
            );

            if (adRequestConfiguration.ContextQuery != null)
            {
                adRequestConfigurationBuilder.Call<AndroidJavaObject>(
                    "setContextQuery",
                    adRequestConfiguration.ContextQuery
                );
            }

            if (adRequestConfiguration.ContextTags != null)
            {
                adRequestConfigurationBuilder.Call<AndroidJavaObject>("setContextTags",
                    stringListToJavaStringArrayList(adRequestConfiguration.ContextTags));
            }

            if (adRequestConfiguration.Location != null)
            {
                adRequestConfigurationBuilder.Call<AndroidJavaObject>("setLocation",
                    locationToJavaLocation(adRequestConfiguration.Location));
            }

            Dictionary<string, string> parameters = adRequestConfiguration.Parameters;
            if (parameters != null)
            {
                adRequestConfigurationBuilder.Call<AndroidJavaObject>("setParameters",
                    dictionaryToJavaHashMap(parameters));
            }

            if (adRequestConfiguration.Age != null)
            {
                adRequestConfigurationBuilder.Call<AndroidJavaObject>("setAge",
                    adRequestConfiguration.Age);
            }

            if (adRequestConfiguration.Gender != null)
            {
                adRequestConfigurationBuilder.Call<AndroidJavaObject>("setGender",
                    adRequestConfiguration.Gender);
            }

            if (adRequestConfiguration.AdTheme != AdTheme.None)
            {
                AndroidJavaClass adThemeEnum = new AndroidJavaClass(AdThemeClassName);
                AndroidJavaObject enumEntry;

                if (adRequestConfiguration.AdTheme == AdTheme.Light)
                {
                    enumEntry = adThemeEnum.GetStatic<AndroidJavaObject>("LIGHT");

                }
                else
                {
                    enumEntry = adThemeEnum.GetStatic<AndroidJavaObject>("DARK");
                }

                adRequestConfigurationBuilder.Call<AndroidJavaObject>("setPreferredTheme", enumEntry);
            }


            return adRequestConfigurationBuilder.Call<AndroidJavaObject>("build");
        }

        public static AndroidJavaObject GetCurrentActivity()
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(UnityActivityClassName);
            AndroidJavaObject activity =
                playerClass.GetStatic<AndroidJavaObject>("currentActivity");

            return activity;
        }

        private static AndroidJavaObject dictionaryToJavaHashMap(Dictionary<string, string> parameters)
        {
            AndroidJavaObject map = new AndroidJavaObject("java.util.HashMap");

            foreach (KeyValuePair<string, string> entry in parameters)
            {
                map.Call<string>("put", entry.Key, entry.Value);
            }

            return map;
        }

        private static AndroidJavaObject locationToJavaLocation(Location location)
        {
            AndroidJavaObject locationObject = new AndroidJavaObject(LocationClassName, "");

            locationObject.Call("setLatitude", location.Latitude);
            locationObject.Call("setLongitude", location.Longitude);
            locationObject.Call("setAccuracy", (float)location.HorizontalAccuracy);

            return locationObject;
        }

        private static AndroidJavaObject stringListToJavaStringArrayList(List<string> list)
        {
            AndroidJavaObject javaList = new AndroidJavaObject("java.util.ArrayList");

            foreach (string item in list)
            {
                javaList.Call<bool>("add", new object[] { item });
            }

            return javaList;
        }
    }
}
