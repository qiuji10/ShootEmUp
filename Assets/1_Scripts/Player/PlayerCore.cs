using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCore : MonoBehaviour
{
    public static PlayerCore instance;

    public float immunityTime,immunityDuration = 5f;

    [SerializeField]
    private int health = 10, damagedLayerIndex;
    [SerializeField]
    private bool isDamaged, isImmune, getGunPU, getShieldPU, getSpeedPU;

    public Text healthText;
    public GameObject shield, speedBoost;
    Animator animator;

    public bool IsDamaged
    {
        get => isDamaged;
        set => isDamaged = value;
    }

    public bool GetGunPU
    {
        get => getGunPU;
        set => getGunPU = value;
    }

    public bool GetShieldPU
    {
        get => getShieldPU;
        set => getShieldPU = value;
    }

    public bool GetSpeedPU
    {
        get => getSpeedPU;
        set => getSpeedPU = value;
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;

        healthText.text = "Health: " + health.ToString();
        animator = GetComponent<Animator>();
        damagedLayerIndex = animator.GetLayerIndex("Damaged");
    }

    private void Update()
    {
        if (getShieldPU)
        {
            StartCoroutine(ShieldActivation());
        }

        if (getGunPU)
        {
            StartCoroutine(DisableGunPU());
        }

        if (isDamaged)
        {
            if (!shield.activeInHierarchy)
            {
                
                if (health <= 0)
                {
                    //lose game
                }
                
                if (health > 0 && !isImmune)
                {
                    animator.SetLayerWeight(damagedLayerIndex, 1f);
                    health--;
                    isImmune = true;
                    immunityTime = 0;
                    healthText.text = "Health:" + health.ToString();
                }
                else if (isImmune)
                {
                    immunityTime = immunityTime + Time.deltaTime;
                    if (immunityTime >= immunityDuration)
                    {
                        animator.SetLayerWeight(damagedLayerIndex, 0f);
                        isDamaged = false;
                        isImmune = false;
                    }
                }
                
            }
        }
    }

    IEnumerator ShieldActivation()
    {
        shield.SetActive(true);
        yield return new WaitForSeconds(5);
        shield.SetActive(false);
        getShieldPU = false;
    }

    IEnumerator DisableGunPU()
    {
        yield return new WaitForSeconds(10);
        getGunPU = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("EnemyBullet") || col.gameObject.CompareTag("Enemy"))
        {
            isDamaged = true;
        }
    }
}
