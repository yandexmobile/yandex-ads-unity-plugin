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
    /// Represents impression-level revenue data.
    /// </summary>
    public class ImpressionData : EventArgs
    {
        /// <summary>
        /// A raw impression-level revenue data, string with json.
        /// </summary>
        public readonly string rawData;

        public ImpressionData(string rawData){
            this.rawData = rawData;
        }
    }
}
