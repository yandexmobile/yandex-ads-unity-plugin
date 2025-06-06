/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

#import <YandexMobileAds/YandexMobileAds.h>
#import "YMAUnityRewardedAdLoader.h"
#import "YMAUnityStringConverter.h"
#import "YMAUnityObjectIDProvider.h"
#import "YMAUnityObjectsStorage.h"
#import "YMAUnityRewardedAd.h"

@interface YMAUnityRewardedAdLoader() <YMARewardedAdLoaderDelegate>

@property (nonatomic, assign, readonly) YMAUnityRewardedAdLoaderClientRef *clientRef;
@property (nonatomic, strong, readonly) YMARewardedAdLoader *rewardedLoader;

@end

@implementation YMAUnityRewardedAdLoader

- (instancetype)initWithClientRef:(YMAUnityRewardedAdLoaderClientRef *)clientRef
{
    self = [super init];
    if (self != nil) {
        _rewardedLoader = [[YMARewardedAdLoader alloc] init];
        _rewardedLoader.delegate = self;
        _clientRef = clientRef;
    }
    return self;
}

- (void)loadWithRequestConfiguration:(YMAAdRequestConfiguration *)adRequestConfiguration
{
    [self.rewardedLoader loadAdWithRequestConfiguration:adRequestConfiguration];
}

- (void)cancelLoading
{
    [self.rewardedLoader cancelLoading];
}

#pragma mark Callbacks

- (void)rewardedAdLoader:(YMARewardedAdLoader *)adLoader
                 didLoad:(YMARewardedAd *)rewardedAd
{
    if (self.didLoadAdCallback != NULL) {

        const char *objectID = [YMAUnityObjectIDProvider IDForObject:rewardedAd];
        [[YMAUnityObjectsStorage sharedInstance] setObject:rewardedAd withID:objectID];

        self.didLoadAdCallback(self.clientRef, [YMAUnityStringConverter copiedCString:objectID]);
    }
}

- (void)rewardedAdLoader:(YMARewardedAdLoader *)adLoader didFailToLoadWithError:(YMAAdRequestError *)error
{
    if (self.didFailToLoadAdCallback != NULL) {
        char *adUnitId = [YMAUnityStringConverter copiedCStringFromObjCString:error.adUnitId];
        char *message = [YMAUnityStringConverter copiedCStringFromObjCString:error.error.localizedDescription];
        self.didFailToLoadAdCallback(self.clientRef, adUnitId, message);
    }
}
@end
