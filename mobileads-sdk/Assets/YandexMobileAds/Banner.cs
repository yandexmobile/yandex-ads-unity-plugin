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
    /// A class for displaying banner ad view.
    /// </summary>
    public class Banner
    {
        /// <summary>
        /// Notifies that the banner is loaded. At this time, you can add banner if you haven't done so yet.
        /// </summary>
        public event EventHandler<EventArgs> OnAdLoaded;

        /// <summary>
        /// Notifies that the banner failed to load.
        /// </summary>
        public event EventHandler<AdFailureEventArgs> OnAdFailedToLoad;

        /// <summary>
        /// Notifies that the user has clicked on the banner.
        /// </summary>
        public event EventHandler<EventArgs> OnAdClicked;

        /// <summary>
        /// Notifies delegate when an impression was tracked.
        /// </summary>
        public event EventHandler<ImpressionData> OnImpression;

        private readonly IBannerClient _client;
        private readonly AdRequestCreator _adRequestFactory;

        /// <summary>
        /// Initializes an object of the Banner class to display the banner with the specified size.
        /// </summary>
        /// <param name="adSize">The size of banner ad. <see cref="YandexMobileAds.Base.BannerAdSize"/></param>
        /// <param name="position">Banner position on screen. <see cref="YandexMobileAds.Base.AdPosition"/></param>
        public Banner(BannerAdSize adSize, AdPosition position)
        {
            this._adRequestFactory = new AdRequestCreator();
            this._client = YandexMobileAdsClientFactory.BuildBannerClient(adSize, position);

            MainThreadDispatcher.initialize();
            ConfigureBannerEvents();
        }

        /// <summary>
        /// Loads Banner with data for targeting.
        /// </summary>
        /// <param name="request">Data for targeting.</param>
        public void LoadAd(AdRequest request)
        {
            _client.LoadAd(_adRequestFactory.CreateAdRequest(request));
        }

        /// <summary>
        /// Hides Banner from screen.
        /// </summary>
        public void Hide()
        {
            _client.Hide();
        }

        /// <summary>
        /// Shows Banner on screen.
        /// </summary>
        public void Show()
        {
            _client.Show();
        }

        /// <summary>
        /// Destroys Banner.
        /// </summary>
        public void Destroy()
        {
            _client.Destroy();
        }

        /// <summary>
        /// Returns ad information.
        /// </summary>
        /// <returns>An <see cref="YandexMobileAds.Base.AdInfo"/> instance with ad metadata, or null if no ad has been loaded.</returns>
        public AdInfo GetInfo()
        {
            return _client.GetInfo();
        }

        private void ConfigureBannerEvents()
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

                    this.OnAdLoaded(this, args);
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

            this._client.OnAdClicked += (sender, args) =>
            {
                if (this.OnAdClicked == null)
                {
                    return;
                }

                MainThreadDispatcher.EnqueueAction(() =>
                {
                    if (this.OnAdClicked == null)
                    {
                        return;
                    }

                    this.OnAdClicked(this, args);
                });
            };

            this._client.OnImpression += (sender, args) =>
            {
                if (this.OnImpression == null)
                {
                    return;
                }

                MainThreadDispatcher.EnqueueAction(() =>
                {
                    if (this.OnImpression == null)
                    {
                        return;
                    }

                    this.OnImpression(this, args);
                });
            };
        }
    }
}
