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
#import "YMAUnityObjectIDProvider.h"
#import "YMAUnityStringConverter.h"

char *YMAUnityObjectIDWithAdSize(YMABannerAdSize *bannerAdSize)
{
    const char *objectID = [YMAUnityObjectIDProvider IDForObject:bannerAdSize];
    [[YMAUnityObjectsStorage sharedInstance] setObject:bannerAdSize withID:objectID];
    return [YMAUnityStringConverter copiedCString:objectID];
}

double YMAUnityGetBannerAdSizeHeight(char *bannerAdSizeObjectID)
{
    YMAUnityObjectsStorage *objectStorage = [YMAUnityObjectsStorage sharedInstance];
    YMABannerAdSize *bannerAdSize = [objectStorage objectWithID:bannerAdSizeObjectID];
    
    return [bannerAdSize size].height;
}

double YMAUnityGetBannerAdSizeWidth(char *bannerAdSizeObjectID)
{
    YMAUnityObjectsStorage *objectStorage = [YMAUnityObjectsStorage sharedInstance];
    YMABannerAdSize *bannerAdSize = [objectStorage objectWithID:bannerAdSizeObjectID];
    
    return [bannerAdSize size].width;
}


char *YMAUnityCreateFixedBannerAdSize(NSInteger width, NSInteger height)
{
    YMABannerAdSize *bannerAdSize = [YMABannerAdSize fixedSizeWithWidth:width height:height];
    return YMAUnityObjectIDWithAdSize(bannerAdSize);
}

char *YMAUnityCreateStickyBannerAdSize(NSInteger width)
{
    YMABannerAdSize *bannerAdSize = [YMABannerAdSize stickySizeWithContainerWidth:width];
    return YMAUnityObjectIDWithAdSize(bannerAdSize);
}

char *YMAUnityCreateInlineBannerAdSize(NSInteger width, NSInteger maxHeight)
{
    YMABannerAdSize *bannerAdSize = [YMABannerAdSize inlineSizeWithWidth:width maxHeight:maxHeight];
    return YMAUnityObjectIDWithAdSize(bannerAdSize);
}
