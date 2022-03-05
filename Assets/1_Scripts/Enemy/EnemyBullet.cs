using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;

    SpriteRenderer sp;
    Rigidbody2D rb;
    PlayerCore target;

    public Vector3 pos;

    private void Awake()
    {
        pos = transform.position;
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<PlayerCore>();
        rb.velocity = (target.transform.position - transform.position).normalized * speed;
    }

    private void Update()
    {
        if (!sp.isVisible)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
