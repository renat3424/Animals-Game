using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPSystem : MonoBehaviour
{
    [SerializeField] int health;
    public void Awake()
    {

    }
    public void GetDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);
    }
    public void GetHealth(int healPoints)
    {
        health += healPoints;
    }
    public int Health
    {
        get { return health; }
        set { health = value; }
    }
}
