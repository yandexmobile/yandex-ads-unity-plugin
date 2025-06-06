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
using YandexMobileAds.Common;
using UnityEngine;

namespace YandexMobileAds.Platforms.Android
{
    internal class AppStateObserverClient : AndroidJavaProxy, IAppStateObserverClient
    {
        private const string UnityAppStateObserverClassName =
            "com.yandex.mobile.ads.unity.wrapper.AppStateChangedListener";

        private AndroidJavaObject _appStateObserver;
        private event EventHandler<AppStateChangedEventArgs> OnAppStateChangedInternal;

        public event EventHandler<AppStateChangedEventArgs> OnAppStateChanged
        {
            add
            {
                if (value == null)
                {
                    return;
                }
                if (OnAppStateChangedInternal == null && _appStateObserver != null)
                {
                    NativeApi.AttachToProcessLifecycle(_appStateObserver);
                }
                OnAppStateChangedInternal += value;
            }
            remove
            {
                OnAppStateChangedInternal -= value;
                if (OnAppStateChangedInternal == null && _appStateObserver != null)
                {
                    NativeApi.DetachFromProcessLifecycle(_appStateObserver);
                }
            }
        }

        public AppStateObserverClient() : base(UnityAppStateObserverClassName)
        {
            this._appStateObserver = NativeApi.NewInstace(this);
        }

        public void Destroy()
        {
            this.OnAppStateChangedInternal = null;

            if (_appStateObserver == null)
            {
                return;
            }

            NativeApi.DetachFromProcessLifecycle(_appStateObserver);
            _appStateObserver = null;
        }

#pragma warning disable IDE1006
        #region AppStateChangedListener implementation

        public void onAppStateChanged(bool background)
        {
            if (this.OnAppStateChangedInternal != null)
            {
                AppStateChangedEventArgs args = new AppStateChangedEventArgs()
                {
                    IsInBackground = background
                };
                this.OnAppStateChangedInternal(this, args);
            }
        }
        #endregion
#pragma warning restore IDE1006

        private static class NativeApi
        {
            private const string UnityAppStateObserverClassName = "com.yandex.mobile.ads.unity.wrapper.AppStateObserver";
            public static AndroidJavaObject NewInstace(object listener)
            {
                return new AndroidJavaObject(UnityAppStateObserverClassName, listener);
            }

            public static void AttachToProcessLifecycle(AndroidJavaObject unityAppStateObserver)
            {
                unityAppStateObserver.Call("attachToProcessLifecycle");
            }

            public static void DetachFromProcessLifecycle(AndroidJavaObject unityAppStateObserver)
            {
                unityAppStateObserver.Call("detachFromProcessLifecycle");
            }
        }
    }
}
