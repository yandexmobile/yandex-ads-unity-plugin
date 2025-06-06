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
    /// Represents an event, that occurs when the ad failed to load.
    /// </summary>
    public class AdRequestError
    {
        /// <summary>
        /// Message, describing reason of the failure.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// AdUnitId of the requested Ad.
        /// </summary>
        public string AdUnitId { get; private set; }

        internal AdRequestError(string message, string adUnitId)
        {
            this.Message = message;
            this.AdUnitId = adUnitId;
        }
    }
}
