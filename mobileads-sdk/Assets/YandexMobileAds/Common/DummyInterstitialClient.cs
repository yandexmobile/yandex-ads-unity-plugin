/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Unity (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;
using System.Reflection;
using YandexMobileAds.Base;
using UnityEngine;

namespace YandexMobileAds.Common
{
    #pragma warning disable 67
    public class DummyInterstitialClient : IInterstitialClient
    {
        private const string TAG = "Dummy Interstitial ";


        public event EventHandler<AdFailureEventArgs> OnAdFailedToShow;
        public event EventHandler<EventArgs> OnAdShown;
        public event EventHandler<EventArgs> OnAdDismissed;
        public event EventHandler<EventArgs> OnAdClicked;
        public event EventHandler<ImpressionData> OnAdImpression;

        private AdInfo adInfo;

        internal DummyInterstitialClient(AdRequestConfiguration configuration)
        {
            Debug.Log(TAG + MethodBase.GetCurrentMethod().Name);

            adInfo = new AdInfo(configuration.AdUnitId, new AdSize(0, 0));
        }

        public AdInfo GetInfo()
        {
            return adInfo;
        }

        public void Show()
        {
            Debug.Log(TAG + MethodBase.GetCurrentMethod().Name);
        }

        public void Destroy()
        {
            Debug.Log(TAG + MethodBase.GetCurrentMethod().Name);
        }
    }
}
