using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackButtonManager : MonoBehaviour
{
    public static BackButtonManager Instance;

    private List<BackButtonP> ActiveButtons = new List<BackButtonP>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            DontDestroyOnLoad(gameObject);
    }

    public void AddButtonToList(BackButtonP backButton)
    {
        if (!ActiveButtons.Contains(backButton))
        {
            ActiveButtons.Add(backButton);
        }
    }

    public void RemoveButtonFromList(BackButtonP backButton)
    {
        if (ActiveButtons.Contains(backButton))
        {
            ActiveButtons.Remove(backButton);
        }
    }

    public void DoBack(BackButtonP backButton, bool controlChildren)
    {
        if (controlChildren)
        {
            AnimationP[] childrenElements = backButton.GetComponentsInChildren<AnimationP>();

            foreach (var element in childrenElements)
                element.HideMenu();
        }
        else
        {
            int targetIndex = ActiveButtons.Count - 1;

            if (ActiveButtons.Contains(backButton))
            {
                if (backButton == ActiveButtons[targetIndex] && backButton.gameObject.activeSelf)
                {
                    ActiveButtons[targetIndex].GetComponent<AnimationP>().HideMenu();
                }
            }
        }
    }

    public void DoBackOnCurrentObject(BackButtonP backButton, bool controlChildren)
    {
        if (controlChildren)
        {
            AnimationP[] childrenElements = backButton.GetComponentsInChildren<AnimationP>();

            foreach (var element in childrenElements)
            {
                element.HideMenu();
            }
        }
        else
        {
            int targetIndex = ActiveButtons.IndexOf(backButton);

            if (ActiveButtons.Contains(backButton))
            {
                if (backButton.gameObject.activeSelf)
                {
                    ActiveButtons[targetIndex].GetComponent<AnimationP>().HideMenu();
                }
            }
        }
    }
}