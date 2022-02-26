using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    Animator animator;
    Rigidbody2D rb;
    
    Vector2 movement;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.y == 0)
        {
            animator.SetBool("flyNormal", true);
            animator.SetBool("flyUp", false);
            animator.SetBool("flyDown", false);
        }
        else if (movement.y > 0)
        {
            animator.SetBool("flyUp", true);
            animator.SetBool("flyDown", false);
            animator.SetBool("flyNormal", false);
        }
        else if (movement.y < 0)
        {
            animator.SetBool("flyUp", false);
            animator.SetBool("flyDown", true);
            animator.SetBool("flyNormal", false);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
    }
}
