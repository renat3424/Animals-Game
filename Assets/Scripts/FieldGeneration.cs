using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FieldGeneration : MonoBehaviour
{

    public string SceneName;
    public void GenerateBtnClick()
    {
        instance = this;
        SceneManager.LoadScene(SceneName);
    }
    public static FieldGeneration instance;

    public void Quit()
    {

        Application.Quit();
    }
    public void Awake()
    {
        
    }
}
