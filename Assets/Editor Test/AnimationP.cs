using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimationP : MonoBehaviour
{
    public float animationDuration = 3;

    public AnimationType animationType;

    private RectTransform rect;

    private Vector3 initialPosition;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown("z"))
            StartCoroutine(AnimateFromCornerWithScale_SHOW());
        if (Input.GetKeyDown("v"))
            StartCoroutine(AnimateFromCornerWithScale_HIDE());
        if (Input.GetKeyDown("x"))
            StartCoroutine(AnimateElasticScale());
        if (Input.GetKeyDown("c"))
            StartCoroutine(AnimateScale());
    }

    private IEnumerator AnimateFromCornerWithScale_SHOW()
    {
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

        if (animationType == AnimationType.ShopFromBottomRight)
        {
            startX = Screen.width;
            startY = 0;
        }
        else if (animationType == AnimationType.ShopFromBottomLeft)
        {
            startX = 0;
            startY = 0;
        }
        else if (animationType == AnimationType.ShopFromTopRight)
        {
            startX = Screen.width;
            startY = Screen.height;
        }
        else if (animationType == AnimationType.ShopFromTopLeft)
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

    private IEnumerator AnimateFromCornerWithScale_HIDE()
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

        if (animationType == AnimationType.ShopFromBottomRight)
        {
            endX = Screen.width;
            endY = 0;
        }
        else if (animationType == AnimationType.ShopFromBottomLeft)
        {
            endX = 0;
            endY = 0;
        }
        else if (animationType == AnimationType.ShopFromTopRight)
        {
            endX = Screen.width;
            endY = Screen.height;
        }
        else if (animationType == AnimationType.ShopFromTopLeft)
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

    private IEnumerator AnimateElasticScale()
    {
        Vector3 startPosition = rect.position;
        float startTime = Time.time;

        while (Time.time <= startTime + animationDuration)
        {
            float t = (Time.time - startTime) / animationDuration;

            rect.localScale = Vector3.Lerp(Vector3.zero, Vector3.one + new Vector3(0.1f, 0.1f, 0.1f), t);

            yield return (null);
        }

        rect.localScale = Vector3.one + new Vector3(0.1f, 0.1f, 0.1f);

        startTime = Time.time;
        while (Time.time <= startTime + animationDuration / 5)
        {
            float t = (Time.time - startTime) / (animationDuration / 5);
            rect.localScale = Vector3.Lerp(Vector3.one + new Vector3(0.1f, 0.1f, 0.1f), Vector3.one, t);
            yield return (null);
        }
    }

    private IEnumerator AnimateScale()
    {
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
}

public enum AnimationType
{
    ShopFromBottomRight, ShopFromTopRight, ShopFromBottomLeft, ShopFromTopLeft
}