using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeIcon1Behaviour : MonoBehaviour
{
    void Update()
    {
        if (maingame.remainingLives == 1)
        {
            Destroy(gameObject);
        }
    }
}
