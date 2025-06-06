/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

#import <YandexMobileAds/YandexMobileAds.h>
#import "YMAUnityRewardedAd.h"
#import "YMAUnityObjectsStorage.h"
#import "YMAUnityStringConverter.h"
#import "YMAUnityObjectIDProvider.h"

char *YMAUnityCreateRewardedAd(YMAUnityRewardedAdClientRef *clientRef, char *rewardedAdObjectID)
{
    YMAUnityObjectsStorage *objectStorage = [YMAUnityObjectsStorage sharedInstance];
    YMARewardedAd *rewardedAd = [objectStorage objectWithID:rewardedAdObjectID];

    YMAUnityRewardedAd *unityRewardedAd = [[YMAUnityRewardedAd alloc] initWithClientRef:clientRef
                                                                                rewardedAd:rewardedAd];
    const char *unityRewardedAdObjectID = [YMAUnityObjectIDProvider IDForObject:unityRewardedAd];
    [objectStorage setObject:unityRewardedAd withID:unityRewardedAdObjectID];
    [objectStorage removeObjectWithID:rewardedAdObjectID];

    return [YMAUnityStringConverter copiedCString:unityRewardedAdObjectID];
}

void YMAUnitySetRewardedAdCallbacks(char *unityRewardedAdObjectID,
                                      YMAUnityRewardedAdDidRewardCallback didRewardCallback,
                                      YMAUnityRewardedAdDidFailToShowCallback didFailToShowCallback,
                                      YMAUnityRewardedAdDidShowCallback didShowCallback,
                                      YMAUnityRewardedAdDidDismissCallback didDismissCallback,
                                      YMAUnityRewardedAdDidClickCallback didClickCallback,
                                      YMAUnityRewardedAdDidTrackImpressionCallback didTrackImpressionCallback)
{
    YMAUnityRewardedAd *unityRewardedAd = [[YMAUnityObjectsStorage sharedInstance] objectWithID:unityRewardedAdObjectID];

    unityRewardedAd.didRewardCallback = didRewardCallback;
    unityRewardedAd.didFailToShowCallback = didFailToShowCallback;
    unityRewardedAd.didShowCallback = didShowCallback;
    unityRewardedAd.didDismissCallback = didDismissCallback;
    unityRewardedAd.didClickCallback = didClickCallback;
    unityRewardedAd.didTrackImpressionCallback = didTrackImpressionCallback;
}

char *YMAUnityGetRewardedInfo(char *unityRewardedAdObjectID)
{
    YMAUnityObjectsStorage *objectStorage = [YMAUnityObjectsStorage sharedInstance];
    YMAUnityRewardedAd *unityRewardedAd = [objectStorage objectWithID:unityRewardedAdObjectID];
    YMAAdInfo *adInfo = [unityRewardedAd getInfo];

    const char *adInfoObjectID = [YMAUnityObjectIDProvider IDForObject:adInfo];
    [[YMAUnityObjectsStorage sharedInstance] setObject:adInfo withID:adInfoObjectID];

    return [YMAUnityStringConverter copiedCString:adInfoObjectID];
}

void YMAUnityShowRewardedAd(char *unityRewardedAdObjectID)
{
    YMAUnityRewardedAd *unityRewardedAd = [[YMAUnityObjectsStorage sharedInstance] objectWithID:unityRewardedAdObjectID];
    [unityRewardedAd show];
}

void YMAUnityDestroyRewardedAd(char *unityRewardedAdObjectID)
{
    YMAUnityRewardedAd *unityRewardedAd = [[YMAUnityObjectsStorage sharedInstance] objectWithID:unityRewardedAdObjectID];
    unityRewardedAd.didRewardCallback = NULL;
    unityRewardedAd.didFailToShowCallback = NULL;
    unityRewardedAd.didClickCallback = NULL;
    unityRewardedAd.didDismissCallback = NULL;
    unityRewardedAd.didTrackImpressionCallback = NULL;
    [[YMAUnityObjectsStorage sharedInstance] removeObjectWithID:unityRewardedAdObjectID];
}
