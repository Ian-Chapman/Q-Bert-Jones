using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class maingame : MonoBehaviour
{
    public static int remainingTiles = 28;
    public static int remainingLives = 3;
    public static int coilyLives = 1;
    public static bool death = false; // qbert lost a life
    public static bool isCoilyDead = false;
    
    public static bool isCoilyBallActive = false;
    public static int score = 0;
    public Text UIScore;
    public static Vector3 coilyStart;
    public static bool freeze = false;

    public static bool winGame = false;

    LeaderboardBehaviour ldrboard;

    public Transform redBallBounceObj;
    public Transform greenBallBounceObj;
    public Transform coilyBallBounceObj;


    public Animator animator;
   

    void Start()
    {
        StartCoroutine(spawnRedBallBounce());
        StartCoroutine(spawnGreenBallBounce());
        animator.enabled = false;
        ldrboard = FindObjectOfType<LeaderboardBehaviour>(); 
        remainingLives = 3;
        score = 0;
        remainingTiles = 28;
        isCoilyBallActive = false;
    }

    void Update()
    {
        if (isCoilyBallActive == false)
        {
            StartCoroutine(spawnCoilyBallBounce());
        }

        if (remainingTiles == 0) // change this to demo win condition, also change tileBehavior 
        {
            Debug.Log("Win");
            winGame = true;
            StartCoroutine(WinGame()); //check for all tiles changed - flash the tiles
        }

        if (remainingLives == 0)
        {
            freeze = true;
            GameObject.FindGameObjectWithTag("Qbert").GetComponent<Rigidbody>().velocity = new  Vector3(-1, 3, 0);
            StartCoroutine(deathPause());
            ldrboard.currentScore = score;
        }

        if (freeze == true)
        {
            StartCoroutine(freezeAll());
        }

        UIScore.text = score.ToString(); //display a score on the UI
        //convert ToString for int to string
    }

        IEnumerator WinGame() //reload the level - may add other menus to load here later or remove for end game animation
        {
        score += 1000;
        remainingLives = 0;
            if (lElevatorBehaviour.lAvailable == true) score += 100;
            if (rElevatorBehaviour.rAvailable == true) score += 100; // elevator bonus

        remainingTiles = 28; //reset number of tiles to be added to the game
        ldrboard.currentScore = score;
        animator.enabled = true; //pyramid flashes several colors
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(4); // add short delay before map reloads on itself
        Time.timeScale = 1;
            
        if (ldrboard.isHighScore())
            {
            SceneManager.LoadScene("Game Over"); //go to the game over screen
            winGame = false;
            restartGame();
            }
        }
     
        IEnumerator spawnRedBallBounce()
        {
            yield return new WaitForSeconds(5.13f); // how often to spawn
        
        if (freeze == true)
        {
            yield return new WaitForSeconds(3.625f);
        }
        Instantiate(redBallBounceObj, new Vector3(0, 3, 0), redBallBounceObj.rotation); //Ball will spawn above top tile
        StartCoroutine(spawnRedBallBounce()); //Recursive
        }

        IEnumerator spawnGreenBallBounce()
        {
        yield return new WaitForSeconds(17.33f);
        Instantiate(greenBallBounceObj, new Vector3(0, 2, 0), greenBallBounceObj.rotation);

        StartCoroutine(spawnGreenBallBounce()); //Recursive
        }

   IEnumerator spawnCoilyBallBounce()
    {
        if(isCoilyBallActive == false)
        {
            isCoilyBallActive = true ;
            yield return new WaitForSeconds(7);
        
            if (Random.Range(0, 2) == 0)
            {
                Instantiate(coilyBallBounceObj, new Vector3(0, 2, -1), coilyBallBounceObj.rotation); //Spawn at T2
            }
            else                                                                                         //OR
            {
                Instantiate(coilyBallBounceObj, new Vector3(1, 2, 0), coilyBallBounceObj.rotation); //Spawn at T3
            }
            isCoilyDead = false;
        }
    }

    IEnumerator freezeAll()
    {
        yield return new WaitForSeconds(5);
        freeze = false;
    }

    IEnumerator deathPause()
    {
        yield return new WaitForSecondsRealtime(5);
        SceneManager.LoadScene("Game Over");
    }


    void restartGame()
    {
        death = false;
        maingame.score = 0;
        maingame.remainingLives = 3;
        maingame.remainingTiles = 28;
        winGame = false;
        CoilyBallBehaviour.spawnCoily = false;
        animator.enabled = false;
    }
}
