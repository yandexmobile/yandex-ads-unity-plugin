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
#import "YMAUnityObjectIDProvider.h"

char *YMAUnityAdInfoGetAdUnitId(char *adInfoObjectID)
{
    YMAUnityObjectsStorage *objectStorage = [YMAUnityObjectsStorage sharedInstance];
    YMAAdInfo *adInfo = [objectStorage objectWithID:adInfoObjectID];

    return [YMAUnityStringConverter copiedCStringFromObjCString:[adInfo adUnitID]];
}

char *YMAUnityAdInfoGetExtraData(char *adInfoObjectID)
{
    YMAUnityObjectsStorage *objectStorage = [YMAUnityObjectsStorage sharedInstance];
    YMAAdInfo *adInfo = [objectStorage objectWithID:adInfoObjectID];
    return [YMAUnityStringConverter copiedCStringFromObjCString:[adInfo extraData]];
}

char *YMAUnityAdInfoGetPartnerText(char *adInfoObjectID)
{
    YMAUnityObjectsStorage *objectStorage = [YMAUnityObjectsStorage sharedInstance];
    YMAAdInfo *adInfo = [objectStorage objectWithID:adInfoObjectID];
    return [YMAUnityStringConverter copiedCStringFromObjCString:[adInfo partnerText]];
}

int YMAUnityAdInfoGetCreativesCount(char *adInfoObjectID)
{
    YMAUnityObjectsStorage *objectStorage = [YMAUnityObjectsStorage sharedInstance];
    YMAAdInfo *adInfo = [objectStorage objectWithID:adInfoObjectID];
    return (int)[adInfo creatives].count;
}

char *YMAUnityAdInfoGetCreativeAtIndex(char *adInfoObjectID, int index)
{
    YMAUnityObjectsStorage *objectStorage = [YMAUnityObjectsStorage sharedInstance];
    YMAAdInfo *adInfo = [objectStorage objectWithID:adInfoObjectID];
    NSArray<YMACreative *> *creatives = [adInfo creatives];
    if (index < 0 || index >= (int)creatives.count) {
        return NULL;
    }
    YMACreative *creative = creatives[index];
    const char *creativeObjectID = [YMAUnityObjectIDProvider IDForObject:creative];
    [[YMAUnityObjectsStorage sharedInstance] setObject:creative withID:creativeObjectID];
    return [YMAUnityStringConverter copiedCString:creativeObjectID];
}
