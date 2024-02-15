using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSettings
{
    List<GameObject> enemyUnits;
    int enemyType;
    public int enemyCount;
    int newHealth;
    float newSpeed;
    public WaveSettings()
    {
    }
    public WaveSettings(int enemyType, int enemyCount, int newHealth = 0, float newSpeed = 0)
    {
        enemyUnits = SpawnEnemy.instance.enemyUnits;
        this.enemyType = enemyType;
        this.enemyCount = enemyCount;
        this.newHealth = newHealth;
        this.newSpeed = newSpeed;
    }
    public GameObject GetEnemyObject()
    {
        GameObject res = enemyUnits[enemyType - 1];
        if(newHealth !=0) res.GetComponent<HPSystem>().Health = newHealth;
        if(newSpeed != 0) res.transform.GetChild(0).GetComponent<EnemyUnitControl>().speed = newSpeed;
        return res;
    }

}
