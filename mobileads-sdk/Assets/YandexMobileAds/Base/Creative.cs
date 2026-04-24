/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Unity (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

namespace YandexMobileAds.Base
{
    /// <summary>
    /// Contains information about a creative in the ad.
    /// </summary>
    public class Creative
    {
        /// <summary>
        /// Creative ID — a unique identifier for the creative.
        /// </summary>
        public string CreativeId { get; private set; }

        /// <summary>
        /// Campaign ID — an identifier for the campaign.
        /// </summary>
        public string CampaignId { get; private set; }

        /// <summary>
        /// The unique identifier for the ad placement.
        /// </summary>
        public string PlaceId { get; private set; }

        internal Creative(string creativeId, string campaignId, string placeId)
        {
            this.CreativeId = creativeId;
            this.CampaignId = campaignId;
            this.PlaceId = placeId;
        }

        public override string ToString()
        {
            return $"{{CreativeId={CreativeId}, CampaignId={CampaignId}, PlaceId={PlaceId}}}";
        }
    }
}
