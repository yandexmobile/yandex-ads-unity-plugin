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

    internal class AppOpenAdLoaderClient : IAppOpenAdLoaderClient, IDisposable
    {
        internal delegate void YMAUnityAppOpenAdDidLoadAdCallback(
            IntPtr bannerClient, string appOpenAdObjectId);
        internal delegate void YMAUnityAppOpenAdDidFailToLoadAdCallback(
            IntPtr bannerClient, string adUnitId, string error);

        public event EventHandler<GenericEventArgs<IAppOpenAdClient>> OnAdLoaded;
        public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

        public string ObjectId { get; private set; }

        private readonly IntPtr _selfPointer;

        public AppOpenAdLoaderClient()
        {
            this._selfPointer = GCHandle.ToIntPtr(GCHandle.Alloc(this));
            this.ObjectId = AppOpenAdLoaderBridge.YMAUnityCreateAppOpenAdLoader(
                this._selfPointer);
            AppOpenAdLoaderBridge.YMAUnitySetAppOpenAdLoaderCallbacks(
                this.ObjectId,
                AppOpenAdDidLoadAdCallback,
                AppOpenAdDidFailToLoadAdCallback);
        }

        public void LoadAd(AdRequestConfiguration adRequestConfiguration)
        {
            if (adRequestConfiguration == null) {
                AppOpenAdDidFailToLoadAdCallback(
                    _selfPointer,
                    "",
                    Constants.AdRequestConfigurationIsNullErrorMessage);
                return;
            }

            AdRequestConfigurationClient adRequest = new AdRequestConfigurationClient(adRequestConfiguration);
            AppOpenAdLoaderBridge.YMAUnityLoadAppOpenAd(this.ObjectId, adRequest.ObjectId);
        }

        public void CancelLoading()
        {
            AppOpenAdLoaderBridge.YMAUnityCancelLoadingAppOpenAd(this.ObjectId);
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

        ~AppOpenAdLoaderClient()
        {
            this.Destroy();
        }

        private static AppOpenAdLoaderClient IntPtrToAppOpenAdLoaderClient(
            IntPtr appOpenAdLoaderClient)
        {
            GCHandle handle = GCHandle.FromIntPtr(appOpenAdLoaderClient);
            return handle.Target as AppOpenAdLoaderClient;
        }

        #region AppOpenAdLoader callback methods

        [MonoPInvokeCallback(typeof(YMAUnityAppOpenAdDidLoadAdCallback))]
        private static void AppOpenAdDidLoadAdCallback(
            IntPtr appOpenAdLoaderClient,
            string appOpenAdObjectId)
        {
            AppOpenAdLoaderClient client = IntPtrToAppOpenAdLoaderClient(appOpenAdLoaderClient);
            if (client.OnAdLoaded != null)
            {
                GenericEventArgs<IAppOpenAdClient> args = new GenericEventArgs<IAppOpenAdClient>()
                {
                    Value = new AppOpenAdClient(appOpenAdObjectId)
                };
                client.OnAdLoaded(client, args);
            }
        }

        [MonoPInvokeCallback(typeof(YMAUnityAppOpenAdDidFailToLoadAdCallback))]
        private static void AppOpenAdDidFailToLoadAdCallback(
            IntPtr appOpenAdClient, string adUnitId, string error)
        {
            AppOpenAdLoaderClient client = IntPtrToAppOpenAdLoaderClient(appOpenAdClient);
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
