using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnimationP))]
public class AnimationPInspector : Editor
{
    private AnimationP animationP;
    private bool showFromCornerAnimation;

    private void OnEnable()
    {
        animationP = (AnimationP)target;
    }

    public override void OnInspectorGUI()
    {
        var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.UpperCenter, fontSize = 20, fontStyle = FontStyle.Bold, fixedHeight = 50 };

        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("AnimationP Inspector", style);
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical();

        animationP.animationType = (AnimationType)EditorGUILayout.EnumPopup("Animation Type", animationP.animationType);

        EditorGUILayout.Space();

        if (animationP.animationType == AnimationType.ShowFromCorner)
        {
            //showFromCornerAnimation = EditorGUILayout.Foldout(showFromCornerAnimation, "Show From Corner Animation");

            //if (showFromCornerAnimation)
            {
                //EditorGUI.indentLevel += 1;
                animationP.animationFromCornerType = (AnimationFromCornerType)EditorGUILayout.EnumPopup("Show From Corner Animation", animationP.animationFromCornerType);
                //EditorGUI.indentLevel -= 1;
            }
        }
        else if (animationP.animationType == AnimationType.ScaleElastic)
        {
            animationP.elasticPower = EditorGUILayout.FloatField("Elastic Power", animationP.elasticPower);
        }

        EditorGUILayout.Space();

        animationP.animationDuration = EditorGUILayout.FloatField("Duration", animationP.animationDuration);

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        animationP.withDelay = EditorGUILayout.Toggle("With Delay", animationP.withDelay);
        if (animationP.withDelay)
            animationP.delay = EditorGUILayout.FloatField("Delay", animationP.delay);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.EndVertical();
    }
}