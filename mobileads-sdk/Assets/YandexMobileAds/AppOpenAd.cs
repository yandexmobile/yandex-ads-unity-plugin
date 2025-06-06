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

namespace YandexMobileAds
{
    /// <summary>
    /// Full-screen app open ad.
    /// </summary>
    public class AppOpenAd
    {
        private const string AdAlreadyPresentedErrorMessage =
                "App open ad was already presented. Single App open ad can be presented just once.";

        /// <summary>
        /// Notifies that an app open ad has been shown.
        /// </summary>
        public event EventHandler<EventArgs> OnAdShown;

        /// <summary>
        /// Notifies that an app open ad failed to show.
        /// </summary>
        public event EventHandler<AdFailureEventArgs> OnAdFailedToShow;

        /// <summary>
        /// Notifies that an app open ad has been dismissed.
        /// </summary>
        public event EventHandler<EventArgs> OnAdDismissed;

        /// <summary>
        /// Notifies that the user clicked on the ad.
        /// </summary>
        public event EventHandler<EventArgs> OnAdClicked;

        /// <summary>
        /// Notifies that an impression was observed.
        /// </summary>
        public event EventHandler<ImpressionData> OnAdImpression;

        private IAppOpenAdClient _client;
        private bool _adShown;

        internal AppOpenAd(IAppOpenAdClient client)
        {
            this._client = client;
            this._adShown = false;

            MainThreadDispatcher.initialize();
            ConfigureAppOpenAdEvents(this._client);
        }

        /// <summary>
        /// Shows the app open ad. Single app open ad can be showed just once.
        /// </summary>
        public void Show()
        {
            if (_client == null)
            {
                return;
            }

            if (_adShown == true)
            {
                AdFailureEventArgs args = new AdFailureEventArgs()
                {
                    Message = AdAlreadyPresentedErrorMessage
                };
                this.OnAdFailedToShow(this, args);

                return;
            }

            _adShown = true;
            _client.Show();
        }

        /// <summary>
        /// Destroys AppOpenAd entirely and cleans up resources.
        /// </summary>
        public void Destroy()
        {
            _adShown = true;
            if (_client != null)
            {
                _client.Destroy();
            }

            _client = null;

            this.OnAdShown = null;
            this.OnAdFailedToShow = null;
            this.OnAdImpression = null;
            this.OnAdClicked = null;
            this.OnAdDismissed = null;
        }

        private void ConfigureAppOpenAdEvents(IAppOpenAdClient client)
        {
            if (client == null)
            {
                return;
            }

            client.OnAdClicked += (sender, args) =>
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

            client.OnAdShown += (sender, args) =>
            {
                if (this.OnAdShown == null)
                {
                    return;
                }

                MainThreadDispatcher.EnqueueAction(() =>
                {
                    if (this.OnAdShown == null)
                    {
                        return;
                    }

                    this.OnAdShown(this, args);
                });
            };

            client.OnAdDismissed += (sender, args) =>
                {
                    if (this.OnAdDismissed == null)
                    {
                        return;
                    }

                    MainThreadDispatcher.EnqueueAction(() =>
                    {
                        if (this.OnAdDismissed == null)
                        {
                            return;
                        }

                        this.OnAdDismissed(this, args);
                    });
                };

            client.OnAdImpression += (sender, args) =>
                {
                    if (this.OnAdImpression == null)
                    {
                        return;
                    }

                    MainThreadDispatcher.EnqueueAction(() =>
                    {
                        if (this.OnAdImpression == null)
                        {
                            return;
                        }

                        this.OnAdImpression(this, args);
                    });
                };

            client.OnAdFailedToShow += (sender, args) =>
                {
                    if (this.OnAdFailedToShow == null)
                    {
                        return;
                    }

                    MainThreadDispatcher.EnqueueAction(() =>
                    {
                        if (this.OnAdFailedToShow == null)
                        {
                            return;
                        }

                        this.OnAdFailedToShow(this, args);
                    });
                };
        }
    }
}
