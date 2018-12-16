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
        AddRemoveAnimationManager_BUTTONS();
        AddRemoveAnimation_BUTTONS();
        AddRemoveBackBtn_BUTTONS();
        CloseWindow_BUTTON();
    }

    private void WindowTitle_LABEL()
    {
        var titleLabelStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.UpperCenter, fontSize = 30, fontStyle = FontStyle.Bold, fixedHeight = 50 };

        EditorGUILayout.LabelField("UI Animation Editor", titleLabelStyle);
        GUILayout.Space(50);
    }

    private void AddRemoveAnimationManager_BUTTONS()
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Add Animation Manager", GUILayout.Height(100)))
        {
            foreach (GameObject g in Selection.gameObjects)
            {
                if (g.GetComponent<AnimationPManager>() == null)
                {
                    g.AddComponent<AnimationPManager>();
                }
            }
        }
        if (GUILayout.Button("Remove Animation Manager", GUILayout.Height(100)))
        {
            foreach (GameObject g in Selection.gameObjects)
            {
                if (g.GetComponent<AnimationPManager>() != null)
                    DestroyImmediate(g.GetComponent<AnimationPManager>());
            }
        }

        GUILayout.EndHorizontal();

        GUILayout.Space(20);
    }

    private void AddRemoveAnimation_BUTTONS()
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Add Animator", GUILayout.Height(100)))
        {
            foreach (GameObject g in Selection.gameObjects)
            {
                if (g.GetComponent<AnimationPElement>() == null)
                {
                    g.AddComponent<AnimationPElement>();
                }
            }
        }
        if (GUILayout.Button("Remove Animator", GUILayout.Height(100)))
        {
            foreach (GameObject g in Selection.gameObjects)
            {
                if (g.GetComponent<AnimationPElement>() != null)
                    DestroyImmediate(g.GetComponent<AnimationPElement>());
            }
        }

        GUILayout.EndHorizontal();

        GUILayout.Space(20);
    }

    private void AddRemoveAnchor_BUTTONS()
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Add Anchor Controller", GUILayout.Height(100)))
        {
            foreach (GameObject g in Selection.gameObjects)
            {
                if (g.GetComponent<AnchorP>() == null)
                {
                    g.AddComponent<AnchorP>();
                }
            }
        }

        if (GUILayout.Button("Remove Anchor Controller", GUILayout.Height(100)))
        {
            foreach (GameObject g in Selection.gameObjects)
            {
                if (g.GetComponent<AnchorP>() != null)
                    DestroyImmediate(g.GetComponent<AnchorP>());
            }
        }

        GUILayout.EndHorizontal();

        GUILayout.Space(20);
    }

    private void AddRemoveBackBtn_BUTTONS()
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Add Back Button Functionality", GUILayout.Height(100)))
        {
            foreach (GameObject g in Selection.gameObjects)
            {
                if (g.GetComponent<BackButtonP>() == null)
                {
                    g.AddComponent<BackButtonP>();
                }
            }
        }

        if (GUILayout.Button("Remove Back Button Functionality", GUILayout.Height(100)))
        {
            foreach (GameObject g in Selection.gameObjects)
            {
                if (g.GetComponent<BackButtonP>() != null)
                    DestroyImmediate(g.GetComponent<BackButtonP>());
            }
        }

        GUILayout.EndHorizontal();

        GUILayout.Space(20);
    }

    private void CloseWindow_BUTTON()
    {
        GUILayout.BeginVertical();
        if (GUILayout.Button("Close", GUILayout.Height(100)))
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
