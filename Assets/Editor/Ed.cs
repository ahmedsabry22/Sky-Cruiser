using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class Ed : EditorWindow
{
    private static EditorWindow window;

    [MenuItem("Window/Ed %#q")]
    private static void ShowWindow()
    {
        window = GetWindow<Ed>("Ed Test");
        window.Show();
        window.maxSize = new Vector2(350, 450);
        window.minSize = new Vector2(350, 450);
    }

    private void OnGUI()
    {
        GUI.color = Color.gray;

        WindowTitle_LABEL();

        GUI.color = Color.white;
        GUI.backgroundColor = Color.cyan;

        AddRemoveAnimationManager_BUTTONS();

        GUI.color = Color.white;
        GUI.backgroundColor = Color.magenta;

        AddRemoveAnimation_BUTTONS();

        GUI.color = Color.white;
        GUI.backgroundColor = Color.blue;

        AddRemoveBackBtn_BUTTONS();
    }

    private void WindowTitle_LABEL()
    {
        GUILayout.Space(10);

        var titleLabelStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.UpperCenter, fontSize = 30, fontStyle = FontStyle.Bold, fixedHeight = 50 };

        EditorGUILayout.LabelField("UI Animation Editor", titleLabelStyle);
        GUILayout.Space(50);
    }

    private void AddRemoveAnimationManager_BUTTONS()
    {
        if (GUILayout.Button("Add Animation Manager", GUILayout.Height(50)))
        {
            foreach (GameObject g in Selection.gameObjects)
            {
                if (g.GetComponent<AnimationPManager>() == null)
                {
                    g.AddComponent<AnimationPManager>();
                }
            }
        }
        if (GUILayout.Button("Remove Animation Manager", GUILayout.Height(50)))
        {
            foreach (GameObject g in Selection.gameObjects)
            {
                if (g.GetComponent<AnimationPManager>() != null)
                    DestroyImmediate(g.GetComponent<AnimationPManager>());
            }
        }

        GUILayout.Space(20);
    }

    private void AddRemoveAnimation_BUTTONS()
    {
        if (GUILayout.Button("Add Animator", GUILayout.Height(50)))
        {
            foreach (GameObject g in Selection.gameObjects)
            {
                if (g.GetComponent<AnimationPElement>() == null)
                {
                    g.AddComponent<AnimationPElement>();
                }
            }
        }
        if (GUILayout.Button("Remove Animator", GUILayout.Height(50)))
        {
            foreach (GameObject g in Selection.gameObjects)
            {
                if (g.GetComponent<AnimationPElement>() != null)
                    DestroyImmediate(g.GetComponent<AnimationPElement>());
            }
        }

        GUILayout.Space(20);
    }

    private void AddRemoveAnchor_BUTTONS()
    {
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

        GUILayout.Space(20);
    }

    private void AddRemoveBackBtn_BUTTONS()
    {
        if (GUILayout.Button("Add Back Button Functionality", GUILayout.Height(50)))
        {
            foreach (GameObject g in Selection.gameObjects)
            {
                if (g.GetComponent<BackButtonP>() == null)
                {
                    g.AddComponent<BackButtonP>();
                }
            }
        }

        if (GUILayout.Button("Remove Back Button Functionality", GUILayout.Height(50)))
        {
            foreach (GameObject g in Selection.gameObjects)
            {
                if (g.GetComponent<BackButtonP>() != null)
                    DestroyImmediate(g.GetComponent<BackButtonP>());
            }
        }

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
