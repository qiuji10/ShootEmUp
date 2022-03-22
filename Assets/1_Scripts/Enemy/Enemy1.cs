using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    [SerializeField]
    private float speed, frequency, magnitude;
    private int health = 3;
    private bool isDamaged;

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

    private void Awake()
    {
        pos = transform.position;
        sp = GetComponent<SpriteRenderer>();
        scoringSystem = GameObject.Find("GameManager").GetComponent<UIManager>();
    }

    private void Update()
    {
        pos -= transform.right * Time.deltaTime * speed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;

        if (!sp.isVisible)
            Destroy(gameObject);

        if (isDamaged)
        {
            if (health <= 1)
            {
                scoringSystem.score += 100;
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
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            isDamaged = true;
            col.gameObject.SetActive(false);
        }
    }
}
