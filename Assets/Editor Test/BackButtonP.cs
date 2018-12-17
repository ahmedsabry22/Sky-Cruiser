using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AnimationPElement))]
public class BackButtonP : MonoBehaviour
{
    [Tooltip("if false, it will work only on devices that has back button, and ESC button on standalone devices. If true, there will be a button on the screen")]
    public bool withGraphic;

    public bool controlChildren = true;

    public Positions position;

    public GraphicType graphicType;

    public Color imageColor = Color.white;
    public Color textColor = Color.white;

    [Range(0.1f, 1)] public float scale = 0.5f;

    public float offsetX = 0, offsetY = 0;

    public string buttonText = "Back";
    public Sprite graphicSprite;

    [HideInInspector] public BackButtonManager backButtonManager;

    private Button backButtonPrefab;
    private Button backButton;

    private AnimationPElement animationP;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        animationP = GetComponent<AnimationPElement>();

        InstantiateManager();
    }

    private void Start()
    {
        if (withGraphic)
        {
            InstantiateBackButton();
            animationP.OnShow.AddListener(SetPositionAndScale);
        }

        animationP.OnShow.AddListener(() => BackButtonManager.Instance.AddButtonToList(this));
        animationP.OnShow.AddListener(() => backButton.gameObject.SetActive(true));
        animationP.OnShow.AddListener(() => SetGraphics());
        animationP.OnShow.AddListener(() => SetPositionAndScale());

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

        if (backButton == null)
        {
            backButtonPrefab = Resources.Load<Button>("Back Button");

            backButton = Instantiate(backButtonPrefab, transform, false);
            backButton.onClick.AddListener(DoBackOnThisObject);
            backButton.name = backButtonPrefab.name;

            SetGraphics();
            SetPositionAndScale();
        }
        else
        {
            SetGraphics();
            SetPositionAndScale();
        }
    }

    private void OnEnable()
    {
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

    private void SetPositionAndScale()
    {
        backButton.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, scale);

        switch (position)
        {
            case (Positions.TopRight):
                // Instantiate at top right
                backButton.GetComponent<RectTransform>().localPosition = new Vector3(rectTransform.rect.width / 2 - offsetX, rectTransform.rect.height / 2 - offsetY, 0);
                break;
            case (Positions.TopLeft):
                // Instantiate at top left
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

    private void SetGraphics()
    {
        Image img = backButton.transform.Find("Image").GetComponent<Image>();
        Text txt = backButton.transform.Find("Text").GetComponent<Text>();

        if (graphicType == GraphicType.Image)
        {
            img.sprite = graphicSprite;
            img.color = imageColor;
            backButton.targetGraphic = img;

            txt.gameObject.SetActive(false);
        }
        else if (graphicType == GraphicType.Text)
        {
            txt.text = buttonText;
            txt.color = textColor;
            backButton.targetGraphic = txt;

            img.gameObject.SetActive(false);
        }
        else if (graphicType == GraphicType.Both)
        {
            img.sprite = graphicSprite;
            img.color = imageColor;
            backButton.targetGraphic = img;

            txt.text = buttonText;
            txt.color = textColor;
            backButton.targetGraphic = txt;
        }
    }

    public enum Positions { TopRight, TopLeft, BottomRight, BottomLeft }

    public enum GraphicType { Image, Text, Both }
}