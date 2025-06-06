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
    /// Container for base Ad information.
    /// </summary>
    public class AdInfo
    {

        /// <summary>
        /// AdUnitId of the ad.
        /// </summary>
        public string AdUnitId { get; private set; }

        /// <summary>
        /// Size of the ad.
        /// </summary>
        public AdSize AdSize { get; private set; }

        internal AdInfo(string adUnitId, AdSize adSize)
        {
            this.AdUnitId = adUnitId;
            this.AdSize = adSize;
        }

        override public string ToString()
        {
            return $"AdInfo(AdUnitId='{this.AdUnitId}', AdSize={AdSize})";
        }

    }
}
