/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Android (C) 2018 YANDEX
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
    private readonly float ButtonWidth = 0.8f * Screen.width;
    private readonly float ButtonHeight = 0.1f * Screen.height;
    private readonly float ColumnPosition = 0.1f * Screen.width;
    private readonly float RowFirstPosition = 0.05f * Screen.height;
    private readonly float RowSecondPosition = 0.225f * Screen.height;
    private readonly float RowThirdPosition = 0.4f * Screen.height;
    private readonly int FontSize = (int) (0.035f * Screen.width);

    private Interstitial interstitial;

    void Start() {
        Screen.orientation = ScreenOrientation.Portrait;
    }
    
    public void OnGUI()
    {
        GUIStyle style = new GUIStyle();

        GUI.skin.button.fontSize = FontSize;

        Rect requestInterstitialRect = this.CreateButton(ColumnPosition, RowFirstPosition);
        if (GUI.Button(requestInterstitialRect, "Request\nInterstitial"))
        {
            this.RequestInterstitial();
        }

        Rect showInterstitialRect = this.CreateButton(ColumnPosition, RowSecondPosition);
        if (GUI.Button(showInterstitialRect, "Show\nInterstitial"))
        {
            this.ShowInterstitial();
        }

        Rect destroyInterstitialRect = this.CreateButton(ColumnPosition, RowThirdPosition);
        if (GUI.Button(destroyInterstitialRect, "Destroy\nInterstitial"))
        {
            this.interstitial.Destroy();
        }
    }

    private void RequestInterstitial()
    {
        // Replace demo R-M-DEMO-240x400-context with actual Block ID
        // Following demo Block IDs may be used for testing:
        // R-M-DEMO-240x400-context
        // R-M-DEMO-400x240-context
        // R-M-DEMO-320x480
        // R-M-DEMO-480x320
        string adUnitId = "R-M-DEMO-240x400-context";

        if (this.interstitial != null)
        {
            this.interstitial.Destroy();
        }

        this.interstitial = new Interstitial(adUnitId);

        this.interstitial.OnInterstitialLoaded += this.HandleInterstitialLoaded;
        this.interstitial.OnInterstitialFailedToLoad += this.HandleInterstitialFailedToLoad;
        this.interstitial.OnInterstitialOpened += this.HandleInterstitialOpened;
        this.interstitial.OnInterstitialClosed += this.HandleInterstitialClosed;
        this.interstitial.OnInterstitialLeftApplication += this.HandleInterstitialLeftApplication;
        this.interstitial.OnInterstitialShown += this.HandleInterstitialShown;
        this.interstitial.OnInterstitialFailedToShow += this.HandleInterstitialFailedToShow;
        this.interstitial.OnInterstitialDismissed += this.HandleInterstitialDismissed;

        this.interstitial.LoadAd(this.CreateAdRequest());
    }

    private Rect CreateButton(float positionX, float positionY)
    {
        return new Rect(
            positionX,
            positionY,
            ButtonWidth,
            ButtonHeight);
    }

    private void ShowInterstitial()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
        else
        {
            MonoBehaviour.print("Interstitial is not ready yet");
        }
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    #region Interstitial callback handlers

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialLoaded event received");
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailureEventArgs args)
    {
        MonoBehaviour.print(
            "HandleInterstitialFailedToLoad event received with message: " + args.Message);
    }

    public void HandleInterstitialOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialOpened event received");
    }

    public void HandleInterstitialClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialClosed event received");
    }

    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialLeftApplication event received");
    }

    public void HandleInterstitialShown(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialShown event received");
    }

    public void HandleInterstitialFailedToShow(object sender, AdFailureEventArgs args)
    {
        MonoBehaviour.print(
            "HandleInterstitialFailedToShow event received with message: " + args.Message);
    }

    public void HandleInterstitialDismissed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleInterstitialDismissed event received");
    }

    #endregion
}