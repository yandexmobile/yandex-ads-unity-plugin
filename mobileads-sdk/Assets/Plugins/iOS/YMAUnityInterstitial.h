#pragma once

/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

#import <Foundation/Foundation.h>
#import "YMAUnityInterstitialTypes.h"

@interface YMAUnityInterstitialAd: NSObject

- (instancetype)initWithClientRef:(YMAUnityInterstitialAdClientRef*)clientRef
                   interstitialAd:(YMAInterstitialAd*)interstitial;
@property(nonatomic, assign) YMAUnityInterstitialAdDidFailToShowCallback didFailToShowCallback;
@property(nonatomic, assign) YMAUnityInterstitialAdDidShowCallback didShowCallback;
@property(nonatomic, assign) YMAUnityInterstitialAdDidDismissCallback didDismissCallback;
@property(nonatomic, assign) YMAUnityInterstitialAdDidClickCallback didClickCallback;
@property(nonatomic, assign) YMAUnityInterstitialAdDidTrackImpressionCallback didTrackImpressionCallback;

- (YMAAdInfo*)getInfo;
- (void)show;

@end
