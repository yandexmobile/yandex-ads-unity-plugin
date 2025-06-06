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

    internal class InterstitialAdLoaderClient : IInterstitialAdLoaderClient, IDisposable
    {
        internal delegate void YMAUnityInterstitialDidLoadAdCallback(
            IntPtr bannerClient, string interstitialAdObjectId);

        internal delegate void YMAUnityInterstitialDidFailToLoadAdCallback(
            IntPtr bannerClient, string adUnitId, string error);

        public event EventHandler<GenericEventArgs<IInterstitialClient>> OnAdLoaded;
        public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

        public string ObjectId { get; private set; }

        private readonly IntPtr _selfPointer;

        public InterstitialAdLoaderClient()
        {
            this._selfPointer = GCHandle.ToIntPtr(GCHandle.Alloc(this));
            this.ObjectId = InterstitialAdLoaderBridge.YMAUnityCreateInterstitialAdLoader(
                this._selfPointer);
            InterstitialAdLoaderBridge.YMAUnitySetInterstitialAdLoaderCallbacks(
                this.ObjectId,
                InterstitialDidLoadAdCallback,
                InterstitialDidFailToLoadAdCallback);
        }

        public void LoadAd(AdRequestConfiguration adRequestConfiguration)
        {
            if (adRequestConfiguration == null) {
                InterstitialDidFailToLoadAdCallback(
                    _selfPointer,
                    "",
                    Constants.AdRequestConfigurationIsNullErrorMessage);
                return;
            }

            AdRequestConfigurationClient adRequest = new AdRequestConfigurationClient(adRequestConfiguration);
            InterstitialAdLoaderBridge.YMAUnityLoadInterstitialAd(this.ObjectId, adRequest.ObjectId);
        }

        public void CancelLoading()
        {
            InterstitialAdLoaderBridge.YMAUnityCancelLoadingInterstitialAd(this.ObjectId);
        }

        public void Destroy()
        {
            ObjectBridge.YMAUnityDestroyObject(this.ObjectId);

            this.OnAdLoaded = null;
            this.OnAdFailedToLoad = null;
        }

        public void Dispose()
        {
            this.Destroy();
        }

        ~InterstitialAdLoaderClient()
        {
            this.Destroy();
        }

        private static InterstitialAdLoaderClient IntPtrToInterstitialAdLoaderClient(
            IntPtr interstitialAdLoaderClient)
        {
            GCHandle handle = GCHandle.FromIntPtr(interstitialAdLoaderClient);
            return handle.Target as InterstitialAdLoaderClient;
        }

        #region InterstitialAdLoader callback methods

        [MonoPInvokeCallback(typeof(YMAUnityInterstitialDidLoadAdCallback))]
        private static void InterstitialDidLoadAdCallback(
            IntPtr interstitialAdLoaderClient,
            string interstitialAdObjectId)
        {
            InterstitialAdLoaderClient client = IntPtrToInterstitialAdLoaderClient(interstitialAdLoaderClient);
            if (client.OnAdLoaded != null)
            {
                GenericEventArgs<IInterstitialClient> args = new GenericEventArgs<IInterstitialClient>()
                {
                    Value = new InterstitialClient(interstitialAdObjectId)
                };
                client.OnAdLoaded(client, args);
            }
        }

        [MonoPInvokeCallback(typeof(YMAUnityInterstitialDidFailToLoadAdCallback))]
        private static void InterstitialDidFailToLoadAdCallback(
            IntPtr interstitialClient, string adUnitId, string error)
        {
            InterstitialAdLoaderClient client = IntPtrToInterstitialAdLoaderClient(interstitialClient);
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
