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
#import "YMAUnityAppOpenAdLoaderTypes.h"

@class YMAAdRequestConfiguration;

@interface YMAUnityAppOpenAdLoader: NSObject

- (instancetype)initWithClientRef:(YMAUnityAppOpenAdLoaderClientRef*)clientRef;

@property(nonatomic, assign) YMAUnityAppOpenDidLoadAdCallback didLoadAdCallback;
@property(nonatomic, assign) YMAUnityAppOpenDidFailToLoadAdCallback didFailToLoadAdCallback;

- (void)loadWithRequestConfiguration:(YMAAdRequestConfiguration*)adRequestConfiguration;
- (void)cancelLoading;

@end
