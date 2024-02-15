using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float angularVelocity;
    public float radius;
    Vector3 vect;
    Vector3 init;
    public float reloadDelta;
    public GameObject bullet;
    public float bulletSpeed;
    //public void Init()
    //{
    //    vect = new Vector3(0, 0, radius);
    //    init = transform.position;
    //    Debug.Log("Initiated");
    //}
    //public void RotateAroundY()
    //{

    //    //transform.rotation = Quaternion.AngleAxis(angularVelocity*Time.deltaTime, new Vector3(0, 1, 0))*transform.rotation;
    //    vect = Quaternion.AngleAxis(angularVelocity * Time.deltaTime, new Vector3(0, 1, 0)) * vect;
    //    transform.position = init + vect;

    //}
    public void Die()
    {
        Game_Manager.instance.activeUnits.Remove(gameObject.transform.parent.gameObject);
        Destroy(gameObject);
    }
    public void Attack(GameObject other, int damage)
    {
        other.GetComponent<HPSystem>().GetDamage(damage);
    }
    public void SplashAttack(float range, int damage)
    {
        SplashAttack(transform.position, range, damage);
    }
    public void SplashAttack(Vector3 position, float range, int damage)
    {
        GameObject[] enemies = FindEnemiesInRange(position, range);
        foreach(GameObject gObj in enemies)
        {
            if(gObj != null)
                Attack(gObj, damage);
        }
    }
    public void CreateBullet(Vector3 position, Vector3 direction)
    {
        GameObject bull = Instantiate(bullet, position, transform.rotation);
        bull.transform.GetChild(0).GetComponent<Rigidbody>().velocity = direction.normalized*bulletSpeed;
    }
    public void SingleAttack(float range, int damage)
    {
        GameObject[] enemies = FindEnemiesInRange(transform.position, range);
        if (enemies.Length != 0)
        {
            //Attack(enemies[0], damage);
            Vector3 pos = enemies[0].transform.GetChild(0).GetComponent<Rigidbody>().position;
            CreateBullet(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), pos - transform.position);
        }
    }
    public void LookAtEnemy(float range)
    {
        GameObject[] enemies = FindEnemiesInRange(transform.position, range);
        if (enemies.Length != 0)
        {
            transform.LookAt(enemies[0].transform.GetChild(0).GetComponent<Rigidbody>().position);

        }
    }
   
    public GameObject[] FindEnemiesInRange(float range)
    {
        return FindEnemiesInRange(transform.position, range);
    }
    public GameObject[] FindEnemiesInRange(Vector3 position, float range)
    {
        List<GameObject> enem = Game_Manager.instance.enemies;
        GameObject[] enemies = new GameObject[enem.Count];
        int i = 0;
        foreach(GameObject gObj in enem)
        {
            if ((position - gObj.transform.GetChild(0).GetComponent<Rigidbody>().position).magnitude < range)
            {
                enemies[i] = gObj;
                i++;
            }
            
        }
        GameObject[] res = new GameObject[i];
        for(int j = 0; j < res.Length; j++)
        {
            res[j] = enemies[j];
        }
        return res;
    }
    public bool IsReloading()
    {
        return reloadDelta > 0;
    }
    public void WaitReload()
    {
        reloadDelta -= Time.deltaTime;
    }
      
}