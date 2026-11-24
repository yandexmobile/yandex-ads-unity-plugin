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
using YandexMobileAds.Platforms;

namespace YandexMobileAds
{
    /// <summary>
    /// Controls publisher mute and volume for ads rendered by the SDK, except In-Stream.
    /// On iOS only native video is affected, excluding HTML/MRAID and mediated ads;
    /// settings apply when a renderer is configured, without updating active players immediately.
    /// A video's own controls can enable sound. Call from the Unity main thread.
    /// </summary>
    public sealed class AdVolumeController
    {
        private readonly IAdVolumeControllerClient _client;

        internal AdVolumeController()
        {
            _client = YandexMobileAdsClientFactory.CreateAdVolumeControllerClient();
        }

        /// <summary>
        /// Mutes or unmutes ads without changing the saved volume or the device's audio session.
        /// Explicitly unmuting selects the publisher volume, or 1 if no volume was set.
        /// Completes after the SDK applies the setting.
        /// </summary>
        public Task SetMutedAsync(bool muted)
        {
            return _client.SetMutedAsync(muted);
        }

        /// <summary>
        /// Replaces creative volume with a value in [0, 1], without changing mute.
        /// Out-of-range values are clamped; NaN is ignored. Until a setter is called,
        /// creative settings are preserved. Completes after the SDK applies the setting.
        /// </summary>
        public Task SetVolumeAsync(float volume)
        {
            return _client.SetVolumeAsync(volume);
        }
    }
}
