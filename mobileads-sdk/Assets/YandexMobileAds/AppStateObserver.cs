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
using YandexMobileAds.Platforms;

namespace YandexMobileAds
{
    public class AppStateObserver
    {
        public static event EventHandler<AppStateChangedEventArgs> OnAppStateChanged
        {
            add { _client.OnAppStateChanged += value; }
            remove { _client.OnAppStateChanged -= value; }
        }

        private static readonly IAppStateObserverClient _client;

        static AppStateObserver()
        {
            _client = YandexMobileAdsClientFactory.BuildAppStateObserverClient();
        }
    }
}
