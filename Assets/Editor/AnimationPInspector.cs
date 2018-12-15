using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnimationP))]
[CanEditMultipleObjects]
public class AnimationPInspector : Editor
{
    private AnimationP animationP;
    private bool showFromCornerAnimation;

    private SerializedProperty _showOnStart;
    private SerializedProperty _animationShowDuration;
    private SerializedProperty _animationHideDuration;
    private SerializedProperty _showAnimationType;
    private SerializedProperty _hideAnimationType;
    private SerializedProperty _fadeChildren;
    private SerializedProperty _animationFromCornerType;
    private SerializedProperty _animationToCornerType;
    private SerializedProperty _elasticPower;
    private SerializedProperty _withDelay;
    private SerializedProperty _showDelay;
    private SerializedProperty _hideDelay;
    private SerializedProperty _onShowEvent;
    private SerializedProperty _onHideEvent;
    private SerializedProperty _onShowCompleteEvent;
    private SerializedProperty _onHideCompleteEvent;

    private int currentTabIndex = 0;
    private string[] tabsTexts = { "Show", "Hide" };

    private void OnEnable()
    {
        animationP = (AnimationP)target;

        _showOnStart = serializedObject.FindProperty("showOnStart");
        _animationShowDuration = serializedObject.FindProperty("animationShowDuration");
        _animationHideDuration = serializedObject.FindProperty("animationHideDuration");
        _showAnimationType = serializedObject.FindProperty("showAnimationType");
        _hideAnimationType = serializedObject.FindProperty("hideAnimationType");
        _fadeChildren = serializedObject.FindProperty("fadeChildren");
        _animationFromCornerType = serializedObject.FindProperty("animationFromCornerType");
        _animationToCornerType = serializedObject.FindProperty("animationToCornerType");
        _elasticPower = serializedObject.FindProperty("elasticityPower");
        _withDelay = serializedObject.FindProperty("withDelay");
        _showDelay = serializedObject.FindProperty("showDelay");
        _hideDelay = serializedObject.FindProperty("hideDelay");
        _onShowEvent = serializedObject.FindProperty("OnShow");
        _onHideEvent = serializedObject.FindProperty("OnHide");
        _onShowCompleteEvent = serializedObject.FindProperty("OnShowComplete");
        _onHideCompleteEvent = serializedObject.FindProperty("OnHideComplete");
    }

    public override void OnInspectorGUI()
    {
        InspectorTitle_LABEL();

        GUILayout.Space(20);
        currentTabIndex = GUILayout.Toolbar(currentTabIndex, tabsTexts);
        GUILayout.Space(20);

        if (currentTabIndex == 0)
        {
            GUI.color = Color.cyan;

            AnimationWorksOnStart_TOGGLE();
            ShowAnimationType_DROPDOWN();
            FadeChildrenShow_TOGGLE();
            AnimationShowDuration_INPUT();
            ShowAnimationDelay_PROPERTIES();
            AutomateChildrenShowDelays_BUTTON();
            OnShow_EVENTS();
        }
        else if (currentTabIndex == 1)
        {
            GUI.color = Color.gray;

            HideAnimationType_DROPDOWN();
            FadeChildrenHide_TOGGLE();
            AnimationHideDuration_INPUT();
            HideAnimationDelay_PROPERTIES();
            AutomateChildrenHideDelays_BUTTON();
            OnHide_EVENTS();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void InspectorTitle_LABEL()
    {
        var titleLabelStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.UpperCenter, fontSize = 20, fontStyle = FontStyle.Bold, fixedHeight = 50 };

        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("AnimationP Inspector", titleLabelStyle);
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(); EditorGUILayout.Space();
    }

    private void AnimationWorksOnStart_TOGGLE()
    {
        EditorGUILayout.BeginVertical();

        EditorGUILayout.PropertyField(_showOnStart, new GUIContent("Show On Start"));

        GUILayout.Space(10);
    }

    private void ShowAnimationType_DROPDOWN()
    {
        EditorGUILayout.BeginVertical();

        EditorGUILayout.PropertyField(_showAnimationType, new GUIContent("Animation Type"));

        EditorGUILayout.Space(); EditorGUILayout.Space();

        switch (animationP.showAnimationType)
        {
            case (AnimationType.FromCornerWithScale):
            case (AnimationType.FromCornerWithoutScale):
                EditorGUILayout.PropertyField(_animationFromCornerType, new GUIContent("Animation From Corner Type"));
                break;
            case (AnimationType.ScaleElastic):
                EditorGUILayout.PropertyField(_elasticPower, new GUIContent("Elastic Power"));
                break;
        }
    }

    private void HideAnimationType_DROPDOWN()
    {
        EditorGUILayout.BeginVertical();

        EditorGUILayout.PropertyField(_hideAnimationType, new GUIContent("Animation Type"));

        EditorGUILayout.Space(); EditorGUILayout.Space();

        switch (animationP.hideAnimationType)
        {
            case (AnimationType.FromCornerWithScale):
            case (AnimationType.FromCornerWithoutScale):
                EditorGUILayout.PropertyField(_animationToCornerType, new GUIContent("Animation To Corner Type"));
                break;
            case (AnimationType.ScaleElastic):
                EditorGUILayout.PropertyField(_elasticPower, new GUIContent("Elastic Power"));
                break;
        }
    }

    private void FadeChildrenShow_TOGGLE()
    {
        if (animationP.showAnimationType == AnimationType.Fade)
            EditorGUILayout.PropertyField(_fadeChildren, new GUIContent("Fade Children With Parent"));
    }

    private void FadeChildrenHide_TOGGLE()
    {
        if (animationP.hideAnimationType == AnimationType.Fade)
            EditorGUILayout.PropertyField(_fadeChildren, new GUIContent("Fade Children With Parent"));
    }

    private void AnimationShowDuration_INPUT()
    {
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_animationShowDuration, new GUIContent("Animation Show Duration"));

        EditorGUILayout.Space(); EditorGUILayout.Space();
    }

