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
    /// Represents error, that occurs when ad fails to perform action.
    /// </summary>
    public class AdError
    {
        /// <summary>
        /// Message, describing reason of failure.
        /// </summary>
        public string Message { get; private set; }

        public AdError(string message)
        {
            this.Message = message;
        }
    }
}
