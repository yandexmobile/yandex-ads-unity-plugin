/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Unity (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;

namespace YandexMobileAds.Base
{
    /// <summary>
    /// Represents an event, that occurs when a <see cref="YandexMobileAds.RewardedAd"/> was loaded successfuly.
    /// </summary>
    public class RewardedAdLoadedEventArgs : EventArgs
    {
        /// <summary>
        /// A loaded <see cref="YandexMobileAds.RewardedAd">.
        /// </summary>
        public RewardedAd RewardedAd { get; set; }
    }
}
