# Yandex Mobile Ads Unity Plugin

This repository contains Yandex Mobile Ads Unity plugin. This plugin allows Unity developers to easily integrate Yandex Mobile Ads on Android and iOS apps.

## Documentation
Documentation could be found at the [official website][DOCUMENTATION]

## License
EULA is available at [EULA website][LICENSE]

## Quick start

1. To use Yandex Mobile Ads Unity plugin in your project download folder `mobileads-unity-plugin`

2. Open your project in the Unity editor

3. Select `Assets > Import Package > Custom Package` and find the `yandex-mobileads-<version>.unitypackage` file.

   If you are already using [Google resolver] just use `yandex-mobileads-lite-<version>.unitypackage`

4. Make sure all of the files are selected and click Import.

5. Add [Metrica Unity plugin](https://yandex.com/dev/appmetrica/doc/mobile-sdk-dg/concepts/unity-plugin-docpage/) to the project, only if you use `yandex-mobileads-<version>.unitypackage`.

6. You can use one of demo scripts in folder `samples` to test plugin. Just add one of this files to your project.

## Yandex Mobile Ads Mediation

Third-party networks can be connected to Yandex Mobile Ads Mediation by several steps:

1. Import `yandex-mobileads-lite-<version>.unitypackage` to your project

2. Import unity package of the desired third-party network

3. Set up meditation according to [AdFox documentation](https://yandex.com/dev/mobile-ads/doc/plugins/unity/mob-mediation/list-network-docpage/)

## Third-party mediation

### AdMob

1. Integrate [AdMob](https://developers.google.com/admob/unity/start)

2. Import package `admob-mobileads-mediation-<version>.unitypackage` from folder `third-party-networks-mediation`

3. Get the Block ID in the Yandex Partner interface for each Ad Unit created in AdMob. Then set up mediation in [the AdMob web interface](https://apps.admob.com).

   For more information, please visit our:
   * [Android AdMob setup documentation](https://yandex.com/dev/mobile-ads/doc/android/adapters/admob-adapter-docpage/#setting)
   * [iOS AdMob setup documentation](https://yandex.com/dev/mobile-ads/doc/ios/adapters/admob-adapter-docpage/#setting)

### MoPub

1. Integrate [MoPub](https://developers.mopub.com/docs/unity)

2. Import package `mopub-mobileads-mediation-<version>.unitypackage` from the folder `third-party-networks-mediation`

3. Get the Block ID in the Yandex Partner interface for each Ad Unit created in MoPub. Then set up mediation in [the MoPub web interface](https://app.mopub.com).

   For more information, please visit our:
   * [Android MoPub setup documentation](https://yandex.com/dev/mobile-ads/doc/android/adapters/mopub-adapter-docpage/#setting)
   * [iOS MoPub setup documentation](https://yandex.com/dev/mobile-ads/doc/ios/adapters/mopub-adapter-docpage/#setting)

## Unity packages

| Package | Description |
| --- | --- |
| yandex-mobileads-0.6.0 | Main Yandex Mobile Ads package |
| yandex-mobileads-lite-0.6.0 | Main Yandex Mobile Ads package distributed for using with [Google resolver]|
| mobileads-admob-mediation-0.6.0 | AdMob mediation |
| mobileads-applovin-mediation-0.3.0 | AppLovin mediation |
| mobileads-facebook-mediation-0.6.0 | Facebook mediation |
| mobileads-ironsource-mediation-0.3.0 | IronSource mediation |
| mobileads-mytarget-mediation-0.6.0 | MyTarget mediation |
| mobileads-mopub-mediation-0.6.0 | MoPub mediation |
| mobileads-startapp-mediation-0.6.0 | StartApp mediation |
| mobileads-unityads-mediation-0.6.0 | UnityAds mediation |
| admob-mobileads-mediation-0.6.0 | Third-party mediation with AdMob |
| mopub-mobileads-mediation-0.6.0 | Third-party mediation with MoPub |

[Google resolver]: https://github.com/googlesamples/unity-jar-resolver
[DOCUMENTATION]: https://yandex.com/dev/mobile-ads/
[LICENSE]: https://legal.yandex.com/partner_ch/
