using UnityEngine;
using System.Collections;

public class AnimationStartPosition : MonoBehaviour
{
    /// <summary>
    /// Used for initializing the position from a certian corner.
    /// </summary>
    /// <param name="initialPosition">the start position of the transform</param>
    /// <param name="rect">the rect transform</param>
    /// <param name="animationFromCornerType">BottomRight, TopRight, BottomLeft, TopLeft, Up, Bottom, Left, Or Right</param>
    /// <param name="animationFromCornerStartFromType">Screen or Rect</param>
    /// <returns></returns>
    public static Vector3 GetStartPositionFromCorner
        (Vector3 initialPosition, RectTransform rect, AnimationFromCornerType animationFromCornerType, AnimationFromCornerStartFromType animationFromCornerStartFromType)
    {
        float startPositionX = 0;
        float startPositionY = 0;

        if (animationFromCornerStartFromType == AnimationFromCornerStartFromType.Screen)
        {
            switch (animationFromCornerType)
            {
                case (AnimationFromCornerType.BottomRight):
                    startPositionX = Screen.width + (rect.rect.width / 2);
                    startPositionY = 0 - (rect.rect.height / 2);
                    break;
                case (AnimationFromCornerType.BottomLeft):
                    startPositionX = 0 - (rect.rect.width / 2);
                    startPositionY = 0 - (rect.rect.height / 2);
                    break;
                case (AnimationFromCornerType.TopRight):
                    startPositionX = Screen.width + (rect.rect.width / 2);
                    startPositionY = Screen.height + (rect.rect.height / 2);
                    break;
                case (AnimationFromCornerType.TopLeft):
                    startPositionX = 0 - (rect.rect.width / 2);
                    startPositionY = Screen.height + (rect.rect.height / 2);
                    break;
                case (AnimationFromCornerType.Up):
                    startPositionX = initialPosition.x;
                    startPositionY = Screen.height + (rect.rect.height / 2);
                    break;
                case (AnimationFromCornerType.Bottom):
                    startPositionX = initialPosition.x;
                    startPositionY = 0 - (rect.rect.height / 2);
                    break;
                case (AnimationFromCornerType.Left):
                    startPositionX = 0 - (rect.rect.width / 2);
                    startPositionY = initialPosition.y;
                    break;
                case (AnimationFromCornerType.Right):
                    startPositionX = Screen.width + (rect.rect.width / 2);
                    startPositionY = initialPosition.y;
                    break;
            }
        }
        else if (animationFromCornerStartFromType == AnimationFromCornerStartFromType.Screen)
        {
            switch (animationFromCornerType)
            {
                case (AnimationFromCornerType.BottomRight):
                    startPositionX = rect.parent.GetComponent<RectTransform>().rect.width + (rect.rect.width / 2);
                    startPositionY = 0 - (rect.rect.height / 2);
                    break;
                case (AnimationFromCornerType.BottomLeft):
                    startPositionX = 0 - (rect.rect.width / 2);
                    startPositionY = 0 - (rect.rect.height / 2);
                    break;
                case (AnimationFromCornerType.TopRight):
                    startPositionX = rect.parent.GetComponent<RectTransform>().rect.width + (rect.rect.width / 2);
                    startPositionY = rect.parent.GetComponent<RectTransform>().rect.height + (rect.rect.height / 2);
                    break;
                case (AnimationFromCornerType.TopLeft):
                    startPositionX = 0 - (rect.rect.width / 2);
                    startPositionY = rect.parent.GetComponent<RectTransform>().rect.height + (rect.rect.height / 2);
                    break;
                case (AnimationFromCornerType.Up):
                    startPositionX = initialPosition.x;
                    startPositionY = rect.parent.GetComponent<RectTransform>().rect.height + (rect.rect.height / 2);
                    break;
                case (AnimationFromCornerType.Bottom):
                    startPositionX = initialPosition.x;
                    startPositionY = 0 - (rect.rect.height / 2);
                    break;
                case (AnimationFromCornerType.Left):
                    startPositionX = 0 - (rect.rect.width / 2);
                    startPositionY = initialPosition.y;
                    break;
                case (AnimationFromCornerType.Right):
                    startPositionX = rect.parent.GetComponent<RectTransform>().rect.width + (rect.rect.width / 2);
                    startPositionY = initialPosition.y;
                    break;
            }
        }

        Vector3 startPos = new Vector3(startPositionX, startPositionY, 0);

        return (startPos);
    }
}

// TODO: Move this enum to AnimationPElement script when you go home.
public enum AnimationFromCornerStartFromType
{
    // Screen is used to start the animation from bordres of the screen.
    // Rect is used to start the animation from bordres of the current rect.
    Screen, Rect
}