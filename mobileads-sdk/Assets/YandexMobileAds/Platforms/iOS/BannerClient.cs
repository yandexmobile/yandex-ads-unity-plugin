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

    public class BannerClient : IBannerClient, IDisposable
    {
        internal delegate void YMAUnityAdViewDidReceiveAdCallback(
            IntPtr bannerClient);

        internal delegate void YMAUnityAdViewDidFailToReceiveAdWithErrorCallback(
                IntPtr bannerClient, string error);

        internal delegate void YMAUnityAdViewWillPresentScreenCallback(
            IntPtr bannerClient);

        internal delegate void YMAUnityAdViewDidDismissScreenCallback(
            IntPtr bannerClient);

        internal delegate void YMAUnityAdViewDidTrackImpressionCallback(
            IntPtr bannerClient, string rawImpressionData);

        internal delegate void YMAUnityAdViewWillLeaveApplicationCallback(
            IntPtr bannerClient);

        internal delegate void YMAUnityAdViewDidClickCallback(
            IntPtr bannerClient);

        public event EventHandler<EventArgs> OnAdLoaded;
        public event EventHandler<AdFailureEventArgs> OnAdFailedToLoad;
        public event EventHandler<EventArgs> OnReturnedToApplication;
        public event EventHandler<EventArgs> OnLeftApplication;
        public event EventHandler<EventArgs> OnAdClicked;
        public event EventHandler<ImpressionData> OnImpression;

        public string ObjectId { get; private set; }

        private readonly IntPtr _selfPointer;

        internal BannerClient(string blockId, BannerAdSizeClient adSizeClient, AdPosition position)
        {
            this._selfPointer = GCHandle.ToIntPtr(GCHandle.Alloc(this));

            this.ObjectId = BannerBridge.YMAUnityCreateBannerView(
                this._selfPointer, blockId, adSizeClient.ObjectId,
                (int)position);
            BannerBridge.YMAUnitySetBannerCallbacks(
                this.ObjectId,
                    AdViewDidReceiveAdCallback,
                    AdViewDidFailToReceiveAdWithErrorCallback,
                    AdViewWillPresentScreenCallback,
                    AdViewDidDismissScreenCallback,
                    AdViewDidTrackImpression,
                    AdViewWillLeaveApplicationCallback,
                    AdViewDidClickCallback);
        }

        public void LoadAd(AdRequest adRequest)
        {
            if (adRequest == null) {
                AdViewDidFailToReceiveAdWithErrorCallback(
                    _selfPointer,
                    Constants.AdRequestConfigurationIsNullErrorMessage);
                return;
            }

            AdRequestClient adRequestClient = new AdRequestClient(adRequest);
            BannerBridge.YMAUnityLoadBannerView(this.ObjectId, adRequestClient.ObjectId);
        }

        public void Show()
        {
            BannerBridge.YMAUnityShowBannerView(this.ObjectId);
        }

        public void Hide()
        {
            BannerBridge.YMAUnityHideBannerView(this.ObjectId);
        }

        public void Destroy()
        {
            this.Hide();
            ObjectBridge.YMAUnityDestroyObject(this.ObjectId);
        }

        public void Dispose()
        {
            this.Destroy();
        }

        ~BannerClient()
        {
            this.Destroy();
        }

        private static BannerClient IntPtrToBannerClient(IntPtr bannerClient)
        {
            GCHandle handle = GCHandle.FromIntPtr(bannerClient);
            return handle.Target as BannerClient;
        }

        #region Banner callback methods

        [MonoPInvokeCallback(typeof(YMAUnityAdViewDidReceiveAdCallback))]
        private static void AdViewDidReceiveAdCallback(IntPtr bannerClient)
        {
            BannerClient client = IntPtrToBannerClient(bannerClient);
            if (client.OnAdLoaded != null)
            {
                client.OnAdLoaded(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(YMAUnityAdViewDidReceiveAdCallback))]
        private static void AdViewDidFailToReceiveAdWithErrorCallback(
                IntPtr bannerClient, string error)
        {
            BannerClient client = IntPtrToBannerClient(bannerClient);
            if (client.OnAdFailedToLoad != null)
            {
                AdFailureEventArgs args = new AdFailureEventArgs()
                {
                    Message = error
                };
                client.OnAdFailedToLoad(client, args);
            }
        }

        [MonoPInvokeCallback(typeof(YMAUnityAdViewWillPresentScreenCallback))]
        private static void AdViewWillPresentScreenCallback(IntPtr bannerClient)
        {
            BannerClient client = IntPtrToBannerClient(bannerClient);
            if (client.OnLeftApplication != null)
            {
                client.OnLeftApplication(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(YMAUnityAdViewDidDismissScreenCallback))]
        private static void AdViewDidDismissScreenCallback(IntPtr bannerClient)
        {
            BannerClient client = IntPtrToBannerClient(bannerClient);
            if (client.OnReturnedToApplication != null)
            {
                client.OnReturnedToApplication(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(YMAUnityAdViewDidTrackImpressionCallback))]
        private static void AdViewDidTrackImpression(IntPtr bannerClient, string rawImpressionData)
        {
            BannerClient client = IntPtrToBannerClient(bannerClient);
            if (client.OnImpression != null)
            {
                ImpressionData impressionData = new ImpressionData(rawImpressionData == null ? "" : rawImpressionData);
                client.OnImpression(client, impressionData);
            }
        }

        [MonoPInvokeCallback(typeof(YMAUnityAdViewWillLeaveApplicationCallback))]
        private static void AdViewWillLeaveApplicationCallback(IntPtr bannerClient)
        {
            BannerClient client = IntPtrToBannerClient(bannerClient);
            if (client.OnLeftApplication != null)
            {
                client.OnLeftApplication(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(YMAUnityAdViewDidClickCallback))]
        private static void AdViewDidClickCallback(IntPtr bannerClient)
        {
            BannerClient client = IntPtrToBannerClient(bannerClient);
            if (client.OnAdClicked != null)
            {
                client.OnAdClicked(client, EventArgs.Empty);
            }
        }

        #endregion
    }

    #endif
}
