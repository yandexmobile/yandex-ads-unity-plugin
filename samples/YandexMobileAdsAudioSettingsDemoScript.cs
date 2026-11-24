/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Unity (C) 2026 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;
using UnityEngine;
using UnityEngine.UI;
using YandexMobileAds;

// Attach to a settings panel and assign a Toggle and a Slider with range [0, 1].
public class YandexMobileAdsAudioSettingsDemoScript : MonoBehaviour
{
    public Toggle muteToggle;
    public Slider volumeSlider;

    private void Start()
    {
        // These are proposed settings; opening the panel does not write to the SDK.
        muteToggle.SetIsOnWithoutNotify(false);
        volumeSlider.SetValueWithoutNotify(1f);
        muteToggle.onValueChanged.AddListener(OnMutedChanged);
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    private async void OnMutedChanged(bool muted)
    {
        try
        {
            await YandexAds.AdVolumeController.SetMutedAsync(muted);
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
        }
    }

    private async void OnVolumeChanged(float volume)
    {
        try
        {
            await YandexAds.AdVolumeController.SetVolumeAsync(volume);
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
        }
    }

    private void OnDestroy()
    {
        muteToggle.onValueChanged.RemoveListener(OnMutedChanged);
        volumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);
    }
}
