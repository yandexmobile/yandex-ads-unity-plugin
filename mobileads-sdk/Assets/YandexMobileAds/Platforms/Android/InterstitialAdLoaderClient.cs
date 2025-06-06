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
    internal class InterstitialAdLoaderClient : AndroidJavaProxy, IInterstitialAdLoaderClient
    {
        private const string UnityInterstitialAdLoadListenerClassName =
            "com.yandex.mobile.ads.unity.wrapper.interstitial.UnityInterstitialAdLoadListener";

        public event EventHandler<GenericEventArgs<IInterstitialClient>> OnAdLoaded;
        public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

        private readonly AndroidJavaObject _interstitialAdLoader;

        public InterstitialAdLoaderClient() : base(UnityInterstitialAdLoadListenerClassName)
        {
            AndroidJavaObject activity = Utils.GetCurrentActivity();
            AndroidJavaObject applicationContext =
                activity.Call<AndroidJavaObject>("getApplicationContext");

            this._interstitialAdLoader = NativeApi.NewInstance(applicationContext);
            NativeApi.SetUnityInterstitialAdLoadListener(this._interstitialAdLoader, this);
        }

        public void LoadAd(AdRequestConfiguration adRequestConfiguration)
        {
            if (adRequestConfiguration == null) {
                onAdFailedToLoad(Constants.AdRequestConfigurationIsNullErrorMessage);
                return;
            }

            NativeApi.LoadAd(this._interstitialAdLoader,
                Utils.GetAdRequestConfigurationJavaObject(adRequestConfiguration));
        }

        public void CancelLoading()
        {
            NativeApi.CancelLoading(this._interstitialAdLoader);
        }

        public void Destroy()
        {
            NativeApi.CancelLoading(this._interstitialAdLoader);
            NativeApi.ClearUnityInterstitialListener(this._interstitialAdLoader);
        }

#pragma warning disable IDE1006 // naming rules violation
        #region UnityInterstitialAdLoadListener implementation

        public void onAdLoaded(AndroidJavaObject interstitialAd)
        {
            if (this.OnAdLoaded != null)
            {
                GenericEventArgs<IInterstitialClient> adLoadedEventArgs = new GenericEventArgs<IInterstitialClient>()
                {
                    Value = new InterstitialClient(interstitialAd)
                };
                this.OnAdLoaded(this, adLoadedEventArgs);
            }
        }

        public void onAdFailedToLoad(string errorReason)
        {
            if (this.OnAdFailedToLoad != null)
            {
                AdFailedToLoadEventArgs args = new AdFailedToLoadEventArgs()
                {
                    Message = errorReason
                };
                this.OnAdFailedToLoad(this, args);
            }
        }
        #endregion
#pragma warning restore IDE1006

        private static class NativeApi
        {
            private const string JAVA_CLASS_NAME =
            "com.yandex.mobile.ads.unity.wrapper.interstitial.InterstitialAdLoaderWrapper";

            public static AndroidJavaObject NewInstance(AndroidJavaObject context)
            {
                return new AndroidJavaObject(JAVA_CLASS_NAME, context);
            }

            public static void SetUnityInterstitialAdLoadListener(AndroidJavaObject interstitialAdLoader, object listener)
            {
                interstitialAdLoader.Call("setUnityInterstitialAdLoadListener", listener);
            }

            public static void ClearUnityInterstitialListener(AndroidJavaObject interstitialAdLoader)
            {
                interstitialAdLoader.Call("clearUnityInterstitialAdLoadListener");
            }

            public static void LoadAd(AndroidJavaObject interstitialAdLoader, AndroidJavaObject adRequestConfiguration)
            {
                interstitialAdLoader.Call("loadAd", adRequestConfiguration);
            }

            public static void CancelLoading(AndroidJavaObject interstitialAdLoader)
            {
                interstitialAdLoader.Call("cancelLoading");
            }
        }
    }
}
