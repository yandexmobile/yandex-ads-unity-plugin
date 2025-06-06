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
    internal class AppOpenAdClient : AndroidJavaProxy, IAppOpenAdClient
    {
        private const string UnityAppOpenAdListenerClassName =
            "com.yandex.mobile.ads.unity.wrapper.appopenad.UnityAppOpenAdListener";

        public event EventHandler<EventArgs> OnAdClicked;
        public event EventHandler<EventArgs> OnAdShown;
        public event EventHandler<EventArgs> OnAdDismissed;
        public event EventHandler<ImpressionData> OnAdImpression;
        public event EventHandler<AdFailureEventArgs> OnAdFailedToShow;

        private AndroidJavaObject _appOpenAd;

        public AppOpenAdClient(AndroidJavaObject appOpenAd) : base(UnityAppOpenAdListenerClassName)
        {
            if (appOpenAd == null)
            {
                return;
            }

            this._appOpenAd = appOpenAd;
            NativeApi.SetUnityAppOpenAdListener(appOpenAd, this);
        }

        public void Show()
        {
            if (_appOpenAd == null)
            {
                return;
            }

            AndroidJavaObject activity = Utils.GetCurrentActivity();
            NativeApi.Show(_appOpenAd, activity);
        }

        public void Destroy()
        {
            this.OnAdClicked = null;
            this.OnAdShown = null;
            this.OnAdDismissed = null;
            this.OnAdImpression = null;
            this.OnAdFailedToShow = null;

            if (_appOpenAd == null)
            {
                return;
            }

            NativeApi.DestroyAppOpenAd(_appOpenAd);
        }

#pragma warning disable IDE1006
        #region UnityAppOpenAdListener implementation

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
            public static void SetUnityAppOpenAdListener(AndroidJavaObject appOpenAd, object listener)
            {
                appOpenAd.Call("setUnityAppOpenAdListener", listener);
            }

            public static void ClearUnityAppOpenAdListener(AndroidJavaObject appOpenAd)
            {
                appOpenAd.Call("clearUnityAppOpenAdListener");
            }

            public static void Show(AndroidJavaObject appOpenAd, AndroidJavaObject activity)
            {
                appOpenAd.Call("show", activity);
            }

            public static void DestroyAppOpenAd(AndroidJavaObject appOpenAd)
            {
                appOpenAd.Call("destroyAppOpenAd");
            }
        }
    }
}
