/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

#import <YandexMobileAds/YandexMobileAds.h>
#import "YMAUnityAppOpenAdLoader.h"
#import "YMAUnityStringConverter.h"
#import "YMAUnityObjectIDProvider.h"
#import "YMAUnityObjectsStorage.h"
#import "YMAUnityAppOpenAd.h"

@interface YMAUnityAppOpenAdLoader() <YMAAppOpenAdLoaderDelegate>

@property (nonatomic, assign, readonly) YMAUnityAppOpenAdLoaderClientRef *clientRef;
@property (nonatomic, strong, readonly) YMAAppOpenAdLoader *appOpenLoader;

@end

@implementation YMAUnityAppOpenAdLoader

- (instancetype)initWithClientRef:(YMAUnityAppOpenAdLoaderClientRef *)clientRef
{
    self = [super init];
    if (self != nil) {
        _appOpenLoader = [[YMAAppOpenAdLoader alloc] init];
        _appOpenLoader.delegate = self;
        _clientRef = clientRef;
    }
    return self;
}

- (void)loadWithRequestConfiguration:(YMAAdRequestConfiguration *)adRequestConfiguration
{
    [self.appOpenLoader loadAdWithRequestConfiguration:adRequestConfiguration];
}

- (void)cancelLoading
{
    [self.appOpenLoader cancelLoading];
}

#pragma mark Callbacks

- (void)appOpenAdLoader:(YMAAppOpenAdLoader *)adLoader didLoad:(YMAAppOpenAd *)appOpenAd {
    if (self.didLoadAdCallback != NULL) {

        const char *objectID = [YMAUnityObjectIDProvider IDForObject:appOpenAd];
        [[YMAUnityObjectsStorage sharedInstance] setObject:appOpenAd withID:objectID];

        self.didLoadAdCallback(self.clientRef, [YMAUnityStringConverter copiedCString:objectID]);
    }
}

- (void)appOpenAdLoader:(YMAAppOpenAdLoader *)adLoader didFailToLoadWithError:(YMAAdRequestError *)error {
    if (self.didFailToLoadAdCallback != NULL) {
        char *adUnitId = [YMAUnityStringConverter copiedCStringFromObjCString:error.adUnitId];
        char *message = [YMAUnityStringConverter copiedCStringFromObjCString:error.error.localizedDescription];
        self.didFailToLoadAdCallback(self.clientRef, adUnitId, message);
    }
}

@end
