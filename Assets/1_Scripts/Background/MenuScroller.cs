using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScroller : MonoBehaviour
{
    public float speed = 0.1f;
    RawImage bg_image;

    private void Awake()
    {
        bg_image = GetComponent<RawImage>();
    }

    void Update()
    {
        Vector2 offset = new Vector2(Time.time * speed, 0);
        bg_image.material.mainTextureOffset = offset;
    }
}
