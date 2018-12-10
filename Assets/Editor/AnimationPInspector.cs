using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnimationP))]
[CanEditMultipleObjects]
public class AnimationPInspector : Editor
{
    private AnimationP animationP;
    private bool showFromCornerAnimation;

    private SerializedProperty _animationDuration;
    private SerializedProperty _animationType;
    private SerializedProperty _animationFromCornerType;
    private SerializedProperty _elasticPower;
    private SerializedProperty _withDelay;
    private SerializedProperty _showDelay;
    private SerializedProperty _hideDelay;

    private void OnEnable()
    {
        animationP = (AnimationP)target;
        _animationDuration = serializedObject.FindProperty("animationDuration");
        _animationType = serializedObject.FindProperty("animationType");
        _animationFromCornerType = serializedObject.FindProperty("animationFromCornerType");
        _elasticPower = serializedObject.FindProperty("elasticityPower");
        _withDelay = serializedObject.FindProperty("withDelay");
        _showDelay = serializedObject.FindProperty("showDelay");
        _hideDelay = serializedObject.FindProperty("hideDelay");
    }

    public override void OnInspectorGUI()
    {
        InspectorTitle_LABEL();
        AnimationType_DROPDOWN();
        AnimationDuration_INPUT();
        AnimationDelay_PROPERTIES();

        serializedObject.ApplyModifiedProperties();
    }

    private void InspectorTitle_LABEL()
    {
        var titleLabelStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.UpperCenter, fontSize = 20, fontStyle = FontStyle.Bold, fixedHeight = 50 };

        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("AnimationP Inspector", titleLabelStyle);
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(); EditorGUILayout.Space();
    }

    private void AnimationType_DROPDOWN()
    {
        EditorGUILayout.BeginVertical();

        EditorGUILayout.PropertyField(_animationType, new GUIContent("Animation Type"));

        EditorGUILayout.Space(); EditorGUILayout.Space();

        switch (animationP.animationType)
        {
            case (AnimationType.ShowFromCorner):
                EditorGUILayout.PropertyField(_animationFromCornerType, new GUIContent("Animation From Corner Type"));
                break;
            case (AnimationType.ScaleElastic):
                EditorGUILayout.PropertyField(_elasticPower, new GUIContent("Elastic Power"));
                break;
        }

    }

    private void AnimationDuration_INPUT()
    {
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_animationDuration, new GUIContent("Animation Duration"));

        EditorGUILayout.Space(); EditorGUILayout.Space();
    }

    private void AnimationDelay_PROPERTIES()
    {
        EditorGUILayout.BeginVertical();

        EditorGUILayout.PropertyField(_withDelay, new GUIContent("With Delay"));

        if (animationP.withDelay)
        {
            EditorGUILayout.PropertyField(_showDelay, new GUIContent("Show Delay"));
            EditorGUILayout.PropertyField(_hideDelay, new GUIContent("Hide Delay"));
        }

        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(); EditorGUILayout.Space();

        EditorGUILayout.EndVertical();
    }
}