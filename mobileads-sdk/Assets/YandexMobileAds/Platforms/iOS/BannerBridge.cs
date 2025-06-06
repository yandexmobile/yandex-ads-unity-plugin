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

    internal class BannerBridge
    {
        [DllImport("__Internal")]
        internal static extern string YMAUnityCreateBannerView(IntPtr clientRef,
            string adUnitId, string adSizeId, int position);

        [DllImport("__Internal")]
        internal static extern void YMAUnityLoadBannerView(string objectId,
                                                           string adRequestId);

        [DllImport("__Internal")]
        internal static extern void YMAUnityShowBannerView(string objectId);

        [DllImport("__Internal")]
        internal static extern void YMAUnityHideBannerView(string objectId);

        [DllImport("__Internal")]
        internal static extern void YMAUnitySetBannerCallbacks(
            string objectId,
            BannerClient.YMAUnityAdViewDidReceiveAdCallback adReceivedCallback,
            BannerClient.YMAUnityAdViewDidFailToReceiveAdWithErrorCallback adFailedCallback,
            BannerClient.YMAUnityAdViewWillPresentScreenCallback willPresentCallback,
            BannerClient.YMAUnityAdViewDidDismissScreenCallback didDismissCallback,
            BannerClient.YMAUnityAdViewDidTrackImpressionCallback didTrackImpressionCallback,
            BannerClient.YMAUnityAdViewWillLeaveApplicationCallback willLeaveCallback,
            BannerClient.YMAUnityAdViewDidClickCallback didClickCallback);
    }

    #endif
}
