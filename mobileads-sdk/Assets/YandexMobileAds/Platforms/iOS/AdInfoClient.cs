/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;
using System.Collections.Generic;
using YandexMobileAds.Base;

namespace YandexMobileAds.Platforms.iOS
{
    #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE

    internal class AdInfoClient : IDisposable
    {
        public string AdUnitId { get; private set; }
        public string ExtraData { get; private set; }
        public string PartnerText { get; private set; }
        public List<Creative> Creatives { get; private set; }

        private readonly string _objectId;

        public AdInfoClient(string adInfoObjectId)
        {
            this._objectId = adInfoObjectId;
            this.AdUnitId = AdInfoBridge.YMAUnityAdInfoGetAdUnitId(adInfoObjectId);
            this.ExtraData = AdInfoBridge.YMAUnityAdInfoGetExtraData(adInfoObjectId);
            this.PartnerText = AdInfoBridge.YMAUnityAdInfoGetPartnerText(adInfoObjectId);
            this.Creatives = FetchCreatives(adInfoObjectId);
        }

        public void Destroy()
        {
            ObjectBridge.YMAUnityDestroyObject(this._objectId);
        }

        public void Dispose()
        {
            Destroy();
        }

        private static List<Creative> FetchCreatives(string adInfoObjectId)
        {
            int count = AdInfoBridge.YMAUnityAdInfoGetCreativesCount(adInfoObjectId);
            var creatives = new List<Creative>(count);
            for (int i = 0; i < count; i++)
            {
                string creativeObjectId = AdInfoBridge.YMAUnityAdInfoGetCreativeAtIndex(adInfoObjectId, i);
                CreativeClient creativeClient = new CreativeClient(creativeObjectId);
                creatives.Add(creativeClient.ToCreative());
                creativeClient.Destroy();
            }
            return creatives;
        }
    }

    #endif
}
