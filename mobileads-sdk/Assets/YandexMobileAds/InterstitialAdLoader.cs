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
    /// Full-screen interstitial ads.
    /// </summary>
    public class InterstitialAdLoader
    {
        private readonly AdRequestCreator _adRequestCreator;
        private readonly IInterstitialAdLoaderClient _client;

        /// <summary>
        /// Constructs an object of the <see cref="InterstitialAdLoader"/> class.
        /// </summary>
        public InterstitialAdLoader()
        {
            _adRequestCreator = new AdRequestCreator();
            _client = YandexMobileAdsClientFactory.BuildInterstitialAdLoaderClient();
            MainThreadDispatcher.initialize();
        }

        /// <summary>
        /// Starts loading the ad by <see cref="AdRequest"/>.
        /// Invokes <paramref name="onLoaded"/> on the main thread when the ad is ready,
        /// or <paramref name="onFailed"/> if loading fails.
        /// </summary>
        public void LoadAd(
            AdRequest adRequest,
            Action<Interstitial> onLoaded,
            Action<AdFailedToLoadEventArgs> onFailed)
        {
            EventHandler<GenericEventArgs<IInterstitialClient>> loadedHandler = null;
            EventHandler<AdFailedToLoadEventArgs> failedHandler = null;

            loadedHandler = (sender, args) =>
            {
                _client.OnAdLoaded -= loadedHandler;
                _client.OnAdFailedToLoad -= failedHandler;
                MainThreadDispatcher.EnqueueAction(() => onLoaded(new Interstitial(args.Value)));
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
        /// Returns a <see cref="Task{Interstitial}"/> that completes on the main thread.
        /// Throws <see cref="AdLoadingException"/> if loading fails.
        /// </summary>
        public Task<Interstitial> LoadAd(AdRequest adRequest)
        {
            var tcs = new TaskCompletionSource<Interstitial>();
            LoadAd(adRequest,
                onLoaded: ad => tcs.TrySetResult(ad),
                onFailed: args => tcs.TrySetException(new AdLoadingException(args.Message, args.AdUnitId)));
            return tcs.Task;
        }

        /// <summary>
        /// Cancels active loading of interstitial ads.
        /// </summary>
        public void CancelLoading()
        {
            _client.CancelLoading();
        }
    }
}
