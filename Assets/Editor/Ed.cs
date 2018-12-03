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

    public string[] options = new string[] { "Cube", "Sphere", "Plane" };
    int index = 0;

    private void OnGUI()
    {
        index = EditorGUILayout.Popup(index, options);
        EditorGUILayout.LabelField("This is the first real editor window I create");

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Add Animator"))
        {
            foreach (GameObject g in Selection.gameObjects)
            {
                if (g.GetComponent<Animator>() == null)
                {
                    g.AddComponent<Animator>();
                    if (animatorType == AnimatorTypes.EaseIn)
                        Debug.Log("Ease In");
                    else if (animatorType == AnimatorTypes.PingPong)
                        Debug.Log("Ping Pong");
                    else if (animatorType == AnimatorTypes.EaseOut)
                        Debug.Log("Ease Out");
                }
            }
        }
        if (GUILayout.Button("Remove Animator"))
        {
            foreach (GameObject g in Selection.gameObjects)
            {
                if (g.GetComponent<Animator>() != null)
                    DestroyImmediate(g.GetComponent<Animator>());
            }
        }

        GUILayout.EndHorizontal();
    }
}

public enum AnimatorTypes
{
    PingPong, EaseIn, EaseOut
}
