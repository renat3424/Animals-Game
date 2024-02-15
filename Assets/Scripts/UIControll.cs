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
    public GameObject chosentower;
    public List<GameObject> towers;
    public List<Button> buttons;
    public InputField textField;
    public Text livesCount;
    public Text money;
    public GameObject Tw;
    public Image blackScreen;
    public float fadeSpeed=1.5f;
    public void ScreenFade()
    {

        blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
    }

    public void ScreenBright()
    {

        blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

    }
    public void Awake()
    {
        instance = this;
    }

    void MouseClick()
    {
      
        GameObject resObject = null;

        if (Input.GetMouseButtonDown(0))
        {
            if (Tw != null)
            {

                Tw.GetComponentInChildren<TowersControl>().CloseUi();
            }
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray.origin, ray.direction, out hit, 100, ~LayerMask.GetMask("Environment", "Ignore Raycast", "Plane")))
            {

                resObject = hit.transform.gameObject;
                Debug.Log(resObject.tag);

                if (resObject.tag=="Player")
                {

                    Debug.Log("Player here");

                       Tw = resObject;

                    Tw.GetComponentInChildren<TowersControl>().OpenUi();


                }
                    

            }



        }




    }

    Vector3 GetPlanePosition()
    {


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Physics.Raycast(ray.origin, ray.direction, out hit, 100, LayerMask.GetMask("Plane"));

        return hit.point;



    }
    void KeyClickListener()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("MainMenu");
        }

    }
    public void TowerType(int type)
    {
        if (chosentower != null)
        {
            Destroy(chosentower);
        }
        tower = towers[type - 1];
        tower.GetComponentInChildren<TowersControl>().enabled = false;

        tower.GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        chosentower = Instantiate(tower, GetPlanePosition() + new Vector3(0, 1, 0), tower.transform.rotation);
        tower.GetComponentInChildren<TowersControl>().enabled = true;
        tower.GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.None;


        ColourButtons(type);
    }

    public void MoveTower()
    {

        if (chosentower != null)
        {

            chosentower.transform.position = GetPlanePosition() + new Vector3(0, 1, 0);

        }
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
        MouseClick();
        KeyClickListener();
        MoveTower();
        textField.text = Math.Round(SpawnEnemy.instance.waitTime, 1).ToString();
        livesCount.text = "Lives: " + Game_Manager.instance.playerLives.Lives.ToString();
        money.text = "Money: " + Economics.instance.Money.ToString();
    }



}
