/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Unity (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using YandexMobileAds.Common;
using YandexMobileAds.Base;
using System;

namespace YandexMobileAds.Platforms
{
    public class YandexMobileAdsClientFactory
    {

        internal static IBannerAdSizeClient BuildBannerAdSizeClient(int width, int height, BannerAdSizeType bannerAdSizeType)
        {
            #if UNITY_EDITOR
                return new DummyBannerAdSizeClient(width, height);
            #elif UNITY_ANDROID
                return YandexMobileAds.Platforms.Android.BannerAdSizeUtils.GetBannerAdSizeClient(width, height, bannerAdSizeType);
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return new YandexMobileAds.Platforms.iOS.BannerAdSizeClient(width, height, bannerAdSizeType);
            #else
                return new DummyBannerAdSizeClient(width, height);
            #endif
        }

        internal static IBannerClient BuildBannerClient(string blockId, BannerAdSize adSize, AdPosition position)
        {
            #if UNITY_EDITOR
                return new DummyBannerClient();
            #elif UNITY_ANDROID
                var adSizeClient = (YandexMobileAds.Platforms.Android.BannerAdSizeClient) adSize.GetClient();
                return new YandexMobileAds.Platforms.Android.BannerClient(blockId, adSizeClient, position);
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                var adSizeClient = (YandexMobileAds.Platforms.iOS.BannerAdSizeClient) adSize.GetClient();
                return new YandexMobileAds.Platforms.iOS.BannerClient(blockId, adSizeClient, position);
            #else
                return new DummyBannerClient();
            #endif
        }

        internal static IInterstitialAdLoaderClient BuildInterstitialAdLoaderClient()
        {
            #if UNITY_EDITOR
                return new DummyInterstitialAdLoaderClient();
            #elif UNITY_ANDROID
                return new YandexMobileAds.Platforms.Android.InterstitialAdLoaderClient();
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return new YandexMobileAds.Platforms.iOS.InterstitialAdLoaderClient();
            #else
                return new DummyInterstitialAdLoaderClient();
            #endif
        }

        internal static IRewardedAdLoaderClient BuildRewardedAdLoaderClient()
        {
            #if UNITY_EDITOR
                return new DummyRewardedAdLoaderClient();
            #elif UNITY_ANDROID
                return new YandexMobileAds.Platforms.Android.RewardedAdLoaderClient();
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return new YandexMobileAds.Platforms.iOS.RewardedAdLoaderClient();
            #else
                return new DummyRewardedAdLoaderClient();
            #endif
        }

        internal static IAppOpenAdLoaderClient BuildAppOpenAdLoaderClient()
        {
            #if UNITY_EDITOR
                return new DummyAppOpenAdLoaderClient();
            #elif UNITY_ANDROID
                return new YandexMobileAds.Platforms.Android.AppOpenAdLoaderClient();
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return new YandexMobileAds.Platforms.iOS.AppOpenAdLoaderClient();
            #else
                return new DummyAppOpenAdLoaderClient();
            #endif
        }

        internal static IAppStateObserverClient BuildAppStateObserverClient()
        {
            #if UNITY_EDITOR
                return YandexMobileAds.Common.AppStateObserverClient.Instance;
            #elif UNITY_ANDROID
                return new YandexMobileAds.Platforms.Android.AppStateObserverClient();
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return YandexMobileAds.Common.AppStateObserverClient.Instance;
            #else
                return YandexMobileAds.Common.AppStateObserverClient.Instance;
            #endif
        }

        internal static IMobileAdsClient CreateMobileAdsClient()
        {
            #if UNITY_EDITOR
                return new YandexMobileAds.Common.DummyMobileAdsClient();
            #elif UNITY_ANDROID
                return YandexMobileAds.Platforms.Android.MobileAdsClient.GetInstance();
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return YandexMobileAds.Platforms.iOS.MobileAdsClient.GetInstance();
            #else
                return new YandexMobileAds.Common.DummyMobileAdsClient();
            #endif
        }

        internal static IScreenClient CreateScreenClient()
        {
            #if UNITY_EDITOR
                return new YandexMobileAds.Common.DummyScreenClient();
            #elif UNITY_ANDROID
                return YandexMobileAds.Platforms.Android.ScreenClient.GetInstance();
            #elif (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
                return YandexMobileAds.Platforms.iOS.ScreenClient.GetInstance();
            #else
                return new YandexMobileAds.Common.DummyScreenClient();
            #endif
        }
    }
}
