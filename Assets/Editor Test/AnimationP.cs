using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimationP : MonoBehaviour
{
    public delegate void ItemVisibility();
    public event ItemVisibility onItemShow;
    public event ItemVisibility onItemHide;

    public float animationDuration = 3;

    public AnimationType animationType;
    public AnimationFromCornerType animationFromCornerType;

    public float elasticPower = 1;

    public bool withDelay;
    public float showDelay;
    public float hideDelay;


    private RectTransform rect;

    private Vector3 initialPosition;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Start()
    {
        initialPosition = transform.position;

        onItemShow += () => print("SHOW event invoked");
        onItemHide += () => print("HIDE event invoked");
    }

    private void Update()
    {
        if (Input.GetKeyDown("z"))
            ShowItem();
        if (Input.GetKeyDown("x"))
            HideItem();
    }

    public void ShowItem()
    {
        if (animationType == AnimationType.Scale)
        {
            StartCoroutine(AnimateScale_SHOW());
        }
        else if (animationType == AnimationType.ScaleElastic)
        {
            StartCoroutine(AnimateElasticScale_SHOW());
        }
        else if (animationType == AnimationType.Fade)
        {
            StartCoroutine(FadeIn_SHOW());
        }
        else if (animationType == AnimationType.ShowFromCorner)
        {
            StartCoroutine(AnimateFromCornerWithScale_SHOW());
        }

        onItemShow.Invoke();
    }

    public void HideItem()
    {
        if (animationType == AnimationType.Scale)
        {
            StartCoroutine(AnimateScale_HIDE());
        }
        else if (animationType == AnimationType.ScaleElastic)
        {
            StartCoroutine(AnimateElasticScale_HIDE());
        }
        else if (animationType == AnimationType.Fade)
        {
            StartCoroutine(FadeIn_HIDE());
        }
        else if (animationType == AnimationType.ShowFromCorner)
        {
            StartCoroutine(AnimateFromCornerWithScale_HIDE());
        }

        onItemHide.Invoke();
    }

    #region Show Coroutines

    private IEnumerator AnimateFromCornerWithScale_SHOW()
    {
        if (withDelay)
            yield return (new WaitForSeconds(showDelay));

        float startX = 0;
        float startY = 0;

        Image[] images = GetComponentsInChildren<Image>();
        Color[] startColors = new Color[images.Length];
        Color[] endColors = new Color[images.Length];

        for (int i = 0; i < images.Length; i++)
        {
            startColors[i] = images[i].color;
            startColors[i].a = 0;

            endColors[i] = images[i].color;
            endColors[i].a = 1;
        }

        if (animationFromCornerType == AnimationFromCornerType.ShopFromBottomRight)
        {
            startX = Screen.width;
            startY = 0;
        }
        else if (animationFromCornerType == AnimationFromCornerType.ShopFromBottomLeft)
        {
            startX = 0;
            startY = 0;
        }
        else if (animationFromCornerType == AnimationFromCornerType.ShopFromTopRight)
        {
            startX = Screen.width;
            startY = Screen.height;
        }
        else if (animationFromCornerType == AnimationFromCornerType.ShopFromTopLeft)
        {
            startX = 0;
            startY = Screen.height;
        }

        float startTime = Time.time;

        Vector3 startPos = new Vector3(startX, startY, 0);
        Vector3 targetPosition = initialPosition;

        rect.position = startPos;

        while (Time.time <= startTime + animationDuration)
        {
            float t = (Time.time - startTime) / animationDuration;
            rect.position = Vector3.Lerp(startPos, targetPosition, t);
            rect.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);

            for (int i = 0; i < images.Length; i++)
            {
                images[i].color = Color.Lerp(startColors[i], endColors[i], t);
            }

            yield return (null);
        }

        rect.position = targetPosition;
        rect.localScale = Vector3.one;

        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = endColors[i];
        }
    }

    private IEnumerator AnimateElasticScale_SHOW()
    {
        if (withDelay)
            yield return (new WaitForSeconds(showDelay));

        Vector3 startPosition = rect.position;
        float startTime = Time.time;

        while (Time.time <= startTime + animationDuration)
        {
            float t = (Time.time - startTime) / animationDuration;

            rect.localScale = Vector3.Lerp(Vector3.zero, Vector3.one + new Vector3(0.1f, 0.1f, 0.1f) * elasticPower, t);

            yield return (null);
        }

        rect.localScale = Vector3.one + new Vector3(0.1f, 0.1f, 0.1f) * elasticPower;

        startTime = Time.time;
        while (Time.time <= startTime + animationDuration / 5)
        {
            float t = (Time.time - startTime) / (animationDuration / 5);
            rect.localScale = Vector3.Lerp(Vector3.one + new Vector3(0.1f, 0.1f, 0.1f) * elasticPower, Vector3.one, t);
            yield return (null);
        }
    }

    private IEnumerator AnimateScale_SHOW()
    {
        if (withDelay)
            yield return (new WaitForSeconds(showDelay));

        Vector3 startPosition = rect.position;
        float startTime = Time.time;

        while (Time.time <= startTime + animationDuration)
        {
            float t = (Time.time - startTime) / animationDuration;

            rect.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);

            yield return (null);
        }

        rect.localScale = Vector3.one;
    }

    private IEnumerator FadeIn_SHOW()
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

    private IEnumerator FadeIn_HIDE()
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

        Vector3 startPosition = rect.position;
        float startTime = Time.time;

        while (Time.time <= startTime + animationDuration)
        {
            float t = (Time.time - startTime) / animationDuration;

            rect.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);

            yield return (null);
        }

        rect.localScale = Vector3.zero;
    }

    private IEnumerator AnimateElasticScale_HIDE()
    {
        if (withDelay)
            yield return (new WaitForSeconds(hideDelay));

        Vector3 startPosition = rect.position;
        float startTime = Time.time;

        while (Time.time <= startTime + animationDuration)
        {
            float t = (Time.time - startTime) / animationDuration;

            rect.localScale = Vector3.Lerp(Vector3.one + new Vector3(0.1f, 0.1f, 0.1f) * elasticPower, Vector3.zero, t);

            yield return (null);
        }

        rect.localScale = Vector3.zero;
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

        if (animationFromCornerType == AnimationFromCornerType.ShopFromBottomRight)
        {
            endX = Screen.width;
            endY = 0;
        }
        else if (animationFromCornerType == AnimationFromCornerType.ShopFromBottomLeft)
        {
            endX = 0;
            endY = 0;
        }
        else if (animationFromCornerType == AnimationFromCornerType.ShopFromTopRight)
        {
            endX = Screen.width;
            endY = Screen.height;
        }
        else if (animationFromCornerType == AnimationFromCornerType.ShopFromTopLeft)
        {
            endX = 0;
            endY = Screen.height;
        }

        float startTime = Time.time;

        Vector3 startPos = initialPosition;
        Vector3 targetPosition = new Vector3(endX, endY, 0);
        rect.position = startPos;

        while (Time.time <= startTime + animationDuration)
        {
            float t = (Time.time - startTime) / animationDuration;
            rect.position = Vector3.Lerp(startPos, targetPosition, t);
            rect.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);

            for (int i = 0; i < images.Length; i++)
            {
                images[i].color = Color.Lerp(startColors[i], endColors[i], t);
            }

            yield return (null);
        }

        rect.position = targetPosition;
        rect.localScale = Vector3.zero;

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
    ShopFromBottomRight, ShopFromTopRight, ShopFromBottomLeft, ShopFromTopLeft
}