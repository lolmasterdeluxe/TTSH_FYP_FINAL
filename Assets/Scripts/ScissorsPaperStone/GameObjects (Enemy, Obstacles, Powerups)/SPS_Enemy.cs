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

    //store enemy animation clips HERE
    public GameObject scissorsAnim, paperAnim, stoneAnim;

    #endregion

    #region Unity Callbacks

    private void Start()
    {

        enemyAnim = GetComponent<Animation>();
        enemyAC = GetComponent<Animator>();

        objectspawningInstance = FindObjectOfType<SPS_ObjectSpawningScript>();
        ai_choice = AIChoice.AI_NONE;

        //at the start, find prefabs to assign values
        scissorsAnim = GameObject.Find("ScissorsAnimation");
        paperAnim = GameObject.Find("PaperAnimation");
        stoneAnim = GameObject.Find("StoneAnimation");

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
            enemyAnim = scissorsAnim.gameObject.GetComponent<Animation>();
            enemyAC = scissorsAnim.gameObject.GetComponent<Animator>();
        }
        if (objectspawningInstance.enemywaveType == SPS_ObjectSpawningScript.EnemyWaveType.ENEMY_WAVE_PAPER)
        {
            ai_choice = AIChoice.AI_PAPER;
        }
        if (objectspawningInstance.enemywaveType == SPS_ObjectSpawningScript.EnemyWaveType.ENEMY_WAVE_STONE)
        {
            ai_choice = AIChoice.AI_STONE;
        }
    }

    public void RandomizeEnemyType()
    {
        int randVal = Random.Range(0, 3);
        switch (randVal)
        {
            case 0:
                ai_choice = AIChoice.AI_SCISSOR;
                enemyAnim = scissorsAnim.gameObject.GetComponent<Animation>();
                enemyAC = scissorsAnim.gameObject.GetComponent<Animator>();
                break;
            case 1:
                ai_choice = AIChoice.AI_PAPER;
                break;
            case 2:
                ai_choice = AIChoice.AI_STONE;
                break;
        }
    }

    #endregion
}
