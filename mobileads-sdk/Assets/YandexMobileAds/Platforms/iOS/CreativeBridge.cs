/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System.Runtime.InteropServices;

namespace YandexMobileAds.Platforms.iOS
{
    #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE

    internal class CreativeBridge
    {
        [DllImport("__Internal")]
        internal static extern string YMAUnityCreativeGetCreativeId(string creativeObjectId);

        [DllImport("__Internal")]
        internal static extern string YMAUnityCreativeGetCampaignId(string creativeObjectId);

        [DllImport("__Internal")]
        internal static extern string YMAUnityCreativeGetPlaceId(string creativeObjectId);


    }

    #endif
}
