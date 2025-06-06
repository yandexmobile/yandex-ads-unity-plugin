/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

#import "YMAUnityNumberFormatter.h"
#import "YMAUnityStringConverter.h"

@interface YMAUnityNumberFormatter ()

@property (nonatomic, strong, readonly) NSNumberFormatter *numberFormatter;

@end

@implementation YMAUnityNumberFormatter

- (instancetype)init
{
    self = [super init];
    if (self != nil) {
        _numberFormatter = [[NSNumberFormatter alloc] init];
        _numberFormatter.numberStyle = NSNumberFormatterDecimalStyle;
        _numberFormatter.decimalSeparator = @".";
    }
    return self;
}

- (NSNumber *)numberFromCString:(char *)cString
{
    NSString *string = [YMAUnityStringConverter NSStringFromCString:cString];
    return [self.numberFormatter numberFromString:string];
}

@end
