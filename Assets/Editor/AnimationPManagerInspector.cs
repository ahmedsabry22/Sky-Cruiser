using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnimationPManager))]
[CanEditMultipleObjects]
public class AnimationPManagerInspector : Editor
{
    private AnimationPManager animationPManager;

    private SerializedProperty _showMenuOnStart;

    private void OnEnable()
    {
        animationPManager = (AnimationPManager)target;

        _showMenuOnStart = serializedObject.FindProperty("showMenuOnStart");
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Space(20);

        EditorGUILayout.PropertyField(_showMenuOnStart);

        GUILayout.Space(20);

        if (GUILayout.Button("Update Animated Elements"))
        {
            animationPManager.UpdateElementsInChildren();
            Debug.Log("Updated UI Elements - current UI elements in children : " + animationPManager.childrenElements.Length);
        }

        GUILayout.Space(20);

        serializedObject.ApplyModifiedProperties();
    }
}