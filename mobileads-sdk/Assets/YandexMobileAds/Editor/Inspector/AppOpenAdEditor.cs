using UnityEditor;
using UnityEngine;
using YandexMobileAds;

[CustomEditor(typeof(AppOpenAdComponent))]
public class AppOpenAdComponentEditor : Editor
{
    SerializedProperty adName;
    SerializedProperty adUnitIdAndroid;
    SerializedProperty adUnitIdiOS;
    SerializedProperty showAfterLoading;
    SerializedProperty autoLoading;
    SerializedProperty showAppOpenAdOnce;

    SerializedProperty onAdLoaded;
    SerializedProperty onAdFailedToLoad;
    SerializedProperty onAdShown;
    SerializedProperty onAdDismissed;
    SerializedProperty onAdClicked;
    SerializedProperty onAdFailedToShow;
    SerializedProperty onImpression;
    SerializedProperty onAppStateChange;

    private bool showLoadCallbacks = false;
    private bool showInteractionCallbacks = false;

    private void OnEnable()
    {
        adName = serializedObject.FindProperty("adName");
        adUnitIdAndroid = serializedObject.FindProperty("adUnitIdAndroid");
        adUnitIdiOS = serializedObject.FindProperty("adUnitIdiOS");
        showAfterLoading = serializedObject.FindProperty("showAfterLoading");
        autoLoading = serializedObject.FindProperty("autoLoading");
        showAppOpenAdOnce = serializedObject.FindProperty("showAppOpenAdOnce");

        onAdLoaded = serializedObject.FindProperty("OnAdLoaded");
        onAdFailedToLoad = serializedObject.FindProperty("OnAdFailedToLoad");
        onAdShown = serializedObject.FindProperty("OnAdShown");
        onAdDismissed = serializedObject.FindProperty("OnAdDismissed");
        onAdClicked = serializedObject.FindProperty("OnAdClicked");
        onAdFailedToShow = serializedObject.FindProperty("OnAdFailedToShow");
        onImpression = serializedObject.FindProperty("OnImpression");
        onAppStateChange = serializedObject.FindProperty("OnAppStateChange");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("App open ad", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(adName, new GUIContent("Ad Name", "Name of the ad for identification."));
        EditorGUILayout.PropertyField(adUnitIdAndroid, new GUIContent("Ad Unit ID (Android)", "Ad Unit ID for Android."));
        EditorGUILayout.PropertyField(adUnitIdiOS, new GUIContent("Ad Unit ID (iOS)", "Ad Unit ID for iOS."));

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(autoLoading, new GUIContent("Auto Load", "Automatically load the ad"));
        EditorGUILayout.PropertyField(showAfterLoading, new GUIContent("Show After Loading", "Automatically show the ad after it has been loaded."));
        EditorGUILayout.PropertyField(showAppOpenAdOnce, new GUIContent("Show AppOpenAd Once"));

        EditorGUILayout.Space();

        showLoadCallbacks = EditorGUILayout.Foldout(showLoadCallbacks, "Load Callbacks", true);
        if (showLoadCallbacks)
        {
            EditorGUILayout.PropertyField(onAdLoaded, new GUIContent("On Ad Loaded", "Event triggered when the ad is successfully loaded."));
            EditorGUILayout.PropertyField(onAdFailedToLoad, new GUIContent("On Ad Failed To Load", "Event triggered when the ad fails to load."));
        }

        EditorGUILayout.Space();

        showInteractionCallbacks = EditorGUILayout.Foldout(showInteractionCallbacks, "Interaction Callbacks", true);
        if (showInteractionCallbacks)
        {
            EditorGUILayout.PropertyField(onAdShown, new GUIContent("On Ad Shown", "Event triggered when the ad is shown."));
            EditorGUILayout.PropertyField(onAdDismissed, new GUIContent("On Ad Dismissed", "Event triggered when the ad is dismissed."));
            EditorGUILayout.PropertyField(onAdClicked, new GUIContent("On Ad Clicked", "Event triggered when the ad is clicked."));
            EditorGUILayout.PropertyField(onAdFailedToShow, new GUIContent("On Ad Failed To Show", "Event triggered when the ad fails to show."));
            EditorGUILayout.PropertyField(onImpression, new GUIContent("On Impression", "Event triggered when an impression is recorded for the ad."));
            EditorGUILayout.PropertyField(onAppStateChange, new GUIContent("On AppState change", "Event triggered when an App backgaund state is changed."));
        }

        serializedObject.ApplyModifiedProperties();
    }
}
