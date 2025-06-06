/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

#import <CoreLocation/CoreLocation.h>
#import "YMAUnityObjectsStorage.h"
#import "YMAUnityObjectIDProvider.h"
#import "YMAUnityStringConverter.h"

char *YMAUnityCreateDictionary(void)
{
    NSMutableDictionary *dictionary = [[NSMutableDictionary alloc] init];
    const char *objectID = [YMAUnityObjectIDProvider IDForObject:dictionary];
    [[YMAUnityObjectsStorage sharedInstance] setObject:dictionary withID:objectID];
    return [YMAUnityStringConverter copiedCString:objectID];
}

void YMAUnitySetDictionaryValue(char *objectID, char *key, char *value)
{
    if (key == NULL || value == NULL) {
        return;
    }
    NSMutableDictionary *dictionary = [[YMAUnityObjectsStorage sharedInstance] objectWithID:objectID];
    NSString *stringKey = [[NSString alloc] initWithUTF8String:key];
    NSString *stringValue = [[NSString alloc] initWithUTF8String:value];
    dictionary[stringKey] = stringValue;
}
