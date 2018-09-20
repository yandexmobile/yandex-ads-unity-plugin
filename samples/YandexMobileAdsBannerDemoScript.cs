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

public class YandexMobileAdsBannerDemoScript : MonoBehaviour
{
    private readonly float ButtonWidth = 0.8f * Screen.width;
    private readonly float ButtonHeight = 0.1f * Screen.height;
    private readonly float ColumnPosition = 0.1f * Screen.width;
    private readonly float RowFirstPosition = 0.05f * Screen.height;
    private readonly float RowSecondPosition = 0.225f * Screen.height;
    private readonly int FontSize = (int) (0.035f * Screen.width);

    private Banner banner;
    
    void Start() {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void OnGUI()
    {
        GUIStyle style = new GUIStyle();

        GUI.skin.button.fontSize = FontSize;

        Rect requestBannerRect = this.CreateButton(ColumnPosition, RowFirstPosition);
        if (GUI.Button(requestBannerRect, "Request\nBanner"))
        {
            this.RequestBanner();
        }

        Rect destroyBannerRect = this.CreateButton(ColumnPosition, RowSecondPosition);
        if (GUI.Button(destroyBannerRect, "Destroy\nBanner"))
        {
            this.banner.Destroy();
        }
    }

    private void RequestBanner()
    {
        // Replace demo R-M-DEMO-320x50 with actual Block ID
        // Following demo Block IDs may be used for testing:
        // R-M-DEMO-320x50
        // R-M-DEMO-320x50-app_install
        // R-M-DEMO-728x90
        // R-M-DEMO-320x100-context
        // R-M-DEMO-300x250
        // R-M-DEMO-300x250-context
        // R-M-DEMO-300x300-context
        string adUnitId = "R-M-DEMO-320x50";

        if (this.banner != null)
        {
            this.banner.Destroy();
        }

        this.banner = new Banner(adUnitId, AdSize.BANNER_320x50, AdPosition.BottomCenter);

        this.banner.OnAdLoaded += this.HandleAdLoaded;
        this.banner.OnAdFailedToLoad += this.HandleAdFailedToLoad;
        this.banner.OnAdOpened += this.HandleAdOpened;
        this.banner.OnAdClosed += this.HandleAdClosed;
        this.banner.OnAdLeftApplication += this.HandleAdLeftApplication;

        this.banner.LoadAd(this.CreateAdRequest());
    }

    private Rect CreateButton(float positionX, float positionY)
    {
        return new Rect(
            positionX,
            positionY,
            ButtonWidth,
            ButtonHeight);
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    #region Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
        this.banner.Show();
    }

    public void HandleAdFailedToLoad(object sender, AdFailureEventArgs args)
    {
        MonoBehaviour.print("HandleAdFailedToLoad event received with message: " + args.Message);
    }

    public void HandleAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleAdLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeftApplication event received");
    }

    #endregion
}