using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    Vector2 direction;

    void Update()
    {
        direction = (transform.localRotation * Vector2.right).normalized;
    }

    public void Shoot()
    {
        GameObject bullet = BulletPool.SharedInstance.GetPooledObject();

        if (bullet != null)
        {
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.GetComponent<Bullet>().direction = direction;
            bullet.SetActive(true);
        }
    }
}
