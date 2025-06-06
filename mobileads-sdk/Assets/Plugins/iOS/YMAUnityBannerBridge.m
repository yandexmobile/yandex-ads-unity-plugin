/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

#import <YandexMobileAds/YandexMobileAds.h>
#import "YMAUnityBanner.h"
#import "YMAUnityObjectsStorage.h"
#import "YMAUnityStringConverter.h"
#import "YMAUnityObjectIDProvider.h"
#import "YMAUnityAdPosition.h"

char *YMAUnityCreateBannerView(YMAUnityBannerClientRef *clientRef,
                               char *adUnitID,
                               char *bannerAdSizeID,
                               YMAUnityAdPosition position)
{
    YMABannerAdSize *bannerAdSize = [[YMAUnityObjectsStorage sharedInstance] objectWithID:bannerAdSizeID];
    YMAUnityBanner *banner = [[YMAUnityBanner alloc] initWithClientRef:clientRef
                                                              adUnitID:adUnitID
                                                                adSize:bannerAdSize
                                                              position:position];
    const char *objectID = [YMAUnityObjectIDProvider IDForObject:banner];
    [[YMAUnityObjectsStorage sharedInstance] setObject:banner withID:objectID];
    return [YMAUnityStringConverter copiedCString:objectID];
}

void YMAUnitySetBannerCallbacks(char *objectID,
                                YMAUnityAdViewDidReceiveAdCallback adReceivedCallback,
                                YMAUnityAdViewDidFailToReceiveAdWithErrorCallback loadingFailedCallback,
                                YMAUnityAdViewWillPresentScreenCallback willPresentScreenCallback,
                                YMAUnityAdViewDidDismissScreenCallback didDismissScreenCallback,
                                YMAUnityAdViewDidTrackImpressionCallback didTrackImpressionCallback,
                                YMAUnityAdViewWillLeaveApplicationCallback willLeaveApplicationCallback,
                                YMAUnityAdViewDidClickCallback didClickCallback) {
    YMAUnityBanner *banner = [[YMAUnityObjectsStorage sharedInstance] objectWithID:objectID];
    banner.adReceivedCallback = adReceivedCallback;
    banner.loadingFailedCallback = loadingFailedCallback;
    banner.willPresentScreenCallback = willPresentScreenCallback;
    banner.didDismissScreenCallback = didDismissScreenCallback;
    banner.didTrackImpressionCallback = didTrackImpressionCallback;
    banner.willLeaveApplicationCallback = willLeaveApplicationCallback;
    banner.didClickCallback = didClickCallback;
}

void YMAUnityLoadBannerView(char *objectID, char *adRequestID)
{
    YMAAdRequest *adRequest = [[YMAUnityObjectsStorage sharedInstance] objectWithID:adRequestID];
    YMAUnityBanner *banner = [[YMAUnityObjectsStorage sharedInstance] objectWithID:objectID];
    [banner loadAdWithRequest:adRequest];
}

void YMAUnityShowBannerView(char *objectID)
{
    YMAUnityBanner *banner = [[YMAUnityObjectsStorage sharedInstance] objectWithID:objectID];
    [banner show];
}

void YMAUnityHideBannerView(char *objectID)
{
    YMAUnityBanner *banner = [[YMAUnityObjectsStorage sharedInstance] objectWithID:objectID];
    [banner hide];
}
