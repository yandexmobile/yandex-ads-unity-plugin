/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

#import <YandexMobileAds/YandexMobileAds-Swift.h>
#import "YMAUnityObjectsStorage.h"
#import "YMAUnityStringConverter.h"

char *YMAUnityCreativeGetCreativeId(char *creativeObjectID)
{
    YMAUnityObjectsStorage *objectStorage = [YMAUnityObjectsStorage sharedInstance];
    YMACreative *creative = [objectStorage objectWithID:creativeObjectID];
    return [YMAUnityStringConverter copiedCStringFromObjCString:[creative creativeID]];
}

char *YMAUnityCreativeGetCampaignId(char *creativeObjectID)
{
    YMAUnityObjectsStorage *objectStorage = [YMAUnityObjectsStorage sharedInstance];
    YMACreative *creative = [objectStorage objectWithID:creativeObjectID];
    return [YMAUnityStringConverter copiedCStringFromObjCString:[creative campaignID]];
}

char *YMAUnityCreativeGetPlaceId(char *creativeObjectID)
{
    YMAUnityObjectsStorage *objectStorage = [YMAUnityObjectsStorage sharedInstance];
    YMACreative *creative = [objectStorage objectWithID:creativeObjectID];
    return [YMAUnityStringConverter copiedCStringFromObjCString:[creative placeID]];
}

