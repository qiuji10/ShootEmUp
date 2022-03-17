using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCore : MonoBehaviour
{
    public static PlayerCore instance;

    [SerializeField]
    private int health = 10;
    [SerializeField]
    private bool isDamaged, getGunPU, getShieldPU, getSpeedPU;

    public Text healthText;
    public GameObject shield, speedBoost;

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
                else
                {
                    health--;
                    isDamaged = false;
                }
                healthText.text = "Health:" + health.ToString();
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
        if (col.gameObject.CompareTag("EnemyBullet"))
        {
            Debug.Log("Player taking damage from enemy");
            isDamaged = true;
        }
    }
}
