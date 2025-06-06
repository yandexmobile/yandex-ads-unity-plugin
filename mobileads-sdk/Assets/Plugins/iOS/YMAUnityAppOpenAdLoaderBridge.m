/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

#import "YMAUnityAppOpenAdLoader.h"
#import "YMAUnityObjectsStorage.h"
#import "YMAUnityStringConverter.h"
#import "YMAUnityObjectIDProvider.h"

char *YMAUnityCreateAppOpenAdLoader(YMAUnityAppOpenAdLoaderClientRef *clientRef)
{
    YMAUnityAppOpenAdLoader *appOpenAdLoader = [[YMAUnityAppOpenAdLoader alloc]
                                                          initWithClientRef:clientRef];
    const char *unityAppOpenAdLoaderObjectID = [YMAUnityObjectIDProvider IDForObject:appOpenAdLoader];
    [[YMAUnityObjectsStorage sharedInstance] setObject:appOpenAdLoader
                                                withID:unityAppOpenAdLoaderObjectID];
    return [YMAUnityStringConverter copiedCString:unityAppOpenAdLoaderObjectID];
}

void YMAUnitySetAppOpenAdLoaderCallbacks(char *unityAppOpenAdLoaderObjectID,
                                      YMAUnityAppOpenDidLoadAdCallback didLoadAdCallback,
                                      YMAUnityAppOpenDidFailToLoadAdCallback didFailToLoadAdCallback)
{
    YMAUnityAppOpenAdLoader *appOpenAdLoader = [[YMAUnityObjectsStorage sharedInstance]
                                                          objectWithID:unityAppOpenAdLoaderObjectID];
    appOpenAdLoader.didLoadAdCallback = didLoadAdCallback;
    appOpenAdLoader.didFailToLoadAdCallback = didFailToLoadAdCallback;
}

void YMAUnityLoadAppOpenAd(char *unityAppOpenAdLoaderObjectID, char *adRequestConfigurationID)
{
    YMAAdRequestConfiguration *adRequestConfiguration = [[YMAUnityObjectsStorage sharedInstance] objectWithID:adRequestConfigurationID];
    YMAUnityAppOpenAdLoader *appOpenAdLoader = [[YMAUnityObjectsStorage sharedInstance] objectWithID:unityAppOpenAdLoaderObjectID];
    [appOpenAdLoader loadWithRequestConfiguration:adRequestConfiguration];
}

void YMAUnityCancelLoadingAppOpenAd(char *unityAppOpenAdLoaderObjectID)
{
    YMAUnityAppOpenAdLoader *appOpenAdLoader = [[YMAUnityObjectsStorage sharedInstance] objectWithID:unityAppOpenAdLoaderObjectID];
    [appOpenAdLoader cancelLoading];
}
