/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2024 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

#include <stdbool.h>

#import <YandexMobileAds/YandexMobileAds-Swift.h>

void YMAUnitySetUserConsent(bool consent)
{
    [YMAYandexAds setUserConsent:consent];
}

void YMAUnitySetLocationConsent(bool consent)
{
    [YMAYandexAds setLocationTracking:consent];
}

void YMAUnitySetAgeRestrictedUser(bool ageRestrictedUser)
{
    [YMAYandexAds setAgeRestricted:ageRestrictedUser];
}

void YMAUnityShowDebugPanel(void)
{
    [YMAYandexAds showDebugPanel];
}

void YMAUnitySetAdMuted(bool muted)
{
    [YMAYandexAds.adVolumeController setMuted:muted];
}

void YMAUnitySetAdVolume(float volume)
{
    [YMAYandexAds.adVolumeController setVolume:volume];
}
