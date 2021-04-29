using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lElevatorBehaviour : MonoBehaviour
{
    public AudioSource discSound;
    public AudioClip upSound;
    public static bool lAvailable = true;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Qbert")
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 2, 2);
            //nn destroy elevator
            discSound.clip = upSound;
            discSound.Play();

            lAvailable = false;
        }
    }
}
