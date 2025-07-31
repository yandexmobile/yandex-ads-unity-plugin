
using System;
using System.Runtime.InteropServices;

namespace YandexMobileAds.Platforms.iOS
{
#if (UNITY_5 && UNITY_IOS) || UNITY_IPHONE
    internal class AudioSessionManagerBridge
    {
        [DllImport("__Internal")]
        internal static extern string YMAUnityCreateAudioSessionManager(
             IntPtr clientRef);

        [DllImport("__Internal")]
        internal static extern void YMAUnitySetAudioSessionManagerCallbacks(
            string objectId,
            AudioSessionManagerClient.YMAUnityAudioSessionManagerWillPlayAudioCallback willPlayAudioCallback,
            AudioSessionManagerClient.YMAUnityAudioSessionManagerDidStopPlayingAudioCallback didStopPlayingAudioCallback
        );
        
        [DllImport("__Internal")]
        internal static extern void YMAUnitySetIsCustomManaged(
            string unityAudioSessionManagerObjectID
        );

        [DllImport("__Internal")]
        internal static extern void YMAUnityDestroyAudioSessionManager(string objectId);
    }
#endif
}
