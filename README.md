# Yandex Mobile Ads Unity Plugin

This repository contains Yandex Mobile Ads Unity plugin. This plugin allows Unity developers to easily integrate Yandex
Mobile Ads on Android and iOS apps.

## Documentation

Documentation could be found at the [official website][DOCUMENTATION]

## License

EULA is available at [EULA website][LICENSE]

## Quick start

1. To use Yandex Mobile Ads Unity plugin in your project download folder `mobileads-unity-plugin`

2. Open your project in the Unity editor

3. Select `Assets > Import Package > Custom Package` and find the `yandex-mobileads-lite-<version>.unitypackage` file.

4. Make sure all of the files are selected and click Import.

5. Add [Google resolver] to your project, if you haven't done it yet. Resolve dependencies.

6. You can use one of demo scripts in folder `samples` to test plugin. Just add one of this files to your project.

## Yandex Mobile Ads Mediation

Third-party networks can be connected to Yandex Mobile Ads Mediation by several steps:

1. Import `yandex-mobileads-lite-<version>.unitypackage` to your project

2. Import unity package of the desired third-party network

3. Add [Google resolver] to your project, if you haven't done it yet. Resolve dependencies.

4. Set up mediation according
   to [AdFox documentation](https://yandex.com/dev/mobile-ads/doc/plugins/unity/mob-mediation/list-network-docpage/)

## Third-party mediation

### AdMob

1. Integrate [AdMob](https://developers.google.com/admob/unity/start)

2. Import package `admob-mobileads-mediation-<version>.unitypackage` from folder `third-party-networks-mediation`

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
| yandex-mobileads-lite-6.0.1          | Main Yandex Mobile Ads package distributed for use with [Google resolver]    |
| yandex-mobileads-mediation-6.0.1     | Main Mobile Ads Mediation package distributed for use with [Google resolver] |
| mobileads-adcolony-mediation-6.0.0   | AdColony mediation (Supported only by android)                               |
| mobileads-google-mediation-6.0.0     | Admob mediation                                                              |
| mobileads-applovin-mediation-6.0.0   | AppLovin mediation                                                           |
| mobileads-chartboost-mediation-6.0.0 | Chartboost mediation (Supported only by android)                             |
| mobileads-inmobi-mediation-6.0.0     | Inmobi mediation                                                             |
| mobileads-ironsource-mediation-6.0.0 | IronSource mediation                                                         |
| mobileads-mintegral-mediation-6.0.0  | Mintegral mediation                                                          |
| mobileads-mytarget-mediation-6.0.0   | MyTarget mediation                                                           |
| mobileads-pangle-mediation-6.0.0     | Pangle mediation (Supported only by android)                                 |
| mobileads-startapp-mediation-6.0.0   | StartApp mediation (Supported only by android)                               |
| mobileads-tapjoy-mediation-6.0.0     | Tapjoy mediation (Supported only by android)                                 |
| mobileads-unityads-mediation-6.0.0   | UnityAds mediation                                                           |
| mobileads-vungle-mediation-6.0.0     | Vungle mediation (Supported only by android)                                 |
| admob-mobileads-mediation-6.0.1      | Third-party mediation with AdMob                                             |
| ironsource-mobileads-mediation-6.0.1 | Third-party mediation with IronSource                                        |

[Google resolver]: https://github.com/googlesamples/unity-jar-resolver

[DOCUMENTATION]: https://yandex.ru/support2/mobile-ads/ru/dev/unity

[LICENSE]: https://legal.yandex.com/partner_ch/
