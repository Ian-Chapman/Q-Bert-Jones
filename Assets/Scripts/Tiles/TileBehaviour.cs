using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    public Material Purple;
    public int colourCode = 1; //Prevent T1 from changing color at game start
    
    public AudioSource tileSound;
    public AudioClip changeColorSound;
    public AudioClip noChangeSound;

    public AudioClip redBounceSound;
    public AudioClip greenBounceSound;
    public AudioClip coilyBallBounceSound;
    
    public AudioClip victorySound;

    void Update()
    {
        if(maingame.winGame == true)
        {
            GetComponent<Animator>().enabled = true;
        }
        else
            GetComponent<Animator>().enabled = false;
    }

    private void OnCollisionEnter(Collision other) //for collision with any other game object
    {
        if (other.gameObject.tag == "Qbert")
        {
            colourCode -= 1;
            if (colourCode == 0) // change a tile to have color code -1
            {
                GetComponent<Renderer>().material = Purple;
                maingame.remainingTiles -= 1;

                maingame.score += 25; // 25 points per tile
                Debug.Log(maingame.score);

                tileSound.clip = changeColorSound; //play sound if tile changes color
                tileSound.Play();
            }

            else
            {
                tileSound.clip = noChangeSound; //no change in the color for this sound
                tileSound.Play();
            }

        }

        if (other.gameObject.tag == "Red_Ball_Bounce")
        {
            tileSound.clip = redBounceSound; //red ball sound
            tileSound.Play();
        }

        if (other.gameObject.tag == "Green_Ball_Bounce")
        {
            tileSound.clip = greenBounceSound; //green ball sound
            tileSound.Play();
        }

        if (other.gameObject.tag == "Coily_Ball")
        {
            tileSound.clip = coilyBallBounceSound; //green ball sound
            tileSound.Play();
        }

        if (maingame.remainingTiles == 0)
        {
            tileSound.clip = victorySound; //play sound if tile changes color
            tileSound.Play();
        }

    }
}


