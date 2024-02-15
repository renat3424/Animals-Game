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
    public GameObject ui;
    public void Awake()
    {
        enemiesInRange = new List<GameObject>();
        reloadDelta = reload;
    }

    public void OpenUi()
    {

        ui.SetActive(true);
        Animator anim = ui.GetComponent<Animator>();
        
        anim.SetBool("Open", true);


    }

    public void CloseUi()
    {
        
        Animator anim = ui.GetComponent<Animator>();
        
        anim.SetBool("Open", false);
        ui.SetActive(false);

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
