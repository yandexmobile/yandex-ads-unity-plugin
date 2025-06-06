
#import "YMAUnityObjectIDProvider.h"

@implementation YMAUnityObjectIDProvider

+ (const char *)IDForObject:(id)object
{
    return [[NSString stringWithFormat:@"%p", (void *)object] UTF8String];
}

@end
