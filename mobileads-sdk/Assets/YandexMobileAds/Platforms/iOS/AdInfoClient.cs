/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;
using YandexMobileAds.Base;
using System.Collections.Generic;

namespace YandexMobileAds.Platforms.iOS
{
#if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE

    internal class AdInfoClient : IDisposable
    {

        public string AdUnitId { get; private set; }
        public AdSize AdSize { get; private set; }

        private readonly string _objectId;

        public AdInfoClient(string adInfoObjectId)
        {
            this._objectId = adInfoObjectId;
            this.AdUnitId = AdInfoBridge.YMAUnityAdInfoGetAdUnitId(adInfoObjectId);
            AdSizeClient adSizeClient = new AdSizeClient(AdInfoBridge.YMAUnityAdInfoGetAdSize(adInfoObjectId));
            this.AdSize = new AdSize(
                (int)adSizeClient.Width,
                (int)adSizeClient.Height
            );
            adSizeClient.Destroy();
        }

        public void Destroy()
        {
            ObjectBridge.YMAUnityDestroyObject(this._objectId);
        }

        public void Dispose()
        {
            this.Destroy();
        }

        ~AdInfoClient()
        {
            this.Destroy();
        }
    }

#endif
}
