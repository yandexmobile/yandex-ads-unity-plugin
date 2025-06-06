using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RewardedAdComponent))]
public class RewardedAdComponentEditor : Editor
{
    SerializedProperty adName;
    SerializedProperty adUnitIdAndroid;
    SerializedProperty adUnitIdiOS;
    SerializedProperty showAfterLoading;
    SerializedProperty autoLoading;

    SerializedProperty onAdLoaded;
    SerializedProperty onAdFailedToLoad;
    SerializedProperty onAdShown;
    SerializedProperty onAdDismissed;
    SerializedProperty onAdClicked;
    SerializedProperty onAdFailedToShow;
    SerializedProperty onImpression;
    SerializedProperty onRewarded;

    private bool showLoadCallbacks = false;
    private bool showInteractionCallbacks = false;

    private void OnEnable()
    {
        adName = serializedObject.FindProperty("adName");
        adUnitIdAndroid = serializedObject.FindProperty("adUnitIdAndroid");
        adUnitIdiOS = serializedObject.FindProperty("adUnitIdiOS");
        showAfterLoading = serializedObject.FindProperty("showAfterLoading");
        autoLoading = serializedObject.FindProperty("autoLoading");

        onAdLoaded = serializedObject.FindProperty("OnAdLoaded");
        onAdFailedToLoad = serializedObject.FindProperty("OnAdFailedToLoad");
        onAdShown = serializedObject.FindProperty("OnAdShown");
        onAdDismissed = serializedObject.FindProperty("OnAdDismissed");
        onAdClicked = serializedObject.FindProperty("OnAdClicked");
        onAdFailedToShow = serializedObject.FindProperty("OnAdFailedToShow");
        onImpression = serializedObject.FindProperty("OnImpression");
        onRewarded = serializedObject.FindProperty("OnRewarded");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Rewarded", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(adName, new GUIContent("Ad Name"));

        EditorGUILayout.PropertyField(adUnitIdAndroid, new GUIContent("Ad Unit ID (Android)"));
        EditorGUILayout.PropertyField(adUnitIdiOS, new GUIContent("Ad Unit ID (iOS)"));


        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(autoLoading, new GUIContent("Auto Load", "Automatically load the ad"));
        EditorGUILayout.PropertyField(showAfterLoading, new GUIContent("Show After Loading", "Automatically show the ad after it has been loaded."));

        EditorGUILayout.Space();

        showLoadCallbacks = EditorGUILayout.Foldout(showLoadCallbacks, "Load Callbacks", true);
        if (showLoadCallbacks)
        {
            EditorGUILayout.PropertyField(onAdLoaded, new GUIContent("On Ad Loaded"));
            EditorGUILayout.PropertyField(onAdFailedToLoad, new GUIContent("On Ad Failed To Load"));
        }

        EditorGUILayout.Space();

        showInteractionCallbacks = EditorGUILayout.Foldout(showInteractionCallbacks, "Interaction Callbacks", true);
        if (showInteractionCallbacks)
        {
            EditorGUILayout.PropertyField(onAdShown, new GUIContent("On Ad Shown"));
            EditorGUILayout.PropertyField(onAdDismissed, new GUIContent("On Ad Dismissed"));
            EditorGUILayout.PropertyField(onAdClicked, new GUIContent("On Ad Clicked"));
            EditorGUILayout.PropertyField(onAdFailedToShow, new GUIContent("On Ad Failed To Show"));
            EditorGUILayout.PropertyField(onImpression, new GUIContent("On Impression"));
            EditorGUILayout.PropertyField(onRewarded, new GUIContent("On Rewarded"));
        }

        serializedObject.ApplyModifiedProperties();
    }
}
