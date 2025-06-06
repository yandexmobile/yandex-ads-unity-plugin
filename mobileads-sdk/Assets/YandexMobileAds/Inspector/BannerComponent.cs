using UnityEngine;
using UnityEngine.Events;
using YandexMobileAds;
using YandexMobileAds.Base;
using System;

[AddComponentMenu("Yandex Ads/Banner Component")]
public class BannerComponent : MonoBehaviour
{
    public enum BannerType
    {
        Inline,
        Sticky
    }

    [Header("Banner Settings")]
    public string bannerName = "Default Banner";
    public BannerType bannerType = BannerType.Inline;

    [Header("Ad Unit IDs")]
    [Tooltip("Ad Unit ID for Android")]
    public string adUnitIdAndroid = "demo-banner-yandex";

    [Tooltip("Ad Unit ID for iOS")]
    public string adUnitIdiOS = "demo-banner-yandex";

    [Header("Banner Configuration")]
    public AdPosition bannerPosition = AdPosition.BottomCenter;
    public bool useScreenWidth = false;
    public int manualWidth = 320;
    public int bannerHeight = 50;
    public bool showAfterLoading = true;

    private Banner banner;

    [System.Serializable]
    public class AdEvent : UnityEvent { }

    [System.Serializable]
    public class AdEventString : UnityEvent<string> { }

    [System.Serializable]
    public class AdEventWithBanner : UnityEvent<Banner> { }

    [Header("Load Callbacks")]
    public AdEventWithBanner OnAdLoaded;
    public AdEventString OnAdFailedToLoad;

    [Header("Interaction Callbacks")]
    public AdEventWithBanner OnAdClicked;
    public AdEventWithBanner OnLeftApplication;
    public AdEventWithBanner OnReturnedToApplication;
    public AdEventString OnImpression;

    [Header("Configure Callbacks")]
    public AdEventWithBanner OnAdConfigured;
    public AdEventString OnAdConfigurationFailed;

    private string currentAdUnitId
    {
        get
        {
#if UNITY_IOS
            return adUnitIdiOS;
#elif UNITY_ANDROID
            return adUnitIdAndroid;
#else
            Debug.LogWarning("Unsupported platform for Yandex Ads. Using IOS Ad Unit ID by default.");
            return adUnitIdAndroid;
#endif
        }
    }

    private void Start()
    {
        if (!string.IsNullOrEmpty(currentAdUnitId))
        {
            ConfigureBanner();
            banner.LoadAd(new AdRequest.Builder().Build());
        }
        else
        {
            Debug.LogError("Banner configuration failed. Ad Unit ID is missing.");
        }
    }

    private void ConfigureBanner()
    {
        BannerAdSize adSize = bannerType == BannerType.Sticky
            ? BannerAdSize.StickySize(useScreenWidth ? ScreenUtils.ConvertPixelsToDp((int)Screen.safeArea.width) : manualWidth)
            : BannerAdSize.InlineSize(useScreenWidth ? ScreenUtils.ConvertPixelsToDp((int)Screen.safeArea.width) : manualWidth, bannerHeight);

        banner = new Banner(currentAdUnitId, adSize, bannerPosition);

        banner.OnAdLoaded += HandleAdLoaded;
        banner.OnAdFailedToLoad += HandleAdFailedToLoad;
        banner.OnAdClicked += HandleAdClicked;
        banner.OnLeftApplication += HandleLeftApplication;
        banner.OnReturnedToApplication += HandleReturnedToApplication;
        banner.OnImpression += HandleImpression;
    }

    public void OnDestroy()
    {
        banner.OnAdLoaded -= HandleAdLoaded;
        banner.OnAdFailedToLoad -= HandleAdFailedToLoad;
        banner.OnAdClicked -= HandleAdClicked;
        banner.OnLeftApplication -= HandleLeftApplication;
        banner.OnReturnedToApplication -= HandleReturnedToApplication;
        banner.OnImpression -= HandleImpression;

        banner.Destroy();
        banner = null;
    }

    #region Event Handlers
    private void HandleAdLoaded(object sender, EventArgs args)
    {
        OnAdLoaded?.Invoke(banner);
        if (showAfterLoading)
        {
            banner.Show();
        }
    }

    private void HandleAdFailedToLoad(object sender, AdFailureEventArgs args) => OnAdFailedToLoad?.Invoke(args.Message);
    private void HandleAdClicked(object sender, EventArgs args) => OnAdClicked?.Invoke(banner);
    private void HandleLeftApplication(object sender, EventArgs args) => OnLeftApplication?.Invoke(banner);
    private void HandleReturnedToApplication(object sender, EventArgs args) => OnReturnedToApplication?.Invoke(banner);
    private void HandleImpression(object sender, ImpressionData impressionData) =>
        OnImpression?.Invoke(impressionData?.rawData ?? string.Empty);
    #endregion
}