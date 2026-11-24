/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Unity (C) 2026 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using YandexMobileAds.Common;

namespace YandexMobileAds.Platforms.Android
{
    internal sealed class AdVolumeControllerClient : IAdVolumeControllerClient
    {
        private readonly AndroidJavaObject _controller = new AndroidJavaObject(
            "com.yandex.mobile.ads.unity.wrapper.AdVolumeControllerWrapper");

        private readonly HashSet<AdVolumeControllerListener> _pendingListeners =
            new HashSet<AdVolumeControllerListener>();

        public Task SetMutedAsync(bool muted)
        {
            return CallAsync(listener => _controller.Call("setMuted", muted, listener));
        }

        public Task SetVolumeAsync(float volume)
        {
            return CallAsync(listener => _controller.Call("setVolume", volume, listener));
        }

        private async Task CallAsync(Action<AdVolumeControllerListener> invoke)
        {
            var listener = new AdVolumeControllerListener();
            // Unity's Java proxy holds a weak managed reference; retain it until the callback completes.
            lock (_pendingListeners)
            {
                _pendingListeners.Add(listener);
            }
            try
            {
                invoke(listener);
                await listener.Task;
            }
            finally
            {
                lock (_pendingListeners)
                {
                    _pendingListeners.Remove(listener);
                }
            }
        }

        private sealed class AdVolumeControllerListener : AndroidJavaProxy
        {
            private readonly TaskCompletionSource<bool> _completion =
                new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

            public AdVolumeControllerListener() : base(Utils.AdVolumeControllerListenerClassName) { }

            public Task Task { get { return _completion.Task; } }

            [UnityEngine.Scripting.Preserve]
            public void onResult()
            {
                _completion.TrySetResult(true);
            }

            [UnityEngine.Scripting.Preserve]
            public void onError(string message)
            {
                _completion.TrySetException(new InvalidOperationException(message));
            }
        }
    }
}
