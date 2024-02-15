using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitControl : Enemy
{
    public Vector2 current;
    public Vector2 next;
    public Vector2 moveTo;
    public float x;
    EnemyUnitControl instance;
    public float delta;
    public float speed;
    public bool leading; //самый первый в волне
    public int KillPrice;
    GameLogic gameLogic;
    public override void ChangeUI()
    {

    }

    public EnemyUnitControl()
    {
    }
    public void Awake()
    {
        instance = this;
        current = new Vector2(0, 0);
        next = new Vector2(0, 0);
        gameLogic = Game_Manager.instance.gameLogic;
        moveTo = gameLogic.start;
        x = 0;
        g = Physics.gravity.magnitude;
    }
    
    public override void LookAt(Vector2 position)
    {
        Vector3 lookVect = Game_Manager.instance.fieldCells[(int)position.x, (int)position.y].transform.position;
        gameObject.transform.LookAt(new Vector3(lookVect.x, transform.position.y, lookVect.z));
    }
    public Vector3 GetVector3(Vector2 vector)
    {
        return new Vector3(vector.y, 0, vector.x);
    }
    
    public void Update()
    {
        
        if (current == next)
        {
            StopAt(current, delta);
            if (current == gameLogic.end)
            {
                Die();
                Game_Manager.instance.playerLives.Lives -= 1;
                Game_Manager.instance.isVictory = false;
                if(Game_Manager.instance.playerLives.isGameOver()) Game_Manager.instance.GameOver();
            }
        }
        if (current != next)
        {
            if (InPosition(moveTo, delta))
            {
                current = moveTo;
                moveTo = next; //new move vector

                Vector2 direction = next - current;
                if (!InPosition(gameLogic.end, delta))
                {
                    MoveTo(GetVector3(direction), speed);
                }
            }
            else
            {
                if (moveTo != gameLogic.start)
                {
                    Vector3 direction = moveTo - current;
                    LookAt(next);
                    MoveTo(GetVector3(direction), speed);
                }
            }
        }
        if (!IsAlive())
        {
            Die();
            Economics.instance.AddMoney(KillPrice);
            //new leader of vawe
            if (Game_Manager.instance.enemies.Count != 0) Game_Manager.instance.enemies[0].transform.GetChild(0).gameObject.GetComponent<EnemyUnitControl>().leading = true; ;
        }
        
    }

}
