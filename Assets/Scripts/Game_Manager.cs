using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;
    int fieldWidth, fieldHeigth;
    public GameObject fieldCell; //клетка
    public GameObject[,] fieldCells; //массив клеток
    public GameObject plane;
    GameObject enemyUnit;
    //public List<GameObject> enemyUnits;
    public GameObject tower;
    public GameLogic gameLogic;
    public PlayerLives playerLives;
    public List<GameObject> activeUnits;
    public List<GameObject> enemies = new List<GameObject>();
    public Material pathCol;
    public Material fieldCol;
    public Vector2 end;
    //public Vector3 spawnPosition;
    public Vector3 startPosition;
    GameObject empty;
    public int lives;
    public bool gameState=false;
    public bool isVictory;
    public bool toMainMenu;
    public void Awake()
    {
        empty = new GameObject();
        instance = this;
        activeUnits = new List<GameObject>();
        fieldWidth = 20;
        fieldHeigth = 20;
        fieldCells = new GameObject[fieldHeigth, fieldWidth];
        gameLogic = new GameLogic(fieldHeigth, fieldWidth, new Vector2(0, 0), new Vector2(fieldHeigth - 1, fieldWidth - 1));
        playerLives = new PlayerLives(lives);
        enemies = new List<GameObject>();
        startPosition = new Vector3(gameLogic.start.x, 0, gameLogic.start.y);
        
        for (int i = 0; i < fieldHeigth; i++)
        {
            for (int j = 0; j < fieldWidth; j++)
            {
                //z - высота, x - ширина
                fieldCells[i, j] = Instantiate(fieldCell, new Vector3((fieldCell.transform.localScale.x + 0.05f) * j, 0, (fieldCell.transform.localScale.z + 0.05f) * i), fieldCell.transform.rotation);
                //Debug.Log($"{fieldCells[i, j].transform.position}");
                fieldCells[i, j].GetComponent<CellControl>().x = i;
                fieldCells[i, j].GetComponent<CellControl>().y = j;
        
            }
        }
        Vector3 pos = ((fieldCells[fieldHeigth - 1, fieldWidth - 1].transform.position - fieldCells[0, 0].transform.position) / 2);
        GameObject newPlane = Instantiate(plane, new Vector3(pos.x, fieldCells[0, 0].transform.GetChild(0).transform.position.y, pos.z), plane.transform.rotation);
        newPlane.transform.localScale =
            new Vector3(plane.transform.localScale.x * (fieldWidth*15f), plane.transform.localScale.y, plane.transform.localScale.z * (fieldHeigth*15f));
        startPosition = new Vector3(gameLogic.start.x, fieldCells[(int)gameLogic.start.x, (int)gameLogic.start.y].transform.position.y, gameLogic.start.y);
        ShowPath();
        gameState = true;
    }
    

    public GameObject GetLastActiveUnit()
    {
        return activeUnits[activeUnits.Count - 1];
    }
    public void ClearPath()
    {
        if (gameLogic.path != null)
        {
            foreach (Vector2 vec in gameLogic.path)
            {
                int x = (int)vec.x, y = (int)vec.y;
                GameObject fieldCube = fieldCells[x, y].transform.GetChild(0).gameObject;
                fieldCube.GetComponent<Renderer>().material = fieldCol;
            }

        }


    }
    public  void ShowPath()
    {

        gameLogic.IfPath(gameLogic.lastCell);
        gameLogic.PathToList();
        foreach (Vector2 vec in gameLogic.path)
        {
            int x = (int)vec.x, y = (int)vec.y;
            GameObject fieldCube = fieldCells[x, y].transform.GetChild(0).gameObject;
            fieldCube.GetComponent<Renderer>().material = pathCol;
        }
    }
 
    public void ClearUnits()
    {

        for(int i = 0; i < activeUnits.Count; i++)
        {
            Destroy(activeUnits[i]);
        }
        activeUnits.Clear();
        ClearPath();
        ShowPath();
    }


    public void GameOver()
    {

        StartCoroutine(GameOver1());
       
    }

    public IEnumerator GameOver1()
    {
        gameState = false;
        yield return new WaitForSeconds(UIControll.instance.fadeSpeed);

        if (isVictory)
        {
            SceneManager.LoadScene("Victory");
        }
        else
        {
            SceneManager.LoadScene("Defeat");
        }
    }
    public void Start()
    {

       
    }
    // Update is called once per frame
    void Update()
    {
        if (gameState == true)
        {
            UIControll.instance.ScreenBright();
        }
        else
        {
            UIControll.instance.ScreenFade();
        }
    }
}
