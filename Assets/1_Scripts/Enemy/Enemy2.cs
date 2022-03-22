using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [SerializeField]
    private float speed, fireRate, nextFire;
    private int health = 5;
    private bool isDamaged, foundPlayer;

    public Transform target, childTransform, st;
    public GameObject enemyBullet;
    public GameObject ExplosionPrefab;
    public AudioData EnemyAudio;
    SpriteRenderer sp;
    UIManager scoringSystem;

    Vector3 pos;

    public int Health
    {
        get => health;
        set => health = value;
    }

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    public float FireRate
    {
        get => fireRate;
        set => fireRate = value;
    }

    private void Awake()
    {
        pos = transform.position;
        sp = GetComponent<SpriteRenderer>();
        childTransform = transform.Find("BlueGun");
        st = childTransform.Find("Gun");
        scoringSystem = GameObject.Find("GameManager").GetComponent<UIManager>();
        fireRate = 2f;
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
            if (health <= 1)
            {
                scoringSystem.score += 500;
                scoringSystem.scoreText.text = "Score: " + scoringSystem.score.ToString();
                GameObject ex = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
                Destroy(ex, 1);
                AudioManager.instance.PlaySFX(EnemyAudio, "EnemyDie");
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            isDamaged = true;
            col.gameObject.SetActive(false);
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
