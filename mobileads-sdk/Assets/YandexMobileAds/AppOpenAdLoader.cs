/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Unity (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;
using System.Threading.Tasks;
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
        private readonly AdRequestCreator _adRequestCreator;
        private readonly IAppOpenAdLoaderClient _client;

        /// <summary>
        /// Constructs an object of the <see cref="AppOpenAdLoader"/> class.
        /// </summary>
        public AppOpenAdLoader()
        {
            _adRequestCreator = new AdRequestCreator();
            _client = YandexMobileAdsClientFactory.BuildAppOpenAdLoaderClient();
            MainThreadDispatcher.initialize();
        }

        /// <summary>
        /// Starts loading the ad by <see cref="AdRequest"/>.
        /// Invokes <paramref name="onLoaded"/> on the main thread when the ad is ready,
        /// or <paramref name="onFailed"/> if loading fails.
        /// </summary>
        public void LoadAd(
            AdRequest adRequest,
            Action<AppOpenAd> onLoaded,
            Action<AdFailedToLoadEventArgs> onFailed)
        {
            EventHandler<GenericEventArgs<IAppOpenAdClient>> loadedHandler = null;
            EventHandler<AdFailedToLoadEventArgs> failedHandler = null;

            loadedHandler = (sender, args) =>
            {
                _client.OnAdLoaded -= loadedHandler;
                _client.OnAdFailedToLoad -= failedHandler;
                MainThreadDispatcher.EnqueueAction(() => onLoaded(new AppOpenAd(args.Value)));
            };
            failedHandler = (sender, args) =>
            {
                _client.OnAdLoaded -= loadedHandler;
                _client.OnAdFailedToLoad -= failedHandler;
                MainThreadDispatcher.EnqueueAction(() => onFailed(args));
            };

            _client.OnAdLoaded += loadedHandler;
            _client.OnAdFailedToLoad += failedHandler;
            _client.LoadAd(_adRequestCreator.CreateAdRequest(adRequest));
        }

        /// <summary>
        /// Starts loading the ad by <see cref="AdRequest"/>.
        /// Returns a <see cref="Task{AppOpenAd}"/> that completes on the main thread.
        /// Throws <see cref="AdLoadingException"/> if loading fails.
        /// </summary>
        public Task<AppOpenAd> LoadAd(AdRequest adRequest)
        {
            var tcs = new TaskCompletionSource<AppOpenAd>();
            LoadAd(adRequest,
                onLoaded: ad => tcs.TrySetResult(ad),
                onFailed: args => tcs.TrySetException(new AdLoadingException(args.Message, args.AdUnitId)));
            return tcs.Task;
        }

        /// <summary>
        /// Cancels active loading of app open ads.
        /// </summary>
        public void CancelLoading()
        {
            _client.CancelLoading();
        }
    }
}
