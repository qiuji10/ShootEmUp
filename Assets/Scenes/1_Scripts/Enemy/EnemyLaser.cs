using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    private bool dealDamage;

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
        if (dealDamage)
        {
            PlayerCore.instance.IsDamaged = true;
            Debug.Log("start deal damage");
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("enter trigger laser");
        if (col.gameObject.CompareTag("Player"))
        {
            dealDamage = true;
            Charging();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("exit trigger laser");
        if (col.gameObject.CompareTag("Player"))
        {
            dealDamage = false;
        }
    }
}
