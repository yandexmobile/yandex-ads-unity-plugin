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
    internal class RewardedAdClient : AndroidJavaProxy, IRewardedAdClient
    {

        public const string UnityRewardedAdListenerClassName =
            "com.yandex.mobile.ads.unity.wrapper.rewarded.UnityRewardedAdListener";

        public event EventHandler<EventArgs> OnAdClicked;
        public event EventHandler<EventArgs> OnAdShown;
        public event EventHandler<EventArgs> OnAdDismissed;
        public event EventHandler<ImpressionData> OnAdImpression;
        public event EventHandler<AdFailureEventArgs> OnAdFailedToShow;
        public event EventHandler<Reward> OnRewarded;

        private AndroidJavaObject _rewarded;
        private readonly AdInfo _adInfo;

        public RewardedAdClient(AndroidJavaObject rewarded) : base(UnityRewardedAdListenerClassName)
        {
            if (rewarded == null)
            {
                return;
            }

            this._rewarded = rewarded;

            AndroidJavaObject adInfoObject = NativeApi.GetInfo(rewarded);
            if (adInfoObject != null)
            {

                this._adInfo = AdInfoFactory.CreateAdInfo(adInfoObject);
            }

            NativeApi.SetUnityRewardedAdListener(rewarded, this);
        }

        public AdInfo GetInfo()
        {
            return this._adInfo;
        }

        public void Show()
        {
            if (_rewarded == null)
            {
                return;
            }

            AndroidJavaObject activity = Utils.GetCurrentActivity();
            NativeApi.Show(_rewarded, activity);
        }

        public void Destroy()
        {
            this.OnAdClicked = null;
            this.OnAdShown = null;
            this.OnAdDismissed = null;
            this.OnAdImpression = null;
            this.OnAdFailedToShow = null;
            this.OnRewarded = null;

            if (_rewarded == null)
            {
                return;
            }

            NativeApi.DestroyRewardedAd(_rewarded);
        }

        #region  UnityRewardedAdListener implementation
#pragma warning disable IDE1006

        public void onAdShown()
        {
            if (this.OnAdShown != null)
            {
                this.OnAdShown(this, EventArgs.Empty);
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

        public void onAdDismissed()
        {
            if (this.OnAdDismissed != null)
            {
                this.OnAdDismissed(this, EventArgs.Empty);
            }
        }

        public void onAdClicked()
        {
            if (this.OnAdClicked != null)
            {
                this.OnAdClicked(this, EventArgs.Empty);
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

        public void onRewarded(int amount, string type)
        {
            if (this.OnRewarded != null)
            {
                Reward reward = new Reward(amount, type);
                this.OnRewarded(this, reward);
            }
        }
#pragma warning restore IDE1006
        #endregion

        private static class NativeApi
        {
            public static void SetUnityRewardedAdListener(AndroidJavaObject rewardedAd, object listener)
            {
                rewardedAd.Call("setUnityRewardedAdListener", listener);
            }

            public static void ClearUnityRewardedListener(AndroidJavaObject rewardedAd)
            {
                rewardedAd.Call("clearUnityRewardedAdListener");
            }

            public static AndroidJavaObject GetInfo(AndroidJavaObject rewardedAd)
            {
                return rewardedAd.Call<AndroidJavaObject>("getInfo");
            }

            public static void Show(AndroidJavaObject rewardedAd, AndroidJavaObject activity)
            {
                rewardedAd.Call("show", activity);
            }

            public static void DestroyRewardedAd(AndroidJavaObject rewardedAd)
            {
                rewardedAd.Call("destroyRewardedAd");
            }
        }
    }
}
