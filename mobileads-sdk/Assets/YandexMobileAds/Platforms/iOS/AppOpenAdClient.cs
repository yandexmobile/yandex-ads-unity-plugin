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

    public class AppOpenAdClient : IAppOpenAdClient, IDisposable
    {
        internal delegate void YMAUnityAppOpenAdDidFailToShowCallback(IntPtr bannerClient, string error);

        internal delegate void YMAUnityAppOpenAdDidShowCallback(IntPtr bannerClient);

        internal delegate void YMAUnityAppOpenAdDidDismissCallback(IntPtr bannerClient);

        internal delegate void YMAUnityAppOpenAdDidClickCallback(IntPtr bannerClient);

        internal delegate void YMAUnityAppOpenAdDidTrackImpressionCallback(
            IntPtr bannerClient,
            string rawImpressionData);

        public event EventHandler<AdFailureEventArgs> OnAdFailedToShow;
        public event EventHandler<EventArgs> OnAdShown;
        public event EventHandler<EventArgs> OnAdDismissed;
        public event EventHandler<EventArgs> OnAdClicked;
        public event EventHandler<ImpressionData> OnAdImpression;

        public string ObjectId { get; private set; }

        private readonly IntPtr _selfPointer;

        public AppOpenAdClient(string appOpenAdObjectId)
        {
            this._selfPointer = GCHandle.ToIntPtr(GCHandle.Alloc(this));
            this.ObjectId = AppOpenAdBridge.YMAUnityCreateAppOpenAd(
                this._selfPointer, appOpenAdObjectId);
            AppOpenAdBridge.YMAUnitySetAppOpenAdCallbacks(
                this.ObjectId,
                AppOpenAdDidFailToShowCallback,
                AppOpenAdDidShowCallback,
                AppOpenAdDidDismissCallback,
                AppOpenAdDidClickCallback,
                AppOpenAdDidTrackImpressionCallback);
        }

        ~AppOpenAdClient()
        {
            this.Destroy();
        }

        public void Show()
        {
            AppOpenAdBridge.YMAUnityShowAppOpenAd(this.ObjectId);
        }

        public void Dispose()
        {
            this.Destroy();
        }

        public void Destroy()
        {
            this.OnAdShown = null;
            this.OnAdClicked = null;
            this.OnAdDismissed = null;
            this.OnAdFailedToShow = null;
            this.OnAdImpression = null;

            AppOpenAdBridge.YMAUnityDestroyAppOpenAd(this.ObjectId);
        }

        private static AppOpenAdClient IntPtrToAppOpenAdClient(IntPtr appOpenAdClient)
        {
            GCHandle handle = GCHandle.FromIntPtr(appOpenAdClient);
            return handle.Target as AppOpenAdClient;
        }

        #region AppOpenAd callback methods

        [MonoPInvokeCallback(typeof(YMAUnityAppOpenAdDidFailToShowCallback))]
        private static void AppOpenAdDidFailToShowCallback(
            IntPtr appOpenAdClient, string error)
        {
            AppOpenAdClient client = IntPtrToAppOpenAdClient(
                appOpenAdClient);
            if (client.OnAdFailedToShow != null)
            {
                AdFailureEventArgs args = new AdFailureEventArgs()
                {
                    Message = error
                };
                client.OnAdFailedToShow(client, args);
            }
        }

        [MonoPInvokeCallback(typeof(YMAUnityAppOpenAdDidShowCallback))]
        private static void AppOpenAdDidShowCallback(IntPtr appOpenAdClient)
        {
            AppOpenAdClient client = IntPtrToAppOpenAdClient(
                appOpenAdClient);
            if (client.OnAdShown != null)
            {
                client.OnAdShown(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(YMAUnityAppOpenAdDidDismissCallback))]
        private static void AppOpenAdDidDismissCallback(IntPtr appOpenAdClient)
        {
            AppOpenAdClient client = IntPtrToAppOpenAdClient(
                appOpenAdClient);
            if (client.OnAdDismissed != null)
            {
                client.OnAdDismissed(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(YMAUnityAppOpenAdDidClickCallback))]
        private static void AppOpenAdDidClickCallback(
            IntPtr appOpenAdClient)
        {
            AppOpenAdClient client = IntPtrToAppOpenAdClient(appOpenAdClient);
            if (client.OnAdClicked != null)
            {
                client.OnAdClicked(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(YMAUnityAppOpenAdDidTrackImpressionCallback))]
        private static void AppOpenAdDidTrackImpressionCallback(
            IntPtr appOpenAdClient, string rawImpressionData)
        {
            AppOpenAdClient client = IntPtrToAppOpenAdClient(
                appOpenAdClient);
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
