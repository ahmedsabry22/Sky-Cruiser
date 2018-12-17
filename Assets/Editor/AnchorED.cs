using UnityEngine;
using UnityEditor;
using System;

public class AnchorED : EditorWindow
{
    private static EditorWindow window;

    [MenuItem("Window/Anchors Window %#a")]
    private static void ShowWindow()
    {
        window = GetWindow<AnchorED>("Anchor Ed");
        window.Show();
    }

    private void OnGUI()
    {
        WindowTitle_LABEL();

        SetAnchorsOfSelectedGameObject();

        SetRectToAnchorSelectedGameObject();

        CloseButton();
    }

    private void WindowTitle_LABEL()
    {
        var titleLabelStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.UpperCenter, fontSize = 30, fontStyle = FontStyle.Bold, fixedHeight = 50 };

        EditorGUILayout.LabelField("Anchors Editor", titleLabelStyle);
        EditorGUILayout.Space(); EditorGUILayout.Space(); EditorGUILayout.Space();

        GUILayout.Space(30);
    }

    [MenuItem("Tools/AnimationP/Align Anchors With Selected %#q")]
    private static void SetAnchorsOfSelectedGameObject()
    {
        var btnContent = new GUIContent() { text = "Align Anchors With Rect", tooltip = "Move Anchors to be aligned with the rect" };

        if (GUILayout.Button(btnContent, GUILayout.Height(100)))
        {
            GameObject[] selectedGameObjects = Selection.gameObjects;

            foreach (var g in selectedGameObjects)
            {
                RectTransform rectTransform = g.GetComponent<RectTransform>();

                if (rectTransform != null)
                {
                    AnchorP.SetAnchorsToRect(rectTransform);
                }
            }
        }

        GUILayout.Space(10);
    }

    [MenuItem("Tools/AnimationP/Align Selected To Anchors %#w")]
    private static void SetRectToAnchorSelectedGameObject()
    {
        var btnContent = new GUIContent() { text = "Align Rect To Anchors", tooltip = "Align The Rect's borders With The Anchors" };

        if (GUILayout.Button(btnContent, GUILayout.Height(100)))
        {
            GameObject[] selectedGameObjects = Selection.gameObjects;

            foreach (var g in selectedGameObjects)
            {
                RectTransform rectTransform = g.GetComponent<RectTransform>();

                if (rectTransform != null)
                {
                    AnchorP.SetRectToAnchor(rectTransform);
                }
            }
        }

        GUILayout.Space(10);
    }

    private void CloseButton()
    {
        var btnContent = new GUIContent() { text = "Close", tooltip = "Close this editor window" };
        if (GUILayout.Button(btnContent, GUILayout.Height(100)))
        {
            window.Close();
        }

        GUILayout.Space(10);
    }
}