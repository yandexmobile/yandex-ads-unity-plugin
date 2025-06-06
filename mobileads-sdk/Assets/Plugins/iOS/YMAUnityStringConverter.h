#pragma once

/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

#import <Foundation/Foundation.h>

@interface YMAUnityStringConverter: NSObject

+ (char*)copiedCStringFromObjCString:(NSString*)string;
+ (char*)copiedCString:(const char*)string;
+ (NSString*)NSStringFromCString:(const char*)string;

@end
