using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    public static PlayerCore instance;

    public float immunityDuration = 3f;

    [SerializeField]
    private int health = 10;
    private int damagedLayerIndex;

    [SerializeField]
    private float immunityTime, shieldTimer, gunTimer, speedTimer;
    [SerializeField]
    private bool isDamaged, isImmune, getGunPU, getShieldPU, getSpeedPU;

    private Animator animator;
    private UIManager uiManager;
    private PlayerController playerController;
    public GameObject shield, speedBoost;
    public AudioData PlayerAudio;
    

    public int Health
    {
        get => health;
    }

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

        uiManager = GameObject.Find("GameManager").GetComponent<UIManager>();
        animator = GetComponent<Animator>();
        damagedLayerIndex = animator.GetLayerIndex("Damaged");
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (GetShieldPU)
        {
            shield.SetActive(true);
            DisableShieldPU();
        }

        if (GetGunPU)
        {
            DisableGunPU();
        }

        if (GetSpeedPU)
        {
            speedBoost.SetActive(true);
            DisableSpeedPU();
        }

        if (isDamaged)
        {
            if (shield.activeInHierarchy)
            {
                animator.SetLayerWeight(damagedLayerIndex, 0f);
            }

            if (!shield.activeInHierarchy)
            {
                if (health <= 0)
                {
                    gameObject.GetComponent<PlayerController>().enabled = false;
                    uiManager.GameOver();
                }
                
                if (health > 0 && !isImmune)
                {
                    animator.SetLayerWeight(damagedLayerIndex, 1f);
                    AudioManager.instance.PlaySFX(PlayerAudio, "Damaged");
                    health--;
                    isImmune = true;
                    immunityTime = 0;
                    uiManager.UpdateHealth();
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

    public void DisableShieldPU()
    {
        shieldTimer += Time.deltaTime;
        if (shieldTimer >= 5)
        {
            shieldTimer = 0;
            shield.SetActive(false);
            GetShieldPU = false;
        }
    }

    public void DisableGunPU()
    {
        gunTimer += Time.deltaTime;
        if (gunTimer >= 5)
        {
            gunTimer = 0;
            GetGunPU = false;
        }
    }

    public void DisableSpeedPU()
    {
        playerController.speed = 10;
        speedTimer += Time.deltaTime;
        if (speedTimer >= 5)
        {
            speedTimer = 0;
            playerController.speed = 5;
            speedBoost.SetActive(false);
            GetSpeedPU = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("EnemyBullet") || col.gameObject.CompareTag("Enemy"))
        {
            isDamaged = true;
        }
    }
}
