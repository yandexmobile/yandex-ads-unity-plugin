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
    internal class AppOpenAdLoaderClient : AndroidJavaProxy, IAppOpenAdLoaderClient
    {
        private const string UnityAppOpenAdLoadListenerClassName =
            "com.yandex.mobile.ads.unity.wrapper.appopenad.UnityAppOpenAdLoadListener";

        public event EventHandler<GenericEventArgs<IAppOpenAdClient>> OnAdLoaded;
        public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

        private readonly AndroidJavaObject _appOpenAdLoader;

        public AppOpenAdLoaderClient() : base(UnityAppOpenAdLoadListenerClassName)
        {
            AndroidJavaObject activity = Utils.GetCurrentActivity();
            AndroidJavaObject applicationContext =
                activity.Call<AndroidJavaObject>("getApplicationContext");

            this._appOpenAdLoader = NativeApi.NewInstance(applicationContext);
            NativeApi.SetUnityAppOpenAdLoadListener(this._appOpenAdLoader, this);
        }

        public void LoadAd(AdRequestConfiguration adRequestConfiguration)
        {
            if (adRequestConfiguration == null) {
                onAdFailedToLoad(Constants.AdRequestConfigurationIsNullErrorMessage);
                return;
            }

            NativeApi.LoadAd(this._appOpenAdLoader,
                Utils.GetAdRequestConfigurationJavaObject(adRequestConfiguration));
        }

        public void CancelLoading()
        {
            NativeApi.CancelLoading(this._appOpenAdLoader);
        }

        public void Destroy()
        {
            NativeApi.CancelLoading(this._appOpenAdLoader);
            NativeApi.ClearUnityAppOpenAdListener(this._appOpenAdLoader);
        }

#pragma warning disable IDE1006 // naming rules violation
        #region UnityAppOpenAdLoadListener implementation

        public void onAdLoaded(AndroidJavaObject appOpenAd)
        {
            if (this.OnAdLoaded != null)
            {
                GenericEventArgs<IAppOpenAdClient> adLoadedEventArgs = new GenericEventArgs<IAppOpenAdClient>()
                {
                    Value = new AppOpenAdClient(appOpenAd)
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
            "com.yandex.mobile.ads.unity.wrapper.appopenad.AppOpenAdLoaderWrapper";

            public static AndroidJavaObject NewInstance(AndroidJavaObject context)
            {
                return new AndroidJavaObject(JAVA_CLASS_NAME, context);
            }

            public static void SetUnityAppOpenAdLoadListener(AndroidJavaObject appOpenAdLoader, object listener)
            {
                appOpenAdLoader.Call("setUnityAppOpenAdLoadListener", listener);
            }

            public static void ClearUnityAppOpenAdListener(AndroidJavaObject appOpenAdLoader)
            {
                appOpenAdLoader.Call("clearUnityAppOpenAdLoadListener");
            }

            public static void LoadAd(AndroidJavaObject appOpenAdLoader, AndroidJavaObject adRequestConfiguration)
            {
                appOpenAdLoader.Call("loadAd", adRequestConfiguration);
            }

            public static void CancelLoading(AndroidJavaObject appOpenAdLoader)
            {
                appOpenAdLoader.Call("cancelLoading");
            }
        }
    }
}
