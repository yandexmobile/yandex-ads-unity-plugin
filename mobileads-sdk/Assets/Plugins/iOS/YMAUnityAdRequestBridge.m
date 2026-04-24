/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

#import <YandexMobileAds/YandexMobileAds-Swift.h>
#import <CoreLocation/CoreLocation.h>
#import "YMAUnityObjectsStorage.h"
#import "YMAUnityObjectIDProvider.h"
#import "YMAUnityStringConverter.h"
#import "YMAUnityNumberFormatter.h"

char *YMAUnityCreateAdRequest(char *adUnitID, char *locationID, char *contextQuery, char *contextTagsID, char *parametersID, char *age, char *gender)
{
    CLLocation *location = [[YMAUnityObjectsStorage sharedInstance] objectWithID:locationID];
    NSString *stringContextQuery = [YMAUnityStringConverter NSStringFromCString:contextQuery];
    NSArray *contextTags = [[YMAUnityObjectsStorage sharedInstance] objectWithID:contextTagsID];
    NSDictionary *parameters = [[YMAUnityObjectsStorage sharedInstance] objectWithID:parametersID];
    NSString *stringGender = [YMAUnityStringConverter NSStringFromCString:gender];
    YMAUnityNumberFormatter *numberFormatter = [[YMAUnityNumberFormatter alloc] init];
    NSNumber *numberAge = [numberFormatter numberFromCString:age];
    
    
    YMAAdTargeting *targeting = [[YMAAdTargeting alloc] initWithAge:numberAge gender:[[YMAGender alloc] init:stringGender] location:location contextQuery:stringContextQuery contextTags:contextTags];
    YMAAdRequest *adRequest = [[YMAAdRequest alloc] initWithAdUnitID: [YMAUnityStringConverter NSStringFromCString:adUnitID] targeting:targeting adTheme:YMAAdThemeUnspecified biddingData:nil headerBiddingData:nil parameters:parameters];

    const char *objectID = [YMAUnityObjectIDProvider IDForObject:adRequest];
    [[YMAUnityObjectsStorage sharedInstance] setObject:adRequest withID:objectID];
    return [YMAUnityStringConverter copiedCString:objectID];
}
