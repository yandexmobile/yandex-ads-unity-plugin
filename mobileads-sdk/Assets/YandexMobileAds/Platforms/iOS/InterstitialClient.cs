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

    public class InterstitialClient : IInterstitialClient, IDisposable
    {
        internal delegate void YMAUnityInterstitialAdDidFailToShowCallback(IntPtr bannerClient, string error);

        internal delegate void YMAUnityInterstitialAdDidShowCallback(IntPtr bannerClient);

        internal delegate void YMAUnityInterstitialAdDidDismissCallback(IntPtr bannerClient);

        internal delegate void YMAUnityInterstitialAdDidClickCallback(IntPtr bannerClient);

        internal delegate void YMAUnityInterstitialAdDidTrackImpressionCallback(
            IntPtr bannerClient,
            string rawImpressionData);

        public event EventHandler<AdFailureEventArgs> OnAdFailedToShow;
        public event EventHandler<EventArgs> OnAdShown;
        public event EventHandler<EventArgs> OnAdDismissed;
        public event EventHandler<EventArgs> OnAdClicked;
        public event EventHandler<ImpressionData> OnAdImpression;

        public string ObjectId { get; private set; }

        private readonly AdInfo _adInfo;
        private readonly IntPtr _selfPointer;

        public InterstitialClient(string interstitialAdObjectId)
        {
            this._selfPointer = GCHandle.ToIntPtr(GCHandle.Alloc(this));
            this.ObjectId = InterstitialBridge.YMAUnityCreateInterstitialAd(
                this._selfPointer, interstitialAdObjectId);
            InterstitialBridge.YMAUnitySetInterstitialAdCallbacks(
                this.ObjectId,
                InterstitialDidFailToShowCallback,
                InterstitialAdDidShowCallback,
                InterstitialAdDidDismissCallback,
                InterstitialAdDidClickCallback,
                InterstitialAdDidTrackImpressionCallback);


            string adInfoObjectId = InterstitialBridge.YMAUnityGetInterstitialInfo(this.ObjectId);
            AdInfoClient adInfoClient = new AdInfoClient(adInfoObjectId);
            this._adInfo = new AdInfo(
                adInfoClient.AdUnitId,
                adInfoClient.AdSize
            );
            adInfoClient.Destroy();
        }

        ~InterstitialClient()
        {
            this.Destroy();
        }

        public AdInfo GetInfo()
        {
            return this._adInfo;
        }

        public void Show()
        {
            InterstitialBridge.YMAUnityShowInterstitialAd(this.ObjectId);
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

            InterstitialBridge.YMAUnityDestroyInterstitialAd(this.ObjectId);
        }

        private static InterstitialClient IntPtrToInterstitialClient(IntPtr interstitialClient)
        {
            GCHandle handle = GCHandle.FromIntPtr(interstitialClient);
            return handle.Target as InterstitialClient;
        }

        #region Interstitial callback methods

        [MonoPInvokeCallback(typeof(YMAUnityInterstitialAdDidFailToShowCallback))]
        private static void InterstitialDidFailToShowCallback(
            IntPtr interstitialClient, string error)
        {
            InterstitialClient client = IntPtrToInterstitialClient(
                interstitialClient);
            if (client.OnAdFailedToShow != null)
            {
                AdFailureEventArgs args = new AdFailureEventArgs()
                {
                    Message = error
                };
                client.OnAdFailedToShow(client, args);
            }
        }

        [MonoPInvokeCallback(typeof(YMAUnityInterstitialAdDidShowCallback))]
        private static void InterstitialAdDidShowCallback(IntPtr interstitialClient)
        {
            InterstitialClient client = IntPtrToInterstitialClient(
                interstitialClient);
            if (client.OnAdShown != null)
            {
                client.OnAdShown(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(YMAUnityInterstitialAdDidDismissCallback))]
        private static void InterstitialAdDidDismissCallback(IntPtr interstitialClient)
        {
            InterstitialClient client = IntPtrToInterstitialClient(
                interstitialClient);
            if (client.OnAdDismissed != null)
            {
                client.OnAdDismissed(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(YMAUnityInterstitialAdDidClickCallback))]
        private static void InterstitialAdDidClickCallback(
            IntPtr interstitialClient)
        {
            InterstitialClient client = IntPtrToInterstitialClient(interstitialClient);
            if (client.OnAdClicked != null)
            {
                client.OnAdClicked(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(YMAUnityInterstitialAdDidTrackImpressionCallback))]
        private static void InterstitialAdDidTrackImpressionCallback(
            IntPtr interstitialClient, string rawImpressionData)
        {
            InterstitialClient client = IntPtrToInterstitialClient(
                interstitialClient);
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
