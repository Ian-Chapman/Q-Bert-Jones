using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Start()
    {
       SceneManager.LoadScene("myMap");
        maingame.score = 0;
        maingame.remainingLives = 3;
        maingame.remainingTiles = 28;
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void Leaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
        maingame.score = 0;
        maingame.remainingLives = 3;
        maingame.remainingTiles = 28;
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
        maingame.score = 0;
        maingame.remainingLives = 3;
        maingame.remainingTiles = 28;
    }
}
