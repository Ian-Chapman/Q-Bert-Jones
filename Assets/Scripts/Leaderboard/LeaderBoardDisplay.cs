using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardDisplay : MonoBehaviour
{
    [SerializeField] int rank;
    [SerializeField] bool isScore;
    LeaderboardBehaviour leaderBoard;
    Text txt;
    // Start is called before the first frame update
    void Update()
    {
        leaderBoard = FindObjectOfType<LeaderboardBehaviour>();

        txt = this.gameObject.GetComponent<Text>();
        if (isScore)
        {
            txt.text = leaderBoard.getScore(rank-1);
        }
        else
        {
            txt.text = leaderBoard.getName(rank - 1);
        }
    }

}
