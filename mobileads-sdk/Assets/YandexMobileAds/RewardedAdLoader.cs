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
using YandexMobileAds.Common;
using YandexMobileAds.Platforms;

namespace YandexMobileAds
{
    /// <summary>
    /// Full-screen rewarded ads.
    /// </summary>
    public class RewardedAdLoader
    {

        /// <summary>
        /// Notifies that ad is loaded.
        /// </summary>
        public event EventHandler<RewardedAdLoadedEventArgs> OnAdLoaded;

        /// <summary>
        /// Notifies that a rewarded ad request failed.
        /// </summary>
        public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

        private readonly AdRequestConfigurationFactory _adRequestConfigurationFactory;
        private readonly IRewardedAdLoaderClient _client;

        /// <summary>
        /// Cunstructs an object of the RewardedAdLoader class.
        /// </summary>
        public RewardedAdLoader()
        {
            this._adRequestConfigurationFactory = new AdRequestConfigurationFactory();
            this._client = YandexMobileAdsClientFactory.BuildRewardedAdLoaderClient();

            MainThreadDispatcher.initialize();
            ConfigureRewardedEvents();
        }

        /// <summary>
        /// Starts loading the ad by <see cref="AdRequestConfiguration"/>.
        /// A successfuly loaded <see cref="RewardedAd"/> will be delivered via <see cref="OnAdLoaded"/> event,
        /// otherwise <see cref="OnAdFailedToLoad"/> event will be fired.
        /// </summary>
        public void LoadAd(AdRequestConfiguration adRequestConfiguration)
        {
            _client.LoadAd(_adRequestConfigurationFactory.CreateAdRequestConfiguration(adRequestConfiguration));
        }

        /// <summary>
        /// Cancel active loading of the rewarded ads.
        /// </summary>
        public void CancelLoading()
        {
            _client.CancelLoading();
        }

        private void ConfigureRewardedEvents()
        {
            this._client.OnAdLoaded += (sender, args) =>
            {
                if (this.OnAdLoaded == null)
                {
                    return;
                }

                MainThreadDispatcher.EnqueueAction(() =>
                {
                    if (this.OnAdLoaded == null)
                    {
                        return;
                    }

                    RewardedAdLoadedEventArgs adLoadedEventArgs = new RewardedAdLoadedEventArgs()
                    {
                        RewardedAd = new RewardedAd(args.Value)
                    };
                    this.OnAdLoaded(this, adLoadedEventArgs);
                });
            };

            this._client.OnAdFailedToLoad += (sender, args) =>
            {
                if (this.OnAdFailedToLoad == null)
                {
                    return;
                }

                MainThreadDispatcher.EnqueueAction(() =>
                {
                    if (this.OnAdFailedToLoad == null)
                    {
                        return;
                    }

                    this.OnAdFailedToLoad(this, args);
                });
            };
        }
    }
}
