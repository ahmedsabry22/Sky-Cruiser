using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AnimationPElement))]
public class AnimationPManager : MonoBehaviour
{
    [HideInInspector] public AnimationPElement[] childrenElements;
    public bool showMenuOnStart;

    private void Start()
    {
        UpdateElementsInChildren();

        if (showMenuOnStart)
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