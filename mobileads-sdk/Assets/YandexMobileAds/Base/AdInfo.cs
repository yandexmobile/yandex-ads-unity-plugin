/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Unity (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System.Collections.Generic;

namespace YandexMobileAds.Base
{
    /// <summary>
    /// Container for ad information.
    /// </summary>
    public class AdInfo
    {
        /// <summary>
        /// The ad unit identifier.
        /// </summary>
        public string AdUnitId { get; private set; }

        /// <summary>
        /// Extra data of the ad.
        /// </summary>
        public string ExtraData { get; private set; }

        /// <summary>
        /// Any string in the ad (set in the Partner interface).
        /// </summary>
        /// <remarks>This property is only used for working with ADFOX.</remarks>
        public string PartnerText { get; private set; }

        /// <summary>
        /// Information about creatives of the ad.
        /// </summary>
        public List<Creative> Creatives { get; private set; }

        internal AdInfo(
            string adUnitId,
            string extraData = null,
            string partnerText = null,
            List<Creative> creatives = null)
        {
            this.AdUnitId = adUnitId;
            this.ExtraData = extraData;
            this.PartnerText = partnerText;
            this.Creatives = creatives ?? new List<Creative>();
        }

        public override string ToString()
        {
            return $"AdInfo(AdUnitId='{this.AdUnitId}')";
        }
    }
}
