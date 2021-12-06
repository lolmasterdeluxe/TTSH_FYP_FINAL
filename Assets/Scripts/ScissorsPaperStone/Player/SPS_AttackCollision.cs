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

    public BoxCollider attackCollider;

    Animator ac;
    SPS_Player playerInstance;
    SPS_ScoreManager scoreInstance;
    SPS_ObjectSpawningScript objectspawningInstance;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        ac = FindObjectOfType<SPS_Player>().GetComponent<Animator>();
        attackCollider = GetComponent<BoxCollider>();
        
        playerInstance = GetComponentInParent<SPS_Player>();
        scoreInstance = FindObjectOfType<SPS_ScoreManager>();
        objectspawningInstance = FindObjectOfType<SPS_ObjectSpawningScript>();
    }

    private void Update()
    {
        ButtonUptime();

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

        if (playerInstance.hasPowerup == true)
        {
            attackCollider.size = new Vector3(2.89f, attackCollider.size.y, attackCollider.size.z);
            attackCollider.center = new Vector3(2.59f, attackCollider.center.y, attackCollider.center.z);
        }

    }

    public void ButtonUptime()
    {
        if (buttonPressed == true)
        {
            rangeUptime += Time.deltaTime;
            if (rangeUptime >= 0.2f)
            {
                rangeUptime = 0f;
                buttonPressed = false;
                playerInstance.p_choice = SPS_Player.PlayerChoice.P_NONE;

                //set the powerup boolean to be false
                playerInstance.hasPowerup = false;

                //set the attack collider to be the default size
                attackCollider.size = new Vector3(1.65f, attackCollider.size.y, attackCollider.size.z);
                attackCollider.center = new Vector3(2f, attackCollider.center.y, attackCollider.center.z);
                Debug.Log(attackCollider.center.x + "AAAAAAA");


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
                if (playerInstance.p_choice == SPS_Player.PlayerChoice.P_SCISSOR
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
                    ComboManager.Instance.AddCombo();
                }

                else if (buttonPressed == true && playerInstance.p_choice == SPS_Player.PlayerChoice.P_PAPER
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
                    ComboManager.Instance.AddCombo();
                }

                else if (buttonPressed == true && playerInstance.p_choice == SPS_Player.PlayerChoice.P_STONE
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
                    ComboManager.Instance.AddCombo();
                }
            }

            else if (other.gameObject.tag == "Obstacle") //obstacle
            {
                if (buttonPressed == true && playerInstance.p_choice == SPS_Player.PlayerChoice.P_JUMP
                    && (other.GetComponent<SPS_Obstacles>().obstacle_choice == SPS_Obstacles.ObstacleChoice.OBS_MOUNTAIN || other.GetComponent<SPS_Obstacles>().obstacle_choice == SPS_Obstacles.ObstacleChoice.OBS_LOG))
                {
                    Debug.Log("Player Jump successful");

                    other.gameObject.tag = "SafeObstacle";
                }
            }
        }
    }

}
