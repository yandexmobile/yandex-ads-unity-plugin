/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;

namespace YandexMobileAds.Platforms.iOS
{

    /// attribute that allows static functions to have callbacks (from C) generated AOT
    public class MonoPInvokeCallbackAttribute : System.Attribute
    {
        private readonly Type _type;
        public MonoPInvokeCallbackAttribute(Type t) { _type = t; }
    }

}
