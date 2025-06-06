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
using YandexMobileAds.Base;
using YandexMobileAds.Common;

namespace YandexMobileAds.Platforms.iOS
{
    #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE

    internal class RewardedAdClient : IRewardedAdClient, IDisposable
    {
        internal delegate void YMAUnityRewardedAdDidRewardCallback(IntPtr bannerClient, int amount, string type);

        internal delegate void YMAUnityRewardedAdDidFailToShowCallback(IntPtr bannerClient, string error);

        internal delegate void YMAUnityRewardedAdDidShowCallback(IntPtr bannerClient);

        internal delegate void YMAUnityRewardedAdDidDismissCallback(IntPtr bannerClient);

        internal delegate void YMAUnityRewardedAdDidClickCallback(IntPtr bannerClient);

        internal delegate void YMAUnityRewardedAdDidTrackImpressionCallback(
            IntPtr bannerClient,
            string rawImpressionData);

        public event EventHandler<Reward> OnRewarded;
        public event EventHandler<AdFailureEventArgs> OnAdFailedToShow;
        public event EventHandler<EventArgs> OnAdShown;
        public event EventHandler<EventArgs> OnAdDismissed;
        public event EventHandler<EventArgs> OnAdClicked;
        public event EventHandler<ImpressionData> OnAdImpression;

        public string ObjectId { get; private set; }

        private readonly IntPtr _selfPointer;
        private readonly AdInfo _adInfo;

        public RewardedAdClient(string rewardedAdObjectId)
        {
            this._selfPointer = GCHandle.ToIntPtr(GCHandle.Alloc(this));
            this.ObjectId = RewardedAdBridge.YMAUnityCreateRewardedAd(
                this._selfPointer, rewardedAdObjectId);
            RewardedAdBridge.YMAUnitySetRewardedAdCallbacks(
                this.ObjectId,
                RewardedAdDidRewardCallback,
                RewardedDidFailToShowCallback,
                RewardedAdDidShowCallback,
                RewardedAdDidDismissCallback,
                RewardedAdDidClickCallback,
                RewardedAdDidTrackImpressionCallback);

            string adInfoObjectId = RewardedAdBridge.YMAUnityGetRewardedInfo(this.ObjectId);
            AdInfoClient adInfoClient = new AdInfoClient(adInfoObjectId);
            this._adInfo = new AdInfo(
                adInfoClient.AdUnitId,
                adInfoClient.AdSize
            );
            adInfoClient.Destroy();
        }

        public AdInfo GetInfo()
        {
            return this._adInfo;
        }

        public void Show()
        {
            RewardedAdBridge.YMAUnityShowRewardedAd(this.ObjectId);
        }

        public void Destroy()
        {
            this.OnAdShown = null;
            this.OnAdClicked = null;
            this.OnAdDismissed = null;
            this.OnAdFailedToShow = null;
            this.OnAdImpression = null;
            this.OnRewarded = null;

            RewardedAdBridge.YMAUnityDestroyRewardedAd(this.ObjectId);
        }

        public void Dispose()
        {
            this.Destroy();
        }

        ~RewardedAdClient()
        {
            this.Destroy();
        }

        private static RewardedAdClient IntPtrToRewardedAdClient(IntPtr rewardedAdClient)
        {
            GCHandle handle = GCHandle.FromIntPtr(rewardedAdClient);
            return handle.Target as RewardedAdClient;
        }

        #region Rewarded callback methods

        [MonoPInvokeCallback(typeof(YMAUnityRewardedAdDidRewardCallback))]
        private static void RewardedAdDidRewardCallback(
            IntPtr rewardedAdClient, int amount, string type)
        {
            RewardedAdClient client = IntPtrToRewardedAdClient(rewardedAdClient);
            Reward reward = new Reward(amount, type);
            if (client.OnRewarded != null)
            {
                client.OnRewarded(client, reward);
            }
        }


        [MonoPInvokeCallback(typeof(YMAUnityRewardedAdDidFailToShowCallback))]
        private static void RewardedDidFailToShowCallback(
            IntPtr rewardedAdClient, string error)
        {
            RewardedAdClient client = IntPtrToRewardedAdClient(
                rewardedAdClient);
            if (client.OnAdFailedToShow != null)
            {
                AdFailureEventArgs args = new AdFailureEventArgs()
                {
                    Message = error
                };
                client.OnAdFailedToShow(client, args);
            }
        }

        [MonoPInvokeCallback(typeof(YMAUnityRewardedAdDidShowCallback))]
        private static void RewardedAdDidShowCallback(IntPtr rewardedAdClient)
        {
            RewardedAdClient client = IntPtrToRewardedAdClient(
                rewardedAdClient);
            if (client.OnAdShown != null)
            {
                client.OnAdShown(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(YMAUnityRewardedAdDidDismissCallback))]
        private static void RewardedAdDidDismissCallback(IntPtr rewardedAdClient)
        {
            RewardedAdClient client = IntPtrToRewardedAdClient(
                rewardedAdClient);
            if (client.OnAdDismissed != null)
            {
                client.OnAdDismissed(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(YMAUnityRewardedAdDidClickCallback))]
        private static void RewardedAdDidClickCallback(
            IntPtr rewardedAdClient)
        {
            RewardedAdClient client = IntPtrToRewardedAdClient(rewardedAdClient);
            if (client.OnAdClicked != null)
            {
                client.OnAdClicked(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(YMAUnityRewardedAdDidTrackImpressionCallback))]
        private static void RewardedAdDidTrackImpressionCallback(
            IntPtr rewardedAdClient, string rawImpressionData)
        {
            RewardedAdClient client = IntPtrToRewardedAdClient(
                rewardedAdClient);
            if (client.OnAdImpression != null)
            {
                ImpressionData impressionData = new ImpressionData(rawImpressionData == null ? "" : rawImpressionData);
                client.OnAdImpression(client, impressionData);
            }
        }

        #endregion
    }

    #endif
}
