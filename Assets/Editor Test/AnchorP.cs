using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AnchorP : MonoBehaviour
{
    private static float screenWidth, screenHeight;

    private static RectTransform rectTransform;

    private static float rectWidth, rectHeight;

    private static Vector3 rectInitialPosition;
    private static Vector2 minAnchors;
    private static Vector2 maxAnchors;
    private static Vector2 rectRelativeToScreenPosition;

    private static float widthRelativeToScreen;
    private static float heightRelativeToScreen;

    public static void SetAnchorsToRect(RectTransform rect)
    {
        // First, We will get the current position, width, and height of the rect transform, because we will reset these values after setting the anchor.
        // That's because when the anchors positions change, they autmatically change the coordinates of the rect transform.
        // Second, we will get the width and height of the screen.
        // Second, we will get the width and height of rect transform.
        // Third, we have to find the rect transform's position in relativity with screen width, and height. (rectTransform.position.x / screenWidth)

        rectTransform = rect;

        SetInitialValues();

        // And finally setting the anchors.
        rectTransform.anchorMin = minAnchors;
        rectTransform.anchorMax = maxAnchors;

        // Resetting the rect to its initial position, width, and height.
        rectTransform.position = rectInitialPosition;

        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rectHeight);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectWidth);
    }

    public static void SetRectToAnchor(RectTransform rect)
    {
        rectTransform = rect;

        SetInitialValues();

        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
    }

    private static void SetInitialValues()
    {
        // Initial Position, because we will reset the position later.
        rectInitialPosition = rectTransform.position;

        // Screen dimensions.
        // We do not use Screen.width and Screen.height here, because these variables are only set correctly in play mode, in edit mode, they return the size of the window, it's a litte bit tricky -_- .
        string[] resolution = UnityEditor.UnityStats.screenRes.Split('x');
        //screenWidth = int.Parse(resolution[0]);
        //screenHeight = int.Parse(resolution[1]);

        // Rect dimensions.
        rectWidth = rectTransform.rect.width;
        rectHeight = rectTransform.rect.height;

        screenWidth = rectTransform.parent.GetComponent<RectTransform>().rect.width;
        screenHeight = rectTransform.parent.GetComponent<RectTransform>().rect.height;

        print("screenWidth " + screenWidth);
        print("screenHeight " + screenHeight);
        print("rectHeight " + rectHeight);
        print("rectWidth " + rectWidth);

        // Relative position, width, and height.
        rectRelativeToScreenPosition = new Vector2(rectTransform.position.x / screenWidth, rectTransform.position.y / screenHeight);
        widthRelativeToScreen = rectWidth / screenWidth;
        heightRelativeToScreen = rectHeight / screenHeight;

        print("rectRelativeToScreenPosition.x " + rectRelativeToScreenPosition.x);
        print("rectRelativeToScreenPosition.y " + rectRelativeToScreenPosition.y);

        float zz = (screenHeight / rectHeight * 0.5f) - rectTransform.parent.GetComponent<RectTransform>().position.normalized.y;
        float xx = (screenWidth / rectWidth * 0.5f) - rectTransform.parent.GetComponent<RectTransform>().position.normalized.x;

        print("zz " + zz);
        print("xx " + xx);

        // Before 17-12
        //float minX = rectRelativeToScreenPosition.x - (widthRelativeToScreen / 2) ;
        //float minY = rectRelativeToScreenPosition.y - (heightRelativeToScreen / 2);
        //float maxX = rectRelativeToScreenPosition.x + (widthRelativeToScreen / 2) ;
        //float maxY = rectRelativeToScreenPosition.y + (heightRelativeToScreen / 2);

        float minX = 0;
        float minY = 0;
        float maxX = 0;
        float maxY = 0;

        if (1600 / screenWidth == 1)
        {
            minX = rectRelativeToScreenPosition.x - (widthRelativeToScreen / 2) * (1600 / screenWidth);
            minY = rectRelativeToScreenPosition.y - (heightRelativeToScreen / 2) * (900 / screenHeight);
            maxX = rectRelativeToScreenPosition.x + (widthRelativeToScreen / 2) * (1600 / screenWidth);
            maxY = rectRelativeToScreenPosition.y + (heightRelativeToScreen / 2) * (900 / screenHeight);
        }
        else
        {
            minX = rectRelativeToScreenPosition.x - (widthRelativeToScreen / 2) - .13f;
            minY = rectRelativeToScreenPosition.y - (heightRelativeToScreen / 2) + -.04f;
            maxX = rectRelativeToScreenPosition.x + (widthRelativeToScreen / 2) - .13f;
            maxY = rectRelativeToScreenPosition.y + (heightRelativeToScreen / 2) + -.04f;
        }

        print("minX " + minX);
        print("minY " + minY);
        print("maxX " + maxX);
        print("maxY " + maxY);


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