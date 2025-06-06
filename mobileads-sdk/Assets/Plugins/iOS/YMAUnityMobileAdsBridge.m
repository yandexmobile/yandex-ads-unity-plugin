/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2024 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

#import <YandexMobileAds/YandexMobileAds.h>

void YMAUnitySetUserConsent(bool consent)
{
    [YMAMobileAds setUserConsent:consent];
}

void YMAUnitySetLocationConsent(bool consent)
{
    [YMAMobileAds setLocationTrackingEnabled:consent];
}

void YMAUnitySetAgeRestrictedUser(bool ageRestrictedUser)
{
    [YMAMobileAds setAgeRestrictedUser:ageRestrictedUser];
}

void YMAUnityShowDebugPanel(void)
{
    [YMAMobileAds showDebugPanel];
}
