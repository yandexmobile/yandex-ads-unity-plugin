using System;
using System.Runtime.InteropServices;
using YandexMobileAds.Base;
using YandexMobileAds.Common;
using UnityEngine;

namespace YandexMobileAds.Platforms.iOS
{
#if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE

    internal class AudioSessionManagerClient
    {
        internal delegate void YMAUnityAudioSessionManagerWillPlayAudioCallback(IntPtr audioClient);

        internal delegate void YMAUnityAudioSessionManagerDidStopPlayingAudioCallback(IntPtr audioClient);

        internal string ObjectId { get; private set; }

        private readonly IntPtr _selfPointer;

        internal AudioSessionManagerClient()
        {
            this._selfPointer = GCHandle.ToIntPtr(GCHandle.Alloc(this));
            this.ObjectId = AudioSessionManagerBridge.YMAUnityCreateAudioSessionManager(
                this._selfPointer);
            AudioSessionManagerBridge.YMAUnitySetAudioSessionManagerCallbacks(
                this.ObjectId,
                AudioSessionManagerWillPlayAudioCallback,
                AudioSessionManagerDidStopPlayingAudioCallback
            );
        }

        internal void SetIsCustomManaged()
        {
            AudioSessionManagerBridge.YMAUnitySetIsCustomManaged(
                this.ObjectId
            );
        }

        internal void Destroy() {
            AudioSessionManagerBridge.YMAUnityDestroyAudioSessionManager(
                this.ObjectId
            );
        }

        [MonoPInvokeCallback(typeof(YMAUnityAudioSessionManagerWillPlayAudioCallback))]
        private static void AudioSessionManagerWillPlayAudioCallback(IntPtr audioSessionManagerClient)
        {
            AudioListener.pause = true;
        }

        [MonoPInvokeCallback(typeof(YMAUnityAudioSessionManagerDidStopPlayingAudioCallback))]
        private static void AudioSessionManagerDidStopPlayingAudioCallback(IntPtr audioSessionManagerClient)
        {
            AudioListener.pause = false;
        }
    }
#endif
}
