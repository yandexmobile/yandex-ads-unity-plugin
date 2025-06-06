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
    #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE

    public class DictionaryClient : IDisposable
    {
        public string ObjectId { get; private set; }

        public DictionaryClient()
        {
            this.ObjectId = DictionaryBridge.YMAUnityCreateDictionary();
        }

        public void Put(string key, string value)
        {
            DictionaryBridge.YMAUnitySetDictionaryValue(this.ObjectId, key, value);
        }

        public void Destroy()
        {
            ObjectBridge.YMAUnityDestroyObject(this.ObjectId);
        }

        public void Dispose()
        {
            this.Destroy();
        }

        ~DictionaryClient()
        {
            this.Destroy();
        }
    }

    #endif
}
