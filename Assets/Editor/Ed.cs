using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class Ed : EditorWindow
{
     AnimatorTypes animatorType;

    [MenuItem("Window/Ed")]
    private static void ShowWindow()
    {
        EditorWindow window = GetWindow<Ed>("Ed Test");
        window.Show();
    }

    private void OnGUI()
    {
        var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.UpperCenter, fontSize = 30, fontStyle = FontStyle.Bold, fixedHeight = 50 };

        EditorGUILayout.LabelField("UI Animation Editor", style);
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

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
    }
}

public enum AnimatorTypes
{
    PingPong, EaseIn, EaseOut
}
