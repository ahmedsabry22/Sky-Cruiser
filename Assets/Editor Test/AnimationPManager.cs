using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AnimationPElement))]
public class AnimationPManager : MonoBehaviour
{
    [HideInInspector] public AnimationPElement[] childrenElements;
    [Tooltip("Wheather or not to show the animation when the menu is enabled")]public bool showMenuOnEnable;

    private void Start()
    {
        UpdateElementsInChildren();

        if (showMenuOnEnable)
        {
            ShowMenu();
        }
    }

    public void ShowMenu()
    {
        foreach (var element in childrenElements)
        {
            if (element.showOnStart)
                element.ShowElement();
        }
    }

    public void UpdateElementsInChildren()
    {
        childrenElements = GetComponentsInChildren<AnimationPElement>();
    }
}