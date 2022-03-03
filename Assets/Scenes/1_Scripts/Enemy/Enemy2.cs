using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private int health = 3;
    private bool isDamaged, foundPlayer;

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
        transform.position = pos;

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

        if (foundPlayer)
        {
            //stop movement
            speed = 0;
            //get player position, adjust gun and bullet to player position
            //instantiate enemy bullet
        }
        else
            speed = 1;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("bullet hit");
            isDamaged = true;
            Destroy(col.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("found player");
            foundPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("found player");
            foundPlayer = false;
        }
    }
}
