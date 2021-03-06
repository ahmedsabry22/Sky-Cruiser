﻿using UnityEngine;
using UnityEditor;
using System;

public class AnchorED : EditorWindow
{
    private static EditorWindow window;

    [MenuItem("Window/Anchors Window %#r")]
    private static void ShowWindow()
    {
        window = GetWindow<AnchorED>("Anchor Ed");
        window.Show();
        window.maxSize = new Vector2(270, 490);
        window.minSize = new Vector2(270, 490);
    }

    private void OnGUI()
    {
        GUI.color = Color.gray;

        WindowTitle_LABEL();

        GUI.color = Color.white;
        GUI.backgroundColor = Color.cyan;

        SetAnchorsToFitRect();

        GUI.color = Color.white;
        GUI.backgroundColor = Color.magenta;

        SetAnchorsCenterOfRect();
        SetAnchorsTopRight();
        SetAnchorsTopLeft();
        SetAnchorsBottomRight();
        SetAnchorsBottomLeft();

        GUI.color = Color.white;
        GUI.backgroundColor = Color.yellow;

        SetRectToAnchorSelectedGameObject();
    }

    private void WindowTitle_LABEL()
    {
        GUILayout.Space(10);

        var titleLabelStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.UpperCenter, fontSize = 30, fontStyle = FontStyle.Bold, fixedHeight = 50 };

        EditorGUILayout.LabelField("Anchors Editor", titleLabelStyle);
        EditorGUILayout.Space(); EditorGUILayout.Space(); EditorGUILayout.Space();

        GUILayout.Space(30);
    }

    [MenuItem("Tools/AnimationP/Align Anchors With Selected %#k")]
    private static void SetAnchorsToFitRect()
    {
        var btnContent = new GUIContent() { text = "Align Anchors With Rect", tooltip = "Move Anchors to be aligned with the rect" };

        if (GUILayout.Button(btnContent, GUILayout.Height(50)))
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

        GUILayout.Space(20);
    }

    private void SetAnchorsCenterOfRect()
    {
        var btnContent = new GUIContent() { text = "Set Anchors To Center Of Rect", tooltip = "Move Anchors to the center of the rect" };

        if (GUILayout.Button(btnContent, GUILayout.Height(50)))
        {
            GameObject[] selectedGameObjects = Selection.gameObjects;

            foreach (var g in selectedGameObjects)
            {
                RectTransform rectTransform = g.GetComponent<RectTransform>();

                if (rectTransform != null)
                {
                    AnchorP.SetAnchorsCenterOfRect(rectTransform);
                }
            }
        }
    }

    private void SetAnchorsTopRight()
    {
        var btnContent = new GUIContent() { text = "Set Anchors Top Right", tooltip = "Move Anchors to top right of the rect" };

        if (GUILayout.Button(btnContent, GUILayout.Height(50)))
        {
            GameObject[] selectedGameObjects = Selection.gameObjects;

            foreach (var g in selectedGameObjects)
            {
                RectTransform rectTransform = g.GetComponent<RectTransform>();

                if (rectTransform != null)
                {
                    AnchorP.SetAnchorsTopRight(rectTransform);
                }
            }
        }
    }

    private void SetAnchorsTopLeft()
    {
        var btnContent = new GUIContent() { text = "Set Anchors Top Left", tooltip = "Move Anchors to top left of the rect" };

        if (GUILayout.Button(btnContent, GUILayout.Height(50)))
        {
            GameObject[] selectedGameObjects = Selection.gameObjects;

            foreach (var g in selectedGameObjects)
            {
                RectTransform rectTransform = g.GetComponent<RectTransform>();

                if (rectTransform != null)
                {
                    AnchorP.SetAnchorsTopLeft(rectTransform);
                }
            }
        }
    }

    private void SetAnchorsBottomRight()
    {
        var btnContent = new GUIContent() { text = "Set Anchors Bottom Right", tooltip = "Move Anchors to bottom right of the rect" };

        if (GUILayout.Button(btnContent, GUILayout.Height(50)))
        {
            GameObject[] selectedGameObjects = Selection.gameObjects;

            foreach (var g in selectedGameObjects)
            {
                RectTransform rectTransform = g.GetComponent<RectTransform>();

                if (rectTransform != null)
                {
                    AnchorP.SetAnchorsBottomRight(rectTransform);
                }
            }
        }
    }

    private void SetAnchorsBottomLeft()
    {
        var btnContent = new GUIContent() { text = "Set Anchors Bottom Left", tooltip = "Move Anchors to bottom left of the rect" };

        if (GUILayout.Button(btnContent, GUILayout.Height(50)))
        {
            GameObject[] selectedGameObjects = Selection.gameObjects;

            foreach (var g in selectedGameObjects)
            {
                RectTransform rectTransform = g.GetComponent<RectTransform>();

                if (rectTransform != null)
                {
                    AnchorP.SetAnchorsBottomLeft(rectTransform);
                }
            }
        }

        GUILayout.Space(20);
    }

    [MenuItem("Tools/AnimationP/Align Selected To Anchors %#w")]
    private static void SetRectToAnchorSelectedGameObject()
    {
        var btnContent = new GUIContent() { text = "Align Rect To Anchors", tooltip = "Align The Rect's borders With The Anchors" };

        if (GUILayout.Button(btnContent, GUILayout.Height(50)))
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

        GUILayout.Space(5);
    }

    private void CloseButton()
    {
        var btnContent = new GUIContent() { text = "Close", tooltip = "Close this editor window" };
        if (GUILayout.Button(btnContent, GUILayout.Height(50)))
        {
            window.Close();
        }

        GUILayout.Space(5);
    }
}