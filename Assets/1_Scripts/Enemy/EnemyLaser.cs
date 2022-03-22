using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    private bool isInRange;

    PlayerCore target;

    private void Awake()
    {
        target = FindObjectOfType<PlayerCore>();
        Vector3 targetDirection = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
        Destroy(gameObject, 2.5f);
    }

    void Charging()
    {
        StartCoroutine(DelayLaserDamage());
    }

    IEnumerator DelayLaserDamage()
    {
        yield return new WaitForSeconds(1f);
        if (isInRange)
        {
            target.IsDamaged = true;
            isInRange = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        Charging();
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isInRange = false;
        }
    }
}
