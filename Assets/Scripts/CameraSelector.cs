using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelector : MonoBehaviour
{
    public Camera[] Cameras;

    private int activeIndex = 0;
    private void Start()
    {
        Cameras = FindObjectsOfType<Camera>();
    }

    public void ChangeActiveCamera()
    {
        foreach (Camera cam in Cameras)
        {
            cam.enabled = false;
        }

        activeIndex++;

        if (activeIndex == Cameras.Length)
        {
            activeIndex = 0;
        }

        Cameras[activeIndex].enabled = true;
    }
}
