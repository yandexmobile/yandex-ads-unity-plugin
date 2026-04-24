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
    /// Contains targeting information used to fetch an ad.
    /// </summary>
    public class AdTargeting
    {
        /// <summary>
        /// The string representation of the user's age.
        /// </summary>
        public string Age { get; private set; }

        /// <summary>
        /// The string representation of the user's gender.
        /// See the list of values in <see cref="YandexMobileAds.Base.Gender"/>.
        /// </summary>
        public string Gender { get; private set; }

        /// <summary>
        /// User location.
        /// </summary>
        public Location Location { get; private set; }

        /// <summary>
        /// The search query that the user entered in the app.
        /// </summary>
        public string ContextQuery { get; private set; }

        /// <summary>
        /// A list of tags. Matches the context in which the ad will be displayed.
        /// </summary>
        public List<string> ContextTags { get; private set; }

        /// <summary>
        /// Creates targeting data.
        /// </summary>
        /// <param name="age">The string representation of the user's age.</param>
        /// <param name="gender">
        /// The string representation of the user's gender.
        /// See <see cref="YandexMobileAds.Base.Gender"/>.
        /// </param>
        /// <param name="location">User location.</param>
        /// <param name="contextQuery">The search query that the user entered in the app.</param>
        /// <param name="contextTags">
        /// A list of tags. Matches the context in which the ad will be displayed.
        /// </param>
        public AdTargeting(
            string age = null,
            string gender = null,
            Location location = null,
            string contextQuery = null,
            List<string> contextTags = null)
        {
            this.Age = age;
            this.Gender = gender;
            this.Location = location;
            this.ContextQuery = contextQuery;
            this.ContextTags = contextTags != null ? new List<string>(contextTags) : null;
        }
    }
}
