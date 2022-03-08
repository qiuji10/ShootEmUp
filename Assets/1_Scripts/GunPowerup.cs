using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPowerup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerCore.instance.GetGunPU = true;
            Destroy(gameObject);
        }
    }
}
