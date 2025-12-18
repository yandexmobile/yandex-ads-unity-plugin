/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

#import <YandexMobileAds/YandexMobileAds.h>
#import "YMAUnityRewardedAd.h"
#import "YMAUnityStringConverter.h"

@interface YMAUnityRewardedAd() <YMARewardedAdDelegate>

@property (nonatomic, assign, readonly) YMAUnityRewardedAdClientRef *clientRef;
@property (nonatomic, strong, readonly) YMARewardedAd *rewardedAd;

@end

@implementation YMAUnityRewardedAd

- (instancetype)initWithClientRef:(YMAUnityRewardedAdClientRef *)clientRef
                         rewardedAd:(YMARewardedAd *)rewardedAd
{
    self = [super init];
    if (self != nil) {
        _rewardedAd = rewardedAd;
        _rewardedAd.delegate = self;
        _clientRef = clientRef;
    }
    return self;
}

- (YMAAdInfo*)getInfo
{
    return [self.rewardedAd adInfo];
}

- (void)show
{
    UIViewController *viewController = [UIApplication sharedApplication].keyWindow.rootViewController;
    [self.rewardedAd showFromViewController:viewController];
}

#pragma mark Callbacks

- (void)rewardedAd:(YMARewardedAd *)rewardedAd didReward:(id<YMAReward>)reward
{
    if (self.didRewardCallback != NULL) {
        char *type = [YMAUnityStringConverter copiedCStringFromObjCString:reward.type];
        int amount = (int)reward.amount;
        self.didRewardCallback(self.clientRef, amount, type);
    }
}

- (void)rewardedAd:(YMARewardedAd *)rewardedAd didFailToShowWithError:(NSError *)error
{
    if (self.didFailToShowCallback != NULL) {
        char *message = [YMAUnityStringConverter copiedCStringFromObjCString:error.localizedDescription];
        self.didFailToShowCallback(self.clientRef, message);
    }
}

- (void)rewardedAdDidShow:(YMARewardedAd *)rewardedAd
{
    if (self.didShowCallback != NULL) {
        self.didShowCallback(self.clientRef);
    }
}

- (void)rewardedAdDidDismiss:(YMARewardedAd *)rewardedAd
{
    if (self.didDismissCallback != NULL) {
        self.didDismissCallback(self.clientRef);
    }
}


- (void)rewardedAdDidClick:(YMARewardedAd *)rewardedAd
{
    if (self.didClickCallback != NULL) {
        self.didClickCallback(self.clientRef);
    }
}

- (void)rewardedAd:(YMARewardedAd *)rewardedAd
didTrackImpressionWithData:(nullable id<YMAImpressionData>)impressionData
{
    if (self.didTrackImpressionCallback != NULL) {
        if (impressionData != nil) {
            char *rawData = [YMAUnityStringConverter copiedCStringFromObjCString:impressionData.rawData];
            self.didTrackImpressionCallback(self.clientRef, rawData);
        }
        else {
            self.didTrackImpressionCallback(self.clientRef, nil);
        }
    }
}

@end
