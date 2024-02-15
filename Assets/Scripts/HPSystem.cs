using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPSystem : MonoBehaviour
{
    [SerializeField] int health;
    public Slider healthbar;
    public void Awake()
    {
        healthbar.maxValue = health;
        healthbar.value = health;
    }
    public void GetDamage(int damage)
    {
        health -= damage;
        healthbar.value -= damage;
        Debug.Log(health);
    }
    public void GetHealth(int healPoints)
    {
        health += healPoints;
        healthbar.value += healPoints;
    }
    public int Health
    {
        get { return health; }
        set { health = value; }
    }
}
