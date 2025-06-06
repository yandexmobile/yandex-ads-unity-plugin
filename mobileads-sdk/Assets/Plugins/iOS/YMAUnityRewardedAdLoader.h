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
#import "YMAUnityRewardedAdLoaderTypes.h"

@class YMAAdRequestConfiguration;

@interface YMAUnityRewardedAdLoader: NSObject

- (instancetype)initWithClientRef:(YMAUnityRewardedAdLoaderClientRef*)clientRef;

@property(nonatomic, assign) YMAUnityRewardedDidLoadAdCallback didLoadAdCallback;
@property(nonatomic, assign) YMAUnityRewardedDidFailToLoadAdCallback didFailToLoadAdCallback;

- (void)loadWithRequestConfiguration:(YMAAdRequestConfiguration*)adRequestConfiguration;
- (void)cancelLoading;

@end
