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

    internal class RewardedAdLoaderClient : IRewardedAdLoaderClient, IDisposable
    {
        public string ObjectId { get; private set; }


        internal delegate void YMAUnityRewardedDidLoadAdCallback(
            IntPtr bannerClient, string rewardedAdObjectId);

        internal delegate void YMAUnityRewardedDidFailToLoadAdCallback(
            IntPtr bannerClient, string adUnitId, string error);

        public event EventHandler<GenericEventArgs<IRewardedAdClient>> OnAdLoaded;
        public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

        private readonly IntPtr _selfPointer;

        public RewardedAdLoaderClient()
        {
            this._selfPointer = GCHandle.ToIntPtr(GCHandle.Alloc(this));
            this.ObjectId = RewardedAdLoaderBridge.YMAUnityCreateRewardedAdLoader(
                this._selfPointer);
            RewardedAdLoaderBridge.YMAUnitySetRewardedAdLoaderCallbacks(
                this.ObjectId,
                RewardedDidLoadAdCallback,
                RewardedDidFailToLoadAdCallback);
        }

        public void LoadAd(AdRequestConfiguration adRequestConfiguration)
        {
            if (adRequestConfiguration == null) {
                RewardedDidFailToLoadAdCallback(
                    _selfPointer,
                    "",
                    Constants.AdRequestConfigurationIsNullErrorMessage);
                return;
            }

            AdRequestConfigurationClient adRequest = new AdRequestConfigurationClient(adRequestConfiguration);
            RewardedAdLoaderBridge.YMAUnityLoadRewardedAd(this.ObjectId, adRequest.ObjectId);
        }

        public void CancelLoading()
        {
            RewardedAdLoaderBridge.YMAUnityCancelLoadingRewardedAd(this.ObjectId);
        }

        public void Destroy()
        {
            this.OnAdLoaded = null;
            this.OnAdFailedToLoad = null;

            ObjectBridge.YMAUnityDestroyObject(this.ObjectId);
        }

        public void Dispose()
        {
            this.Destroy();
        }

        ~RewardedAdLoaderClient()
        {
            this.Destroy();
        }

        private static RewardedAdLoaderClient IntPtrToRewardedAdLoaderClient(
            IntPtr rewardedAdLoaderClient)
        {
            GCHandle handle = GCHandle.FromIntPtr(rewardedAdLoaderClient);
            return handle.Target as RewardedAdLoaderClient;
        }

        #region RewardedAdLoader callback methods

        [MonoPInvokeCallback(typeof(YMAUnityRewardedDidLoadAdCallback))]
        private static void RewardedDidLoadAdCallback(
            IntPtr rewardedAdLoaderClient,
            string rewardedAdObjectId)
        {
            RewardedAdLoaderClient client = IntPtrToRewardedAdLoaderClient(rewardedAdLoaderClient);
            if (client.OnAdLoaded != null)
            {
                GenericEventArgs<IRewardedAdClient> args = new GenericEventArgs<IRewardedAdClient>()
                {
                    Value = new RewardedAdClient(rewardedAdObjectId)
                };
                client.OnAdLoaded(client, args);
            }
        }

        [MonoPInvokeCallback(typeof(YMAUnityRewardedDidFailToLoadAdCallback))]
        private static void RewardedDidFailToLoadAdCallback(
            IntPtr rewardedClient, string adUnitId, string error)
        {
            RewardedAdLoaderClient client = IntPtrToRewardedAdLoaderClient(rewardedClient);
            if (client.OnAdLoaded != null)
            {
                AdFailedToLoadEventArgs args = new AdFailedToLoadEventArgs()
                {
                    Message = error,
                    AdUnitId = adUnitId
                };
                client.OnAdFailedToLoad(client, args);
            }
        }

        #endregion
    }

    #endif
}
