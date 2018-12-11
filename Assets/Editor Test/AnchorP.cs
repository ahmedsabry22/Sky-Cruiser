using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorP : MonoBehaviour
{
    public RectTransform rectTransform;

    public float width, height;

    public Vector2 minAnchors;
    public Vector2 maxAnchors;

    public float factor = 0.000001f;
    public float screenWidth = 1920, screenHeight = 1080;

    public float xC, yC;

    public Vector3 initialPosition;
    public Rect rectCords;

    public float xFactor = 3.67f;
    public float yFactor = 1.16f;

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown("a"))
            SetAnchors();
    }

    private void SetInitialValues()
    {
        initialPosition = rectTransform.position;
        rectCords = rectTransform.rect;

        width = rectTransform.rect.width;
        height = rectTransform.rect.height;

        xC = (width / 2) * (Screen.width * factor);
        yC = (height / 2) * (Screen.height * factor);
    }

    [ContextMenu("XXX")]
    private void YYY()
    {
        StartCoroutine(XXX());
    }
    private IEnumerator XXX()
    {
        yield return (new WaitForSeconds(1));
        print("x");

        yield return (new WaitForSeconds(1));
        print("y");
    }

    [ContextMenu("Set Anchores")]
    public void SetAnchors()
    {
        // (anchor in center of screen) = 0.5, 0.5
        // (center of rect for a value between 0 and 1) = (rect.position.x / screenWidth, rect.position.y / screenHeight)
        // To move the xMin and xMax, we move xMin by xC / 2 to the left, And move xMax by xC / 2 to the right away from the center
        // To move the yMin and yMax, we move yMin by yC / 2 up, And move yMax by xC / 2 down away from the center.

        SetInitialValues();

        Vector2 relativePos = new Vector2(rectTransform.position.x / screenWidth, rectTransform.position.y / screenHeight);

        float xMin = relativePos.x - (xC / xFactor);
        float xMax = relativePos.x + (xC / xFactor);

        float yMin = relativePos.y - (yC / yFactor);
        float yMax = relativePos.y + (yC / yFactor);

        minAnchors = new Vector2(xMin, yMin);
        maxAnchors = new Vector2(xMax, yMax);

        rectTransform.anchorMin = minAnchors;
        rectTransform.anchorMax = maxAnchors;

        rectTransform.position = initialPosition;

        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }
}