    private void AnimationHideDuration_INPUT()
    {
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_animationHideDuration, new GUIContent("Animation Hide Duration"));

        EditorGUILayout.Space(); EditorGUILayout.Space();
    }

    private void ShowAnimationDelay_PROPERTIES()
    {
        EditorGUILayout.BeginVertical();

        EditorGUILayout.PropertyField(_withDelay, new GUIContent("With Delay"));

        if (animationP.withDelay)
        {
            EditorGUILayout.PropertyField(_showDelay, new GUIContent("Show Delay"));
        }

        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(); EditorGUILayout.Space();

        EditorGUILayout.EndVertical();
    }

    private void HideAnimationDelay_PROPERTIES()
    {
        EditorGUILayout.BeginVertical();

        EditorGUILayout.PropertyField(_withDelay, new GUIContent("With Delay"));

        if (animationP.withDelay)
        {
            EditorGUILayout.PropertyField(_hideDelay, new GUIContent("Hide Delay"));
        }

        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(); EditorGUILayout.Space();

        EditorGUILayout.EndVertical();
    }

    private void OnShow_EVENTS()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.PropertyField(_onShowEvent, new GUIContent("On Show"));
        EditorGUILayout.PropertyField(_onShowCompleteEvent, new GUIContent("On Show Complete"));
        EditorGUILayout.EndVertical();
    }

    private void OnHide_EVENTS()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.PropertyField(_onHideEvent, new GUIContent("On Hide"));
        EditorGUILayout.PropertyField(_onHideCompleteEvent, new GUIContent("On Hide Complete"));
        EditorGUILayout.EndVertical();
    }

    private void AutomateChildrenShowDelays_BUTTON()
    {
        if (GUILayout.Button("Automate Show Delays In Children"))
        {
            AnimationP[] elementsInChildren = Selection.activeGameObject.GetComponentsInChildren<AnimationP>();

            float step = (animationP.animationShowDuration / elementsInChildren.Length);
            float currentValue = 0;

            for (int i = 1; i < elementsInChildren.Length; i++)
            {
                if (elementsInChildren[i].withDelay)
                    elementsInChildren[i].showDelay = animationP.animationShowDuration + animationP.showDelay + currentValue;

                currentValue += step;
            }
        }

        GUILayout.Space(5);
    }

    private void AutomateChildrenHideDelays_BUTTON()
    {
        if (GUILayout.Button("Automate Hide Delays In Children"))
        {
            AnimationP[] elementsInChildren = Selection.activeGameObject.GetComponentsInChildren<AnimationP>();

            float step = (animationP.hideDelay / elementsInChildren.Length);
            float currentValue = 0;

            elementsInChildren[elementsInChildren.Length - 1].hideDelay = 0;
            for (int i = elementsInChildren.Length - 2; i >= 1 ; i--)
            {
                if (elementsInChildren[i].withDelay)
                    elementsInChildren[i].hideDelay = step + currentValue;

                currentValue += step;
            }
        }

        GUILayout.Space(20);
    }
}