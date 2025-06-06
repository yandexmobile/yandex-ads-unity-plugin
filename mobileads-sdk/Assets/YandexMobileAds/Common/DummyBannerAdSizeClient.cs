/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Unity (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;
using System.Reflection;
using YandexMobileAds.Base;
using UnityEngine;

namespace YandexMobileAds.Common
{
    public class DummyBannerAdSizeClient : IBannerAdSizeClient
    {
        private int _width;
        private int _height;

        public DummyBannerAdSizeClient(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public int GetHeight()
        {
            return _height;
        }

        public int GetWidth()
        {
            return _width;
        }
    }
}
