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

    public void Start()
    {
        //we establish the number of lives a player has at the start:
        numberofLives = 5;
        //we establish the boolean flags on Start
        playerisAlive = true;
    }

    #endregion

    #region Functions

    public void PlayerDies()
    {
        playerisAlive = false;
        Debug.Log("Player has died! Game has ended xD");
    }

    public void PlayerTakesDamage()
    {
        numberofLives -= 1;
        Debug.Log("You lost a life!");

        if (numberofLives < 0)
        {
            numberofLives = 0;
        }
    }

    #endregion

}
