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
    /// Current user location
    /// </summary>
    public class Location
    {
        public double Latitude { get; private set; }

        public double Longitude { get; private set; }

        public double HorizontalAccuracy { get; private set; }

        private Location(Builder builder)
        {
            this.Latitude = builder.Latitude;
            this.Longitude = builder.Longitude;
            this.HorizontalAccuracy = builder.HorizontalAccuracy;
        }

        public class Builder
        {
            internal double Latitude { get; private set; }

            internal double Longitude { get; private set; }

            internal double HorizontalAccuracy { get; private set; }

            public Builder SetLatitude(double latitude)
            {
                this.Latitude = latitude;
                return this;
            }

            public Builder SetLongitude(double longitude)
            {
                this.Longitude = longitude;
                return this;
            }

            public Builder SetHorizontalAccuracy(double horizontalAccuracy)
            {
                this.HorizontalAccuracy = horizontalAccuracy;
                return this;
            }

            public Location Build()
            {
                return new Location(this);
            }
        }
    }
}
