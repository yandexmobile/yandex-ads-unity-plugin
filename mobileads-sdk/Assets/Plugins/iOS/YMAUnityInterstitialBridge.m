/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

#import <YandexMobileAds/YandexMobileAds.h>
#import "YMAUnityInterstitial.h"
#import "YMAUnityObjectsStorage.h"
#import "YMAUnityStringConverter.h"
#import "YMAUnityObjectIDProvider.h"

char *YMAUnityCreateInterstitialAd(YMAUnityInterstitialAdClientRef *clientRef, char *interstitialAdObjectID)
{
    YMAUnityObjectsStorage *objectStorage = [YMAUnityObjectsStorage sharedInstance];
    YMAInterstitialAd *interstitialAd = [objectStorage objectWithID:interstitialAdObjectID];

    YMAUnityInterstitialAd *unityInterstitialAd = [[YMAUnityInterstitialAd alloc] initWithClientRef:clientRef
                                                                                interstitialAd:interstitialAd];
    const char *unityInterstitialAdObjectID = [YMAUnityObjectIDProvider IDForObject:unityInterstitialAd];
    [objectStorage setObject:unityInterstitialAd withID:unityInterstitialAdObjectID];
    [objectStorage removeObjectWithID:interstitialAdObjectID];

    return [YMAUnityStringConverter copiedCString:unityInterstitialAdObjectID];
}

void YMAUnitySetInterstitialAdCallbacks(char *unityInterstitialAdObjectID,
                                      YMAUnityInterstitialAdDidFailToShowCallback didFailToShowCallback,
                                      YMAUnityInterstitialAdDidShowCallback didShowCallback,
                                      YMAUnityInterstitialAdDidDismissCallback didDismissCallback,
                                      YMAUnityInterstitialAdDidClickCallback didClickCallback,
                                      YMAUnityInterstitialAdDidTrackImpressionCallback didTrackImpressionCallback)
{
    YMAUnityInterstitialAd *unityInterstitialAd = [[YMAUnityObjectsStorage sharedInstance] objectWithID:unityInterstitialAdObjectID];

    unityInterstitialAd.didFailToShowCallback = didFailToShowCallback;
    unityInterstitialAd.didShowCallback = didShowCallback;
    unityInterstitialAd.didDismissCallback = didDismissCallback;
    unityInterstitialAd.didClickCallback = didClickCallback;
    unityInterstitialAd.didTrackImpressionCallback = didTrackImpressionCallback;
}

char *YMAUnityGetInterstitialInfo(char *unityInterstitialAdObjectID)
{
    YMAUnityObjectsStorage *objectStorage = [YMAUnityObjectsStorage sharedInstance];
    YMAUnityInterstitialAd *unityInterstitialAd = [objectStorage objectWithID:unityInterstitialAdObjectID];
    YMAAdInfo *adInfo = [unityInterstitialAd getInfo];

    const char *adInfoObjectID = [YMAUnityObjectIDProvider IDForObject:adInfo];
    [[YMAUnityObjectsStorage sharedInstance] setObject:adInfo withID:adInfoObjectID];

    return [YMAUnityStringConverter copiedCString:adInfoObjectID];
}

void YMAUnityShowInterstitialAd(char *unityInterstitialAdObjectID)
{
    YMAUnityInterstitialAd *unityInterstitialAd = [[YMAUnityObjectsStorage sharedInstance] objectWithID:unityInterstitialAdObjectID];
    [unityInterstitialAd show];
}

void YMAUnityDestroyInterstitialAd(char *unityInterstitialAdObjectID)
{
    YMAUnityInterstitialAd *unityInterstitialAd = [[YMAUnityObjectsStorage sharedInstance] objectWithID:unityInterstitialAdObjectID];
    unityInterstitialAd.didFailToShowCallback = NULL;
    unityInterstitialAd.didClickCallback = NULL;
    unityInterstitialAd.didDismissCallback = NULL;
    unityInterstitialAd.didTrackImpressionCallback = NULL;
    [[YMAUnityObjectsStorage sharedInstance] removeObjectWithID:unityInterstitialAdObjectID];
}
