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
    private String message = "";

    private RewardedAd rewardedAd;
    
    public void OnGUI()
    {
        var fontSize = (int) (0.05f * Math.Min(Screen.width, Screen.height));

        var labelStyle = GUI.skin.GetStyle("label");
        labelStyle.fontSize = fontSize;

        var buttonStyle = GUI.skin.GetStyle("button");
        buttonStyle.fontSize = fontSize;

        #if UNITY_EDITOR
            this.message = "Mobile ads SDK is not available in editor. Only Android and iOS environments are supported";
        #else
            if (GUILayout.Button("Request Rewarded Ad", buttonStyle, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height / 8)))
            {
                this.RequestRewardedAd();
            }

            if (this.rewardedAd != null && this.rewardedAd.IsLoaded()) {
                if (GUILayout.Button("Show Rewarded Ad", buttonStyle, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height / 8)))
                {
                    this.ShowRewardedAd();
                }
            }

            if (GUILayout.Button("Destroy Rewarded Ad", buttonStyle, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height / 8)))
            {
                this.rewardedAd.Destroy();
            }
        #endif

        GUILayout.Label(this.message, labelStyle);
    }

    private void RequestRewardedAd()
    {
        if (this.rewardedAd != null)
        {
            this.rewardedAd.Destroy();
        }
        
        // Replace demo R-M-DEMO-rewarded-client-side-rtb with actual Ad Unit ID
        string adUnitId = "R-M-DEMO-rewarded-client-side-rtb";
        this.rewardedAd = new RewardedAd(adUnitId);

        this.rewardedAd.OnRewardedAdLoaded += this.HandleRewardedAdLoaded;
        this.rewardedAd.OnRewardedAdFailedToLoad += this.HandleRewardedAdFailedToLoad;
        this.rewardedAd.OnReturnedToApplication += this.HandleReturnedToApplication;
        this.rewardedAd.OnLeftApplication += this.HandleLeftApplication;
        this.rewardedAd.OnAdClicked += this.HandleAdClicked;
        this.rewardedAd.OnRewardedAdShown += this.HandleRewardedAdShown;
        this.rewardedAd.OnRewardedAdDismissed += this.HandleRewardedAdDismissed;
        this.rewardedAd.OnImpression += this.HandleImpression;
        this.rewardedAd.OnRewarded += this.HandleRewarded;
        this.rewardedAd.OnRewardedAdFailedToShow += this.HandleRewardedAdFailedToShow;

        this.rewardedAd.LoadAd(this.CreateAdRequest());
        this.DisplayMessage("Rewarded Ad is requested");
    }

    private void ShowRewardedAd()
    {
        this.rewardedAd.Show();
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    private void DisplayMessage(String message)
    {
        this.message = message + (this.message.Length == 0 ? "" : "\n--------\n" + this.message);
        MonoBehaviour.print(message);
    }

    #region Rewarded Ad callback handlers

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailureEventArgs args)
    {
        this.DisplayMessage(
            "HandleRewardedAdFailedToLoad event received with message: " + args.Message);
    }

    public void HandleReturnedToApplication(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleReturnedToApplication event received");
    }

    public void HandleLeftApplication(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleLeftApplication event received");
    }

    public void HandleAdClicked(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleAdClicked event received");
    }

    public void HandleRewardedAdShown(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleRewardedAdShown event received");
    }

    public void HandleRewardedAdDismissed(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleRewardedAdDismissed event received");
    }

    public void HandleImpression(object sender, ImpressionData impressionData)
    {
        var data = impressionData == null ? "null" : impressionData.rawData;
        this.DisplayMessage("HandleImpression event received with data: " + data);
    }

    public void HandleRewarded(object sender, Reward args)
    {
        this.DisplayMessage("HandleRewarded event received: amout = " + args.amount + ", type = " + args.type);
    }

    public void HandleRewardedAdFailedToShow(object sender, AdFailureEventArgs args)
    {
        this.DisplayMessage(
            "HandleRewardedAdFailedToShow event received with message: " + args.Message);
    }

    #endregion
}