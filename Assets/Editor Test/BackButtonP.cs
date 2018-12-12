using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AnimationP))]
public class BackButtonP : MonoBehaviour
{
    [Tooltip("if false, it will work only on devices that has back button, and ESC button on standalone devices. If true, there will be a button on the screen")]
    public bool withGraphic;

    public Positions position;

    public GraphicType graphicType;

    [Range(0.1f, 1)] public float scale = 0.5f;

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

        backButtonManager = Resources.Load<BackButtonManager>("Back Button Manager");
        StartCoroutine(InstantiateManager());

        backButtonPrefab = Resources.Load<Button>("Back Button");

        animationP = GetComponent<AnimationP>();
        animationP.OnShow.AddListener(() => BackButtonManager.Instance.AddButtonToList(this));
        animationP.OnHide.AddListener(() => BackButtonManager.Instance.RemoveButtonFromList(this));
    }

    private IEnumerator InstantiateManager()
    {

        if (BackButtonManager.Instance == null)
        {
            string backButtonManagerName = backButtonManager.gameObject.name;
            backButtonManager = Instantiate(backButtonManager);
            backButtonManager.gameObject.name = backButtonManagerName;
        }
        yield return (new WaitForEndOfFrame());
            
    }

    private void Start()
    {
        if (withGraphic)
        {
            // Instantiate Button and make it child of this game object
            backButton = Instantiate(backButtonPrefab, transform, false);
            backButton.onClick.AddListener(DoBack);
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
                    backButton.GetComponent<RectTransform>().localPosition = new Vector3(rectTransform.rect.width / 2, rectTransform.rect.height / 2, 0);
                    break;
                case (Positions.TopLeft):
                    // Instantiate at top left
                    backButton.GetComponent<RectTransform>().localPosition = new Vector3(-rectTransform.rect.width / 2, rectTransform.rect.height / 2, 0);
                    break;
                case (Positions.BottomRight):
                    // Instantiate at bottom right
                    backButton.GetComponent<RectTransform>().localPosition = new Vector3(rectTransform.rect.width / 2, -rectTransform.rect.height / 2, 0);
                    break;
                case (Positions.BottomLeft):
                    // Instantiate at bottom left
                    backButton.GetComponent<RectTransform>().localPosition = new Vector3(-rectTransform.rect.width / 2, -rectTransform.rect.height / 2, 0);
                    break;
            }
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
        BackButtonManager.Instance.DoBack(this);
    }

    public enum Positions { TopRight, TopLeft, BottomRight, BottomLeft }

    public enum GraphicType { Image, Text }
}