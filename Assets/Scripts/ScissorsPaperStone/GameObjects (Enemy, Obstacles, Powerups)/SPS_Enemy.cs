using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SPS_Enemy : MonoBehaviour
{
    //this script handles all things involved with the enemy

    public enum EnemyType
    {
        ENEMY_SCISSORS, ENEMY_PAPER, ENEMY_STONE, ENEMY_NONE
    };

    #region Variables

    public Animator enemyAC;
    public EnemyType enemy_type;

    SPS_ObjectManager objectmanagerInstance;

    public RuntimeAnimatorController scissorsController, paperController, stoneController;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        //get components HERE
        enemyAC = GetComponent<Animator>();

        //get references HERE
        objectmanagerInstance = FindObjectOfType<SPS_ObjectManager>();

        DetermineEnemyType();
    }

    #endregion  

    #region Helper Functions

    public void DetermineEnemyType()
    {
        if (objectmanagerInstance.current_waveFormat == SPS_ObjectManager.WaveFormat.WAVE_MULTIPLE_RANDOM
            || objectmanagerInstance.current_waveFormat == SPS_ObjectManager.WaveFormat.WAVE_SINGLE_RANDOM)
            EnemyTypeIsRandom();
            
        if (objectmanagerInstance.current_waveFormat == SPS_ObjectManager.WaveFormat.WAVE_MULTIPLE_SCISSORS)
        {
            EnemyTypeIsScissors();
            Debug.Log("Scissors");
        }
        if (objectmanagerInstance.current_waveFormat == SPS_ObjectManager.WaveFormat.WAVE_MULTIPLE_PAPER)
        {
            EnemyTypeIsPaper();
            Debug.Log("paper");
        }
        if (objectmanagerInstance.current_waveFormat == SPS_ObjectManager.WaveFormat.WAVE_MULTIPLE_STONE)
        {
            Debug.Log("Stone");
            EnemyTypeIsStone();
        }
    }

    public void EnemyTypeIsRandom()
    {
        int randVal = Random.Range(0, 3);
        switch (randVal)
        {
            case 0:
                enemy_type = EnemyType.ENEMY_SCISSORS;
                enemyAC.runtimeAnimatorController = scissorsController;
                Debug.Log("Scissors Made");
                break;
            case 1:
                enemy_type = EnemyType.ENEMY_PAPER;
                enemyAC.runtimeAnimatorController = paperController;
                Debug.Log("Paper Made");
                break;
            case 2:
                enemy_type = EnemyType.ENEMY_STONE;
                enemyAC.runtimeAnimatorController = stoneController;
                Debug.Log("STone Made");
                break;
        }
    }

    public void EnemyTypeIsScissors()
    {
        enemy_type = EnemyType.ENEMY_SCISSORS;
        enemyAC.runtimeAnimatorController = scissorsController;
    }

    public void EnemyTypeIsPaper()
    {
        enemy_type = EnemyType.ENEMY_PAPER;
        enemyAC.runtimeAnimatorController = paperController;
    }
    public void EnemyTypeIsStone()
    {
        enemy_type = EnemyType.ENEMY_STONE;
        enemyAC.runtimeAnimatorController = stoneController;
    }

    #endregion

}
