using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class CellControl : MonoBehaviour
{
    public int x, y; //координаты клетки
    public bool allowPut;
    public Vector3 prev;
    bool searchPath;    //public CellControl(int x, int y)
    //{
    //    this.x = x;
    //    this.y = y;
    //}
    Thread pathSearch;
    public void Awake()
    {

    }
    //private async Task RefreshPathAsync()
    //{
    //    Game_Manager.instance.ClearPath();
    //    await Task.Run(()=>Game_Manager.instance.gameLogic.GetPath());
    //    Game_Manager.instance.ShowPath();
    //}

    public void Update()
    {
        
        //if(searchPath)
        //{
        //    if (pathSearch.IsAlive) return;
        //    Game_Manager.instance.ShowPath();
        //    searchPath = false;
        //}
    }



    public void OnTriggerEnter1(Collider other)
    {
        Game_Manager.instance.gameLogic.matrix[x, y] = false;
        if (Game_Manager.instance.gameLogic.anyPathWith(new Vector2(x, y)) && !Game_Manager.instance.gameLogic.moveHistory.Contains(new Vector2(x, y)))
        {
            Game_Manager.instance.ClearPath();
            Game_Manager.instance.ShowPath();
            //Game_Manager.instance.ClearPath();
            //if (pathSearch.IsAlive) pathSearch.Abort();
            //pathSearch.Start();
            //searchPath = true;
            allowPut = true;

        }
        else
        {
            allowPut = false;
            Game_Manager.instance.gameLogic.matrix[x, y] = true;
        }
    }
    public void OnTriggerEnter(Collider other)
    {


       if (other.gameObject.tag == "Enemy")
        {
            Vector2 curr = new Vector2(this.x, this.y);
            


            //other.GetComponent<EnemyUnitControl>().current = curr;
            Vector2 next = new Vector2(0, 0);
            prev = other.gameObject.GetComponent<EnemyUnitControl>().current;
            if (curr != Game_Manager.instance.gameLogic.end) next = Game_Manager.instance.gameLogic.GetNextCell(curr);
            else next = Game_Manager.instance.gameLogic.end;
            other.gameObject.GetComponent<EnemyUnitControl>().next = next;

            //if (other.gameObject.GetComponent<EnemyUnitControl>().current == Game_Manager.instance.gameLogic.start)
            //{
            //    other.gameObject.GetComponent<EnemyUnitControl>().moveTo = next;
            //    Debug.Log("at spawn");
            //}


            if (other.gameObject.GetComponent<EnemyUnitControl>().leading && !Game_Manager.instance.gameLogic.moveHistory.Contains(next))
            {
                Game_Manager.instance.gameLogic.moveHistory.Add(next);
                Game_Manager.instance.gameLogic.lastCell = next;
            }
        }
    }
    //private Vector2 GetNextCell(Vector2 vec)
    //{
    //    List<Vector2> path = Game_Manager.instance.gameLogic.path;
    //    if(path != null)
    //    {
    //        for(int i = 0; i < path.Count; i++)
    //        {
    //            if (path[i] == vec)
    //            {
    //                return path[i + 1];
    //            }
    //        }
    //    }
    //    return new Vector2(0, 0);
    //}
}


