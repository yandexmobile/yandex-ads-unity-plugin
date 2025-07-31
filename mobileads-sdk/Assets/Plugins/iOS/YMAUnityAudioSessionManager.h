#import <Foundation/Foundation.h>
#import "YMAUnityAudioSessionManagerTypes.h"

@interface YMAUnityAudioSessionManager: NSObject

- (instancetype)initWithClientRef:(YMAUnityAudioSessionManagerClientRef*)clientRef
                   audioSessionManager:(YMAAudioSessionManager *)audioSessionManager;
@property(nonatomic, assign) YMAUnityAudioSessionManagerWillPlayAudioCallback willPlayAudioCallback;
@property(nonatomic, assign) YMAUnityAudioSessionManagerDidStopPlayingAudioCallback didStopPlayingAudioCallback;

- (void)SetIsAutomaticallyManaged:(bool)isManaged;

- (void)SetAudioSessionDelegate;

@end
