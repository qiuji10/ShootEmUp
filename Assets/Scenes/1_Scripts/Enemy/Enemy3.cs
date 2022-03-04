using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    [SerializeField]
    private float speed, fireRate, nextFire;
    private int health = 3;
    private bool isDamaged, foundPlayer;

    public Transform target, childTransform, st;
    public GameObject enemyBullet;
    SpriteRenderer sp;

    Vector3 pos;

    private void Awake()
    {
        pos = transform.position;
        sp = GetComponent<SpriteRenderer>();
        childTransform = transform.Find("Laser");
        st = childTransform.Find("ShootingLaser");
        fireRate = 4f;
        nextFire = Time.time;
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
            speed = 0;
            Vector3 targetDirection = (target.position - childTransform.position).normalized;
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            childTransform.eulerAngles = new Vector3(0, 0, angle);
            CheckFire();
        }
        else
        {
            if (!(Time.time > nextFire))
                return;
            else
            {
                speed = 1;
                childTransform.eulerAngles = new Vector3(0, 0, 180);
            }
        }
    }

    void CheckFire()
    {
        if (Time.time > nextFire)
        {
            Instantiate(enemyBullet, st.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
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
            target = col.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("found player");
            foundPlayer = false;
            target = null;
        }
    }
}
