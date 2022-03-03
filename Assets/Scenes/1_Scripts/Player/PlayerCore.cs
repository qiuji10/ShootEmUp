using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    [SerializeField]
    private int health = 5;
    private bool isDamaged;

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
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            isDamaged = true;
        }
    }
}
