using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public bool isInAir = false;
    public GameObject prevTile;
    public GameObject swear;
    public GameObject swearSnake;

    public Animator animator;

    public AudioSource qbertSound;
    public AudioClip deathByBall;
    public AudioClip deathByCoilyBall;
    public AudioClip prizeSound;
    public AudioClip hitGroundSound;


    void Update()
    {
        if (GetComponent<Rigidbody>().velocity != new Vector3(0, 0, 0)) 
            isInAir = true;
        else 
            isInAir = false;
        
            if ((Input.GetKeyDown("a")) && (isInAir == false))//jump down left
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, 4, -1);
                animator.SetBool("isJumping", true);
                animator.SetInteger("jumpDirection", 0);
            }

            if ((Input.GetKeyDown("w")) && (isInAir == false)) //jump up right
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, 6, 1);
                animator.SetBool("isJumping", true);
                animator.SetInteger("jumpDirection", 1);
            }

            if ((Input.GetKeyDown("d")) && (isInAir == false)) //jump down right
            {
                GetComponent<Rigidbody>().velocity = new Vector3(1, 4, 0);
                animator.SetBool("isJumping", true);
                animator.SetInteger("jumpDirection", 2);
            }

            if ((Input.GetKeyDown("e")) && (isInAir == false)) //jump up left
            {
                GetComponent<Rigidbody>().velocity = new Vector3(-1, 6, 0);
                animator.SetBool("isJumping", true);
                animator.SetInteger("jumpDirection", 3);
            }
        
    }

    private void OnTriggerEnter(Collider other) //For collisions with the ground
    {


        if (other.tag == "Ground")
        {
            maingame.remainingLives -= 1;
            StartCoroutine(delayDeath());
            animator.SetBool("isJumping", false);
            qbertSound.clip = hitGroundSound; //qbert cries when he hits ground
            qbertSound.Play();

            Debug.Log(maingame.remainingLives);
        }

        if (other.tag == "Right_Elevator_Stop")
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 4, -1); //Qbert jumps off the right elevator

        }

        if (other.tag == "Left_Elevator_Stop")
        { 
            GetComponent<Rigidbody>().velocity = new Vector3(1, 4, 0);//Jumps off left elevator
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        isInAir = false; //resets after landing on the tile again. This prevents button spamming

        if (other.gameObject.tag == "Red_Ball_Bounce") // nn something like this for coily ball later
        {
            maingame.death = true;
            maingame.remainingLives -= 1;
            swear.GetComponent<SpriteRenderer>().enabled = true;
            StartCoroutine(delayDeath());
            animator.SetBool("isJumping", false);

            qbertSound.clip = deathByBall; //Qbert swears
            qbertSound.Play();
        }

        if (other.gameObject.tag == "Coily_Ball")
        {
            maingame.death = true;
            maingame.remainingLives -= 1;
            swearSnake.GetComponent<SpriteRenderer>().enabled = true;
            StartCoroutine(delayDeath());
            animator.SetBool("isJumping", false);

            qbertSound.clip = deathByCoilyBall; //Qbert swears
            qbertSound.Play();
        }

        if (other.gameObject.tag == "tile")
        {
            prevTile = GameObject.FindWithTag("Target");
            prevTile.tag = "tile";
            other.gameObject.tag = "Target"; //Qbert will land on a tile and change its tag to target. Coily then chases the Target tile.
            animator.SetBool("isJumping", false); // Set jumping animation to false

        }

        if (other.gameObject.tag == "Green_Ball_Bounce")
        {
            qbertSound.clip = prizeSound; 
            qbertSound.Play();
        }
    }

    IEnumerator delayDeath()
    {
        maingame.death = true;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;

        GetComponent<Transform>().position = GameObject.FindWithTag("Target").GetComponent<Transform>().position + new Vector3(0, 1, 0);
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        maingame.death = false;
        maingame.isCoilyBallActive = false;
        swear.GetComponent<SpriteRenderer>().enabled = false;
        swearSnake.GetComponent<SpriteRenderer>().enabled = false;
    }
}

