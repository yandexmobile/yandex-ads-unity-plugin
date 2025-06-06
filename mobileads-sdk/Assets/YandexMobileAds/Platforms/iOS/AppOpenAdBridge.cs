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

    internal class AppOpenAdBridge
    {
        [DllImport("__Internal")]
        internal static extern string YMAUnityCreateAppOpenAd(
            IntPtr clientRef, string appOpenAdObjectId);

        [DllImport("__Internal")]
        internal static extern void YMAUnityShowAppOpenAd(string objectId);

        [DllImport("__Internal")]
        internal static extern void YMAUnitySetAppOpenAdCallbacks(
            string objectId,
            AppOpenAdClient.YMAUnityAppOpenAdDidFailToShowCallback appOpenAdFailedToShowCallback,
            AppOpenAdClient.YMAUnityAppOpenAdDidShowCallback appOpenAdDidShowCallback,
            AppOpenAdClient.YMAUnityAppOpenAdDidDismissCallback appOpenAdDidDismissCallback,
            AppOpenAdClient.YMAUnityAppOpenAdDidClickCallback appOpenAdDidClickCallback,
            AppOpenAdClient.YMAUnityAppOpenAdDidTrackImpressionCallback appOpenAdDidImpressionTracked);

        [DllImport("__Internal")]
        internal static extern void YMAUnityDestroyAppOpenAd(string objectId);
    }

#endif
}
