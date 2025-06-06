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

char *YMAUnityCreateList(void)
{
    NSMutableArray *array = [[NSMutableArray alloc] init];
    const char *objectID = [YMAUnityObjectIDProvider IDForObject:array];
    [[YMAUnityObjectsStorage sharedInstance] setObject:array withID:objectID];
    return [YMAUnityStringConverter copiedCString:objectID];
}

void YMAUnityAddToList(char *objectID, char *value)
{
    if (value == NULL) {
        return;
    }
    NSMutableArray *array = [[YMAUnityObjectsStorage sharedInstance] objectWithID:objectID];
    NSString *stringValue = [[NSString alloc] initWithUTF8String:value];
    [array addObject:stringValue];
}
