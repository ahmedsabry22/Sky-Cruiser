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

    public static bool ButtonClicked(string button)
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
}