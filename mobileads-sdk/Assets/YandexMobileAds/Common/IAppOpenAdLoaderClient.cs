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
    internal interface IAppOpenAdLoaderClient
    {
        /// <summary>
        /// Event fired when an ad has been received and ready to show.
        /// </summary>
        event EventHandler<GenericEventArgs<IAppOpenAdClient>> OnAdLoaded;

        /// <summary>
        /// Event fired when an ad has failed to load.
        /// </summary>
        event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

        /// <summary>
        /// Starts loading the ad by the given
        ///<paramref name="configuration">configuration.</paramref>
        /// </summary>
        /// <param name="configuration">configuration of the ad request</param>
        void LoadAd(AdRequestConfiguration configuration);

        /// <summary>
        /// Cancels all loadings performed by this loader.
        /// </summary>
        void CancelLoading();
    }
}
