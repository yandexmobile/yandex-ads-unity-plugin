/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

#import <YandexMobileAds/YandexMobileAds.h>
#import "YMAUnityObjectsStorage.h"

double YMAUnityAdSizeGetWidth(char *adSizeObjectID)
{
    YMAUnityObjectsStorage *objectStorage = [YMAUnityObjectsStorage sharedInstance];
    YMAAdSize *adSize = [objectStorage objectWithID:adSizeObjectID];

    return [adSize width];
}

double YMAUnityAdSizeGetHeight(char *adSizeObjectID)
{
    YMAUnityObjectsStorage *objectStorage = [YMAUnityObjectsStorage sharedInstance];
    YMAAdSize *adSize = [objectStorage objectWithID:adSizeObjectID];

    return [adSize height];
}
