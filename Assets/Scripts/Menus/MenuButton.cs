using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
        maingame.score = 0;
        maingame.remainingLives = 3;
        maingame.remainingTiles = 28;
    }
}
