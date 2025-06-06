/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;
using System.Runtime.InteropServices;

namespace YandexMobileAds.Platforms.iOS
{
    #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE

    internal class InterstitialAdLoaderBridge
    {

        [DllImport("__Internal")]
        internal static extern string YMAUnityCreateInterstitialAdLoader(IntPtr clientRef);

        [DllImport("__Internal")]
        internal static extern void YMAUnitySetInterstitialAdLoaderCallbacks(
            string objectId,
            InterstitialAdLoaderClient.YMAUnityInterstitialDidLoadAdCallback didLoadAdCallback,
            InterstitialAdLoaderClient.YMAUnityInterstitialDidFailToLoadAdCallback didFailToLoadAdCallback);

        [DllImport("__Internal")]
        internal static extern void YMAUnityLoadInterstitialAd(
            string objectId, string adRequestConfigurationId);

        [DllImport("__Internal")]
        internal static extern void YMAUnityCancelLoadingInterstitialAd(string objectId);

    }

    #endif
}
