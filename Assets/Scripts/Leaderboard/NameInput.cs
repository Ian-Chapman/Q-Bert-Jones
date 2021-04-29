using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NameInput : MonoBehaviour
{
    string playerName;
    bool isNameSet;
    LeaderboardBehaviour ldrBoard;

    Text txt;

    [SerializeField] Text scoretxt;

    [SerializeField] GameObject highScoreImage;
    [SerializeField] GameObject nameImage;
    [SerializeField] GameObject colonImage;

    [SerializeField] GameObject typeYourName;

    // Start is called before the first frame update
    void Start()
    {
        txt = this.gameObject.GetComponent<Text>();
        playerName = "___";
        isNameSet = false;
        ldrBoard = FindObjectOfType<LeaderboardBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        scoretxt.text = ldrBoard.currentScore.ToString();
        if (ldrBoard.isHighScore())
        {
            getNameFromInput();
        }
        else {
            typeYourName.SetActive(false);
            txt.enabled = false;
            highScoreImage.SetActive(false);
            nameImage.SetActive(false);
            colonImage.SetActive(false);
        }
    
    }

    void getNameFromInput()
    {
        if (!isNameSet)
        {

            foreach (char c in Input.inputString)
            {
                if (c == '\b') // has backspace/delete been pressed?
                {
                }
                else if ((c == '\n') || (c == '\r')) // enter/return
                {
                    isNameSet = true;
                    ldrBoard.AddScore(playerName);
                    StartCoroutine(goBackToMenu());
                    
                }
                else //character
                {
                    if (playerName.Length < 3)
                    {
                        playerName = playerName + c;
                    }
                    else
                    {
                        playerName = playerName.Substring(1, 2) + c;
                    }
                }
                txt.text = playerName;
            }
        }
    }

    private IEnumerator goBackToMenu()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        SceneManager.LoadScene("MainMenu");
    }

}
