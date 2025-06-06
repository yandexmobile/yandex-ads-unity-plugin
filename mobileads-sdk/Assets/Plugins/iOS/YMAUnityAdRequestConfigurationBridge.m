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

char *YMAUnityCreateAdRequestConfiguration(char *adUnitID,
                                           char *locationID,
                                           char *contextQuery,
                                           char *contextTagsID,
                                           char *parametersID,
                                           char *age,
                                           char *gender)
{
    NSString *stringAdUnitID = [YMAUnityStringConverter NSStringFromCString:adUnitID];
    CLLocation *location = [[YMAUnityObjectsStorage sharedInstance] objectWithID:locationID];
    NSString *stringContextQuery = [YMAUnityStringConverter NSStringFromCString:contextQuery];
    NSArray *contextTags = [[YMAUnityObjectsStorage sharedInstance] objectWithID:contextTagsID];
    NSDictionary *parameters = [[YMAUnityObjectsStorage sharedInstance] objectWithID:parametersID];
    NSString *stringGender = [YMAUnityStringConverter NSStringFromCString:gender];
    YMAUnityNumberFormatter *numberFormatter = [[YMAUnityNumberFormatter alloc] init];
    NSNumber *numberAge = [numberFormatter numberFromCString:age];

    YMAMutableAdRequestConfiguration *adRequestConfiguration = [[YMAMutableAdRequestConfiguration alloc] initWithAdUnitID:stringAdUnitID];

    adRequestConfiguration.location = location;
    adRequestConfiguration.contextQuery = stringContextQuery;
    adRequestConfiguration.contextTags = contextTags;
    adRequestConfiguration.parameters = parameters;
    adRequestConfiguration.age = numberAge;
    adRequestConfiguration.gender = stringGender;

    const char *adRequestConfigurationObjectID = [YMAUnityObjectIDProvider IDForObject:adRequestConfiguration];
    [[YMAUnityObjectsStorage sharedInstance] setObject:adRequestConfiguration
                                                withID:adRequestConfigurationObjectID];
    return [YMAUnityStringConverter copiedCString:adRequestConfigurationObjectID];
}
