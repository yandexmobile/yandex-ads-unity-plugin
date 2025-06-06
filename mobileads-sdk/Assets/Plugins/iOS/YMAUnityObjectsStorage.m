/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

#import "YMAUnityObjectsStorage.h"

@interface YMAUnityObjectsStorage ()

@property (nonatomic, strong, readonly) NSMutableDictionary *storage;

@end

@implementation YMAUnityObjectsStorage

+ (instancetype)sharedInstance
{
    static dispatch_once_t token;
    static YMAUnityObjectsStorage *sharedInstance = nil;
    dispatch_once(&token, ^{
        sharedInstance = [[YMAUnityObjectsStorage alloc] init];
    });
    return sharedInstance;
}

- (instancetype)init
{
    self = [super init];
    if (self != nil) {
        _storage = [[NSMutableDictionary alloc] init];
    }
    return self;
}

- (NSString *)keyForObjectID:(const char *)objectID
{
    return [[NSString alloc] initWithUTF8String:objectID];
}

- (id)objectWithID:(const char *)objectID
{
    if (objectID == NULL) {
        return nil;
    }
    NSString *key = [self keyForObjectID:objectID];
    return self.storage[key];
}

- (void)setObject:(id)object withID:(const char *)objectID
{
    if (objectID == NULL) {
        return;
    }
    NSString *key = [self keyForObjectID:objectID];
    self.storage[key] = object;
}

- (void)removeObjectWithID:(const char *)objectID
{
    if (objectID == NULL) {
        return;
    }
    NSString *key = [self keyForObjectID:objectID];
    self.storage[key] = nil;
}

@end
