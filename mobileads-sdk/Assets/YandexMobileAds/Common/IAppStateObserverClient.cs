/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Unity (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;
using YandexMobileAds.Base;

namespace YandexMobileAds.Common
{
    internal interface IAppStateObserverClient
    {
        /// <summary>
        /// Event fired when the app state has been changed.
        /// </summary>
        event EventHandler<AppStateChangedEventArgs> OnAppStateChanged;

        /// <summary>
        /// Clear all subscribers and destroys the <see cref="IAppStateObserverClient"/>.
        /// </summary>
        void Destroy();
    }
}
