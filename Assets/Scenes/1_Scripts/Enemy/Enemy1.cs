using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    [SerializeField]
    private float speed, frequency, magnitude;

    SpriteRenderer sp;

    Vector3 pos;

    private void Awake()
    {
        pos = transform.position;
        sp = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        pos -= transform.right * Time.deltaTime * speed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
        if (!sp.isVisible)
            Destroy(gameObject);
    }
}
