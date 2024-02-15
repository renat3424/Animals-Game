using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    int lives;
    public PlayerLives(int lives)
    {
        this.lives = lives;
    }
    public int Lives
    {
        get { return lives; }
        set { lives = value; }
    }
    public bool isGameOver()
    {
        return lives <= 0;
    }
}
