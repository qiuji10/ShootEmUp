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
                PlayerCore.instance.GetGunPU = true;
            if (gameObject.CompareTag("ShieldPowerUp"))
                PlayerCore.instance.GetShieldPU = true;
            if (gameObject.CompareTag("SpeedPowerUp"))
                PlayerCore.instance.GetSpeedPU = true;
            Destroy(gameObject);
        }
    }
}
