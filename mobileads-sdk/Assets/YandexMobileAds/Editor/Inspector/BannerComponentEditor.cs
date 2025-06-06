using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BannerComponent))]
public class BannerComponentEditor : Editor
{
    SerializedProperty bannerType;
    SerializedProperty adUnitIdAndroid;
    SerializedProperty adUnitIdiOS;
    SerializedProperty bannerPosition;
    SerializedProperty useScreenWidth;
    SerializedProperty manualWidth;
    SerializedProperty bannerHeight;
    SerializedProperty showAfterLoading;

    SerializedProperty onAdLoaded;
    SerializedProperty onAdFailedToLoad;
    SerializedProperty onAdClicked;
    SerializedProperty onLeftApplication;
    SerializedProperty onReturnedToApplication;
    SerializedProperty onImpression;

    SerializedProperty bannerName;

    private bool showLoadAdCallbacks = false;
    private bool showContentCallbacks = false;

    private void OnEnable()
    {
        bannerName = serializedObject.FindProperty("bannerName");
        bannerType = serializedObject.FindProperty("bannerType");
        adUnitIdAndroid = serializedObject.FindProperty("adUnitIdAndroid");
        adUnitIdiOS = serializedObject.FindProperty("adUnitIdiOS");
        bannerPosition = serializedObject.FindProperty("bannerPosition");
        useScreenWidth = serializedObject.FindProperty("useScreenWidth");
        manualWidth = serializedObject.FindProperty("manualWidth");
        bannerHeight = serializedObject.FindProperty("bannerHeight");
        showAfterLoading = serializedObject.FindProperty("showAfterLoading");

        onAdLoaded = serializedObject.FindProperty("OnAdLoaded");
        onAdFailedToLoad = serializedObject.FindProperty("OnAdFailedToLoad");
        onAdClicked = serializedObject.FindProperty("OnAdClicked");
        onLeftApplication = serializedObject.FindProperty("OnLeftApplication");
        onReturnedToApplication = serializedObject.FindProperty("OnReturnedToApplication");
        onImpression = serializedObject.FindProperty("OnImpression");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Banner", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(bannerName, new GUIContent("Banner Name"));
        EditorGUILayout.PropertyField(bannerType, new GUIContent("Banner Type"));

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(adUnitIdAndroid, new GUIContent("Ad Unit ID (Android)"));
        EditorGUILayout.PropertyField(adUnitIdiOS, new GUIContent("Ad Unit ID (iOS)"));

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(bannerPosition, new GUIContent("Banner Position"));

        EditorGUILayout.PropertyField(useScreenWidth, new GUIContent("Use Screen Width"));
        EditorGUI.BeginDisabledGroup(useScreenWidth.boolValue);
        EditorGUILayout.PropertyField(manualWidth, new GUIContent("Manual Width (dp)"));
        EditorGUI.EndDisabledGroup();

        if ((BannerComponent.BannerType)bannerType.enumValueIndex == BannerComponent.BannerType.Inline)
        {
            EditorGUILayout.PropertyField(bannerHeight, new GUIContent("Banner Height (dp)"));
        }

        EditorGUILayout.PropertyField(showAfterLoading, new GUIContent("Show After Loading"));

        EditorGUILayout.Space();

        showLoadAdCallbacks = EditorGUILayout.Foldout(showLoadAdCallbacks, "Load Ad Callbacks", true);
        if (showLoadAdCallbacks)
        {
            EditorGUILayout.PropertyField(onAdLoaded, new GUIContent("On Ad Successfully Loaded"));
            EditorGUILayout.PropertyField(onAdFailedToLoad, new GUIContent("On Ad Failed To Load"));
        }

        EditorGUILayout.Space();

        showContentCallbacks = EditorGUILayout.Foldout(showContentCallbacks, "Content Callbacks", true);
        if (showContentCallbacks)
        {
            EditorGUILayout.PropertyField(onAdClicked, new GUIContent("On Ad Clicked"));
            EditorGUILayout.PropertyField(onLeftApplication, new GUIContent("On User Left Application"));
            EditorGUILayout.PropertyField(onReturnedToApplication, new GUIContent("On User Returned To Application"));
            EditorGUILayout.PropertyField(onImpression, new GUIContent("On Ad Impression Recorded"));
        }

        serializedObject.ApplyModifiedProperties();
    }
}