using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    [SerializeField]
    private float speed, frequency, magnitude;
    private int health = 3;
    private bool isDamaged;

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

        if (isDamaged)
        {
            if (health <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                health--;
                isDamaged = false;
            } 
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("enter collide");
        if (col.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("bullet hit");
            isDamaged = true;
            Destroy(col.gameObject);
        }
    }
}
