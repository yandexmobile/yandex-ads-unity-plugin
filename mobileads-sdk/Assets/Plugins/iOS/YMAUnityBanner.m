/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for iOS (C) 2018-2019 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

#import <YandexMobileAds/YandexMobileAds.h>
#import "YMAUnityBanner.h"
#import "YMAUnityStringConverter.h"

@interface YMAUnityBanner() <YMAAdViewDelegate>

@property (nonatomic, assign, readonly) YMAUnityBannerClientRef *clientRef;
@property (nonatomic, strong, readonly) YMAAdView *adView;
@property (nonatomic, assign, readonly) YMAUnityAdPosition position;

@end

@implementation YMAUnityBanner

- (instancetype)initWithClientRef:(YMAUnityBannerClientRef *)clientRef
                         adUnitID:(char *)adUnitID
                           adSize:(YMABannerAdSize *)bannerAdSize
                         position:(YMAUnityAdPosition)position
{
    self = [super init];
    if (self != nil) {
        NSString *adUnitIDString = [[NSString alloc] initWithUTF8String:adUnitID];
        _adView = [[YMAAdView alloc] initWithAdUnitID:adUnitIDString adSize:bannerAdSize];
        _adView.delegate = self;
        _position = position;
        _clientRef = clientRef;
    }
    return self;
}

- (void)adViewDidLoad:(YMAAdView *)adView
{
    if (self.adReceivedCallback != NULL) {
        self.adReceivedCallback(self.clientRef);
    }
}

- (void)adViewDidFailLoading:(YMAAdView *)adView error:(NSError *)error
{
    if (self.loadingFailedCallback != NULL) {
        char *message = [YMAUnityStringConverter copiedCStringFromObjCString:error.localizedDescription];
        self.loadingFailedCallback(self.clientRef, message);
    }
}

- (void)adView:(YMAAdView *)adView willPresentScreen:(UIViewController *)viewController
{
    if (self.willPresentScreenCallback != NULL) {
        self.willPresentScreenCallback(self.clientRef);
    }
}

- (void)adView:(YMAAdView *)adView didDismissScreen:(UIViewController *)viewController
{
    if (self.didDismissScreenCallback != NULL) {
        self.didDismissScreenCallback(self.clientRef);
    }
}

- (void)adView:(YMAAdView *)adView didTrackImpressionWithData:(nullable id<YMAImpressionData>)impressionData
{
    if (self.didTrackImpressionCallback != NULL) {
        if (impressionData != nil) {
            char *rawData = [YMAUnityStringConverter copiedCStringFromObjCString:impressionData.rawData];
            self.didTrackImpressionCallback(self.clientRef, rawData);
        }
        else {
            self.didTrackImpressionCallback(self.clientRef, nil);
        }
    }
}

- (void)adViewWillLeaveApplication:(YMAAdView *)adView
{
    if (self.willLeaveApplicationCallback != NULL) {
        self.willLeaveApplicationCallback(self.clientRef);
    }
}

- (void)adViewDidClick:(YMAAdView *)adView
{
    if (self.didClickCallback != NULL) {
        self.didClickCallback(self.clientRef);
    }
}

- (void)loadAdWithRequest:(YMAAdRequest *)adRequest
{
    [self.adView loadAdWithRequest:adRequest];
}

- (void)show
{
    if (self.adView == nil || self.adView.superview != nil) {
        return;
    }
    UIViewController *viewController = [UIApplication sharedApplication].keyWindow.rootViewController;
    UIView *parentView = viewController.view;
    [parentView addSubview:self.adView];
    self.adView.translatesAutoresizingMaskIntoConstraints = NO;
    
    if (@available(iOS 11.0, *)) {
        [self showBannerInParentViewSafeArea:parentView];
    }
    else {
        [self showBannerInParentView:parentView];
    }
}

- (void)hide
{
    [self.adView removeFromSuperview];
}

#pragma mark - Private

