/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Android (C) 2019 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;

public class YandexMobileAdsRewardedAdDemoScript : MonoBehaviour
{
    private readonly float ButtonWidth = 0.8f * Screen.width;
    private readonly float ButtonHeight = 0.1f * Screen.height;
    private readonly float ColumnPosition = 0.1f * Screen.width;
    private readonly float RowFirstPosition = 0.05f * Screen.height;
    private readonly float RowSecondPosition = 0.225f * Screen.height;
    private readonly float RowThirdPosition = 0.4f * Screen.height;
    private readonly int FontSize = (int) (0.035f * Screen.width);

    private RewardedAd rewardedAd;
    
    public void OnGUI()
    {
        GUIStyle style = new GUIStyle();

        GUI.skin.button.fontSize = FontSize;

        Rect requestRewardedAdRect = this.CreateButton(ColumnPosition, RowFirstPosition);
        if (GUI.Button(requestRewardedAdRect, "Request\nRewarded Ad"))
        {
            this.RequestRewardedAd();
        }

        Rect showRewardedAdRect = this.CreateButton(ColumnPosition, RowSecondPosition);
        if (GUI.Button(showRewardedAdRect, "Show\nRewarded Ad"))
        {
            this.ShowRewardedAd();
        }

        Rect destroyRewardedAdRect = this.CreateButton(ColumnPosition, RowThirdPosition);
        if (GUI.Button(destroyRewardedAdRect, "Destroy\nRewarded Ad"))
        {
            this.rewardedAd.Destroy();
        }
    }

    private void RequestRewardedAd()
    {
        if (this.rewardedAd != null)
        {
            this.rewardedAd.Destroy();
        }
        
        // Replace demo R-M-DEMO-rewarded-client-side-rtb with actual Block ID
        string adUnitId = "R-M-DEMO-rewarded-client-side-rtb";
        this.rewardedAd = new RewardedAd(adUnitId);

        this.rewardedAd.OnRewardedAdLoaded += this.HandleRewardedAdLoaded;
        this.rewardedAd.OnRewardedAdFailedToLoad += this.HandleRewardedAdFailedToLoad;
        this.rewardedAd.OnRewardedAdOpened += this.HandleRewardedAdOpened;
        this.rewardedAd.OnRewardedAdClosed += this.HandleRewardedAdClosed;
        this.rewardedAd.OnRewardedAdLeftApplication += this.HandleRewardedAdLeftApplication;
        this.rewardedAd.OnRewardedAdShown += this.HandleRewardedAdShown;
        this.rewardedAd.OnRewardedAdFailedToShow += this.HandleRewardedAdFailedToShow;
        this.rewardedAd.OnRewardedAdDismissed += this.HandleRewardedAdDismissed;
        this.rewardedAd.OnRewarded += this.HandleOnRewarded;

        this.rewardedAd.LoadAd(this.CreateAdRequest());
    }

    private Rect CreateButton(float positionX, float positionY)
    {
        return new Rect(
            positionX,
            positionY,
            ButtonWidth,
            ButtonHeight);
    }

    private void ShowRewardedAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
        else
        {
            MonoBehaviour.print("Rewarded Ad is not ready yet");
        }
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    #region Rewarded Ad callback handlers

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailureEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: " + args.Message);
    }

    public void HandleRewardedAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpened event received");
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
    }

    public void HandleRewardedAdLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLeftApplication event received");
    }

    public void HandleRewardedAdShown(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdShown event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdFailureEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: " + args.Message);
    }

    public void HandleRewardedAdDismissed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdDismissed event received");
    }

    public void HandleOnRewarded(object sender, Reward args)
    {
        MonoBehaviour.print("HandleOnRewarded event received: amout = " + args.amount + ", type = " + args.type);
    }

    #endregion
}