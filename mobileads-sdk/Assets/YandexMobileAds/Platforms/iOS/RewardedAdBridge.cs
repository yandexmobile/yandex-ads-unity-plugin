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

    internal class RewardedAdBridge
    {
        [DllImport("__Internal")]
        internal static extern string YMAUnityCreateRewardedAd(
            IntPtr clientRef, string rewardedAdObjectId);

        [DllImport("__Internal")]
        internal static extern string YMAUnityGetRewardedInfo(string rewardedAdObjectId);

        [DllImport("__Internal")]
        internal static extern void YMAUnityShowRewardedAd(string objectId);

        [DllImport("__Internal")]
        internal static extern void YMAUnitySetRewardedAdCallbacks(
            string objectId,
            RewardedAdClient.YMAUnityRewardedAdDidRewardCallback rewardedDidRewardCallback,
            RewardedAdClient.YMAUnityRewardedAdDidFailToShowCallback rewardedFailedToShowCallback,
            RewardedAdClient.YMAUnityRewardedAdDidShowCallback rewardedDidShowCallback,
            RewardedAdClient.YMAUnityRewardedAdDidDismissCallback rewardedDidDismissCallback,
            RewardedAdClient.YMAUnityRewardedAdDidClickCallback rewardedDidClickCallback,
            RewardedAdClient.YMAUnityRewardedAdDidTrackImpressionCallback rewardedDidImpressionTracked);

        [DllImport("__Internal")]
        internal static extern void YMAUnityDestroyRewardedAd(string objectId);
    }

    #endif
}
