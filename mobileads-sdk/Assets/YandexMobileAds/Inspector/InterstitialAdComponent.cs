using UnityEngine;
using UnityEngine.Events;
using YandexMobileAds;
using YandexMobileAds.Base;
using System;

[AddComponentMenu("Yandex Ads/Interstitial Ad Component")]
public class InterstitialAdComponent : MonoBehaviour
{
    [Header("Ad Settings")]
    public string adName = "Default Interstitial Ad";

    [Header("Ad Unit IDs")]
    [Tooltip("Ad Unit ID for Android")]
    public string adUnitIdAndroid = "demo-interstitial-yandex";

    [Tooltip("Ad Unit ID for iOS")]
    public string adUnitIdiOS = "demo-interstitial-yandex";

    [Header("Configuration")]
    public bool autoLoading = true;
    public bool showAfterLoading = true;

    private Interstitial interstitial;
    private InterstitialAdLoader interstitialAdLoader;

    [System.Serializable]
    public class AdEvent : UnityEvent { }

    [System.Serializable]
    public class AdEventString : UnityEvent<string> { }

    [System.Serializable]
    public class AdEventWithAd : UnityEvent<Interstitial> { }

    [Header("Load Callbacks")]
    public AdEventWithAd OnAdLoaded;
    public AdEventString OnAdFailedToLoad;

    [Header("Interaction Callbacks")]
    public AdEventWithAd OnAdShown;
    public AdEventWithAd OnAdDismissed;
    public AdEventWithAd OnAdClicked;
    public AdEventString OnAdFailedToShow;
    public AdEventString OnImpression;

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
            SetupLoader();
            if (autoLoading)
            {
                ConfigureAd();
            }
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("InterstitialAd: Ad Unit ID is missing.");
        }
    }

    private void SetupLoader()
    {
        interstitialAdLoader = new InterstitialAdLoader();
        interstitialAdLoader.OnAdLoaded += HandleAdLoaded;
        interstitialAdLoader.OnAdFailedToLoad += HandleAdFailedToLoad;
    }

    private void ConfigureAd()
    {
        AdRequestConfiguration adRequestConfiguration = new AdRequestConfiguration.Builder(CurrentAdUnitId).Build();

        try
        {
            interstitialAdLoader.LoadAd(adRequestConfiguration);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Configuration failed: {ex.Message}");
        }
    }

    public void Load()
    {
        ConfigureAd();
        
    }

    public void Show()
    {
        if (interstitial != null)
        {
            interstitial.Show();
        }
        else
        {
            Debug.Log("Failed to show ad. Ad object not loaded");
        }
    }

    public void OnDestroy()
    {
        if (interstitial != null)
        {
            interstitial.OnAdShown -= HandleAdShown;
            interstitial.OnAdDismissed -= HandleAdDismissed;
            interstitial.OnAdClicked -= HandleAdClicked;
            interstitial.OnAdFailedToShow -= HandleAdFailedToShow;
            interstitial.OnAdImpression -= HandleImpression;
            interstitial.Destroy();
            interstitial = null;
        }

        if (interstitialAdLoader != null)
        {
            interstitialAdLoader.OnAdLoaded -= HandleAdLoaded;
            interstitialAdLoader.OnAdFailedToLoad -= HandleAdFailedToLoad;
            interstitialAdLoader = null;
        }
    }

    #region Event Handlers

    private void HandleAdLoaded(object sender, InterstitialAdLoadedEventArgs args)
    {
        interstitial = args.Interstitial;

        interstitial.OnAdShown += HandleAdShown;
        interstitial.OnAdDismissed += HandleAdDismissed;
        interstitial.OnAdClicked += HandleAdClicked;
        interstitial.OnAdFailedToShow += HandleAdFailedToShow;
        interstitial.OnAdImpression += HandleImpression;

        OnAdLoaded?.Invoke(interstitial);

        if (showAfterLoading)
        {
            interstitial.Show();
        }
    }

    private void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) => OnAdFailedToLoad?.Invoke(args.Message);

    private void HandleAdShown(object sender, EventArgs args) => OnAdShown?.Invoke(interstitial);

    private void HandleAdDismissed(object sender, EventArgs args) => OnAdDismissed?.Invoke(interstitial);

    private void HandleAdClicked(object sender, EventArgs args) => OnAdClicked?.Invoke(interstitial);

    private void HandleAdFailedToShow(object sender, AdFailureEventArgs args) => OnAdFailedToShow?.Invoke(args.Message);

    private void HandleImpression(object sender, ImpressionData impressionData) =>
        OnImpression?.Invoke(impressionData?.rawData ?? string.Empty);

    #endregion
}
