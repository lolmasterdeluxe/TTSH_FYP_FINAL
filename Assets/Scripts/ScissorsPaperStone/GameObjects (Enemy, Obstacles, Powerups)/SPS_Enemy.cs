using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SPS_Enemy : MonoBehaviour
{
    //this script handles all things involved with the enemy

    public enum AIChoice
    {
        AI_SCISSOR, AI_PAPER, AI_STONE, AI_NONE
    };

    #region Variables

    Animation enemyAnim;
    Animator enemyAC;
    public AIChoice ai_choice;

    SPS_ObjectSpawningScript objectspawningInstance;

    [SerializeField]
    private RuntimeAnimatorController scissorsController, paperController, stoneController;

    #endregion

    #region Unity Callbacks

    private void Start()
    {

        enemyAnim = GetComponent<Animation>();
        enemyAC = GetComponent<Animator>();

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
        if (objectspawningInstance.enemywaveType == SPS_ObjectSpawningScript.EnemyWaveType.ENEMY_WAVE_SCISSORS)
        {
            ai_choice = AIChoice.AI_SCISSOR;
            enemyAC.runtimeAnimatorController = scissorsController;
        }
        if (objectspawningInstance.enemywaveType == SPS_ObjectSpawningScript.EnemyWaveType.ENEMY_WAVE_PAPER)
        {
            ai_choice = AIChoice.AI_PAPER;
            enemyAC.runtimeAnimatorController = paperController;
        }
        if (objectspawningInstance.enemywaveType == SPS_ObjectSpawningScript.EnemyWaveType.ENEMY_WAVE_STONE)
        {
            ai_choice = AIChoice.AI_STONE;
            enemyAC.runtimeAnimatorController = stoneController;
        }
    }

    public void RandomizeEnemyType()
    {
        int randVal = Random.Range(0, 3);
        switch (randVal)
        {
            case 0:
                ai_choice = AIChoice.AI_SCISSOR;
                enemyAC.runtimeAnimatorController = scissorsController;
                break;
            case 1:
                ai_choice = AIChoice.AI_PAPER;
                enemyAC.runtimeAnimatorController = paperController;
                break;
            case 2:
                ai_choice = AIChoice.AI_STONE;
                enemyAC.runtimeAnimatorController = stoneController;
                break;
        }
    }

    #endregion
}
