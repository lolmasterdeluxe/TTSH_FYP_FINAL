using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SPS_ScoreManager : MonoBehaviour
{
    //this script handles the score the player gets
    //we take the final score and sent it into a local general pooled score

    #region Variables

    int playerScore;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        //player starts with a score of 0
        playerScore = 0;
    }

    private void Update()
    {

    }

    #endregion

    #region Functions   

    public void PlayerScores()
    {
        playerScore += 1;
    }

    #endregion
}
