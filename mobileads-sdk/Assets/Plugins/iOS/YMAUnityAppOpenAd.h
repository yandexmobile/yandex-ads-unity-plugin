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
#import "YMAUnityAppOpenAdTypes.h"

@interface YMAUnityAppOpenAd: NSObject

- (instancetype)initWithClientRef:(YMAUnityAppOpenAdClientRef*)clientRef
                        appOpenAd:(YMAAppOpenAd*)appOpenAd;
@property(nonatomic, assign) YMAUnityAppOpenAdDidFailToShowCallback didFailToShowCallback;
@property(nonatomic, assign) YMAUnityAppOpenAdDidShowCallback didShowCallback;
@property(nonatomic, assign) YMAUnityAppOpenAdDidDismissCallback didDismissCallback;
@property(nonatomic, assign) YMAUnityAppOpenAdDidClickCallback didClickCallback;
@property(nonatomic, assign) YMAUnityAppOpenAdDidTrackImpressionCallback didTrackImpressionCallback;

- (void)show;

@end
