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
#import "YMAUnityBannerTypes.h"
#import "YMAUnityAdPosition.h"

@class YMAAdRequest;

@interface YMAUnityBanner: NSObject

- (instancetype)initWithClientRef:(YMAUnityBannerClientRef*)clientRef
                         adUnitID:(char*)adUnitID
                           adSize:(YMABannerAdSize*)bannerAdSize
                         position:(YMAUnityAdPosition)position;

@property(nonatomic, assign) YMAUnityAdViewDidReceiveAdCallback adReceivedCallback;
@property(nonatomic, assign) YMAUnityAdViewDidFailToReceiveAdWithErrorCallback loadingFailedCallback;
@property(nonatomic, assign) YMAUnityAdViewWillPresentScreenCallback willPresentScreenCallback;
@property(nonatomic, assign) YMAUnityAdViewDidDismissScreenCallback didDismissScreenCallback;
@property(nonatomic, assign) YMAUnityAdViewDidTrackImpressionCallback didTrackImpressionCallback;
@property(nonatomic, assign) YMAUnityAdViewWillLeaveApplicationCallback willLeaveApplicationCallback;
@property(nonatomic, assign) YMAUnityAdViewDidClickCallback didClickCallback;

- (void)loadAdWithRequest:(YMAAdRequest*)adRequest;

- (void)show;

- (void)hide;

@end
