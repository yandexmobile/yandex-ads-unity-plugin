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

    internal class InterstitialBridge
    {
        [DllImport("__Internal")]
        internal static extern string YMAUnityCreateInterstitialAd(
            IntPtr clientRef, string interstitialAdObjectId);

        [DllImport("__Internal")]
        internal static extern string YMAUnityGetInterstitialInfo(string interstitialAdObjectId);

        [DllImport("__Internal")]
        internal static extern void YMAUnityShowInterstitialAd(string objectId);

        [DllImport("__Internal")]
        internal static extern void YMAUnitySetInterstitialAdCallbacks(
            string objectId,
            InterstitialClient.YMAUnityInterstitialAdDidFailToShowCallback interstitialFailedToShowCallback,
            InterstitialClient.YMAUnityInterstitialAdDidShowCallback interstitialDidShowCallback,
            InterstitialClient.YMAUnityInterstitialAdDidDismissCallback interstitialDidDismissCallback,
            InterstitialClient.YMAUnityInterstitialAdDidClickCallback interstitialDidClickCallback,
            InterstitialClient.YMAUnityInterstitialAdDidTrackImpressionCallback interstitialDidImpressionTracked);

        [DllImport("__Internal")]
        internal static extern void YMAUnityDestroyInterstitialAd(string objectId);
    }

#endif
}
