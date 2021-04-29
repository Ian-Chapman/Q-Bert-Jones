using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBallBehaviour : MonoBehaviour
{
    public static Vector3 restoreA;
    public static Vector3 restoreB;
    public static bool isVecRestore = false;

    // Update is called once per frame
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
