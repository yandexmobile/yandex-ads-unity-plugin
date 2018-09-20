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

5. Add [Metrica Unity plugin](https://appmetrica.yandex.com/doc//mobile-sdk-dg/concepts/unity-plugin-docpage/) to the project.

6. You can use one of demo scripts in folder `samples` to test plugin. Just add one of this files to your project.

## Yandex Mobile Ads Mediation

Third-party networks can be connected to Yandex Mobile Ads Mediation by several steps:

1. Import `yandex-mobileads-lite-<version>.unitypackage` to your project

2. Import unity package of the desired third-party network

3. Set up meditation according to [AdFox documentation](https://tech.yandex.com/mobile-ads/doc/dg/ios/adfox/my-target-adfox-docpage/)

## Third-party mediation

### AdMob

1. Integrate [AdMob](https://developers.google.com/admob/unity/start)

2. Import package `admob-mobileads-mediation-<version>.unitypackage` from folder `third-party-networks-mediation`

3. Get the Block ID in the Yandex Partner interface for each Ad Unit created in AdMob. Then set up mediation in [the AdMob web interface](apps.admob.com).

   For more information, please visit [AdMob documentation](https://tech.yandex.com/mobile-ads/doc/dg/android/quick-start/admob-adapter-docpage/#setting)

### MoPub

1. Integrate [MoPub](https://developers.mopub.com/docs/unity)

2. Import package `mopub-mobileads-mediation-<version>.unitypackage` from the folder `third-party-networks-mediation`

3. Get the Block ID in the Yandex Partner interface for each Ad Unit created in MoPub. Then set up mediation in [the MoPub web interface](app.mopub.com).

   For more information, please visit [MoPub documentation](https://tech.yandex.com/mobile-ads/doc/dg/android/quick-start/mopub-adapter-docpage/)

## Unity packages

| Package | Description |
| --- | --- |
| yandex-mobileads | Main Yandex Mobile Ads package |
| yandex-mobileads-lite | Main Yandex Mobile Ads package distributed for using with [Google resolver]|
| mobileads-admob-mediation | AdMob mediation |
| mobileads-facebook-mediation | Facebook mediation |
| mobileads-mytarget-mediation | MyTarget mediation |
| mobileads-startapp-mediation | StartApp mediation |
| admob-mobileads-mediation | Third-party mediation with AdMob |
| mopub-mobileads-mediation| Third-party mediation with MoPub |

[Google resolver]: https://github.com/googlesamples/unity-jar-resolver
[DOCUMENTATION]: https://tech.yandex.com/mobile-ads/
[LICENSE]: https://legal.yandex.com/partner_ch/