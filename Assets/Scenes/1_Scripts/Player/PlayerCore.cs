using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCore : MonoBehaviour
{
    public static PlayerCore instance;

    [SerializeField]
    private int health = 5;
    private bool isDamaged;

    public Text healthText;

    public bool IsDamaged
    {
        get => isDamaged;
        set => isDamaged = value;
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;

        healthText.text = health.ToString();
    }

    private void Update()
    {
        if (isDamaged)
        {
            if (health <= 0)
            {
                //lose game
            }
            else
            {
                health--;
                isDamaged = false;
            }
            healthText.text = health.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Enemy enter player collision");
        if (col.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player taking damage from enemy");
            isDamaged = true;
        }
    }
}
