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
    internal class DummyInterstitialAdLoaderClient : IInterstitialAdLoaderClient
    {
        private const string TAG = "Dummy InterstitialAdLoader ";


        public event EventHandler<GenericEventArgs<IInterstitialClient>> OnAdLoaded;
        public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

        public DummyInterstitialAdLoaderClient()
        {
            Debug.Log(TAG + MethodBase.GetCurrentMethod().Name);
        }

        public void CancelLoading()
        {
            Debug.Log(TAG + MethodBase.GetCurrentMethod().Name);
        }

        public void LoadAd(AdRequestConfiguration configuration)
        {
            Debug.Log(TAG + MethodBase.GetCurrentMethod().Name);

            if (OnAdLoaded != null)
            {
                GenericEventArgs<IInterstitialClient> adLoadedEventArgs = new GenericEventArgs<IInterstitialClient>()
                {
                    Value = new DummyInterstitialClient(configuration)
                };
                OnAdLoaded(this, adLoadedEventArgs);
            }
        }
    }
}
