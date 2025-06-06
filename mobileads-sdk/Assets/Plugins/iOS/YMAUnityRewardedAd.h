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
#import "YMAUnityRewardedAdTypes.h"

@interface YMAUnityRewardedAd: NSObject

- (instancetype)initWithClientRef:(YMAUnityRewardedAdClientRef*)clientRef
                       rewardedAd:(YMARewardedAd*)rewardedAd;
@property(nonatomic, assign) YMAUnityRewardedAdDidRewardCallback didRewardCallback;
@property(nonatomic, assign) YMAUnityRewardedAdDidFailToShowCallback didFailToShowCallback;
@property(nonatomic, assign) YMAUnityRewardedAdDidShowCallback didShowCallback;
@property(nonatomic, assign) YMAUnityRewardedAdDidDismissCallback didDismissCallback;
@property(nonatomic, assign) YMAUnityRewardedAdDidClickCallback didClickCallback;
@property(nonatomic, assign) YMAUnityRewardedAdDidTrackImpressionCallback didTrackImpressionCallback;

- (YMAAdInfo*)getInfo;
- (void)show;

@end
