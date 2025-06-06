using UnityEngine;
using UnityEngine.Events;
using YandexMobileAds;
using YandexMobileAds.Base;
using System;

[AddComponentMenu("Yandex Ads/Rewarded Ad Component")]
public class RewardedAdComponent : MonoBehaviour
{
    [Header("Ad Settings")]
    public string adName = "Default Rewarded Ad";

    [Header("Ad Unit IDs")]
    [Tooltip("Ad Unit ID for Android")]
    public string adUnitIdAndroid = "demo-rewarded-yandex";

    [Tooltip("Ad Unit ID for iOS")]
    public string adUnitIdiOS = "demo-rewarded-yandex";

    [Header("Configuration")]
    public bool autoLoading = true;
    public bool showAfterLoading = true;

    private RewardedAd rewardedAd;
    private RewardedAdLoader rewardedAdLoader;

    [System.Serializable]
    public class AdEvent : UnityEvent { }

    [System.Serializable]
    public class AdEventString : UnityEvent<string> { }

    [System.Serializable]
    public class AdEventWithAd : UnityEvent<RewardedAd> { }

    [System.Serializable]
    public class AdEventRewared : UnityEvent<Reward> { }

    [Header("Load Callbacks")]
    public AdEventWithAd OnAdLoaded;
    public AdEventString OnAdFailedToLoad;

    [Header("Interaction Callbacks")]
    public AdEventWithAd OnAdShown;
    public AdEventWithAd OnAdDismissed;
    public AdEventWithAd OnAdClicked;
    public AdEventString  OnAdFailedToShow;
    public AdEventString OnImpression;
    public AdEventRewared OnRewarded;

    private string CurrentAdUnitId
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
            Debug.Log("RewardedAdComponent: Ad Unit ID is missing.");
        }
    }

    private void SetupLoader()
    {
        rewardedAdLoader = new RewardedAdLoader();
        rewardedAdLoader.OnAdLoaded += HandleAdLoaded;
        rewardedAdLoader.OnAdFailedToLoad += HandleAdFailedToLoad;
    }

    private void ConfigureAd()
    {
        AdRequestConfiguration adRequestConfiguration = new AdRequestConfiguration.Builder(CurrentAdUnitId).Build();

        try
        {
            rewardedAdLoader.LoadAd(adRequestConfiguration);
        }
        catch (Exception ex)
        {
            Debug.LogError($"{adName}: Configuration failed: {ex.Message}");
        }
    }

    public void Load()
    {
        ConfigureAd();
        
    }

    public void Show()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Show();
        }
        else
        {
            Debug.Log("Failed to show ad. Ad object not loaded");
        }
    }

    public void OnDestroy()
    {
        if (rewardedAd != null)
        {
            rewardedAd.OnAdShown -= HandleAdShown;
            rewardedAd.OnAdDismissed -= HandleAdDismissed;
            rewardedAd.OnAdClicked -= HandleAdClicked;
            rewardedAd.OnAdFailedToShow -= HandleAdFailedToShow;
            rewardedAd.OnAdImpression -= HandleImpression;
            rewardedAd.OnRewarded-= HandleRewarded;
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        if (rewardedAdLoader != null)
        {
            rewardedAdLoader.OnAdLoaded -= HandleAdLoaded;
            rewardedAdLoader.OnAdFailedToLoad -= HandleAdFailedToLoad;
            rewardedAdLoader = null;
        }
    }

    #region Event Handlers

    private void HandleAdLoaded(object sender, RewardedAdLoadedEventArgs args)
    {
        rewardedAd = args.RewardedAd;

        rewardedAd.OnAdShown += HandleAdShown;
        rewardedAd.OnAdDismissed += HandleAdDismissed;
        rewardedAd.OnAdClicked += HandleAdClicked;
        rewardedAd.OnAdFailedToShow += HandleAdFailedToShow;
        rewardedAd.OnAdImpression += HandleImpression;
        rewardedAd.OnRewarded += HandleRewarded;

        OnAdLoaded?.Invoke(rewardedAd);

        if (showAfterLoading)
        {
            rewardedAd.Show();
        }
    }

    private void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) => OnAdFailedToLoad?.Invoke(args.Message);

    private void HandleAdShown(object sender, EventArgs args) => OnAdShown?.Invoke(rewardedAd);

    private void HandleAdDismissed(object sender, EventArgs args) => OnAdDismissed?.Invoke(rewardedAd);

    private void HandleAdClicked(object sender, EventArgs args) => OnAdClicked?.Invoke(rewardedAd);

    private void HandleAdFailedToShow(object sender, AdFailureEventArgs args) => OnAdFailedToShow?.Invoke(args.Message);

    private void HandleImpression(object sender, ImpressionData impressionData) =>
        OnImpression?.Invoke(impressionData?.rawData ?? string.Empty);

    private void HandleRewarded(object sender, Reward args) => OnRewarded?.Invoke(args);

    #endregion
}