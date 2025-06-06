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
    internal interface IInterstitialClient
    {
        /// <summary>
        /// Event fired when the ad is clicked.
        /// </summary>
        event EventHandler<EventArgs> OnAdClicked;

        /// <summary>
        /// Event fired when the ad is shown.
        /// </summary>
        event EventHandler<EventArgs> OnAdShown;

        /// <summary>
        /// Event fired when the ad is dismissed.
        /// </summary>
        event EventHandler<EventArgs> OnAdDismissed;

        /// <summary>
        /// Event fired when the ad impression tracked.
        /// </summary>
        event EventHandler<ImpressionData> OnAdImpression;

        /// <summary>
        /// Event fired when the ad has failed to show.
        /// </summary>
        event EventHandler<AdFailureEventArgs> OnAdFailedToShow;

        /// <summary>
        /// Returns information about loaded ad.
        /// </summary>
        /// <returns>an <see cref="AdInfo"/> object</returns>
        AdInfo GetInfo();

        /// <summary>
        /// Shows the ad.
        /// </summary>
        void Show();

        /// <summary>
        /// Destroys the ad.
        /// </summary>
        void Destroy();
    }
}
