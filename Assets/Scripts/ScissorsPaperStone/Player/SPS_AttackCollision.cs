using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SPS_AttackCollision : MonoBehaviour
{
    //this script handles when the player is attacking and has hit an enemy

    #region Variables

    public bool buttonPressed;
    public float rangeUptime;
    SPS_Player playerChoice;
    SPS_ScoreManager scoreInstance;
    SPS_ObjectSpawningScript waveCheck;
    ComboManager comboManager_instance;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        playerChoice = GetComponentInParent<SPS_Player>();
        scoreInstance = FindObjectOfType<SPS_ScoreManager>();
        comboManager_instance = FindObjectOfType<ComboManager>();
    }

    private void Update()
    {
        Timer();
    }

    #endregion

    #region Functions

    public void ButtonPress()
    {
        buttonPressed = true;
    }

    public void Timer()
    {
        if (buttonPressed == true)
        {
            rangeUptime += Time.deltaTime;
            if (rangeUptime >= 0.2f)
            {
                rangeUptime = 0f;
                buttonPressed = false;
                Debug.Log("finished");
                playerChoice.p_choice = SPS_Player.PlayerChoice.P_NONE;
            }
        }
    }

    //call this function when the wave of enemy is completed
    public void WaveCompleted()
    {
        waveCheck.waveCompleted = true;
    }

    #endregion


    private void OnTriggerStay(Collider other)
    {
        //we now check that the matchup is correct to kill the enemy
        //firstly we check to see if ANY button has been pressed
        if (buttonPressed == true)
        {
            //we now need to determine if the interaction is with a enemy OR an obstacle

            if (other.gameObject.tag == "EnemyTag") //enemy
            {
                if (playerChoice.p_choice == SPS_Player.PlayerChoice.P_SCISSOR
                    && other.GetComponent<SPS_Enemy>().ai_choice == SPS_Enemy.AIChoice.AI_PAPER)
                {
                    Debug.Log("Enemy goes OW: trigger stay");
                    waveCheck.waveCompleted = true;
                    Destroy(other.gameObject);
                    Destroy(other.gameObject.GetComponent<Rigidbody>());
                    scoreInstance.PlayerScores();
                    comboManager_instance.AddCombo();
                }
                else if (buttonPressed == true && playerChoice.p_choice == SPS_Player.PlayerChoice.P_PAPER
                    && other.GetComponent<SPS_Enemy>().ai_choice == SPS_Enemy.AIChoice.AI_STONE)
                {
                    Debug.Log("Enemy goes OW: trigger stay");
                    waveCheck.waveCompleted = false;
                    Destroy(other.gameObject.GetComponent<Rigidbody>());
                    Destroy(other.gameObject);
                    scoreInstance.PlayerScores();
                    comboManager_instance.AddCombo();
                }
                else if (buttonPressed == true && playerChoice.p_choice == SPS_Player.PlayerChoice.P_STONE
                    && other.GetComponent<SPS_Enemy>().ai_choice == SPS_Enemy.AIChoice.AI_SCISSOR)
                {
                    Debug.Log("Enemy goes OW: trigger stay");
                    waveCheck.waveCompleted = false;
                    Destroy(other.gameObject.GetComponent<Rigidbody>());
                    Destroy(other.gameObject);
                    scoreInstance.PlayerScores();
                    comboManager_instance.AddCombo();
                }
            }
            else if (other.gameObject.tag == "Obstacle")
            {
                if (playerChoice.p_choice == SPS_Player.PlayerChoice.P_JUMP
                    && other.GetComponent<SPS_Obstacles>().obstacle_choice == SPS_Obstacles.ObstacleChoice.OBS_MOUNTAIN)
                {
                    Debug.Log("Jumped over perfectly");
                    /* ADD CODE HERE FOR PLAYER STUN */
                }
                else if ((playerChoice.p_choice == SPS_Player.PlayerChoice.P_SLIDE)
                    && other.GetComponent<SPS_Obstacles>().obstacle_choice == SPS_Obstacles.ObstacleChoice.OBS_LOG)
                {
                    Debug.Log("Jumped over perfectly");
                    /* ADD CODE HERE FOR PLAYER STUN */
                }
            }
        }
    }

}
