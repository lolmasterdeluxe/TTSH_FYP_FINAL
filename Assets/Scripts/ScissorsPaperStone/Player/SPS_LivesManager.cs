using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPS_LivesManager : MonoBehaviour
{
    //this handles the lives of the players

    #region Variables

    int numberofLives;
    public bool playerisAlive;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        //we establish the number of lives a player has at the start:
        numberofLives = 5;
        //we establish the boolean flags on Start
        playerisAlive = true;
    }

    private void Update()
    {
        //when player is dead
        if (playerisAlive == false)
        {
            //player is DEAD
            ScoreManager.Instance.EndCurrentGameScore();
        }
    }

    #endregion

    #region Functions

    public void PlayerTakesDamage()
    {
        numberofLives -= 1;
        Debug.Log("You lost a life!");

        if (numberofLives <= 0)
        {
            playerisAlive = false; 
            numberofLives = 0;
        }
    }

    #endregion

}
