using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FieldGeneration : MonoBehaviour
{
    public InputField width, height;
    public string SceneName;
    public void GenerateBtnClick()
    {
        instance = this;
        SceneManager.LoadScene(SceneName);
    }
    public static FieldGeneration instance;
    public void Awake()
    {
        
    }
}
