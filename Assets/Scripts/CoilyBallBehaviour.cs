using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//

public class CoilyBallBehaviour : MonoBehaviour
{
    public GameObject coilyObj;
    public GameObject qBertObj;

    public Vector3 coilyPos;
    public static bool spawnCoily = false;

    public static bool coilyBallActive = false;
    public Vector3 qBertPos;

    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;

    public static Vector3 restoreA;
    public static Vector3 restoreB;
    public static bool isVecRestore = false;


    public Animator animator;

    public AudioSource coilySound;
    public AudioClip jumpSound;

   // Update is called once per frame
    void Update()
    {
        if (maingame.death == true)
        {
            spawnCoily = false;
            Destroy(gameObject);
        }

        //Freeze the AI
        if (maingame.freeze == true)
        {
            if (isVecRestore == false)
            {
                restoreA = GetComponent<Rigidbody>().velocity;
                restoreB = GetComponent<Rigidbody>().angularVelocity;
            }
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            isVecRestore = true;
        }

        else
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            if (isVecRestore == true)
            {
                GetComponent<Rigidbody>().velocity = restoreA;
                GetComponent<Rigidbody>().angularVelocity = restoreB;
            }
            isVecRestore = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        coilyBallActive = true;

        if (spawnCoily == false)
        {

            if (other.gameObject.GetComponent<bottomRow>().bottom == true)
            {
                spawnCoily = true;
                GetComponent<Rigidbody>().velocity = new Vector3(0, 4, 0);
                ChangeSprite();
            }

            else
            {
                if (Random.Range(0, 2) == 0)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(0, 4, -1);
                }
                else
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(1, 4, 0);
                }
               
                if (other.gameObject.tag == "Qbert")
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                    spawnCoily = false;
                    maingame.freeze = true;
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            int x, y, z;
            qBertObj = GameObject.FindWithTag("Target");
            qBertPos = qBertObj.GetComponent<Transform>().position;
            coilyPos = other.gameObject.GetComponent<Transform>().position;

            if (qBertPos.y > coilyPos.y)
            {
                y = 6;
                coilySound.clip = jumpSound; //play sound if tile changes color
                coilySound.Play();
            }
            else
            {
                y = 4;
                coilySound.clip = jumpSound; //play sound if tile changes color
                coilySound.Play();
            }

            if (qBertPos.y == coilyPos.y)
            {
                if (Random.Range(0, 2) == 0)
                {
                    y = 4;
                    coilySound.clip = jumpSound; //play sound if tile changes color
                    coilySound.Play();
                }
                else
                {
                    y = 6;
                    coilySound.clip = jumpSound; //play sound if tile changes color
                    coilySound.Play();
                }
            }

            if (other.gameObject.tag == "Ground")
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                maingame.score += 500;
                spawnCoily = false;
                maingame.isCoilyBallActive = false;
                maingame.freeze = true;
                maingame.isCoilyDead = true;
                Destroy(gameObject);
            }
            else if (other.gameObject.name == "Elevator_Target_Right")
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                maingame.score += 500;
                spawnCoily = false;
                maingame.isCoilyBallActive = false;
                maingame.isCoilyDead = true;
                Destroy(gameObject);
            }

            else if (other.gameObject.name == "Elevator_Target_Left")
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                maingame.score += 500;
                spawnCoily = false;
                maingame.isCoilyBallActive = false;
                maingame.freeze = true;
                maingame.isCoilyDead = true;
                Destroy(gameObject);
            }

            else if (other.gameObject.tag == "Elevator")
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                maingame.score += 500;
                spawnCoily = false;
                maingame.isCoilyBallActive = false;
                maingame.freeze = true;
                maingame.isCoilyDead = true;
                Destroy(gameObject);
            }

            if (other.gameObject.GetComponent<bottomRow>().bottom == true)
            {
                y = 6;
                coilySound.clip = jumpSound;
                coilySound.Play();
            }

            if (y == 6)
            {
                if (qBertPos.z == coilyPos.z)
                {
                    x = -1;
                    z = 0;
                }

                else if (qBertPos.z > coilyPos.z && qBertPos.x < coilyPos.x)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        x = -1;
                        z = 0;
                    }
                    else
                    {
                        x = 0;
                        z = 1;
                    }
                }

                else if (qBertPos.z < coilyPos.z && qBertPos.x < coilyPos.x) { x = -1; z = 0; }

                else { z = 1; x = 0; }
            }

            else
            {
                if (qBertPos.x == coilyPos.x)
                {
                    x = 0;
                    z = -1;
                }

                else if (qBertPos.z < coilyPos.z && qBertPos.x > coilyPos.x)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        x = 1;
                        z = 0;
                    }
                    else
                    {
                        x = 0;
                        z = -1;
                    }
                }

                else if (qBertPos.z > coilyPos.z && qBertPos.x > coilyPos.x) { x = 1; z = 0; }

                else { z = -1; x = 0; }


            }
            GetComponent<Rigidbody>().velocity = new Vector3(x, y, z);
        }
    }

    void ChangeSprite()
    {
        spriteRenderer.sprite = newSprite;
    }
}

