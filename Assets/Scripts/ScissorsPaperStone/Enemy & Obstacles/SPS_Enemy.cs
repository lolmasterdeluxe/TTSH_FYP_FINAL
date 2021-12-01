using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPS_Enemy : MonoBehaviour
{
    //this script handles all things involved with the enemy

    public enum AIChoice
    {
        AI_SCISSOR, AI_PAPER, AI_STONE, AI_NONE
    };

    #region Variables

    public AIChoice ai_choice;
    public Material scissor, paper, stone;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        ai_choice = AIChoice.AI_NONE;
        RandomizeEnemyType();
    }

    #endregion  

    #region Functions

    public void RandomizeEnemyType()
    {
        int randVal = Random.Range(0, 3);
        switch (randVal)
        {
            case 0:
                ai_choice = AIChoice.AI_SCISSOR;
                GetComponent<Renderer>().material = scissor;
                break;
            case 1:
                ai_choice = AIChoice.AI_PAPER;
                GetComponent<Renderer>().material = paper;
                break;
            case 2:
                ai_choice = AIChoice.AI_STONE;
                GetComponent<Renderer>().material = stone;
                break;
        }

    }


    #endregion
}
