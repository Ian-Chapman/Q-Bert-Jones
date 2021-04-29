using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeIcon2Behaviour : MonoBehaviour
{
    void Update()
    {
        if (maingame.remainingLives == 2)
        {
            Destroy(gameObject);
        }
    }
}
