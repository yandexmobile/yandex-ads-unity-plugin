#pragma once

/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

// Unity banner client reference is needed to pass banner client in callback.
typedef const void* YMAUnityBannerClientRef;

typedef void (*YMAUnityAdViewDidReceiveAdCallback)(YMAUnityBannerClientRef* bannerClient);
typedef void (*YMAUnityAdViewDidFailToReceiveAdWithErrorCallback)(YMAUnityBannerClientRef* bannerClient, char* error);
typedef void (*YMAUnityAdViewWillPresentScreenCallback)(YMAUnityBannerClientRef* bannerClient);
typedef void (*YMAUnityAdViewWillDismissScreenCallback)(YMAUnityBannerClientRef* bannerClient);
typedef void (*YMAUnityAdViewDidDismissScreenCallback)(YMAUnityBannerClientRef* bannerClient);
typedef void (*YMAUnityAdViewDidTrackImpressionCallback)(YMAUnityBannerClientRef* bannerClient, char* rawData);
typedef void (*YMAUnityAdViewWillLeaveApplicationCallback)(YMAUnityBannerClientRef* bannerClient);
typedef void (*YMAUnityAdViewDidClickCallback)(YMAUnityBannerClientRef* bannerClient);
