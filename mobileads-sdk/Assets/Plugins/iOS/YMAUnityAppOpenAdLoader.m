/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

#import <YandexMobileAds/YandexMobileAds-Swift.h>
#import "YMAUnityAppOpenAdLoader.h"
#import "YMAUnityStringConverter.h"
#import "YMAUnityObjectIDProvider.h"
#import "YMAUnityObjectsStorage.h"
#import "YMAUnityAppOpenAd.h"
#import "YMAMainThreadExecutor.h"

@interface YMAUnityAppOpenAdLoader()

@property (nonatomic, assign, readonly) YMAUnityAppOpenAdLoaderClientRef *clientRef;
@property (nonatomic, strong, readonly) YMAAppOpenAdLoader *appOpenLoader;

@end

@implementation YMAUnityAppOpenAdLoader

- (instancetype)initWithClientRef:(YMAUnityAppOpenAdLoaderClientRef *)clientRef
{
    self = [super init];
    if (self != nil) {
        _appOpenLoader = [[YMAAppOpenAdLoader alloc] init];
        _clientRef = clientRef;
    }
    return self;
}

- (void)loadAdWithRequest:(YMAAdRequest *)adRequest
{
    __weak __typeof__(self) weakSelf = self;
    [self.appOpenLoader loadAdWith:adRequest completionHandler:^(YMAAppOpenAd * _Nullable ad, NSError * _Nullable error) {
        [YMAMainThreadExecutor executeAsync:^{
            if (error != nil) {
                if (weakSelf.didFailToLoadAdCallback != NULL) {
                    char *adUnitId = [YMAUnityStringConverter copiedCStringFromObjCString:adRequest.adUnitID];
                    char *message = [YMAUnityStringConverter copiedCStringFromObjCString:error.localizedDescription];
                    weakSelf.didFailToLoadAdCallback(weakSelf.clientRef, adUnitId, message);
                }
            } else {
                if (weakSelf.didLoadAdCallback != NULL) {
                    const char *objectID = [YMAUnityObjectIDProvider IDForObject:ad];
                    [[YMAUnityObjectsStorage sharedInstance] setObject:ad withID:objectID];

                    weakSelf.didLoadAdCallback(weakSelf.clientRef, [YMAUnityStringConverter copiedCString:objectID]);
                }
            }
        }];
    }];
}

- (void)cancelLoading
{
    [self.appOpenLoader cancelLoading];
}

@end
