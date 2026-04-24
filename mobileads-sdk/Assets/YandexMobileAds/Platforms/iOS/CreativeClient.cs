/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using YandexMobileAds.Base;

namespace YandexMobileAds.Platforms.iOS
{
    #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE

    internal class CreativeClient
    {
        private readonly string _objectId;

        public CreativeClient(string creativeObjectId)
        {
            this._objectId = creativeObjectId;
        }

        public Creative ToCreative()
        {
            string creativeId = CreativeBridge.YMAUnityCreativeGetCreativeId(this._objectId);
            string campaignId = CreativeBridge.YMAUnityCreativeGetCampaignId(this._objectId);
            string placeId    = CreativeBridge.YMAUnityCreativeGetPlaceId(this._objectId);
            return new Creative(creativeId, campaignId, placeId);
        }

        public void Destroy()
        {
            ObjectBridge.YMAUnityDestroyObject(this._objectId);
        }
    }

    #endif
}
