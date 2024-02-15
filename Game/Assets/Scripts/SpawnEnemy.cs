using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public static SpawnEnemy instance;
    public int waveCount;
    public Stages stages;
    public float nextRoundTime;
    bool nextRound;
    public float waitTime;
    public float nextUnitDelay;
    public List<GameObject> enemyUnits;
    public Vector3 spawnPosition = new Vector3(-2, 2, -2);
    Vector3 startPosition;
    float unitDelay;
    WaveSettings waveSettings;

    private void Awake()
    {
        startPosition = Game_Manager.instance.startPosition;
        instance = this;
        stages = new Stages();
        waitTime = nextRoundTime;
        unitDelay = nextUnitDelay;
        NextRound();
    }
    public GameObject Spawn(WaveSettings waveSettings, Vector3 position)
    {
        GameObject gObject = waveSettings.GetEnemyObject();
        return Instantiate(gObject, position, gObject.transform.rotation);
    }
    public void NextRound()
    {
        nextRound = true;
        waitTime = nextRoundTime;
        ReCalculatePath();
        waveSettings = stages.NextStage();
        waveCount= waveSettings.enemyCount;
        if (waveCount == 0) Game_Manager.instance.GameOver();
    }
    public void ReCalculatePath()
    {
        Game_Manager.instance.gameLogic.moveHistory = new HashSet<Vector2>();
        Game_Manager.instance.gameLogic.moveHistory.Add(startPosition);
        Game_Manager.instance.gameLogic.lastCell = startPosition;
        Game_Manager.instance.ClearPath();
        Game_Manager.instance.ShowPath();
    }
    public void DeployUnits()
    {
        if (waitTime < 0 && unitDelay < 0 && waveCount != 0)
        {
            unitDelay = nextUnitDelay;
            nextRound = false;
            EnemyUnitPlace();
        }
        else if (nextRound) waitTime -= Time.deltaTime;
        unitDelay -= Time.deltaTime;
    }
    public void EnemyUnitPlace(bool leading = false)
    {
        startPosition = Game_Manager.instance.startPosition;
        GameObject enemy = Spawn(waveSettings, spawnPosition);
        enemy.transform.LookAt(new Vector3(startPosition.x, enemy.transform.position.y, startPosition.z));
        enemy.transform.GetChild(0).GetComponent<Rigidbody>().velocity = enemy.transform.forward.normalized * 2f;
        Game_Manager.instance.enemies.Add(enemy);
        enemy.transform.GetChild(0).gameObject.GetComponent<EnemyUnitControl>().current = new Vector2(startPosition.x, startPosition.y);
        enemy.transform.GetChild(0).gameObject.GetComponent<EnemyUnitControl>().leading = true;
        waveCount--;
    }
    private void Update()
    {
        DeployUnits();
        if (waveCount == 0 && Game_Manager.instance.enemies.Count == 0) NextRound();
    }
}
