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
    /// Represents event, that occurs when AdLoader fails to load an ad.
    /// </summary>
    public class AdFailedToLoadEventArgs : EventArgs
    {
        /// <summary>
        /// Message, describing reason of the failure.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// AdUnitId of the requested Ad.
        /// </summary>
        public string AdUnitId { get; set; }
    }
}
