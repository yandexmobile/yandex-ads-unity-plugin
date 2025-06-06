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
    internal class InterstitialClient : AndroidJavaProxy, IInterstitialClient
    {
        private const string UnityInterstitialAdListenerClassName =
            "com.yandex.mobile.ads.unity.wrapper.interstitial.UnityInterstitialAdListener";

        public event EventHandler<EventArgs> OnAdClicked;
        public event EventHandler<EventArgs> OnAdShown;
        public event EventHandler<EventArgs> OnAdDismissed;
        public event EventHandler<ImpressionData> OnAdImpression;
        public event EventHandler<AdFailureEventArgs> OnAdFailedToShow;

        private AndroidJavaObject _interstitial;
        private readonly AdInfo _adInfo;

        public InterstitialClient(AndroidJavaObject interstitial) : base(UnityInterstitialAdListenerClassName)
        {
            if (interstitial == null)
            {
                return;
            }

            this._interstitial = interstitial;

            AndroidJavaObject adInfoObject = NativeApi.GetInfo(interstitial);
            if (adInfoObject != null)
            {

                this._adInfo = AdInfoFactory.CreateAdInfo(adInfoObject);
            }

            NativeApi.SetUnityInterstitialAdListener(interstitial, this);
        }

        public AdInfo GetInfo()
        {
            return this._adInfo;
        }

        public void Show()
        {
            if (_interstitial == null)
            {
                return;
            }

            AndroidJavaObject activity = Utils.GetCurrentActivity();
            NativeApi.Show(_interstitial, activity);
        }

        public void Destroy()
        {
            this.OnAdClicked = null;
            this.OnAdShown = null;
            this.OnAdDismissed = null;
            this.OnAdImpression = null;
            this.OnAdFailedToShow = null;

            if (_interstitial == null)
            {
                return;
            }

            NativeApi.DestroyInterstitialAd(_interstitial);
        }

#pragma warning disable IDE1006
        #region UnityInterstitialAdListener implementation

        public void onAdClicked()
        {
            if (this.OnAdClicked != null)
            {
                this.OnAdClicked(this, EventArgs.Empty);
            }
        }

        public void onAdShown()
        {
            if (this.OnAdShown != null)
            {
                this.OnAdShown(this, EventArgs.Empty);
            }
        }

        public void onAdDismissed()
        {
            if (this.OnAdDismissed != null)
            {
                this.OnAdDismissed(this, EventArgs.Empty);
            }
        }

        public void onAdImpression(string rawImpressionData)
        {
            if (this.OnAdImpression != null)
            {
                ImpressionData impressionData = new ImpressionData(rawImpressionData);
                this.OnAdImpression(this, impressionData);
            }
        }

        public void onAdFailedToShow(string errorDescription)
        {
            if (this.OnAdFailedToShow != null)
            {
                AdFailureEventArgs args = new AdFailureEventArgs()
                {
                    Message = errorDescription
                };
                this.OnAdFailedToShow(this, args);
            }
        }
        #endregion
#pragma warning restore IDE1006

        private static class NativeApi
        {
            public static void SetUnityInterstitialAdListener(AndroidJavaObject interstitialAd, object listener)
            {
                interstitialAd.Call("setUnityInterstitialAdListener", listener);
            }

            public static void ClearUnityInterstitialAdListener(AndroidJavaObject interstitialAd)
            {
                interstitialAd.Call("clearUnityInterstitialAdListener");
            }

            public static AndroidJavaObject GetInfo(AndroidJavaObject interstitialAd)
            {
                return interstitialAd.Call<AndroidJavaObject>("getInfo");
            }

            public static void Show(AndroidJavaObject interstitialAd, AndroidJavaObject activity)
            {
                interstitialAd.Call("show", activity);
            }

            public static void DestroyInterstitialAd(AndroidJavaObject interstitialAd)
            {
                interstitialAd.Call("destroyInterstitialAd");
            }
        }
    }
}
