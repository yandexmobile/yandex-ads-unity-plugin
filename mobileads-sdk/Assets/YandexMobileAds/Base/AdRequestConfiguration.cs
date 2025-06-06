/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Unity (C) 2023 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace YandexMobileAds.Base
{
    /// <summary>
    /// A class with data for a targeted ad request.
    /// </summary>
    public class AdRequestConfiguration
    {
        /// <summary>
        /// The string representation of user's age.
        /// </summary>
        public string AdUnitId { get; private set; }

        /// <summary>
        /// The string representation of user's age.
        /// </summary>
        public string Age { get; private set; }

        /// <summary>
        /// The search query that the user entered in the app.
        /// </summary>
        public string ContextQuery { get; private set; }

        /// <summary>
        /// An array of tags.Matches the context in which the ad will be displayed.
        /// </summary>
        public List<string> ContextTags { get; private set; }

        /// <summary>
        /// The string representation of user's gender. See the list of values in Gender.
        /// </summary>
        public string Gender { get; private set; }

        /// <summary>
        /// User location.
        /// </summary>
        public Location Location { get; private set; }

        /// <summary>
        /// Preferred theme.
        /// </summary>
        public AdTheme AdTheme { get; private set; }

        /// <summary>
        /// A set of arbitrary input parameters.
        /// </summary>
        public Dictionary<string, string> Parameters { get; private set; }

        private AdRequestConfiguration(Builder builder)
        {
            this.AdUnitId = builder.AdUnitId;
            this.Age = builder.Age;
            this.ContextQuery = builder.ContextQuery;

            if (builder.ContextTags != null)
            {
                this.ContextTags = new List<string>(builder.ContextTags);
            }

            this.Gender = builder.Gender;
            this.Location = builder.Location;
            this.AdTheme = builder.AdTheme;

            if (builder.Parameters != null)
            {
                this.Parameters = new Dictionary<string, string>(builder.Parameters);
            }
        }

        /// <summary>
        /// A class responsible for creating AdRequest objects.
        /// </summary>
        public class Builder
        {

            internal string AdUnitId { get; private set; }

            internal string Age { get; private set; }

            internal string ContextQuery { get; private set; }

            internal List<string> ContextTags { get; private set; }

            internal string Gender { get; private set; }

            internal Location Location { get; private set; }

            internal AdTheme AdTheme { get; private set; }

            internal Dictionary<string, string> Parameters { get; private set; }

            /// <summary>
            /// Instantiates a Builder instance.
            /// <param name="adUnitId">unique identifier in R-M-XXXXXX-Y format</param>
            /// </summary>
            public Builder(string adUnitId)
            {
                this.AdUnitId = adUnitId;
            }

            /// <summary>
            /// AdRequest Builder initialized with user's Age for targeting process.
            /// </summary>
            /// <param name="age">The string representation of user's age.</param>
            /// <returns>this <see cref="AdRequestConfiguration.Builder"/></returns>
            public Builder WithAge(string age)
            {
                this.Age = age;
                return this;
            }

            /// <summary>
            /// AdRequest Builder initialized with current user query entered inside app.
            /// </summary>
            /// <param name="contextQuery">The search query that the user entered in the app.</param>
            /// <returns>this <see cref="AdRequestConfiguration.Builder"/></returns>
            public Builder WithContextQuery(string contextQuery)
            {
                this.ContextQuery = contextQuery;
                return this;
            }

            /// <summary>
            /// AdRequest Builder initialized with tags describing current user context inside app.
            /// </summary>
            /// <param name="contextTags">A list of tags.Matches the context in which the ad will be displayed.</param>
            /// <returns>this <see cref="AdRequestConfiguration.Builder"/>.</returns>
            public Builder WithContextTags(List<string> contextTags)
            {
                this.ContextTags = contextTags;
                return this;
            }

            /// <summary>
            /// AdRequest Builder initialized with user's Gender for targeting process.
            /// </summary>
            /// <param name="gender">The string representation of user's gender. See the list of values in Gender.</param>
            /// <returns>this <see cref="AdRequestConfiguration.Builder"/>.</returns>
            public Builder WithGender(string gender)
            {
                this.Gender = gender;
                return this;
            }

            /// <summary>
            /// AdRequest Builder initialized with user's Location for targeting process.
            /// </summary>
            /// <param name="location">User location.</param>
            /// <returns>this <see cref="AdRequestConfiguration.Builder"/>.</returns>
            public Builder WithLocation(Location location)
            {
                this.Location = location;
                return this;
            }

            /// <summary>
            /// Sets preferred theme.
            /// </summary>
            /// <param name="preferredTheme">preferred ad theme</param>
            /// <returns>this <see cref="AdRequestConfiguration.Builder"/> with preferred theme.</returns>
            public Builder WithPreferredTheme(AdTheme preferredTheme)
            {
                this.AdTheme = preferredTheme;
                return this;
            }

            /// <summary>
            /// AdRequest Builder initialized with custom Parameters.
            /// </summary>
            /// <param name="parameters">A set of arbitrary input parameters.</param>
            /// <returns>this <see cref="AdRequestConfiguration.Builder"/>.</returns>
            public Builder WithParameters(Dictionary<string, string> parameters)
            {
                this.Parameters = parameters;
                return this;
            }

            /// <summary>
            /// AdRequest Builder initialized with AdRequest
            /// </summary>
            /// <param name="adRequest">AdRequest.</param>
            /// <returns>this <see cref="AdRequestConfiguration.Builder"/>.</returns>
            public Builder WithAdRequestConfiguration(AdRequestConfiguration adRequestConfiguration)
            {
                if (adRequestConfiguration != null)
                {
                    this.ContextQuery = adRequestConfiguration.ContextQuery;
                    this.ContextTags = adRequestConfiguration.ContextTags;
                    this.Parameters = adRequestConfiguration.Parameters;
                    this.Location = adRequestConfiguration.Location;
                    this.Age = adRequestConfiguration.Age;
                    this.AdTheme = adRequestConfiguration.AdTheme;
                    this.Gender = adRequestConfiguration.Gender;
                }
                return this;
            }

            /// <summary>
            /// Creates AdRequest based on current builder parameters.
            /// </summary>
            /// <returns>new <see cref="AdRequestConfiguration"></returns>
            public AdRequestConfiguration Build()
            {
                if (this.Parameters == null)
                {
                    this.Parameters = new Dictionary<string, string>();
                }
                return new AdRequestConfiguration(this);
            }
        }
    }
}
