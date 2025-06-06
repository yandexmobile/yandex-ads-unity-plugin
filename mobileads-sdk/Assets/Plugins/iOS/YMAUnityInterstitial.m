/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

#import <YandexMobileAds/YandexMobileAds.h>
#import "YMAUnityInterstitial.h"
#import "YMAUnityStringConverter.h"

@interface YMAUnityInterstitialAd() <YMAInterstitialAdDelegate>

@property (nonatomic, assign, readonly) YMAUnityInterstitialAdClientRef *clientRef;
@property (nonatomic, strong, readonly) YMAInterstitialAd *interstitialAd;

@end

@implementation YMAUnityInterstitialAd

- (instancetype)initWithClientRef:(YMAUnityInterstitialAdClientRef *)clientRef
                         interstitialAd:(YMAInterstitialAd *)interstitialAd
{
    self = [super init];
    if (self != nil) {
        _interstitialAd = interstitialAd;
        _interstitialAd.delegate = self;
        _clientRef = clientRef;
    }
    return self;
}

- (YMAAdInfo*)getInfo
{
    return [self.interstitialAd adInfo];
}

- (void)show
{
    UIViewController *viewController = [UIApplication sharedApplication].keyWindow.rootViewController;
    [self.interstitialAd showFromViewController:viewController];
}

#pragma mark Callbacks

- (void)interstitialAd:(YMAInterstitialAd *)interstitialAd didFailToShowWithError:(NSError *)error
{
    if (self.didFailToShowCallback != NULL) {
        char *message = [YMAUnityStringConverter copiedCStringFromObjCString:error.localizedDescription];
        self.didFailToShowCallback(self.clientRef, message);
    }
}

- (void)interstitialAdDidShow:(YMAInterstitialAd *)interstitialAd
{
    if (self.didShowCallback != NULL) {
        self.didShowCallback(self.clientRef);
    }
}

- (void)interstitialAdDidDismiss:(YMAInterstitialAd *)interstitialAd
{
    if (self.didDismissCallback != NULL) {
        self.didDismissCallback(self.clientRef);
    }
}


- (void)interstitialAdDidClick:(YMAInterstitialAd *)interstitialAd
{
    if (self.didClickCallback != NULL) {
        self.didClickCallback(self.clientRef);
    }
}

- (void)interstitialAd:(YMAInterstitialAd *)interstitialAd
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
