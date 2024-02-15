using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CellBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public Material col1;
    GameObject parent;
    bool activeCell=true;
    GameObject cellObj;
    public Material col2;
    public Material col3;
    GameObject tower;

    public void Awake()
    {
        parent = gameObject.transform.parent.gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cellObj == null)
        {

            activeCell = true;
        }
    }
    void OnMouseEnter()
    {
        
        gameObject.GetComponent<Renderer>().material = col1;
            
    }

void OnMouseExit()
    {
        int x = parent.GetComponent<CellControl>().x;
        int y= parent.GetComponent<CellControl>().y;
        if (Game_Manager.instance.gameLogic.path.Contains(new Vector2(x, y)))
        {
            gameObject.GetComponent<Renderer>().material = col3;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = col2;
        }
        
    }

       void OnMouseDown()
    {
        if (activeCell)
        {


            tower = UIControll.instance.tower;
            if (tower == null) return;
            gameObject.transform.parent.gameObject.GetComponent<CellControl>().OnTriggerEnter(tower.transform.GetChild(0).GetComponent<Collider>());
            if (gameObject.transform.parent.gameObject.GetComponent<CellControl>().allowPut)
            {
                cellObj = Instantiate(tower, transform.position + new Vector3(0, 1, 0), tower.transform.rotation);
                try
                {
                    Game_Manager.instance.activeUnits.Add(cellObj);
                }
                catch
                {
                    Debug.Log("error");
                }
                cellObj.transform.GetChild(0).GetComponent<TowersControl>().pos = new Vector2(parent.GetComponent<CellControl>().x, parent.GetComponent<CellControl>().y);
                UIControll.instance.BuyTower();
                UIControll.instance.ColourButtons();
                tower = UIControll.instance.tower = null;
                activeCell = false;
            }

        }
    }
}
