#pragma once

/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

// Unity interstitial client reference is needed to pass banner client in callback.
typedef const void* YMAUnityInterstitialAdClientRef;

typedef void (*YMAUnityInterstitialAdDidFailToShowCallback)(YMAUnityInterstitialAdClientRef* interstitialAdClient, char* error);
typedef void (*YMAUnityInterstitialAdDidShowCallback)(YMAUnityInterstitialAdClientRef* interstitialAdClient);
typedef void (*YMAUnityInterstitialAdDidDismissCallback)(YMAUnityInterstitialAdClientRef* interstitialAdClient);
typedef void (*YMAUnityInterstitialAdDidClickCallback)(YMAUnityInterstitialAdClientRef* interstitialAdClient);
typedef void (*YMAUnityInterstitialAdDidTrackImpressionCallback)(YMAUnityInterstitialAdClientRef* interstitialAdClient, char* rawData);
