using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Timers;

public class TowersControl : Player
{
    public Vector2 pos; 
    public int damage;
    public float reload;
    public int activeRadius;
    public int health;
    List<GameObject> enemiesInRange;
    public void Awake()
    {
        enemiesInRange = new List<GameObject>();
        reloadDelta = reload;
    }
    public void Update()
    {
        
        WaitReload();
        LookAtEnemy(radius);
        if (!IsReloading())
        {
            reloadDelta = reload;
            SingleAttack(radius, damage);
        }
    }
}
