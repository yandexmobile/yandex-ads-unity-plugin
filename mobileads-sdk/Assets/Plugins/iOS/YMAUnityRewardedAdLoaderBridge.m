/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

#import "YMAUnityRewardedAdLoader.h"
#import "YMAUnityObjectsStorage.h"
#import "YMAUnityStringConverter.h"
#import "YMAUnityObjectIDProvider.h"

char *YMAUnityCreateRewardedAdLoader(YMAUnityRewardedAdLoaderClientRef *clientRef)
{
    YMAUnityRewardedAdLoader *rewardedAdLoader = [[YMAUnityRewardedAdLoader alloc]
                                                          initWithClientRef:clientRef];
    const char *unityRewardedAdLoaderObjectID = [YMAUnityObjectIDProvider IDForObject:rewardedAdLoader];
    [[YMAUnityObjectsStorage sharedInstance] setObject:rewardedAdLoader
                                                withID:unityRewardedAdLoaderObjectID];

    return [YMAUnityStringConverter copiedCString:unityRewardedAdLoaderObjectID];
}

void YMAUnitySetRewardedAdLoaderCallbacks(char *unityRewardedAdLoaderObjectID,
                                      YMAUnityRewardedDidLoadAdCallback didLoadAdCallback,
                                      YMAUnityRewardedDidFailToLoadAdCallback didFailToLoadAdCallback)
{
    YMAUnityRewardedAdLoader *rewardedAdLoader = [[YMAUnityObjectsStorage sharedInstance]
                                                          objectWithID:unityRewardedAdLoaderObjectID];

    rewardedAdLoader.didLoadAdCallback = didLoadAdCallback;
    rewardedAdLoader.didFailToLoadAdCallback = didFailToLoadAdCallback;
}

void YMAUnityLoadRewardedAd(char *unityRewardedAdLoaderObjectID, char *adRequestConfigurationID)
{
    YMAAdRequestConfiguration *adRequestConfiguration = [[YMAUnityObjectsStorage sharedInstance] objectWithID:adRequestConfigurationID];
    YMAUnityRewardedAdLoader *rewardedAdLoader = [[YMAUnityObjectsStorage sharedInstance] objectWithID:unityRewardedAdLoaderObjectID];

    [rewardedAdLoader loadWithRequestConfiguration:adRequestConfiguration];
}

void YMAUnityCancelLoadingRewardedAd(char *unityRewardedAdLoaderObjectID)
{
    YMAUnityRewardedAdLoader *rewardedAdLoader = [[YMAUnityObjectsStorage sharedInstance] objectWithID:unityRewardedAdLoaderObjectID];

    [rewardedAdLoader cancelLoading];
}
