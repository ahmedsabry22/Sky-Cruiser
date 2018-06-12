using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxSelector : MonoBehaviour
{
    public Material[] skyboxes;

    private void Start()
    {
        int index = Random.Range(0, skyboxes.Length);
        RenderSettings.skybox = skyboxes[index];
    }
}