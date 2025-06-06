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
#import "YMAUnityInterstitialAdLoaderTypes.h"

@class YMAAdRequestConfiguration;

@interface YMAUnityInterstitialAdLoader: NSObject

- (instancetype)initWithClientRef:(YMAUnityInterstitialAdLoaderClientRef*)clientRef;

@property(nonatomic, assign) YMAUnityInterstitialDidLoadAdCallback didLoadAdCallback;
@property(nonatomic, assign) YMAUnityInterstitialDidFailToLoadAdCallback didFailToLoadAdCallback;

- (void)loadWithRequestConfiguration:(YMAAdRequestConfiguration*)adRequestConfiguration;
- (void)cancelLoading;

@end
