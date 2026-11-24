/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Unity (C) 2026 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System.Runtime.InteropServices;

namespace YandexMobileAds.Platforms.iOS
{
    #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE

    internal static class AdVolumeControllerBridge
    {
        [DllImport("__Internal")]
        internal static extern void YMAUnitySetAdMuted([MarshalAs(UnmanagedType.I1)] bool muted);

        [DllImport("__Internal")]
        internal static extern void YMAUnitySetAdVolume(float volume);
    }

    #endif
}
