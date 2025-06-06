#pragma once

/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */
#import "YMAUnityAppOpenAdTypes.h"

typedef const void* YMAUnityAppOpenAdLoaderClientRef;

typedef void (*YMAUnityAppOpenDidLoadAdCallback)(YMAUnityAppOpenAdLoaderClientRef* appOpenLoaderClient, char* appOpenAdObjectID);
typedef void (*YMAUnityAppOpenDidFailToLoadAdCallback)(YMAUnityAppOpenAdLoaderClientRef* appOpenLoaderClient, char* adUnitId, char* error);
