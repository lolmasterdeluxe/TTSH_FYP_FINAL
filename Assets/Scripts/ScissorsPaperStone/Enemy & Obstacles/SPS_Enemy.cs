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

    SPS_ObjectSpawningScript objectspawningInstance;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        objectspawningInstance = FindObjectOfType<SPS_ObjectSpawningScript>();

        ai_choice = AIChoice.AI_NONE;

        RandomizeWaveFormat();
    }

    #endregion  

    #region Functions

    //this determines the type of enemy wave that will be created
    public void RandomizeWaveFormat()
    {
        if (objectspawningInstance.enemywaveType == SPS_ObjectSpawningScript.EnemyWaveType.ENEMY_SINGLE_RANDOM
            || objectspawningInstance.enemywaveType == SPS_ObjectSpawningScript.EnemyWaveType.ENEMY_WAVE_RANDOM)
        {
            RandomizeEnemyType();
        }

        else if (objectspawningInstance.enemywaveType == SPS_ObjectSpawningScript.EnemyWaveType.ENEMY_WAVE_SCISSORS)
        {
            ai_choice = AIChoice.AI_SCISSOR;
            GetComponent<Renderer>().material = scissor;
        }
        else if (objectspawningInstance.enemywaveType == SPS_ObjectSpawningScript.EnemyWaveType.ENEMY_WAVE_PAPER)
        {
            ai_choice = AIChoice.AI_PAPER;
            GetComponent<Renderer>().material = paper;
        }
        else if (objectspawningInstance.enemywaveType == SPS_ObjectSpawningScript.EnemyWaveType.ENEMY_WAVE_STONE)
        {
            ai_choice = AIChoice.AI_STONE;
            GetComponent<Renderer>().material = stone;
        }
    }

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
