using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("GunPowerUp"))
            {
                if (!PlayerCore.instance.GetGunPU)
                {
                    PlayerCore.instance.GetGunPU = true;
                    Destroy(gameObject);
                }
            }
                
            if (gameObject.CompareTag("ShieldPowerUp"))
            {
                if (!PlayerCore.instance.GetShieldPU)
                {
                    PlayerCore.instance.GetShieldPU = true;
                    Destroy(gameObject);
                }
            }
                
            if (gameObject.CompareTag("SpeedPowerUp"))
            {
                if (!PlayerCore.instance.GetSpeedPU)
                {
                    PlayerCore.instance.GetSpeedPU = true;
                    Destroy(gameObject);
                }  
            }  
        }
    }
}
