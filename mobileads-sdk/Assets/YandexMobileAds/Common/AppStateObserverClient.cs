/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Android (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;
using YandexMobileAds.Base;
using UnityEngine;

namespace YandexMobileAds.Common
{
    internal class AppStateObserverClient : MonoBehaviour, IAppStateObserverClient
    {
        private static IAppStateObserverClient instance;

        internal static IAppStateObserverClient Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject gameObject = new GameObject("AppStateObserverClient");
                    GameObject.DontDestroyOnLoad(gameObject);
                    instance = gameObject.AddComponent<AppStateObserverClient>();
                }
                return instance;
            }
        }

        public event EventHandler<AppStateChangedEventArgs> OnAppStateChanged = delegate { };

        public void Destroy()
        {
            OnAppStateChanged = null;
            GameObject gameObject = GameObject.Find("AppStateObserverClient");

            if (instance == null || gameObject == null)
            {
                return;
            }

            GameObject.Destroy(gameObject);
            instance = null;
        }

        void OnApplicationPause(bool isPaused)
        {
            AppStateChangedEventArgs args = new AppStateChangedEventArgs()
            {
                IsInBackground = isPaused
            };
            this.OnAppStateChanged(this, args);
        }
    }
}
