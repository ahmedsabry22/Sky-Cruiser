﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AnimationP))]
public class BackButtonP : MonoBehaviour
{
    [Tooltip("if false, it will work only on devices that has back button, and ESC button on standalone devices. If true, there will be a button on the screen")]
    public bool withGraphic;

    public bool controlChildren = true;

    public Positions position;

    public GraphicType graphicType;

    [Range(0.1f, 1)] public float scale = 0.5f;

    public float offsetX = 0, offsetY = 0;

    public string buttonText = "Back";
    public Sprite graphicSprite;

    [HideInInspector] public BackButtonManager backButtonManager;

    private Button backButtonPrefab;
    private Button backButton;

    private AnimationP animationP;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        InstantiateManager();

        if (withGraphic)
            InstantiateBackButton();

        animationP = GetComponent<AnimationP>();
        animationP.OnShow.AddListener(() => BackButtonManager.Instance.AddButtonToList(this));
        animationP.OnHide.AddListener(() => BackButtonManager.Instance.RemoveButtonFromList(this));
    }

    private void InstantiateManager()
    {
        backButtonManager = Resources.Load<BackButtonManager>("Back Button Manager");

        if (BackButtonManager.Instance == null)
        {
            string backButtonManagerName = backButtonManager.gameObject.name;
            backButtonManager = Instantiate(backButtonManager);
            backButtonManager.gameObject.name = backButtonManagerName;
        }
    }

    private void InstantiateBackButton()
    {
        // Instantiate Button and make it child of this game object
        backButtonPrefab = Resources.Load<Button>("Back Button");

        backButton = Instantiate(backButtonPrefab, transform, false);
        backButton.onClick.AddListener(DoBackOnThisObject);
        backButton.name = backButtonPrefab.name;
        backButton.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, scale);

        Image img = backButton.transform.Find("Image").GetComponent<Image>();
        Text txt = backButton.transform.Find("Text").GetComponent<Text>();

        if (graphicType == GraphicType.Image)
        {
            img.sprite = graphicSprite;
            backButton.targetGraphic = img;
            img.color = Color.white;

            txt.gameObject.SetActive(false);
        }
        else if (graphicType == GraphicType.Text)
        {
            txt.text = buttonText;
            backButton.targetGraphic = txt;

            img.gameObject.SetActive(false);
        }

        switch (position)
        {
            case (Positions.TopRight):
                // Instantiate at top right
                backButton.GetComponent<RectTransform>().localPosition = new Vector3(rectTransform.rect.width / 2 - offsetX, rectTransform.rect.height / 2 - offsetY, 0);
                break;
            case (Positions.TopLeft):
                // Instantiate at top left
                print("top left");
                backButton.GetComponent<RectTransform>().localPosition = new Vector3(-rectTransform.rect.width / 2 + offsetX, rectTransform.rect.height / 2 - offsetY, 0);
                break;
            case (Positions.BottomRight):
                // Instantiate at bottom right
                backButton.GetComponent<RectTransform>().localPosition = new Vector3(rectTransform.rect.width / 2 - offsetX, -rectTransform.rect.height / 2 + offsetY, 0);
                break;
            case (Positions.BottomLeft):
                // Instantiate at bottom left
                backButton.GetComponent<RectTransform>().localPosition = new Vector3(-rectTransform.rect.width / 2 + offsetX, -rectTransform.rect.height / 2 + offsetY, 0);
                break;
        }
    }

    private void OnEnable()
    {
        if (withGraphic && backButton == null)
            InstantiateBackButton();

        BackButtonManager.Instance.AddButtonToList(this);
    }

    private void OnDisable()
    {
        BackButtonManager.Instance.RemoveButtonFromList(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DoBack();
        }
    }

    public void DoBack()
    {
        BackButtonManager.Instance.DoBack(this, controlChildren);
    }

    public void DoBackOnThisObject()
    {
        BackButtonManager.Instance.DoBackOnCurrentObject(this, controlChildren);
    }

    public enum Positions { TopRight, TopLeft, BottomRight, BottomLeft }

    public enum GraphicType { Image, Text }
}