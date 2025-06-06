/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Unity (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;
using YandexMobileAds.Base;

namespace YandexMobileAds.Common
{
    internal interface IBannerClient
    {
        /// <summary>
        /// Event fired when banner has been received.
        /// </summary>
        event EventHandler<EventArgs> OnAdLoaded;

        /// <summary>
        /// Event fired when banner has failed to load.
        /// </summary>
        event EventHandler<AdFailureEventArgs> OnAdFailedToLoad;

        /// <summary>
        /// Event fired when returned to application.
        /// </summary>
        event EventHandler<EventArgs> OnReturnedToApplication;

        /// <summary>
        /// Event fired when banner is leaving application.
        /// </summary>
        event EventHandler<EventArgs> OnLeftApplication;

        /// <summary>
        /// Event fired when banner is clicked.
        /// </summary>
        event EventHandler<EventArgs> OnAdClicked;

        /// <summary>
        /// Event fired when impression was tracked.
        /// </summary>
        event EventHandler<ImpressionData> OnImpression;

        /// <summary>
        /// Requests new ad for banner view.
        /// </summary>
        /// <param name="request"></param>
        void LoadAd(AdRequest request);

        /// <summary>
        /// Shows banner view on screen.
        /// </summary>
        void Show();

        /// <summary>
        /// Hides banner view from screen.
        /// </summary>
        void Hide();

        /// <summary>
        /// Destroys banner view.
        /// </summary>
        void Destroy();
    }
}
