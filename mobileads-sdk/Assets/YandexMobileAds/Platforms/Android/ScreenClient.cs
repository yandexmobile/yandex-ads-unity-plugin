/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Unity (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using UnityEngine;
using YandexMobileAds.Common;

namespace YandexMobileAds.Platforms.Android
{
    public class ScreenClient : IScreenClient
    {
        private static ScreenClient _instance;

        private static readonly object _lockObject = new object();

        public static ScreenClient GetInstance()
        {
            if (_instance == null)
            {
                lock (_lockObject)
                {
                    if (_instance == null)
                        _instance = new ScreenClient();
                }
            }
            return _instance;
        }

        public float GetScreenScale()
        {
            var currentActivity = Utils.GetCurrentActivity();
            var resources = currentActivity.Call<AndroidJavaObject>("getResources");
            var metrics = resources.Call<AndroidJavaObject>("getDisplayMetrics");
            return metrics.Get<float>("density");
        }
    }
}
