/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

#import <YandexMobileAds/YandexMobileAds.h>
#import "YMAUnityAppOpenAd.h"
#import "YMAUnityStringConverter.h"

@interface YMAUnityAppOpenAd() <YMAAppOpenAdDelegate>

@property (nonatomic, assign, readonly) YMAUnityAppOpenAdClientRef *clientRef;
@property (nonatomic, strong, readonly) YMAAppOpenAd *appOpenAd;

@end

@implementation YMAUnityAppOpenAd

- (instancetype)initWithClientRef:(YMAUnityAppOpenAdClientRef *)clientRef
                         appOpenAd:(YMAAppOpenAd *)appOpenAd
{
    self = [super init];
    if (self != nil) {
        _appOpenAd = appOpenAd;
        _appOpenAd.delegate = self;
        _clientRef = clientRef;
    }
    return self;
}

- (void)show
{
    UIViewController *viewController = [UIApplication sharedApplication].keyWindow.rootViewController;
    [self.appOpenAd showFromViewController:viewController];
}

#pragma mark Callbacks

- (void)appOpenAd:(YMAAppOpenAd *)appOpenAd didFailToShowWithError:(NSError *)error
{
    if (self.didFailToShowCallback != NULL) {
        char *message = [YMAUnityStringConverter copiedCStringFromObjCString:error.localizedDescription];
        self.didFailToShowCallback(self.clientRef, message);
    }
}

- (void)appOpenAdDidShow:(YMAAppOpenAd *)appOpenAd
{
    if (self.didShowCallback != NULL) {
        self.didShowCallback(self.clientRef);
    }
}

- (void)appOpenAdDidDismiss:(YMAAppOpenAd *)appOpenAd
{
    if (self.didDismissCallback != NULL) {
        self.didDismissCallback(self.clientRef);
    }
}


- (void)appOpenAdDidClick:(YMAAppOpenAd *)appOpenAd
{
    if (self.didClickCallback != NULL) {
        self.didClickCallback(self.clientRef);
    }
}

- (void)appOpenAd:(YMAAppOpenAd *)appOpenAd
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
