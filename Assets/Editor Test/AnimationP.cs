using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimationP : MonoBehaviour
{
    public delegate void ItemVisibility();
    public event ItemVisibility onItemShow;
    public event ItemVisibility onItemHide;

    public AnimationType animationType;
    public AnimationFromCornerType animationFromCornerType;

    public float animationDuration = 3;

    public bool withDelay;
    public float showDelay;
    public float hideDelay;

    public float elasticityPower = 1;

    private RectTransform rectTransform;
    private Vector3 initialPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown("z"))
            ShowMenu();
        if (Input.GetKeyDown("x"))
            HideMenu();
    }

    public void ShowMenu()
    {
        switch (animationType)
        {
            case (AnimationType.Scale):
                StartCoroutine(AnimateScale_SHOW());
                break;
            case (AnimationType.ScaleElastic):
                StartCoroutine(AnimateElasticScale_SHOW());
                break;
            case (AnimationType.Fade):
                StartCoroutine(AnimateFadeIn_SHOW());
                break;
            case (AnimationType.ShowFromCorner):
                StartCoroutine(AnimateFromCornerWithScale_SHOW());
                break;
        }

        onItemShow.Invoke();
    }

    public void HideMenu()
    {
        switch (animationType)
        {
            case (AnimationType.Scale):
                StartCoroutine(AnimateScale_HIDE());
                break;
            case (AnimationType.ScaleElastic):
                StartCoroutine(AnimateElasticScale_HIDE());
                break;
            case (AnimationType.Fade):
                StartCoroutine(AnimateFadeIn_HIDE());
                break;
            case (AnimationType.ShowFromCorner):
                StartCoroutine(AnimateFromCornerWithScale_HIDE());
                break;
        }

        onItemHide.Invoke();
    }

    #region Show Coroutines

    private IEnumerator AnimateFromCornerWithScale_SHOW()
    {
        if (withDelay)
            yield return (new WaitForSeconds(showDelay));

        float startPositionX = 0;
        float startPositionY = 0;

        Image[] imagesInMenu = GetComponentsInChildren<Image>();

        Color[] startColors = new Color[imagesInMenu.Length];
        Color[] endColors = new Color[imagesInMenu.Length];

        // Here we get start color to a color with alpha = 0, and the end color to a color with alpha = 1.
        for (int i = 0; i < imagesInMenu.Length; i++)
        {
            startColors[i] = imagesInMenu[i].color;
            startColors[i].a = 0;

            endColors[i] = imagesInMenu[i].color;
            endColors[i].a = 1;
        }

        switch (animationFromCornerType)
        {
            case (AnimationFromCornerType.ShowFromBottomRight):
                startPositionX = Screen.width;
                startPositionY = 0;
                break;
            case (AnimationFromCornerType.ShowFromBottomLeft):
                startPositionX = 0;
                startPositionY = 0;
                break;
            case (AnimationFromCornerType.ShowFromTopRight):
                startPositionX = Screen.width;
                startPositionY = Screen.height;
                break;
            case (AnimationFromCornerType.ShowFromTopLeft):
                startPositionX = 0;
                startPositionY = Screen.height;
                break;
        }

        // Starting animating.
        float startTime = Time.time;

        Vector3 startPos = new Vector3(startPositionX, startPositionY, 0);
        Vector3 targetPosition = initialPosition;

        rectTransform.position = startPos;

        while (Time.time <= startTime + animationDuration)
        {
            float t = (Time.time - startTime) / animationDuration;

            // Here we Lerp the position and the scale as well.
            rectTransform.position = Vector3.Lerp(startPos, targetPosition, t);
            rectTransform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);

            // Here we lerp the color from totally transparent to totally visible.
            for (int i = 0; i < imagesInMenu.Length; i++)
                imagesInMenu[i].color = Color.Lerp(startColors[i], endColors[i], t);

            yield return (null);
        }

        // Here we set the values to their end so we avoid the missing final step.
        rectTransform.position = targetPosition;
        rectTransform.localScale = Vector3.one;

        for (int i = 0; i < imagesInMenu.Length; i++)
            imagesInMenu[i].color = endColors[i];
    }

    private IEnumerator AnimateElasticScale_SHOW()
    {
        if (withDelay)
            yield return (new WaitForSeconds(showDelay));

        Vector3 startPosition = rectTransform.position;
        float startTime = Time.time;

        while (Time.time <= startTime + animationDuration)
        {
            float t = (Time.time - startTime) / animationDuration;

            rectTransform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one + new Vector3(0.1f, 0.1f, 0.1f) * elasticityPower, t);

            yield return (null);
        }

        rectTransform.localScale = Vector3.one + new Vector3(0.1f, 0.1f, 0.1f) * elasticityPower;

        startTime = Time.time;
        while (Time.time <= startTime + animationDuration / 5)
        {
            float t = (Time.time - startTime) / (animationDuration / 5);
            rectTransform.localScale = Vector3.Lerp(Vector3.one + new Vector3(0.1f, 0.1f, 0.1f) * elasticityPower, Vector3.one, t);
            yield return (null);
        }
    }

    private IEnumerator AnimateScale_SHOW()
    {
        if (withDelay)
            yield return (new WaitForSeconds(showDelay));

        Vector3 startPosition = rectTransform.position;
        float startTime = Time.time;

        while (Time.time <= startTime + animationDuration)
        {
            float t = (Time.time - startTime) / animationDuration;

            rectTransform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);

            yield return (null);
        }

        rectTransform.localScale = Vector3.one;
    }

    private IEnumerator AnimateFadeIn_SHOW()
    {
        Image[] images = GetComponentsInChildren<Image>();
        Color[] startColors = new Color[images.Length];
        Color[] endColors = new Color[images.Length];

        for (int i = 0; i < images.Length; i++)
        {
            startColors[i] = images[i].color;
            startColors[i].a = 0;

            endColors[i] = new Color(images[i].color.r, images[i].color.g, images[i].color.b, 1);
        }

        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, 0);
        }

        if (withDelay)
            yield return (new WaitForSeconds(showDelay));

        float startTime = Time.time;

        while (Time.time < startTime + animationDuration)
        {
            float t = (Time.time - startTime) / animationDuration;
            for (int i = 0; i < images.Length; i++)
            {
                images[i].color = Color.Lerp(startColors[i], endColors[i], t);
            }

            yield return (null);
        }
        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = endColors[i];
        }
    }

    #endregion

    //=======================================================================================
    //=======================================================================================

    #region Hide Coroutines

    private IEnumerator AnimateFadeIn_HIDE()
    {
        if (withDelay)
            yield return (new WaitForSeconds(hideDelay));

        Image[] images = GetComponentsInChildren<Image>();
        Color[] startColors = new Color[images.Length];
        Color[] endColors = new Color[images.Length];
        float startTime = Time.time;

        for (int i = 0; i < images.Length; i++)
        {
            startColors[i] = images[i].color;

            endColors[i] = images[i].color;
            endColors[i].a = 0;
        }

        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = startColors[i];
        }

        while (Time.time < startTime + animationDuration)
        {
            float t = (Time.time - startTime) / animationDuration;
            for (int i = 0; i < images.Length; i++)
            {
                images[i].color = Color.Lerp(startColors[i], endColors[i], t);
            }

            yield return (null);
        }
        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = endColors[i];
        }
    }

    private IEnumerator AnimateScale_HIDE()
    {
        if (withDelay)
            yield return (new WaitForSeconds(hideDelay));

        Vector3 startPosition = rectTransform.position;
        float startTime = Time.time;

        while (Time.time <= startTime + animationDuration)
        {
            float t = (Time.time - startTime) / animationDuration;

            rectTransform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);

            yield return (null);
        }

        rectTransform.localScale = Vector3.zero;
    }

    private IEnumerator AnimateElasticScale_HIDE()
    {
        if (withDelay)
            yield return (new WaitForSeconds(hideDelay));

        Vector3 startPosition = rectTransform.position;
        float startTime = Time.time;

        while (Time.time <= startTime + animationDuration)
        {
            float t = (Time.time - startTime) / animationDuration;

            rectTransform.localScale = Vector3.Lerp(Vector3.one + new Vector3(0.1f, 0.1f, 0.1f) * elasticityPower, Vector3.zero, t);

            yield return (null);
        }

        rectTransform.localScale = Vector3.zero;
    }

    private IEnumerator AnimateFromCornerWithScale_HIDE()
    {
        if (withDelay)
            yield return (new WaitForSeconds(hideDelay));

        float endX = 0;
        float endY = 0;
        float startX = Screen.width / 2;
        float startY = Screen.height / 2;

        Image[] images = GetComponentsInChildren<Image>();
        Color[] startColors = new Color[images.Length];
        Color[] endColors = new Color[images.Length];

        for (int i = 0; i < images.Length; i++)
        {
            startColors[i] = images[i].color;
            startColors[i].a = 1;

            endColors[i] = images[i].color;
            endColors[i].a = 0;
        }

        if (animationFromCornerType == AnimationFromCornerType.ShowFromBottomRight)
        {
            endX = Screen.width;
            endY = 0;
        }
        else if (animationFromCornerType == AnimationFromCornerType.ShowFromBottomLeft)
        {
            endX = 0;
            endY = 0;
        }
        else if (animationFromCornerType == AnimationFromCornerType.ShowFromTopRight)
        {
            endX = Screen.width;
            endY = Screen.height;
        }
        else if (animationFromCornerType == AnimationFromCornerType.ShowFromTopLeft)
        {
            endX = 0;
            endY = Screen.height;
        }

        float startTime = Time.time;

        Vector3 startPos = initialPosition;
        Vector3 targetPosition = new Vector3(endX, endY, 0);
        rectTransform.position = startPos;

        while (Time.time <= startTime + animationDuration)
        {
            float t = (Time.time - startTime) / animationDuration;
            rectTransform.position = Vector3.Lerp(startPos, targetPosition, t);
            rectTransform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);

            for (int i = 0; i < images.Length; i++)
            {
                images[i].color = Color.Lerp(startColors[i], endColors[i], t);
            }

            yield return (null);
        }

        rectTransform.position = targetPosition;
        rectTransform.localScale = Vector3.zero;

        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = endColors[i];
        }
    }

    #endregion
}

public enum AnimationType
{
    Scale, ScaleElastic, Fade, ShowFromCorner
}

public enum AnimationFromCornerType
{
    ShowFromBottomRight, ShowFromTopRight, ShowFromBottomLeft, ShowFromTopLeft
}