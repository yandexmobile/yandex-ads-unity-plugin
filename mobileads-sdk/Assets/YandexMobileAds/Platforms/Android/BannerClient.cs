/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Android (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;
using YandexMobileAds.Base;
using YandexMobileAds.Common;
using UnityEngine;

namespace YandexMobileAds.Platforms.Android
{
    public class BannerClient : AndroidJavaProxy, IBannerClient
    {
        private AndroidJavaObject bannerView;

        public event EventHandler<EventArgs> OnAdLoaded;
        public event EventHandler<AdFailureEventArgs> OnAdFailedToLoad;
        public event EventHandler<EventArgs> OnReturnedToApplication;
        public event EventHandler<EventArgs> OnLeftApplication;
        public event EventHandler<EventArgs> OnAdClicked;
        public event EventHandler<ImpressionData> OnImpression;

        internal BannerClient(
            string blockId,
            BannerAdSizeClient bannerAdSizeClient,
            AdPosition position) : base(Utils.UnityBannerAdListenerClassName)
        {
            AndroidJavaObject activity = Utils.GetCurrentActivity();
            this.bannerView = new AndroidJavaObject(
                Utils.BannerViewClassName,
                activity,
                blockId,
                bannerAdSizeClient.GetBannerAdSizeAndroidJavaObject(),
                (int)position
            );
            this.bannerView.Call("createView", activity);
            this.bannerView.Call("setUnityBannerListener", this);
        }

        public void LoadAd(AdRequest request)
        {
            if (request == null) {
                onAdFailedToLoad(Constants.AdRequestIsNullErrorMessage);
                return;
            }

            this.bannerView.Call("loadAd", Utils.GetAdRequestJavaObject(request));
        }

        public void Show()
        {
            this.bannerView.Call("showBanner");
        }

        public void Hide()
        {
            this.bannerView.Call("hideBanner");
        }

        public void Destroy()
        {
            this.bannerView.Call("clearUnityBannerListener");
            this.bannerView.Call("destroyBanner");
        }

        #region UnityBannerAdListener implementation
#pragma warning disable IDE1006 // naming rules violation
        public void onAdLoaded()
        {
            if (this.OnAdLoaded != null)
            {
                this.OnAdLoaded(this, EventArgs.Empty);
            }
        }

        public void onAdFailedToLoad(string errorReason)
        {
            if (this.OnAdFailedToLoad != null)
            {
                AdFailureEventArgs args = new AdFailureEventArgs()
                {
                    Message = errorReason
                };
                this.OnAdFailedToLoad(this, args);
            }
        }

        public void onReturnedToApplication()
        {
            if (this.OnReturnedToApplication != null)
            {
                this.OnReturnedToApplication(this, EventArgs.Empty);
            }
        }

        public void onLeftApplication()
        {
            if (this.OnLeftApplication != null)
            {
                this.OnLeftApplication(this, EventArgs.Empty);
            }
        }

        public void onAdClicked()
        {
            if (this.OnAdClicked != null)
            {
                this.OnAdClicked(this, EventArgs.Empty);
            }
        }

        public void onImpression(string rawImpressionData)
        {
            if (this.OnImpression != null)
            {
                ImpressionData impressionData = new ImpressionData(rawImpressionData);
                this.OnImpression(this, impressionData);
            }
        }
#pragma warning restore IDE1006
        #endregion
    }
}
