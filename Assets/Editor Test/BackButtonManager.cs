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

    public void DoBack(BackButtonP backButton)
    {
        int targetIndex = ActiveButtons.Count - 1;

        if (ActiveButtons.Contains(backButton))
        {
            if (backButton == ActiveButtons[targetIndex] && backButton.gameObject.activeSelf)
            {
                //backButton.gameObject.SetActive(false);
                ActiveButtons[targetIndex].GetComponent<AnimationP>().HideMenu();
            }
        }
    }
}