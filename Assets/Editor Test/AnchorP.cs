using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AnchorP : MonoBehaviour
{
    private float screenWidth, screenHeight;

    private RectTransform rectTransform;

    private float rectwidth, rectheight;

    private Vector3 rectInitialPosition;
    private Vector2 minAnchors;
    private Vector2 maxAnchors;
    private Vector2 rectRelativeToScreenPosition;

    private float widthRelativeToScreen;
    private float heightRelativeToScreen;

    private void Update()
    {
        if (Input.GetKeyDown("a"))
            SetAnchors();
    }

    public void SetAnchors()
    {
        // First, We will get the current position, width, and height of the rect transform, because we will reset these values after setting the anchor.
        // That's because when the anchors positions change, they autmatically change the coordinates of the rect transform.
        // Second, we will get the width and height of the screen.
        // Second, we will get the width and height of rect transform.
        // Third, we have to find the rect transform's position in relativity with screen width, and height. (rectTransform.position.x / screenWidth)

        SetInitialValues();

        // And finally setting the anchors.
        rectTransform.anchorMin = minAnchors;
        rectTransform.anchorMax = maxAnchors;

        // Resetting the rect to its initial position, width, and height.
        rectTransform.position = rectInitialPosition;

        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rectheight);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectwidth);
    }

    private void SetInitialValues()
    {
        rectTransform = GetComponent<RectTransform>();
        // Initial Position, because we will reset the position later.
        rectInitialPosition = rectTransform.position;

        // Screen dimensions.
        // We do not use Screen.width and Screen.height here, because these variables are only set correctly in play mode, in edit mode, they return the size of the window, it's a litte bit tricky -_- .
        string[] resolution = UnityEditor.UnityStats.screenRes.Split('x');
        screenWidth = int.Parse(resolution[0]);
        screenHeight = int.Parse(resolution[1]);

        // Rect dimensions.
        rectwidth = rectTransform.rect.width;
        rectheight = rectTransform.rect.height;

        // Relative position, width, and height.
        rectRelativeToScreenPosition = new Vector2(rectTransform.position.x / screenWidth, rectTransform.position.y / screenHeight);
        widthRelativeToScreen = rectwidth / screenWidth;
        heightRelativeToScreen = rectheight / screenHeight;

        float minX = rectRelativeToScreenPosition.x - (widthRelativeToScreen / 2);
        float minY = rectRelativeToScreenPosition.y - (heightRelativeToScreen / 2);
        float maxX = rectRelativeToScreenPosition.x + (widthRelativeToScreen / 2);
        float maxY = rectRelativeToScreenPosition.y + (heightRelativeToScreen / 2);

        minAnchors = new Vector2(minX, minY);
        maxAnchors = new Vector2(maxX, maxY);
    }

    #region Old
    private void SetInitialValues_OLD()
    {
        // (anchor in center of screen) = 0.5, 0.5
        // (center of rect for a value between 0 and 1) = (rect.position.x / screenWidth, rect.position.y / screenHeight).
        // To move the xMin and xMax, we move xMin by xC / 2 to the left, And move xMax by xC / 2 to the right away from the center.
        // To move the yMin and yMax, we move yMin by yC / 2 up, And move yMax by xC / 2 down away from the center.

        //float factor = 0.0000001f;
        //screenHeight = Screen.currentResolution.height;
        //screenWidth = Screen.currentResolution.width;

        //float heightRatio = screenHeight / screenWidth;
        //float widthRatio = screenWidth / screenHeight;

        //initialPosition = rectTransform.position;
        //Rect rectCoordinatess = rectTransform.rect;

        //rectwidth = rectTransform.rect.width;
        //rectheight = rectTransform.rect.height;

        //float diagonal = Mathf.Sqrt(Mathf.Pow(rectheight, 2) + Mathf.Pow(rectwidth, 2));

        //float xC = (rectwidth / 2) * (Screen.width * factor);
        //float yC = (rectheight / 2) * (Screen.height * factor);
    }
    #endregion

}