- (void)showBannerInParentViewSafeArea:(UIView *)parentView NS_AVAILABLE_IOS(11_0)
{
    UIView *adView = self.adView;
    UILayoutGuide *layoutGuide = parentView.safeAreaLayoutGuide;
    NSArray *constraints = @[];
    NSLayoutConstraint *topConstraint = [adView.topAnchor constraintEqualToAnchor:layoutGuide.topAnchor];
    NSLayoutConstraint *centerYConstraint = [adView.centerYAnchor constraintEqualToAnchor:layoutGuide.centerYAnchor];
    NSLayoutConstraint *bottomConstraint = [adView.bottomAnchor constraintEqualToAnchor:layoutGuide.bottomAnchor];
    NSLayoutConstraint *leftConstraint = [adView.leftAnchor constraintEqualToAnchor:layoutGuide.leftAnchor];
    NSLayoutConstraint *centerXConstraint = [adView.centerXAnchor constraintEqualToAnchor:layoutGuide.centerXAnchor];
    NSLayoutConstraint *rightConstraint = [adView.rightAnchor constraintEqualToAnchor:layoutGuide.rightAnchor];
    switch (self.position) {
        case YMAUnityAdPositionTopLeft:
            constraints = @[ topConstraint, leftConstraint ];
            break;
        case YMAUnityAdPositionTopCenter:
            constraints = @[ topConstraint, centerXConstraint ];
            break;
        case YMAUnityAdPositionTopRight:
            constraints = @[ topConstraint, rightConstraint ];
            break;
        case YMAUnityAdPositionCenterLeft:
            constraints = @[ centerYConstraint, leftConstraint ];
            break;
        case YMAUnityAdPositionCenter:
            constraints = @[ centerYConstraint, centerXConstraint ];
            break;
        case YMAUnityAdPositionCenterRight:
            constraints = @[ centerYConstraint, rightConstraint ];
            break;
        case YMAUnityAdPositionBottomLeft:
            constraints = @[ bottomConstraint, leftConstraint ];
            break;
        case YMAUnityAdPositionBottomCenter:
            constraints = @[ bottomConstraint, centerXConstraint ];
            break;
        case YMAUnityAdPositionBottomRight:
            constraints = @[ bottomConstraint, rightConstraint ];
            break;
    }
    [NSLayoutConstraint activateConstraints:constraints];
}

- (void)showBannerInParentView:(UIView *)parentView
{
    NSArray *constraints = [self constraintsForAdView:self.adView parentView:parentView];
    [parentView addConstraints:constraints];
}

- (NSArray *)constraintsForAdView:(UIView *)adView
                       parentView:(UIView *)parent
{
    NSMutableArray *constraints = [NSMutableArray array];
    switch (self.position) {
        case YMAUnityAdPositionTopLeft:
            [constraints addObject:[self constraintForView:adView parent:parent attribute:NSLayoutAttributeLeft]];
            [constraints addObject:[self constraintForView:adView parent:parent attribute:NSLayoutAttributeTop]];
            break;
        case YMAUnityAdPositionTopCenter:
            [constraints addObject:[self constraintForView:adView parent:parent attribute:NSLayoutAttributeCenterX]];
            [constraints addObject:[self constraintForView:adView parent:parent attribute:NSLayoutAttributeTop]];
            break;
        case YMAUnityAdPositionTopRight:
            [constraints addObject:[self constraintForView:adView parent:parent attribute:NSLayoutAttributeRight]];
            [constraints addObject:[self constraintForView:adView parent:parent attribute:NSLayoutAttributeTop]];
            break;
        case YMAUnityAdPositionCenterLeft:
            [constraints addObject:[self constraintForView:adView parent:parent attribute:NSLayoutAttributeLeft]];
            [constraints addObject:[self constraintForView:adView parent:parent attribute:NSLayoutAttributeCenterY]];
            break;
        case YMAUnityAdPositionCenter:
            [constraints addObject:[self constraintForView:adView parent:parent attribute:NSLayoutAttributeCenterX]];
            [constraints addObject:[self constraintForView:adView parent:parent attribute:NSLayoutAttributeCenterY]];
            break;
        case YMAUnityAdPositionCenterRight:
            [constraints addObject:[self constraintForView:adView parent:parent attribute:NSLayoutAttributeRight]];
            [constraints addObject:[self constraintForView:adView parent:parent attribute:NSLayoutAttributeCenterY]];
            break;
        case YMAUnityAdPositionBottomLeft:
            [constraints addObject:[self constraintForView:adView parent:parent attribute:NSLayoutAttributeLeft]];
            [constraints addObject:[self constraintForView:adView parent:parent attribute:NSLayoutAttributeBottom]];
            break;
        case YMAUnityAdPositionBottomCenter:
            [constraints addObject:[self constraintForView:adView parent:parent attribute:NSLayoutAttributeCenterX]];
            [constraints addObject:[self constraintForView:adView parent:parent attribute:NSLayoutAttributeBottom]];
            break;
        case YMAUnityAdPositionBottomRight:
            [constraints addObject:[self constraintForView:adView parent:parent attribute:NSLayoutAttributeRight]];
            [constraints addObject:[self constraintForView:adView parent:parent attribute:NSLayoutAttributeBottom]];
            break;
    }
    return [constraints copy];
}

- (NSLayoutConstraint *)constraintForView:(UIView *)adView
                                   parent:(UIView *)parentView
                                attribute:(NSLayoutAttribute)attribute
{
    return [NSLayoutConstraint constraintWithItem:adView
                                        attribute:attribute
                                        relatedBy:NSLayoutRelationEqual
                                           toItem:parentView
                                        attribute:attribute
                                       multiplier:1.f
                                         constant:0.f];
}

@end

