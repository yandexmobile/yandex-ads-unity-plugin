#pragma once

/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */
#import "YMAUnityRewardedAdTypes.h"

typedef const void* YMAUnityRewardedAdLoaderClientRef;

typedef void (*YMAUnityRewardedDidLoadAdCallback)(YMAUnityRewardedAdLoaderClientRef* rewardedAdLoaderClient, char* rewardedAdObjectID);
typedef void (*YMAUnityRewardedDidFailToLoadAdCallback)(YMAUnityRewardedAdLoaderClientRef* rewardedAdLoaderClient, char* adUnitId, char* error);
