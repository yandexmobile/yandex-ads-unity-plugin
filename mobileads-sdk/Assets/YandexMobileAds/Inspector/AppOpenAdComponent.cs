using UnityEngine;
using UnityEngine.Events;
using YandexMobileAds;
using YandexMobileAds.Base;
using System;

[AddComponentMenu("Yandex Ads/App Open Ad Component")]
public class AppOpenAdComponent : MonoBehaviour
{
    [Header("Ad Settings")]
    public string adName = "Default App Open Ad";

    [Header("Ad Unit IDs")]
    [Tooltip("Ad Unit ID for Android")]
    public string adUnitIdAndroid = "demo-appopenad-yandex";

    [Tooltip("Ad Unit ID for iOS")]
    public string adUnitIdiOS = "demo-appopenad-yandex";

    [Header("Configuration")]
    public bool autoLoading = true;
    public bool showAfterLoading = true;
    public bool showAppOpenAdOnce = true;

    private AppOpenAd appOpenAd;
    private AppOpenAdLoader appOpenAdLoader;
    private bool isColdStartAdShown = false;

    [System.Serializable]
    public class AdEventWithAd : UnityEvent<AppOpenAd> { }

    [System.Serializable]
    public class AppStateChangedEvent : UnityEvent<AppStateChangedEventArgs, AppOpenAd> { }

    [Header("Load Callbacks")]
    public AdEventWithAd OnAdLoaded;
    public UnityEvent<string> OnAdFailedToLoad;

    [Header("Interaction Callbacks")]
    public AdEventWithAd OnAdShown;
    public AdEventWithAd OnAdDismissed;
    public AdEventWithAd OnAdClicked;
    public UnityEvent<string> OnAdFailedToShow;
    public UnityEvent<string> OnImpression;
    public AppStateChangedEvent OnAppStateChange;

    private string CurrentAdUnitId
    {
        get
        {
#if UNITY_IOS
            return adUnitIdiOS;
#elif UNITY_ANDROID
            return adUnitIdAndroid;
#else
            Debug.LogWarning("Unsupported platform for Yandex Ads. Using Android Ad Unit ID by default.");
            return adUnitIdAndroid;
#endif
        }
    }

    private void Start()
    {
        if (!string.IsNullOrEmpty(CurrentAdUnitId))
        {
            AppStateObserver.OnAppStateChanged += HandleAppStateChanged;
            SetupLoader();
            if (autoLoading)
            {
                Load();
            }
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("App open ad: Ad Unit ID is missing.");
        }
    }

    private void SetupLoader()
    {
        appOpenAdLoader = new AppOpenAdLoader();
        appOpenAdLoader.OnAdLoaded += HandleAdLoaded;
        appOpenAdLoader.OnAdFailedToLoad += HandleAdFailedToLoad;
    }

    private void ConfigureAd()
    {
        AdRequestConfiguration adRequestConfiguration = new AdRequestConfiguration.Builder(CurrentAdUnitId).Build();
        try
        {
            appOpenAdLoader.LoadAd(adRequestConfiguration);
        }
        catch (Exception ex)
        {
            OnAdFailedToLoad?.Invoke($"Configuration failed: {ex.Message}");
        }
    }

    public void Load()
    {
        var isBlcokedForLoad = showAppOpenAdOnce && isColdStartAdShown;
        if (!isBlcokedForLoad)
        {
            ConfigureAd();
        }
    }

    public void Show()
    {
        if (appOpenAd != null)
        {

            if (showAppOpenAdOnce)
            {
                if (!isColdStartAdShown)
                {
                    appOpenAd.Show();
                    isColdStartAdShown = true;
                }
            }
            else
            {
                appOpenAd.Show();
            }
        }
        else
        {
            Debug.Log("Failed to show ad. Ad object not loaded");
        }
    }

    public void OnDestroy()
    {
        AppStateObserver.OnAppStateChanged -= HandleAppStateChanged;
        if (appOpenAd != null)
        {
            appOpenAd.OnAdShown -= HandleAdShown;
            appOpenAd.OnAdDismissed -= HandleAdDismissed;
            appOpenAd.OnAdClicked -= HandleAdClicked;
            appOpenAd.OnAdFailedToShow -= HandleAdFailedToShow;
            appOpenAd.OnAdImpression -= HandleImpression;
            appOpenAd.Destroy();
            appOpenAd = null;
        }
    }

    #region Event Handlers

    private void HandleAdLoaded(object sender, AppOpenAdLoadedEventArgs args)
    {
        appOpenAd = args.AppOpenAd;
        appOpenAd.OnAdShown += HandleAdShown;
        appOpenAd.OnAdDismissed += HandleAdDismissed;
        appOpenAd.OnAdClicked += HandleAdClicked;
        appOpenAd.OnAdFailedToShow += HandleAdFailedToShow;
        appOpenAd.OnAdImpression += HandleImpression;
        OnAdLoaded?.Invoke(appOpenAd);
        if (showAfterLoading)
        {
            if (showAppOpenAdOnce)
            {
                if (!isColdStartAdShown)
                {
                    appOpenAd.Show();
                    isColdStartAdShown = true;
                }
            }
            else
            {
                appOpenAd.Show();
            }
        }
    }

    private void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) => OnAdFailedToLoad?.Invoke(args.Message);

    private void HandleAdShown(object sender, EventArgs args) => OnAdShown?.Invoke(appOpenAd);

    private void HandleAdDismissed(object sender, EventArgs args) => OnAdDismissed?.Invoke(appOpenAd);

    private void HandleAdClicked(object sender, EventArgs args) => OnAdClicked?.Invoke(appOpenAd);

    private void HandleAdFailedToShow(object sender, AdFailureEventArgs args) => OnAdFailedToShow?.Invoke(args.Message);

    private void HandleImpression(object sender, ImpressionData impressionData) => OnImpression?.Invoke(impressionData?.rawData ?? string.Empty);

    private void HandleAppStateChanged(object sender, AppStateChangedEventArgs args) => OnAppStateChange?.Invoke(args, appOpenAd);

    #endregion
}