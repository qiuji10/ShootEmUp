using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public AudioData PowerUpAudio;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("GunPowerUp"))
            {
                if (!PlayerCore.instance.GetGunPU)
                {
                    AudioManager.instance.PlaySFX(PowerUpAudio, "GunPU");
                    PlayerCore.instance.GetGunPU = true;
                    Destroy(gameObject);
                }
            }
                
            if (gameObject.CompareTag("ShieldPowerUp"))
            {
                if (!PlayerCore.instance.GetShieldPU)
                {
                    AudioManager.instance.PlaySFX(PowerUpAudio, "ShieldPU");
                    PlayerCore.instance.GetShieldPU = true;
                    Destroy(gameObject);
                }
            }
                
            if (gameObject.CompareTag("SpeedPowerUp"))
            {
                if (!PlayerCore.instance.GetSpeedPU)
                {
                    AudioManager.instance.PlaySFX(PowerUpAudio, "SpeedPU");
                    PlayerCore.instance.GetSpeedPU = true;
                    Destroy(gameObject);
                }  
            }  
        }
    }
}
