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
    /// Full-screen app open ads.
    /// </summary>
    public class AppOpenAdLoader
    {
        /// <summary>
        /// Notifies that ad is loaded.
        /// </summary>
        public event EventHandler<AppOpenAdLoadedEventArgs> OnAdLoaded;

        /// <summary>
        /// Notifies that an app open ad request failed.
        /// </summary>
        public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

        private readonly AdRequestConfigurationFactory _adRequestConfigurationFactory;
        private readonly IAppOpenAdLoaderClient _client;

        /// <summary>
        /// Cunstructs an object of the <see cref="AppOpenAdLoader"/> class.
        /// </summary>
        public AppOpenAdLoader()
        {
            this._adRequestConfigurationFactory = new AdRequestConfigurationFactory();
            this._client = YandexMobileAdsClientFactory.BuildAppOpenAdLoaderClient();

            MainThreadDispatcher.initialize();
            ConfigureAppOpenAdEvents();
        }

        /// <summary>
        /// Starts loading the ad by <see cref="AdRequestConfiguration"/>.
        /// A successfuly loaded <see cref="AppOpenAd"/> will be delivered via <see cref="OnAdLoaded"/> event,
        /// otherwise <see cref="OnAdFailedToLoad"/> event will be fired.
        /// </summary>
        public void LoadAd(AdRequestConfiguration configuration)
        {
            _client.LoadAd(_adRequestConfigurationFactory.CreateAdRequestConfiguration(configuration));
        }

        /// <summary>
        /// Cancel active loading of the rewarded ads.
        /// </summary>
        public void CancelLoading()
        {
            this._client.CancelLoading();
        }

        private void ConfigureAppOpenAdEvents()
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

                        AppOpenAdLoadedEventArgs adLoadedEventArgs = new AppOpenAdLoadedEventArgs()
                        {
                            AppOpenAd = new AppOpenAd(args.Value)
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
