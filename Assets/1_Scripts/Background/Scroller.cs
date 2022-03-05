using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    public float speed = 0.1f;
    Renderer render;

    private void Awake()
    {
        render = GetComponent<Renderer>();
    }

    void Update()
    {
        Vector2 offset = new Vector2(Time.time * speed, 0);
        render.material.mainTextureOffset = offset;
    }
}
