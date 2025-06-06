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

    internal class AdSizeClient: IDisposable
    {
        public string ObjectId { get; private set; }

        public double Width { get; private set; }

        public double Height { get; private set; }

        public AdSizeClient(string objectId)
        {
            this.ObjectId = objectId;
            this.Width = AdSizeBridge.YMAUnityAdSizeGetWidth(this.ObjectId);
            this.Height = AdSizeBridge.YMAUnityAdSizeGetHeight(this.ObjectId);
        }

        public void Destroy()
        {
            ObjectBridge.YMAUnityDestroyObject(this.ObjectId);
        }

        public void Dispose()
        {
            this.Destroy();
        }

        ~AdSizeClient()
        {
            this.Destroy();
        }
    }

    #endif
}
