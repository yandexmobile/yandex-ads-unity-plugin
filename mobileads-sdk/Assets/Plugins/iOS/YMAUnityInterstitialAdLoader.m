/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

#import <YandexMobileAds/YandexMobileAds.h>
#import "YMAUnityInterstitialAdLoader.h"
#import "YMAUnityStringConverter.h"
#import "YMAUnityObjectIDProvider.h"
#import "YMAUnityObjectsStorage.h"
#import "YMAUnityInterstitial.h"

@interface YMAUnityInterstitialAdLoader() <YMAInterstitialAdLoaderDelegate>

@property (nonatomic, assign, readonly) YMAUnityInterstitialAdLoaderClientRef *clientRef;
@property (nonatomic, strong, readonly) YMAInterstitialAdLoader *interstitialLoader;

@end

@implementation YMAUnityInterstitialAdLoader

- (instancetype)initWithClientRef:(YMAUnityInterstitialAdLoaderClientRef *)clientRef
{
    self = [super init];
    if (self != nil) {
        _interstitialLoader = [[YMAInterstitialAdLoader alloc] init];
        _interstitialLoader.delegate = self;
        _clientRef = clientRef;
    }
    return self;
}

- (void)loadWithRequestConfiguration:(YMAAdRequestConfiguration *)adRequestConfiguration
{
    [self.interstitialLoader loadAdWithRequestConfiguration:adRequestConfiguration];
}

- (void)cancelLoading
{
    [self.interstitialLoader cancelLoading];
}

#pragma mark Callbacks

- (void)interstitialAdLoader:(YMAInterstitialAdLoader *)adLoader didLoad:(YMAInterstitialAd *)interstitialAd {
    if (self.didLoadAdCallback != NULL) {

        const char *objectID = [YMAUnityObjectIDProvider IDForObject:interstitialAd];
        [[YMAUnityObjectsStorage sharedInstance] setObject:interstitialAd withID:objectID];

        self.didLoadAdCallback(self.clientRef, [YMAUnityStringConverter copiedCString:objectID]);
    }
}

- (void)interstitialAdLoader:(YMAInterstitialAdLoader *)adLoader didFailToLoadWithError:(YMAAdRequestError *)error {
    if (self.didFailToLoadAdCallback != NULL) {
        char *adUnitId = [YMAUnityStringConverter copiedCStringFromObjCString:error.adUnitId];
        char *message = [YMAUnityStringConverter copiedCStringFromObjCString:error.error.localizedDescription];
        self.didFailToLoadAdCallback(self.clientRef, adUnitId, message);
    }
}

@end
