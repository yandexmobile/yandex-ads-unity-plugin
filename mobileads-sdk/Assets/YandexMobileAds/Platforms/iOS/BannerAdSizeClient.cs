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
using YandexMobileAds.Common;

namespace YandexMobileAds.Platforms.iOS
{
    #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE

    internal class BannerAdSizeClient : IDisposable, IBannerAdSizeClient
    {
        public string ObjectId { get; private set; }

        public BannerAdSizeClient(int width, int height, BannerAdSizeType bannerAdSizeType)
        {
            if (bannerAdSizeType == BannerAdSizeType.Sticky)
            {
                this.ObjectId = BannerAdSizeBridge.YMAUnityCreateStickyBannerAdSize(width);
            }
            else if (bannerAdSizeType == BannerAdSizeType.Inline)
            {
                this.ObjectId = BannerAdSizeBridge.YMAUnityCreateInlineBannerAdSize(width, height);
            }
            else if (bannerAdSizeType == BannerAdSizeType.Fixed)
            {
                this.ObjectId = BannerAdSizeBridge.YMAUnityCreateFixedBannerAdSize(width, height);
            }
        }

        public int GetHeight()
        {
            return (int) BannerAdSizeBridge.YMAUnityGetBannerAdSizeHeight(ObjectId);
        }

        public int GetWidth()
        {
            return (int) BannerAdSizeBridge.YMAUnityGetBannerAdSizeWidth(ObjectId);
        }

        public void Destroy()
        {
            ObjectBridge.YMAUnityDestroyObject(this.ObjectId);
        }

        public void Dispose()
        {
            this.Destroy();
        }

        ~BannerAdSizeClient()
        {
            this.Destroy();
        }
    }

    #endif
}
