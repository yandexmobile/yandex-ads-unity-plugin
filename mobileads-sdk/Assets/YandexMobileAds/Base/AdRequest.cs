/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Unity (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System.Collections.Generic;

namespace YandexMobileAds.Base
{
    /// <summary>
    /// Contains ad request data used to fetch an ad.
    /// </summary>
    public class AdRequest
    {
        /// <summary>
        /// The ad unit identifier.
        /// </summary>
        public string AdUnitId { get; private set; }

        /// <summary>
        /// Targeting information about the user.
        /// </summary>
        public AdTargeting Targeting { get; private set; }

        /// <summary>
        /// Preferred theme.
        /// </summary>
        public AdTheme AdTheme { get; private set; }

        /// <summary>
        /// A set of arbitrary input parameters.
        /// </summary>
        public Dictionary<string, string> Parameters { get; private set; }

        /// <summary>
        /// Creates an ad request.
        /// </summary>
        /// <param name="adUnitId">
        /// The ad unit identifier in the R-M-XXXXXX-Y format,
        /// assigned in the Partner interface.
        /// </param>
        /// <param name="targeting">Targeting information about the user.</param>
        /// <param name="adTheme">Preferred ad theme.</param>
        /// <param name="parameters">A set of arbitrary input parameters.</param>
        public AdRequest(
            string adUnitId,
            AdTargeting targeting = null,
            AdTheme adTheme = AdTheme.None,
            Dictionary<string, string> parameters = null)
        {
            this.AdUnitId = adUnitId;
            this.Targeting = targeting;
            this.AdTheme = adTheme;
            this.Parameters = parameters != null
                ? new Dictionary<string, string>(parameters)
                : null;
        }
    }
}
