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
    internal class RewardedAdLoaderClient : AndroidJavaProxy, IRewardedAdLoaderClient
    {
        public const string UnityRewardedAdLoadListenerClassName =
            "com.yandex.mobile.ads.unity.wrapper.rewarded.UnityRewardedAdLoadListener";

        public event EventHandler<GenericEventArgs<IRewardedAdClient>> OnAdLoaded;
        public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

        private readonly AndroidJavaObject _rewardedAdLoader;

        public RewardedAdLoaderClient() : base(UnityRewardedAdLoadListenerClassName)
        {
            AndroidJavaObject activity = Utils.GetCurrentActivity();
            AndroidJavaObject applicationContext =
                activity.Call<AndroidJavaObject>("getApplicationContext");

            this._rewardedAdLoader = NativeApi.NewInstance(applicationContext);
            NativeApi.SetUnityRewardedAdLoadListener(this._rewardedAdLoader, this);
        }

        public void LoadAd(AdRequestConfiguration adRequestConfiguration)
        {
            if (adRequestConfiguration == null) {
                onAdFailedToLoad(Constants.AdRequestConfigurationIsNullErrorMessage);
                return;
            }

            NativeApi.LoadAd(this._rewardedAdLoader,
                Utils.GetAdRequestConfigurationJavaObject(adRequestConfiguration));
        }

        public void CancelLoading()
        {
            NativeApi.CancelLoading(this._rewardedAdLoader);
        }

        public void Destroy()
        {
            NativeApi.CancelLoading(this._rewardedAdLoader);
            NativeApi.ClearUnityRewardedListener(this._rewardedAdLoader);
        }


#pragma warning disable IDE1006
        #region UnityRewardedAdLoadListener implementation

        public void onAdLoaded(AndroidJavaObject rewardedAd)
        {
            if (this.OnAdLoaded != null)
            {
                GenericEventArgs<IRewardedAdClient> adLoadedEventArgs = new GenericEventArgs<IRewardedAdClient>()
                {
                    Value = new RewardedAdClient(rewardedAd)
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
            "com.yandex.mobile.ads.unity.wrapper.rewarded.RewardedAdLoaderWrapper";

            public static AndroidJavaObject NewInstance(AndroidJavaObject context)
            {
                return new AndroidJavaObject(JAVA_CLASS_NAME, context);
            }

            public static void SetUnityRewardedAdLoadListener(AndroidJavaObject rewardedAdLoader, object listener)
            {
                rewardedAdLoader.Call("setUnityRewardedAdLoadListener", listener);
            }

            public static void ClearUnityRewardedListener(AndroidJavaObject rewardedAdLoader)
            {
                rewardedAdLoader.Call("clearUnityRewardedAdLoadListener");
            }

            public static void LoadAd(AndroidJavaObject rewardedAdLoader, AndroidJavaObject adRequestConfiguration)
            {
                rewardedAdLoader.Call("loadAd", adRequestConfiguration);
            }

            public static void CancelLoading(AndroidJavaObject rewardedAdLoader)
            {
                rewardedAdLoader.Call("cancelLoading");
            }
        }
    }
}
