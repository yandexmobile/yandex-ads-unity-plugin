/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Unity (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;

namespace YandexMobileAds.Base
{
    /// <summary>
    /// Represents an event, that occurs when the state of the app has changed.
    /// </summary>
    public class AppStateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Is the app in the background.
        /// </summary>
        public bool IsInBackground { get; set; }
    }
}
