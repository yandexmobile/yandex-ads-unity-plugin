/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

#import "YMAUnityInterstitialAdLoader.h"
#import "YMAUnityObjectsStorage.h"
#import "YMAUnityStringConverter.h"
#import "YMAUnityObjectIDProvider.h"

char *YMAUnityCreateInterstitialAdLoader(YMAUnityInterstitialAdLoaderClientRef *clientRef)
{
    YMAUnityInterstitialAdLoader *interstitialAdLoader = [[YMAUnityInterstitialAdLoader alloc]
                                                          initWithClientRef:clientRef];
    const char *unityInterstitialAdLoaderObjectID = [YMAUnityObjectIDProvider IDForObject:interstitialAdLoader];
    [[YMAUnityObjectsStorage sharedInstance] setObject:interstitialAdLoader
                                                withID:unityInterstitialAdLoaderObjectID];
    return [YMAUnityStringConverter copiedCString:unityInterstitialAdLoaderObjectID];
}

void YMAUnitySetInterstitialAdLoaderCallbacks(char *unityInterstitialAdLoaderObjectID,
                                      YMAUnityInterstitialDidLoadAdCallback didLoadAdCallback,
                                      YMAUnityInterstitialDidFailToLoadAdCallback didFailToLoadAdCallback)
{
    YMAUnityInterstitialAdLoader *interstitialAdLoader = [[YMAUnityObjectsStorage sharedInstance]
                                                          objectWithID:unityInterstitialAdLoaderObjectID];
    interstitialAdLoader.didLoadAdCallback = didLoadAdCallback;
    interstitialAdLoader.didFailToLoadAdCallback = didFailToLoadAdCallback;
}

void YMAUnityLoadInterstitialAd(char *unityInterstitialAdLoaderObjectID, char *adRequestConfigurationID)
{
    YMAAdRequestConfiguration *adRequestConfiguration = [[YMAUnityObjectsStorage sharedInstance] objectWithID:adRequestConfigurationID];
    YMAUnityInterstitialAdLoader *interstitialAdLoader = [[YMAUnityObjectsStorage sharedInstance] objectWithID:unityInterstitialAdLoaderObjectID];
    [interstitialAdLoader loadWithRequestConfiguration:adRequestConfiguration];
}

void YMAUnityCancelLoadingInterstitialAd(char *unityInterstitialAdLoaderObjectID)
{
    YMAUnityInterstitialAdLoader *interstitialAdLoader = [[YMAUnityObjectsStorage sharedInstance] objectWithID:unityInterstitialAdLoaderObjectID];
    [interstitialAdLoader cancelLoading];
}
