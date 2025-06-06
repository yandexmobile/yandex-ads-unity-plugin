#pragma once

/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */
#import "YMAUnityInterstitialTypes.h"

typedef const void* YMAUnityInterstitialAdLoaderClientRef;

typedef void (*YMAUnityInterstitialDidLoadAdCallback)(YMAUnityInterstitialAdLoaderClientRef* interstitialLoaderClient, char* interstitialAdObjectID);
typedef void (*YMAUnityInterstitialDidFailToLoadAdCallback)(YMAUnityInterstitialAdLoaderClientRef* interstitialLoaderClient, char* adUnitId, char* error);
