/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

#import <YandexMobileAds/YandexMobileAds.h>
#import "YMAUnityAppOpenAd.h"
#import "YMAUnityObjectsStorage.h"
#import "YMAUnityStringConverter.h"
#import "YMAUnityObjectIDProvider.h"

char *YMAUnityCreateAppOpenAd(YMAUnityAppOpenAdClientRef *clientRef, char *appOpenAdObjectID)
{
    YMAUnityObjectsStorage *objectStorage = [YMAUnityObjectsStorage sharedInstance];
    YMAAppOpenAd *appOpenAd = [objectStorage objectWithID:appOpenAdObjectID];

    YMAUnityAppOpenAd *unityAppOpenAd = [[YMAUnityAppOpenAd alloc] initWithClientRef:clientRef
                                                                                appOpenAd:appOpenAd];
    const char *unityAppOpenAdObjectID = [YMAUnityObjectIDProvider IDForObject:unityAppOpenAd];
    [objectStorage setObject:unityAppOpenAd withID:unityAppOpenAdObjectID];
    [objectStorage removeObjectWithID:appOpenAdObjectID];

    return [YMAUnityStringConverter copiedCString:unityAppOpenAdObjectID];
}

void YMAUnitySetAppOpenAdCallbacks(char *unityAppOpenAdObjectID,
                                      YMAUnityAppOpenAdDidFailToShowCallback didFailToShowCallback,
                                      YMAUnityAppOpenAdDidShowCallback didShowCallback,
                                      YMAUnityAppOpenAdDidDismissCallback didDismissCallback,
                                      YMAUnityAppOpenAdDidClickCallback didClickCallback,
                                      YMAUnityAppOpenAdDidTrackImpressionCallback didTrackImpressionCallback)
{
    YMAUnityAppOpenAd *unityAppOpenAd = [[YMAUnityObjectsStorage sharedInstance] objectWithID:unityAppOpenAdObjectID];

    unityAppOpenAd.didFailToShowCallback = didFailToShowCallback;
    unityAppOpenAd.didShowCallback = didShowCallback;
    unityAppOpenAd.didDismissCallback = didDismissCallback;
    unityAppOpenAd.didClickCallback = didClickCallback;
    unityAppOpenAd.didTrackImpressionCallback = didTrackImpressionCallback;
}

char *YMAUnityGetAppOpenInfo(char *unityAppOpenAdObjectID)
{
    YMAUnityObjectsStorage *objectStorage = [YMAUnityObjectsStorage sharedInstance];
    YMAAppOpenAd *appOpenAd = [objectStorage objectWithID:unityAppOpenAdObjectID];
    YMAAdInfo *adInfo = [appOpenAd adInfo];

    const char *adInfoObjectID = [YMAUnityObjectIDProvider IDForObject:adInfo];
    [[YMAUnityObjectsStorage sharedInstance] setObject:adInfo withID:adInfoObjectID];

    return [YMAUnityStringConverter copiedCString:adInfoObjectID];
}

void YMAUnityShowAppOpenAd(char *unityAppOpenAdObjectID)
{
    YMAUnityAppOpenAd *unityAppOpenAd = [[YMAUnityObjectsStorage sharedInstance] objectWithID:unityAppOpenAdObjectID];
    [unityAppOpenAd show];
}

void YMAUnityDestroyAppOpenAd(char *unityAppOpenAdObjectID)
{
    YMAUnityAppOpenAd *unityAppOpenAd = [[YMAUnityObjectsStorage sharedInstance] objectWithID:unityAppOpenAdObjectID];
    unityAppOpenAd.didFailToShowCallback = NULL;
    unityAppOpenAd.didClickCallback = NULL;
    unityAppOpenAd.didDismissCallback = NULL;
    unityAppOpenAd.didTrackImpressionCallback = NULL;
    [[YMAUnityObjectsStorage sharedInstance] removeObjectWithID:unityAppOpenAdObjectID];
}
