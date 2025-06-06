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
#import "YMAUnityStringConverter.h"
#import "YMAUnityObjectIDProvider.h"

char *YMAUnityAdInfoGetAdUnitId(char *adInfoObjectID)
{
    YMAUnityObjectsStorage *objectStorage = [YMAUnityObjectsStorage sharedInstance];
    YMAAdInfo *adInfo = [objectStorage objectWithID:adInfoObjectID];

    return [YMAUnityStringConverter copiedCStringFromObjCString:[adInfo adUnitId]];
}

char *YMAUnityAdInfoGetAdSize(char *adInfoObjectID)
{
    YMAUnityObjectsStorage *objectStorage = [YMAUnityObjectsStorage sharedInstance];
    YMAAdInfo *adInfo = [objectStorage objectWithID:adInfoObjectID];
    YMAAdSize *adSize = [adInfo adSize];

    const char *adSizeObjectID = [YMAUnityObjectIDProvider IDForObject:adSize];
    [[YMAUnityObjectsStorage sharedInstance] setObject:adSize withID:adSizeObjectID];

    return [YMAUnityStringConverter copiedCString:adSizeObjectID];
}
