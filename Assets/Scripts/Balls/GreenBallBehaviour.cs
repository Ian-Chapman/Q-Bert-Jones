using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBallBehaviour : MonoBehaviour
{

    void Update()
    {
        if (maingame.death == true)
        {
            Destroy(gameObject);
        }

        if (maingame.isCoilyDead == true)
        {
            Destroy(gameObject);
        }

        if (maingame.remainingLives == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        //Create the randomness needed to have the ball fall between a choice of 2 tiles
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
            maingame.freeze = true;
            maingame.score += 100;
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {                                     
        if (other.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
