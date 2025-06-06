#pragma once

/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

// Unity rewarded client reference is needed to pass banner client in callback.
typedef const void* YMAUnityRewardedAdClientRef;

typedef void (*YMAUnityRewardedAdDidRewardCallback)(YMAUnityRewardedAdClientRef* rewardedAdClient, int amount, char* type);
typedef void (*YMAUnityRewardedAdDidFailToShowCallback)(YMAUnityRewardedAdClientRef* rewardedAdClient, char* error);
typedef void (*YMAUnityRewardedAdDidShowCallback)(YMAUnityRewardedAdClientRef* rewardedAdClient);
typedef void (*YMAUnityRewardedAdDidDismissCallback)(YMAUnityRewardedAdClientRef* rewardedAdClient);
typedef void (*YMAUnityRewardedAdDidClickCallback)(YMAUnityRewardedAdClientRef* rewardedAdClient);
typedef void (*YMAUnityRewardedAdDidTrackImpressionCallback)(YMAUnityRewardedAdClientRef* rewardedAdClient, char* rawData);
