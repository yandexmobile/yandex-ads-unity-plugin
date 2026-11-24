/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Unity (C) 2026 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System.Threading.Tasks;
using YandexMobileAds.Common;

namespace YandexMobileAds.Platforms.iOS
{
    #if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE

    internal sealed class AdVolumeControllerClient : IAdVolumeControllerClient
    {
        public Task SetMutedAsync(bool muted)
        {
            AdVolumeControllerBridge.YMAUnitySetAdMuted(muted);
            return Task.CompletedTask;
        }

        public Task SetVolumeAsync(float volume)
        {
            AdVolumeControllerBridge.YMAUnitySetAdVolume(volume);
            return Task.CompletedTask;
        }
    }

    #endif
}
