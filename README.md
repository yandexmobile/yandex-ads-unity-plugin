# Yandex Mobile Ads plugin for Unity

This repository contains Yandex Mobile Ads plugin for Unity. This plugin allows Unity developers to easily integrate Yandex
Mobile Ads in Android and iOS apps.

## Documentation

Documentation could be found at the [official website][DOCUMENTATION]

## License

EULA is available at [EULA website][LICENSE]

## Quick start

### Using OpenUPM CLI

To download Yandex Mobile Ads plugin for Unity to your project you can use the following command:
```
openupm add com.yandex.mobileads
```

### Using UPM

1. Open UPM settings by selecting Edit > Project Settings > Package Manager in the Unity menu

2. Add new scoped registry OpenUPM with URL `https://package.openupm.com` and scopes 'com.yandex', `com.google`

3. Open UPM by selecting Window -> Package Manager in the Unity menu

4. Go to My Registries and find `Yandex Mobile Ads plugin for Unity`

5. Click on Install. The package will be added to your project

### Using .unitypackage

1. To use Yandex Mobile Ads plugin for Unity in your project download the assets linked to the release ([Releases page](https://github.com/yandexmobile/yandex-ads-unity-plugin/releases))

2. Open your project in the Unity editor

3. Select `Assets > Import Package > Custom Package` and find the `yandex-mobileads-lite-<version>.unitypackage` file.

4. Make sure all of the files are selected and click Import.

5. Add [Google resolver] to your project, if you haven't done it yet. Resolve dependencies.

6. You can use one of demo scripts in folder `samples` to test plugin. Just add one of this files to your project.

## Yandex Mobile Ads Mediation

Third-party networks can be connected to Yandex Mobile Ads Mediation in several steps:

1. Import `yandex-mobileads-lite-<version>.unitypackage` to your project

2. Import unity package of the desired third-party network

3. Add [Google resolver] to your project, if you haven't done it yet. Resolve dependencies.

4. Set up mediation according
   to [AdFox documentation](https://yandex.com/dev/mobile-ads/doc/plugins/unity/mob-mediation/list-network-docpage/)

## Third-party mediation

### AdMob

1. Integrate [AdMob](https://developers.google.com/admob/unity/start)

2. Import package `admob-mobileads-mediation-<version>.unitypackage` from the release assets

3. Add [Google resolver] to your project, if you haven't done it yet. Resolve dependencies.

4. Get the Block ID in the Yandex Partner interface for each Ad Unit created in AdMob. Then set up mediation
   in [the AdMob web interface](https://apps.admob.com).

   For more information, please visit our:
    * [Android AdMob setup documentation](https://yandex.ru/support2/mobile-ads/en/dev/android/admob-third)
    * [iOS AdMob setup documentation](https://yandex.ru/support2/mobile-ads/en/dev/ios/admob-third)

### IronSource

1. Integrate [IronSource](https://developers.is.com/ironsource-mobile/unity/unity-plugin/)

2. Import package `ironsource-mobileads-mediation-<version>.unitypackage` from folder `third-party-networks-mediation`

3. Add [Google resolver] to your project, if you haven't done it yet. Resolve dependencies.

4. Get the Block ID in the Yandex Partner interface for each ad placement configured in Ironsource. Then set up
   mediation in [the Ironsource web interface](https://platform.ironsrc.com/partners/dashboard).

   For more information, please visit our:
    * [Android Ironsource setup documentation](https://yandex.com/support2/mobile-ads/en/dev/android/ironsource-third)
    * [iOS Ironsource setup documentation](https://yandex.com/support2/mobile-ads/en/dev/ios/ironsource-third)

## Unity packages

| Package                              | Description                                                                  |
|--------------------------------------|------------------------------------------------------------------------------|
| yandex-mobileads-lite-7.14.0         | Main Yandex Mobile Ads package distributed for use with [Google resolver]    |
| yandex-mobileads-mediation-7.14.0    | Main Mobile Ads Mediation package distributed for use with [Google resolver] |
| mobileads-google-mediation-7.14.0    | Admob mediation                                                              |
| mobileads-applovin-mediation-7.14.0  | AppLovin mediation (Supported only by android)                               |
| mobileads-chartboost-mediation-7.14.0| Chartboost mediation (Supported only by android)                             |
| mobileads-bigoads-mediation-7.14.0   | BigoAds mediation (Supported only by android)                                |
| mobileads-inmobi-mediation-7.14.0    | Inmobi mediation                                                             |
| mobileads-ironsource-mediation-7.14.0| IronSource mediation                                                         |
| mobileads-mintegral-mediation-7.14.0 | Mintegral mediation                                                          |
| mobileads-mytarget-mediation-7.14.0  | MyTarget mediation                                                           |
| mobileads-pangle-mediation-7.14.0    | Pangle mediation (Supported only by android)                                 |
| mobileads-startapp-mediation-7.14.0  | StartApp mediation (Supported only by android)                               |
| mobileads-tapjoy-mediation-7.14.0    | Tapjoy mediation (Supported only by android)                                 |
| mobileads-unityads-mediation-7.14.0  | UnityAds mediation                                                           |
| mobileads-vungle-mediation-7.14.0    | Vungle mediation                                                             |
| admob-mobileads-mediation-7.12.0     | Third-party mediation with AdMob                                       |

[Google resolver]: https://github.com/googlesamples/unity-jar-resolver

[DOCUMENTATION]: https://yandex.ru/support2/mobile-ads/ru/dev/unity

[LICENSE]: https://legal.yandex.com/partner_ch/
