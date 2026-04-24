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
    /// The exception that is thrown when an ad fails to load.
    /// </summary>
    public class AdLoadingException : Exception
    {
        /// <summary>
        /// AdUnitId of the requested ad.
        /// </summary>
        public string AdUnitId { get; }

        public AdLoadingException(string message, string adUnitId = null) : base(message)
        {
            AdUnitId = adUnitId;
        }
    }
}
