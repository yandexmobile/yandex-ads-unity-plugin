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
    /// This class represents the size of the ad.
    /// </summary>
    public class AdSize
    {
        public int Width { get; private set; }

        public int Height { get; private set; }

        internal AdSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        override public string ToString()
        {
            return $"AdSize(Width={Width}, Height={Height})";
        }
    }
}
