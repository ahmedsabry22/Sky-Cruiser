using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BackButtonP))]
[CanEditMultipleObjects]
public class BackButtonInspector : Editor
{
    private BackButtonP backButtonP;

    private SerializedProperty _withGraphic;
    private SerializedProperty _position;
    private SerializedProperty _graphicType;
    private SerializedProperty _buttonText;
    private SerializedProperty _graphicSprite;
    private SerializedProperty _scale;

    private void OnEnable()
    {
        backButtonP = (BackButtonP)target;

        _withGraphic = serializedObject.FindProperty("withGraphic");
        _position = serializedObject.FindProperty("position");
        _graphicType = serializedObject.FindProperty("graphicType");
        _buttonText = serializedObject.FindProperty("buttonText");
        _graphicSprite = serializedObject.FindProperty("graphicSprite");
        _scale = serializedObject.FindProperty("scale");
    }

    public override void OnInspectorGUI()
    {
        ShowGraphics();

        serializedObject.ApplyModifiedProperties();
    }

    private void ShowGraphics()
    {
        GUILayout.Space(20);

        GUI.color = Color.cyan;
        EditorGUILayout.PropertyField(_withGraphic);
        GUI.color = Color.white;

        GUILayout.Space(20);

        if (backButtonP.withGraphic)
        {
            EditorGUILayout.PropertyField(_graphicType);

            EditorGUILayout.PropertyField(_scale);

            if (backButtonP.graphicType == BackButtonP.GraphicType.Image)
            {
                EditorGUILayout.PropertyField(_graphicSprite);
            }

            else if (backButtonP.graphicType == BackButtonP.GraphicType.Text)
            {
                EditorGUILayout.PropertyField(_buttonText);
            }

            GUILayout.Space(20);

            EditorGUILayout.PropertyField(_position);

            GUILayout.Space(20);
            
            EditorGUILayout.HelpBox("if with graphic is false, it will work only on devices that has back button, and ESC button on standalone devices. If true, there will be a button on the screen", MessageType.Info);
        }
    }
}