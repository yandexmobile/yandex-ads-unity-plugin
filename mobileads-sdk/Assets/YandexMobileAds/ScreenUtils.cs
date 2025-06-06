/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using YandexMobileAds.Common;
using YandexMobileAds.Platforms;

namespace YandexMobileAds
{
    public static class ScreenUtils
    {
        /// <summary>
        /// Converts physical pixels to density independent pixels
        /// </summary>
        /// <param name="pixels">Amount of physical pixels</param>
        /// <returns>
        /// Amount of density independent pixels, derived from amount of physical pixels
        /// </returns>
        public static int ConvertPixelsToDp(int pixels)
        {
            IScreenClient client = YandexMobileAdsClientFactory.CreateScreenClient();
            return (int)(pixels / client.GetScreenScale());
        }
    }
}
