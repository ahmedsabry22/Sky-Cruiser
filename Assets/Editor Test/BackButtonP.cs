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

    public string buttonText = "Back";
    public Sprite graphicSprite;

    [HideInInspector] public BackButtonManager backButtonManager;

    private Button backButtonPrefab;
    private Button backButton;

    private AnimationP animationP;

    private void Awake()
    {
        backButtonManager = Resources.Load<BackButtonManager>("Back Button Manager");
        StartCoroutine(InstantiateManager());

        backButtonPrefab = Resources.Load<Button>("Back Button");

        animationP = GetComponent<AnimationP>();
        animationP.onItemShow.AddListener(() => BackButtonManager.Instance.AddButtonToList(this));
        animationP.onItemHide.AddListener(() => BackButtonManager.Instance.RemoveButtonFromList(this));
    }

    private IEnumerator InstantiateManager()
    {

        if (BackButtonManager.Instance == null)
        {
            string backButtonManagerName = backButtonManager.gameObject.name;
            backButtonManager = Instantiate(backButtonManager);
            backButtonManager.gameObject.name = backButtonManagerName;

            switch (position)
            {
                case (Positions.TopRight):
                    // Instantiate at top right
                    break;
                case (Positions.TopLeft):
                    // Instantiate at top left
                    break;
                case (Positions.BottomRight):
                    // Instantiate at bottom right
                    break;
                case (Positions.BottomLeft):
                    // Instantiate at bottom left
                    break;
            }
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