#import <YandexMobileAds/YandexMobileAds.h>
#import "YMAUnityAudioSessionManager.h"
#import "YMAUnityStringConverter.h"

@interface YMAUnityAudioSessionManager() <YMAAudioSessionManagerDelegate>

@property (nonatomic, assign, readonly) YMAUnityAudioSessionManagerClientRef *clientRef;
@property (nonatomic, strong, readonly) YMAAudioSessionManager *audioSessionManager;

@end

@implementation YMAUnityAudioSessionManager

- (instancetype)initWithClientRef:(YMAUnityAudioSessionManagerClientRef *)clientRef
                audioSessionManager:(YMAAudioSessionManager *)audioSessionManager

{
    self = [super init];
    if (self != nil) {
        _audioSessionManager = audioSessionManager;
        _clientRef = clientRef;
    }
    return self;
}

- (void)SetIsAutomaticallyManaged:(bool)isManaged
{
    _audioSessionManager.isAutomaticallyManaged = isManaged;
}

- (void)SetAudioSessionDelegate {
    _audioSessionManager.delegate = self;
}
 
#pragma mark Callbacks


- (void)audioSessionManagerDidStopPlayingAudio:(YMAAudioSessionManager * _Nonnull)audioSessionManager {
    if (self.didStopPlayingAudioCallback != NULL) {
        self.didStopPlayingAudioCallback(self.clientRef);
    }
}

- (void)audioSessionManagerWillPlayAudio:(YMAAudioSessionManager * _Nonnull)audioSessionManager { 
    if (self.willPlayAudioCallback != NULL) {
        self.willPlayAudioCallback(self.clientRef);
    }
}

@end
