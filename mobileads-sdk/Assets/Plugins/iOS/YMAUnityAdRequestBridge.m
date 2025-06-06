/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

#import <YandexMobileAds/YandexMobileAds.h>
#import <CoreLocation/CoreLocation.h>
#import "YMAUnityObjectsStorage.h"
#import "YMAUnityObjectIDProvider.h"
#import "YMAUnityStringConverter.h"
#import "YMAUnityNumberFormatter.h"

char *YMAUnityCreateAdRequest(char *locationID, char *contextQuery, char *contextTagsID, char *parametersID, char *age, char *gender)
{
    CLLocation *location = [[YMAUnityObjectsStorage sharedInstance] objectWithID:locationID];
    NSString *stringContextQuery = [YMAUnityStringConverter NSStringFromCString:contextQuery];
    NSArray *contextTags = [[YMAUnityObjectsStorage sharedInstance] objectWithID:contextTagsID];
    NSDictionary *parameters = [[YMAUnityObjectsStorage sharedInstance] objectWithID:parametersID];
    NSString *stringGender = [YMAUnityStringConverter NSStringFromCString:gender];
    YMAUnityNumberFormatter *numberFormatter = [[YMAUnityNumberFormatter alloc] init];
    NSNumber *numberAge = [numberFormatter numberFromCString:age];

    YMAMutableAdRequest *adRequest = [[YMAMutableAdRequest alloc] init];

    adRequest.location = location;
    adRequest.contextQuery = stringContextQuery;
    adRequest.contextTags = contextTags;
    adRequest.parameters = parameters;
    adRequest.age = numberAge;
    adRequest.gender = stringGender;
    const char *objectID = [YMAUnityObjectIDProvider IDForObject:adRequest];
    [[YMAUnityObjectsStorage sharedInstance] setObject:adRequest withID:objectID];
    return [YMAUnityStringConverter copiedCString:objectID];
}
