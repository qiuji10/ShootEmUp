using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed, shootCD;
    private bool isShoot, canShoot = true;

    Animator animator;
    Rigidbody2D rb;
    Gun[] guns;
    Gun gun;
    
    Vector2 movement;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        guns = transform.GetComponentsInChildren<Gun>();
        gun = transform.GetComponentInChildren<Gun>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        isShoot = Input.GetKey(KeyCode.J);

        if (isShoot && canShoot)
        {
            
            
            ShootCD();
            //foreach (Gun gun in guns)
            //{
            //    gun.Shoot();
            //    ShootCD();
            //}
        }

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

    public void ShootCD()
    {
        StartCoroutine(ShootInterval());
    }

    IEnumerator ShootInterval()
    {
        gun.Shoot();
        canShoot = false;
        yield return new WaitForSeconds(shootCD);
        canShoot = true;
    }
}
