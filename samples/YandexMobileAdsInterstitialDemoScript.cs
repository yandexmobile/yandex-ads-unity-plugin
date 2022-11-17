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

public class YandexMobileAdsInterstitialDemoScript : MonoBehaviour
{
    private String message = "";

    private Interstitial interstitial;

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
            if (GUILayout.Button("Request Interstitial", buttonStyle, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height / 8)))
            {
                this.RequestInterstitial();
            }

            if (this.interstitial != null && this.interstitial.IsLoaded()) {
                if (GUILayout.Button("Show Interstitial", buttonStyle, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height / 8)))
                {
                    this.ShowInterstitial();
                }
            }

            if (GUILayout.Button("Destroy Interstitial", buttonStyle, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height / 8)))
            {
                this.interstitial.Destroy();
            }
        #endif

        GUILayout.Label(this.message, labelStyle);
    }

    private void RequestInterstitial()
    {
        //Sets COPPA restriction for user age under 13
        MobileAds.SetAgeRestrictedUser(true)

        // Replace demo R-M-DEMO-interstitial with actual Ad Unit ID
        string adUnitId = "R-M-DEMO-interstitial";

        if (this.interstitial != null)
        {
            this.interstitial.Destroy();
        }

        this.interstitial = new Interstitial(adUnitId);

        this.interstitial.OnInterstitialLoaded += this.HandleInterstitialLoaded;
        this.interstitial.OnInterstitialFailedToLoad += this.HandleInterstitialFailedToLoad;
        this.interstitial.OnReturnedToApplication += this.HandleReturnedToApplication;
        this.interstitial.OnLeftApplication += this.HandleLeftApplication;
        this.interstitial.OnAdClicked += this.HandleAdClicked;
        this.interstitial.OnInterstitialShown += this.HandleInterstitialShown;
        this.interstitial.OnInterstitialDismissed += this.HandleInterstitialDismissed;
        this.interstitial.OnImpression += this.HandleImpression;
        this.interstitial.OnInterstitialFailedToShow += this.HandleInterstitialFailedToShow;

        this.interstitial.LoadAd(this.CreateAdRequest());
        this.DisplayMessage("Interstitial is requested");
    }

    private void ShowInterstitial()
    {
        this.interstitial.Show();
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

    #region Interstitial callback handlers

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleInterstitialLoaded event received");
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailureEventArgs args)
    {
        this.DisplayMessage("HandleInterstitialFailedToLoad event received with message: " + args.Message);
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

    public void HandleInterstitialShown(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleInterstitialShown event received");
    }

    public void HandleInterstitialDismissed(object sender, EventArgs args)
    {
        this.DisplayMessage("HandleInterstitialDismissed event received");
    }

    public void HandleImpression(object sender, ImpressionData impressionData)
    {
        var data = impressionData == null ? "null" : impressionData.rawData;
        this.DisplayMessage("HandleImpression event received with data: " + data);
    }

    public void HandleInterstitialFailedToShow(object sender, AdFailureEventArgs args)
    {
        this.DisplayMessage("HandleInterstitialFailedToShow event received with message: " + args.Message);
    }

    #endregion
}
