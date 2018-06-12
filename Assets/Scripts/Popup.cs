using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public enum PopupType
    {
        Text, WithOk, WithOkCancel
    }

    public static Popup Instance;

    private static bool popupInQueue = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void Show(string text)
    {
        if (popupInQueue)
        {
           StartCoroutine(ShowCoroutine(text));
        }

        else
        {
            ShowPopup(text);
        }

    }

    private IEnumerator ShowCoroutine(string text)
    {
        yield return (new WaitForSeconds(1.5f));
        popupInQueue = false;
        ShowPopup(text);
    }

    private void ShowPopup(string text)
    {
        popupInQueue = true;
        GameObject popupPrefab = Resources.Load("Prefabs/Popup") as GameObject;

        GameObject popup = Instantiate(popupPrefab, GameObject.FindGameObjectWithTag("UICanvas").transform, false);
        Text popupText = popup.transform.Find("Text").GetComponent<Text>();
        popupText.text = text;

        Destroy(popup, 5);
    }
}