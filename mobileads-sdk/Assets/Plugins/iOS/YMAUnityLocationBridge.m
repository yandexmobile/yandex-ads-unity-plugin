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
#import "YMAUnityStringConverter.h"
#import "YMAUnityObjectIDProvider.h"

static const CLLocationAccuracy kYMAUnityLocationAccuracyInvalid = -1.0;

char *YMAUnityCreateLocation(double latitude, double longitude, double horizontalAccuracy)
{
    CLLocationCoordinate2D coordinate = CLLocationCoordinate2DMake(latitude, longitude);
    CLLocation *location = [[CLLocation alloc] initWithCoordinate:coordinate
                                                         altitude:0.0
                                               horizontalAccuracy:horizontalAccuracy
                                                 verticalAccuracy:kYMAUnityLocationAccuracyInvalid
                                                        timestamp:[NSDate date]];
    const char *objectID = [YMAUnityObjectIDProvider IDForObject:location];
    [[YMAUnityObjectsStorage sharedInstance] setObject:location withID:objectID];
    return [YMAUnityStringConverter copiedCString:objectID];
}
