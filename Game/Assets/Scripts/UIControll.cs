using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UIControll : MonoBehaviour
{
    public static UIControll instance;
    public GameObject tower;
    public List<GameObject> towers;
    public List<Button> buttons;
    public InputField textField;
    public Text livesCount;
    public Text money;
    public void Awake()
    {
        instance = this;
    }

    GameObject MouseClick()
    {
        GameObject resObject=null;

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray.origin, ray.direction, out hit, 100, ~LayerMask.GetMask("Environment", "Ignore Raycast")))
            {

                resObject = hit.transform.gameObject;


                if (resObject.CompareTag("Player") || resObject.CompareTag("Enemy"))
                    Debug.Log(resObject);
                
            }


        }

        return resObject;


    }
     void KeyClickListener()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("Menu");
        }

    }
    public void TowerType(int type)
    {
        tower = towers[type - 1];
        ColourButtons(type);
    }
    public void BuyTower()
    {
        if (tower != null)
        {
            int index = towers.IndexOf(tower);
            Economics.instance.Buy(Economics.instance.prices[index]);
        }
    }
    public void ColourButtons(int buttonIndex = 0)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (i == buttonIndex - 1)
            {
                //    var color = buttons[i].GetComponent<Button>().colors;
                //    color.normalColor = Color.green;
                //    buttons[i].GetComponent<Button>().colors = color;
                buttons[i].GetComponent<Image>().color = Color.blue;
            }
            else
            {
                //var colors = buttons[i].GetComponent<Button>().colors;
                //colors.normalColor = Color.white;
                //buttons[i].GetComponent<Button>().colors = colors;
                buttons[i].GetComponent<Image>().color = Color.white;

            }
        }
    }
    
    void Update()
    {
        KeyClickListener();
        MouseClick();
        textField.text = Math.Round(SpawnEnemy.instance.waitTime, 1).ToString();
        livesCount.text = "Lives: " + Game_Manager.instance.playerLives.Lives.ToString();
        money.text = "Money: " + Economics.instance.Money.ToString();
    }



}
