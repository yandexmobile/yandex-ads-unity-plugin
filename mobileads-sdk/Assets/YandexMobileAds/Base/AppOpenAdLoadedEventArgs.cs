/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Unity (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;
using YandexMobileAds.Common;

namespace YandexMobileAds.Base
{
    /// <summary>
    /// Represents an event, that occurs when an <see cref="YandexMobileAds.AppOpenAd"/> was loaded successfuly.
    /// </summary>
    public class AppOpenAdLoadedEventArgs : EventArgs
    {
        /// <summary>
        /// A loaded <see cref="YandexMobileAds.AppOpenAd">.
        /// </summary>
        public AppOpenAd AppOpenAd { get; set; }
    }
}
