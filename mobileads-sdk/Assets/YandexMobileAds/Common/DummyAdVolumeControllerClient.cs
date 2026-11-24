/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Unity (C) 2026 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System.Threading.Tasks;

namespace YandexMobileAds.Common
{
    internal sealed class DummyAdVolumeControllerClient : IAdVolumeControllerClient
    {
        public Task SetMutedAsync(bool muted)
        {
            return Task.CompletedTask;
        }

        public Task SetVolumeAsync(float volume)
        {
            return Task.CompletedTask;
        }
    }
}
