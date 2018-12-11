using UnityEngine;
using UnityEditor;

public class AnchorED : EditorWindow
{
    private static EditorWindow window;

    [MenuItem ("Window/Anchors Window %#a")]
    private static void ShowWindow()
    {
        window = GetWindow<AnchorED>();
        window.Show();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Set Anchors"))
        {
            GameObject[] selectedGameObjects = Selection.gameObjects;
            foreach(var g in selectedGameObjects)
            {
                AnchorP anchorP = g.GetComponent<AnchorP>();

                if (anchorP != null)
                {
                    anchorP.SetAnchors();
                }
            }
        }

        if (GUILayout.Button("Close"))
        {
            window.Close();
        }
    }
}