using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
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
        childTransform = transform.Find("BlueGun");
        st = childTransform.Find("Gun");
        fireRate = 1f;
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
            speed = 1;
            childTransform.eulerAngles = new Vector3(0, 0, 180);
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

    IEnumerator Shoot(Vector3 vec)
    {
        
        yield return new WaitForSeconds(2f);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            isDamaged = true;
            Destroy(col.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            foundPlayer = true;
            target = col.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            foundPlayer = false;
            target = null;
        }
    }
}
