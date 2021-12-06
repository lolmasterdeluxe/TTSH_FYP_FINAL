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

    Animator ac;
    SPS_Player playerChoice;
    SPS_ScoreManager scoreInstance;
    SPS_ObjectSpawningScript objectspawningInstance;
    ComboManager combomanagerInstance;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        ac = FindObjectOfType<SPS_Player>().GetComponent<Animator>();
        playerChoice = GetComponentInParent<SPS_Player>();
        scoreInstance = FindObjectOfType<SPS_ScoreManager>();
        combomanagerInstance = FindObjectOfType<ComboManager>();
        objectspawningInstance = FindObjectOfType<SPS_ObjectSpawningScript>();
    }

    private void Update()
    {
        Timer();

        //check to see if wave is completed every frame
        if (objectspawningInstance.objectwaveList.Count == 0)
        {
            objectspawningInstance.waveCompleted = false;
        }
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
                playerChoice.p_choice = SPS_Player.PlayerChoice.P_NONE;

                //we set all the action (button )animations to be false since everything should be reset
                ac.SetBool("PlayerAttackingWithScissors", false);
                ac.SetBool("PlayerAttackingWithPaper", false);
                ac.SetBool("PlayerAttackingWithStone", false);
                ac.SetBool("PlayerJumped", false);
            }
        }
    }

    //call this function when the wave of enemy is completed
    public void WaveCompleted()
    {
        objectspawningInstance.waveCompleted = true;
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

                    //we first destroy the instance of the gameObject in the list
                    objectspawningInstance.objectwaveList.Remove(other.gameObject);

                    //now remove the gameobject and its rigidbody from the scene
                    Destroy(other.gameObject);
                    Destroy(other.gameObject.GetComponent<Rigidbody>());

                    //we do lives and combo calculations here
                    scoreInstance.PlayerScores();
                    combomanagerInstance.AddCombo();
                }

                else if (buttonPressed == true && playerChoice.p_choice == SPS_Player.PlayerChoice.P_PAPER
                    && other.GetComponent<SPS_Enemy>().ai_choice == SPS_Enemy.AIChoice.AI_STONE)
                {
                    Debug.Log("Enemy goes OW: trigger stay");

                    //we first destroy the instance of the gameObject in the list
                    objectspawningInstance.objectwaveList.Remove(other.gameObject);

                    //now remove the gameobject and its rigidbody from the scene
                    Destroy(other.gameObject);
                    Destroy(other.gameObject.GetComponent<Rigidbody>());

                    //we do lives and combo calculations here
                    scoreInstance.PlayerScores();
                    combomanagerInstance.AddCombo();
                }

                else if (buttonPressed == true && playerChoice.p_choice == SPS_Player.PlayerChoice.P_STONE
                    && other.GetComponent<SPS_Enemy>().ai_choice == SPS_Enemy.AIChoice.AI_SCISSOR)
                {
                    Debug.Log("Enemy goes OW: trigger stay");

                    //we first destroy the instance of the gameObject in the list
                    objectspawningInstance.objectwaveList.Remove(other.gameObject);

                    //now remove the gameobject and its rigidbody from the scene
                    Destroy(other.gameObject);
                    Destroy(other.gameObject.GetComponent<Rigidbody>());

                    //we do lives and combo calculations here
                    scoreInstance.PlayerScores();
                    combomanagerInstance.AddCombo();
                }
            }

            else if (other.gameObject.tag == "Obstacle") //obstacle
            {
                if (buttonPressed == true && playerChoice.p_choice == SPS_Player.PlayerChoice.P_JUMP
                    && (other.GetComponent<SPS_Obstacles>().obstacle_choice == SPS_Obstacles.ObstacleChoice.OBS_MOUNTAIN || other.GetComponent<SPS_Obstacles>().obstacle_choice == SPS_Obstacles.ObstacleChoice.OBS_LOG))
                {
                    Debug.Log("Player Jump successful");

                    other.gameObject.tag = "SafeObstacle";
                }

            }
        }
    }

}
