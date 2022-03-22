using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Bullet bullet;
    public AudioData ShootAudio;

    Vector2 direction;

    void Update()
    {
        direction = (transform.localRotation * Vector2.right).normalized;
    }

    public void Shoot()
    {
        AudioManager.instance.PlaySFX(ShootAudio, "Shoot");
        GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);
        Bullet goBullet = go.GetComponent<Bullet>();
        goBullet.transform.rotation = transform.rotation;
        goBullet.direction = direction;
    }
}
