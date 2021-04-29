using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rElevatorBehaviour : MonoBehaviour
{
    public AudioSource discSound;
    public AudioClip upSound;
    public static bool rAvailable = true;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == ("Qbert"))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(-2, 2, 0);
            //nn destroy elevator
            discSound.clip = upSound;
            discSound.Play();

            rAvailable = false;
        }
    }
}
