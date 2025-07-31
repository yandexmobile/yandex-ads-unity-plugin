#import <YandexMobileAds/YandexMobileAds.h>
#import "YMAUnityAudioSessionManager.h"
#import "YMAUnityObjectsStorage.h"
#import "YMAUnityStringConverter.h"
#import "YMAUnityObjectIDProvider.h"

char *YMAUnityCreateAudioSessionManager(YMAUnityAudioSessionManagerClientRef *clientRef)
{   
    YMAAudioSessionManager *audioSessionManager = [YMAMobileAds audioSessionManager];
    YMAUnityAudioSessionManager *unityAudioSessionManager = [[YMAUnityAudioSessionManager alloc] initWithClientRef:clientRef
                                                                            audioSessionManager: audioSessionManager];


    const char *unityAudioSessionManagerObjectID = [YMAUnityObjectIDProvider IDForObject:unityAudioSessionManager];
    [[YMAUnityObjectsStorage sharedInstance] setObject:unityAudioSessionManager
                                                withID:unityAudioSessionManagerObjectID];
    return [YMAUnityStringConverter copiedCString:unityAudioSessionManagerObjectID];
}

void YMAUnitySetAudioSessionManagerCallbacks(char *unityAudioSessionManagerAdObjectId,
                                      YMAUnityAudioSessionManagerWillPlayAudioCallback willPlayAudioCallback,
                                      YMAUnityAudioSessionManagerDidStopPlayingAudioCallback didStopPlayingAudioCallback)
{
    YMAUnityAudioSessionManager *unityAudioSessionManager = [[YMAUnityObjectsStorage sharedInstance] objectWithID: unityAudioSessionManagerAdObjectId];
    unityAudioSessionManager.willPlayAudioCallback = willPlayAudioCallback;
    unityAudioSessionManager.didStopPlayingAudioCallback = didStopPlayingAudioCallback;
}

void YMAUnitySetIsCustomManaged(char *unityAudioSessionManagerObjectID)
{
    YMAUnityAudioSessionManager *unityAudioSessionManager = [[YMAUnityObjectsStorage sharedInstance] objectWithID:unityAudioSessionManagerObjectID];
    [unityAudioSessionManager SetIsAutomaticallyManaged: false];
    [unityAudioSessionManager SetAudioSessionDelegate];
}

void YMAUnityDestroyAudioSessionManager(char *objectId) {
    YMAUnityAudioSessionManager *unityAudioSessionManager = [[YMAUnityObjectsStorage sharedInstance] objectWithID:objectId];
    [unityAudioSessionManager SetIsAutomaticallyManaged: true];
    unityAudioSessionManager.didStopPlayingAudioCallback = NULL;
    unityAudioSessionManager.willPlayAudioCallback = NULL;
    [[YMAUnityObjectsStorage sharedInstance] removeObjectWithID:objectId];
}
