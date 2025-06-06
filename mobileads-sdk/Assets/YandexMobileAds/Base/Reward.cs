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
    /// Represents reward given to the user.
    /// </summary>
    public class Reward : EventArgs
    {
        /// <summary>
        /// Amount rewarded to the user
        /// </summary>
        public readonly int amount;

        /// <summary>
        /// Type of the reward.
        /// </summary>
        public readonly string type;

        public Reward(int amount, string type){
            this.amount = amount;
            this.type = type;
        }
    }
}
