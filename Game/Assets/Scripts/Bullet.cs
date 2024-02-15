using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Debug.Log(other.gameObject);
            other.gameObject.GetComponent<EnemyUnitControl>().GetDamage(damage);
            Destroy(gameObject);
        }
    }
}
