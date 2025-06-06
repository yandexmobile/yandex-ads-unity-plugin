using UnityEditor;
using UnityEngine;

namespace YandexAdsEditor
{
    public class YandexAdsIM : MonoBehaviour
    {
        [MenuItem("YandexAds/SDK ChangeLog")]
        public static void OpenChangeLog()
        {
            Application.OpenURL("https://github.com/yandexmobile/yandex-ads-unity-plugin/blob/master/CHANGELOG.md");
        }

        [MenuItem("YandexAds/Documentation")]
        public static void OpenDocumentation()
        {
            Application.OpenURL("https://ads.yandex.com/helpcenter/en/dev/unity/quick-start");
        }
    }
}