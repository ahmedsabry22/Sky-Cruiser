using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class Ed : EditorWindow
{
    private static EditorWindow window;

    [MenuItem("Window/Ed %#e")]
    private static void ShowWindow()
    {
        window = GetWindow<Ed>("Ed Test");
        window.Show();
    }

    private void OnGUI()
    {
        WindowTitle_LABEL();
        AddRemoveAnimation_BUTTONS();
        Close_BUTTON();
    }

    private void WindowTitle_LABEL()
    {
        var titleLabelStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.UpperCenter, fontSize = 30, fontStyle = FontStyle.Bold, fixedHeight = 50 };

        EditorGUILayout.LabelField("UI Animation Editor", titleLabelStyle);
        EditorGUILayout.Space(); EditorGUILayout.Space(); EditorGUILayout.Space();
    }

    private void AddRemoveAnimation_BUTTONS()
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Add Animator"))
        {
            foreach (GameObject g in Selection.gameObjects)
            {
                if (g.GetComponent<AnimationP>() == null)
                {
                    g.AddComponent<AnimationP>();
                }
            }
        }
        if (GUILayout.Button("Remove Animator"))
        {
            foreach (GameObject g in Selection.gameObjects)
            {
                if (g.GetComponent<AnimationP>() != null)
                    DestroyImmediate(g.GetComponent<AnimationP>());
            }
        }

        GUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
    }

    private void Close_BUTTON()
    {
        GUILayout.BeginVertical();
        if (GUILayout.Button("Close"))
        {
            window.Close();
        }
        GUILayout.EndVertical();
    }
}

public enum AnimatorTypes
{
    PingPong, EaseIn, EaseOut
}
