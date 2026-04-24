/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

#import "YMAMainThreadExecutor.h"

@implementation YMAMainThreadExecutor

+ (void)execute:(dispatch_block_t)block
{
    if (NSThread.isMainThread) {
        block();
    } else {
        dispatch_sync(dispatch_get_main_queue(), block);
    }
}

+ (void)executeAsync:(dispatch_block_t)block
{
    dispatch_async(dispatch_get_main_queue(), block);
}

@end
