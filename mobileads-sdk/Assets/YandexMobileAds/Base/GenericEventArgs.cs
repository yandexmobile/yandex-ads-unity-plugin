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
    /// Represents a generic event args with a value.
    /// </summary>
    /// <typeparam name="T">type of value held by this <see cref="EventArgs"/></typeparam>
    internal class GenericEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Value, held by this <see cref="EventArgs"/>.
        /// </summary>
        public T Value { get; set; }
    }
}
