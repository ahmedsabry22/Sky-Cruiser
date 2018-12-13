using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class AnimationP : MonoBehaviour
{
    public UnityEvent OnShow;
    public UnityEvent OnHide;
    public UnityEvent OnShowComplete;
    public UnityEvent OnHideComplete;

    public bool showOnStart;

    public AnimationType showAnimationType;
    public AnimationType hideAnimationType;
    public AnimationFromCornerType animationFromCornerType;
    public AnimationFromCornerType animationToCornerType;

    public bool fadeChildren;

    public float animationShowDuration = 0.5f;
    public float animationHideDuration = 0.5f;

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

        if (showOnStart)
            ShowMenu();
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
        switch (showAnimationType)
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
            case (AnimationType.FromCornerWithScale):
                StartCoroutine(AnimateFromCornerWithScale_SHOW());
                break;
        }

        if (OnShow != null)
            OnShow.Invoke();
    }

    public void HideMenu()
    {
        switch (hideAnimationType)
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
            case (AnimationType.FromCornerWithScale):
                StartCoroutine(AnimateToCornerWithScale_HIDE());
                break;
        }

        if (OnHide != null)
            OnHide.Invoke();
    }

    #region Show Coroutines

    private IEnumerator AnimateFromCornerWithScale_SHOW()
    {
        ResetScale(ResetOptions.Zero);
        ResetColor(ResetOptions.Zero);

        #region Initialization Part
        float startPositionX = 0;
        float startPositionY = 0;

        Graphic[] imagesInMenu = GetComponentsInChildren<Graphic>();

        // Here we get start color to a color with alpha = 0, and the end color to a color with alpha = 1.
        Color[] startColors = new Color[imagesInMenu.Length];
        Color[] endColors = new Color[imagesInMenu.Length];

        for (int i = 0; i < imagesInMenu.Length; i++)
        {
            startColors[i] = imagesInMenu[i].color;
            startColors[i].a = 0;

            endColors[i] = imagesInMenu[i].color;
            endColors[i].a = 1;
        }

        switch (animationFromCornerType)
        {
            case (AnimationFromCornerType.BottomRight):
                startPositionX = Screen.width;
                startPositionY = 0;
                break;
            case (AnimationFromCornerType.BottomLeft):
                startPositionX = 0;
                startPositionY = 0;
                break;
            case (AnimationFromCornerType.TopRight):
                startPositionX = Screen.width;
                startPositionY = Screen.height;
                break;
            case (AnimationFromCornerType.TopLeft):
                startPositionX = 0;
                startPositionY = Screen.height;
                break;
            case (AnimationFromCornerType.Up):
                startPositionX = initialPosition.x;
                startPositionY = Screen.height;
                break;
            case (AnimationFromCornerType.Bottom):
                startPositionX = initialPosition.x;
                startPositionY = 0;
                break;
            case (AnimationFromCornerType.Left):
                startPositionX = 0;
                startPositionY = initialPosition.y;
                break;
            case (AnimationFromCornerType.Right):
                startPositionX = Screen.width;
                startPositionY = initialPosition.y;
                break;
        }

        Vector3 startPos = new Vector3(startPositionX, startPositionY, 0);
        Vector3 targetPosition = initialPosition;

        rectTransform.position = startPos;
        rectTransform.localScale = Vector3.zero;

        #endregion

        if (withDelay)
            yield return (new WaitForSeconds(showDelay));

        // Starting animating.
        float startTime = Time.time;

        rectTransform.position = startPos;

        while (Time.time <= startTime + animationShowDuration)
        {
            float t = (Time.time - startTime) / animationShowDuration;

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

        if (OnShowComplete != null)
            OnShowComplete.Invoke();
    }

    private IEnumerator AnimateElasticScale_SHOW()
    {
        ResetPosition();
        ResetScale(ResetOptions.Zero);
        ResetColor(ResetOptions.One);

        Vector3 startPosition = rectTransform.position;
        rectTransform.localScale = Vector3.zero;

        if (withDelay)
            yield return (new WaitForSeconds(showDelay));

        float startTime = Time.time;

        while (Time.time <= startTime + animationShowDuration)
        {
            float t = (Time.time - startTime) / animationShowDuration;

            rectTransform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one + new Vector3(0.1f, 0.1f, 0.1f) * elasticityPower, t);

            yield return (null);
        }

        rectTransform.localScale = Vector3.one + new Vector3(0.1f, 0.1f, 0.1f) * elasticityPower;

        startTime = Time.time;
        while (Time.time <= startTime + animationShowDuration / 5)
        {
            float t = (Time.time - startTime) / (animationShowDuration / 5);
            rectTransform.localScale = Vector3.Lerp(Vector3.one + new Vector3(0.1f, 0.1f, 0.1f) * elasticityPower, Vector3.one, t);
            yield return (null);
        }

        if (OnShowComplete != null)
            OnShowComplete.Invoke();
    }

    private IEnumerator AnimateScale_SHOW()
    {
        ResetPosition();
        ResetScale(ResetOptions.Zero);
        ResetColor(ResetOptions.One);

        //rectTransform.localScale = Vector3.zero;

        if (withDelay)
            yield return (new WaitForSeconds(showDelay));

        Vector3 startPosition = rectTransform.position;
        float startTime = Time.time;

        while (Time.time <= startTime + animationShowDuration)
        {
            float t = (Time.time - startTime) / animationShowDuration;

            rectTransform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);

            yield return (null);
        }

        rectTransform.localScale = Vector3.one;

        if (OnShowComplete != null)
            OnShowComplete.Invoke();
    }

    private IEnumerator AnimateFadeIn_SHOW()
    {
        ResetPosition();
        ResetScale(ResetOptions.One);
        ResetColor(ResetOptions.Zero);

        #region Initialization Part
        Graphic[] images;

        if (fadeChildren)
            images = GetComponentsInChildren<Graphic>();
        else
            images = GetComponents<Graphic>();

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
            images[i].color = startColors[i];
        }
        #endregion

        if (withDelay)
            yield return (new WaitForSeconds(showDelay));

        float startTime = Time.time;

        while (Time.time < startTime + animationShowDuration)
        {
            float t = (Time.time - startTime) / animationShowDuration;
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

        if (OnShowComplete != null)
            OnShowComplete.Invoke();
    }

    #endregion

    //=======================================================================================
    //=======================================================================================

    #region Hide Coroutines

    private IEnumerator AnimateFadeIn_HIDE()
    {
        #region Initialization Part
        Graphic[] images;

        if (fadeChildren)
            images = GetComponentsInChildren<Graphic>();
        else
            images = GetComponents<Graphic>();

        Color[] startColors = new Color[images.Length];
        Color[] endColors = new Color[images.Length];

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
        #endregion

        if (withDelay)
            yield return (new WaitForSeconds(hideDelay));

        float startTime = Time.time;

        while (Time.time < startTime + animationHideDuration)
        {
            float t = (Time.time - startTime) / animationHideDuration;
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

        if (OnHideComplete != null)
            OnHideComplete.Invoke();
    }

    private IEnumerator AnimateScale_HIDE()
    {
        Vector3 startPosition = rectTransform.position;
        rectTransform.localScale = Vector3.one;

        if (withDelay)
            yield return (new WaitForSeconds(hideDelay));

        float startTime = Time.time;

        while (Time.time <= startTime + animationHideDuration)
        {
            float t = (Time.time - startTime) / animationHideDuration;

            rectTransform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);

            yield return (null);
        }

        rectTransform.localScale = Vector3.zero;

        if (OnHideComplete != null)
            OnHideComplete.Invoke();
    }

    private IEnumerator AnimateElasticScale_HIDE()
    {
        Vector3 startPosition = rectTransform.position;
        rectTransform.localScale = Vector3.one;

        if (withDelay)
            yield return (new WaitForSeconds(hideDelay));

        float startTime = Time.time;

        while (Time.time <= startTime + animationHideDuration / 5)
        {
            float t = (Time.time - startTime) / (animationHideDuration / 5);

            rectTransform.localScale = Vector3.Lerp(Vector3.one, Vector3.one + new Vector3(0.1f, 0.1f, 0.1f) * elasticityPower, t);

            yield return (null);
        }

        startTime = Time.time;

        while (Time.time <= startTime + animationHideDuration)
        {
            float t = (Time.time - startTime) / (animationHideDuration);

            rectTransform.localScale = Vector3.Lerp(Vector3.one + new Vector3(0.1f, 0.1f, 0.1f) * elasticityPower, Vector3.zero, t);

            yield return (null);
        }

        rectTransform.localScale = Vector3.zero;

        if (OnHideComplete != null)
            OnHideComplete.Invoke();
    }

    private IEnumerator AnimateToCornerWithScale_HIDE()
    {
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

        switch (animationToCornerType)
        {
            case (AnimationFromCornerType.BottomRight):
                endX = Screen.width;
                endY = 0;
                break;
            case (AnimationFromCornerType.BottomLeft):
                endX = 0;
                endY = 0;
                break;
            case (AnimationFromCornerType.TopRight):
                endX = Screen.width;
                endY = Screen.height;
                break;
            case (AnimationFromCornerType.TopLeft):
                endX = 0;
                endY = Screen.height;
                break;
            case (AnimationFromCornerType.Up):
                endX = Screen.width / 2;
                endY = Screen.height;
                break;
            case (AnimationFromCornerType.Bottom):
                endX = Screen.width / 2;
                endY = 0;
                break;
            case (AnimationFromCornerType.Left):
                endX = 0;
                endY = initialPosition.y;
                break;
            case (AnimationFromCornerType.Right):
                endX = Screen.width;
                endY = initialPosition.y;
                break;
        }

        Vector3 startPos = initialPosition;
        Vector3 targetPosition = new Vector3(endX, endY, 0);
        rectTransform.position = startPos;

        if (withDelay)
            yield return (new WaitForSeconds(hideDelay));

        float startTime = Time.time;

        while (Time.time <= startTime + animationHideDuration)
        {
            float t = (Time.time - startTime) / animationHideDuration;
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

        if (OnHideComplete != null)
            OnHideComplete.Invoke();
    }

    #endregion

    private void ResetPosition()
    {
        transform.position = initialPosition;
    }

    private void ResetColor(ResetOptions resetOption)
    {
        if (resetOption == ResetOptions.Zero)
        {
            Graphic[] images;

            if (fadeChildren)
                images = GetComponentsInChildren<Graphic>();
            else
                images = GetComponents<Graphic>();

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
                images[i].color = startColors[i];
            }
        }
        else if (resetOption == ResetOptions.One)
        {
            Graphic[] images;

            if (fadeChildren)
                images = GetComponentsInChildren<Graphic>();
            else
                images = GetComponents<Graphic>();

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
                images[i].color = endColors[i];
            }
        }
    }

    private void ResetScale(ResetOptions resetOption)
    {
        if (resetOption == ResetOptions.Zero)
        {
            transform.localScale = Vector3.zero;
        }
        else if (resetOption == ResetOptions.One)
        {
            transform.localScale = Vector3.one;
        }
    }

    private enum ResetOptions
    {
        Zero, One
    }
}

public enum AnimationType
{
    Scale, ScaleElastic, Fade, FromCornerWithScale
}

public enum AnimationFromCornerType
{
    BottomRight, TopRight, BottomLeft, TopLeft, Up, Bottom, Left, Right
}