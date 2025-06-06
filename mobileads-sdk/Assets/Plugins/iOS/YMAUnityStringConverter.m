/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

#import "YMAUnityStringConverter.h"

@implementation YMAUnityStringConverter

+ (char *)copiedCStringFromObjCString:(NSString *)string
{
    const char *cString = [string UTF8String];
    return [self copiedCString:cString];
}

+ (char *)copiedCString:(const char *)string
{
    unsigned long len = 0;
    if (string != NULL) {
        len = strlen(string);
    }
    char *result = (char *)malloc(len + 1);
    if (string != NULL) {
        strcpy(result, string);
    }
    return result;
}

+ (NSString *)NSStringFromCString:(const char *)string
{
    NSString *result = nil;
    if (string != NULL) {
        result = [[NSString alloc] initWithUTF8String:string];
    }
    return result;
}

@end
