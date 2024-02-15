using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public GameObject impact;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Debug.Log(other.gameObject);
            other.gameObject.GetComponent<EnemyUnitControl>().GetDamage(damage);
            Instantiate(impact, gameObject.transform);
            Destroy(gameObject);
        }
    }
}
