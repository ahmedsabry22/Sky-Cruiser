using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanOffset : MonoBehaviour
{
    [SerializeField] private Material oceanMaterial;
    [SerializeField] private float scrollSpeed = -0.1f;

    private float scroll;
    void Awake()
    {
        scroll = 0;
    }
    void Update()
    {
        ChangeOffset();
    }
    private float offset;
    void ChangeOffset()
    {
        scroll = Time.timeSinceLevelLoad / 20;
        scroll = Mathf.Clamp(scroll, 0, 15);
        offset+= scrollSpeed;          // = Time.timeSinceLevelLoad * scrollSpeed;
        oceanMaterial.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }
}