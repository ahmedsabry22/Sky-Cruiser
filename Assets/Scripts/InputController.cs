using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class InputController : MonoBehaviour
{
    public static float GetAxis(string name)
    {
#if UNITY_EDITOR
        return (Input.GetAxis(name));

#elif UNITY_ANDROID
        return (CrossPlatformInputManager.GetAxis(name));
#endif
    }

    /// <summary>
    /// We use this to check buttons are being clicked on cross platforms.
    /// </summary>
    /// <param name="button">pass axis name, not the button itself, eg. pass "Brake" not "Space"</param>
    /// <returns></returns>
    public static bool ButtonClicking(string button)
    {
#if UNITY_EDITOR
        if (Input.GetButton(button))
            return (true);
#elif UNITY_ANDROID
        if (CrossPlatformInputManager.GetButton(button))
            return (true);
#endif
        return (false);
    }

    /// <summary>
    /// We use this to check buttons are clicked on cross platforms.
    /// </summary>
    /// <param name="button">pass axis name, not the button itself, eg. pass "Brake" not "Space"</param>
    /// <returns></returns>
    public static bool ButtonClicked(string button)
    {
#if UNITY_EDITOR
        if (Input.GetButtonDown(button))
            return (true);
#elif UNITY_ANDROID
        if (CrossPlatformInputManager.GetButtonDown(button))
            return (true);
#endif

        return (false);
    }
}