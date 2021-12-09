using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class SPS_AttackCollision : MonoBehaviour
{
    //this script handles when the player is attacking and has hit an enemy

    #region Variables

    public bool attackbuttonPressed;
    public bool jumpbuttonPressed;

    public float rangeUptime;

    public BoxCollider attackCollider;

    Animator ac;
    SPS_Player playerInstance;
    SPS_ScoreManager scoreInstance;
    SPS_ObjectSpawningScript objectspawningInstance;

    //this is for the combo text
    public GameObject comboText;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        ac = FindObjectOfType<SPS_Player>().GetComponent<Animator>();
        attackCollider = GetComponent<BoxCollider>();
        
        playerInstance = GetComponentInParent<SPS_Player>();
        scoreInstance = FindObjectOfType<SPS_ScoreManager>();
        objectspawningInstance = FindObjectOfType<SPS_ObjectSpawningScript>();

        //attach events HERE
        ComboManager.Instance.e_comboAdded.AddListener(AddedCombo);
        ComboManager.Instance.e_comboBreak.AddListener(ComboBroken);
    }

    private void Update()
    {
        AttackButtonUpTime();
        JumpButtonUpTime();

        //check to see if wave is completed every frame
        if (objectspawningInstance.objectwaveList.Count == 0)
        {
            objectspawningInstance.waveCompleted = false;
        }
    }

    #endregion

    #region Functions

    public void AttackButtonPress()
    {
        attackbuttonPressed = true;

        //increase the collider size for powerup
        if (playerInstance.hasPowerup == true)
        {
            attackCollider.size = new Vector3(3.89f, attackCollider.size.y, attackCollider.size.z);
            attackCollider.center = new Vector3(3.59f, attackCollider.center.y, attackCollider.center.z);
        }

    }

    public void JumpButtonPress()
    {
        jumpbuttonPressed = true;
    }

    public void AttackButtonUpTime()
    {
        if (attackbuttonPressed == true)
        {
            rangeUptime += Time.deltaTime;
            if (rangeUptime >= 0.3f)
            {
                rangeUptime = 0f;
                attackbuttonPressed = false;
                playerInstance.p_choice = SPS_Player.PlayerChoice.P_NONE;

                //if the player has the powerup
                if (playerInstance.hasPowerup == true)
                {
                    //set the powerup boolean to be false
                    playerInstance.hasPowerup = false;

                    //set the attack collider to be the default size
                    attackCollider.size = new Vector3(1.65f, attackCollider.size.y, attackCollider.size.z);
                    attackCollider.center = new Vector3(2f, attackCollider.center.y, attackCollider.center.z);
                }

                OnPlayerActionAnimationComplete();

                if (playerInstance.playeractionAC.GetBool("PlayerActionWithScissors") == false 
                    || playerInstance.playeractionAC.GetBool("PlayerActionWithPaper") == false
                    || playerInstance.playeractionAC.GetBool("PlayerActionWithStone") == false)
                {
                    OnPlayerBodyAnimationComplete();
                }
            }
        }
    }

    public void JumpButtonUpTime()
    {
        if (jumpbuttonPressed == true)
        {
            rangeUptime += Time.deltaTime;
            if (rangeUptime >= 0.7f)
            {
                rangeUptime = 0f;
                playerInstance.playerJumped = false;
                jumpbuttonPressed = false;
                playerInstance.p_choice = SPS_Player.PlayerChoice.P_NONE;

                OnPlayerActionAnimationComplete();

                if (playerInstance.playeractionAC.GetBool("PlayerActionWithScissors") == false)
                {
                    OnPlayerBodyAnimationComplete();
                }
            }
        }
    }

    public void OnPlayerBodyAnimationComplete()
    {
        //we set all the action (button )animations to be false since everything should be reset
        ac.SetBool("PlayerAttackingWithScissors", false);
        ac.SetBool("PlayerAttackingWithPaper", false);
        ac.SetBool("PlayerAttackingWithStone", false);
        ac.SetBool("PlayerJumped", false);
    }

    public void OnPlayerActionAnimationComplete()
    {
        //reset all animation states to default
        playerInstance.playeractionAC.SetBool("PlayerActionWithScissors", false);
        playerInstance.playeractionAC.SetBool("PlayerActionWithPaper", false);
        playerInstance.playeractionAC.SetBool("PlayerActionWithStone", false);
    }

    //call this function when the wave of enemy is completed
    public void WaveCompleted()
    {
        objectspawningInstance.waveCompleted = true;
    }

    #endregion

    #region Trigger Functions

    private void OnTriggerStay(Collider other)
    {
        //we now check that the matchup is correct to kill the enemy
        //firstly we check to see if ANY button has been pressed
        if (attackbuttonPressed == true)
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

                    //we do combo text HERE
                    UpdateComboScore();
                }

                else if (attackbuttonPressed == true && playerInstance.p_choice == SPS_Player.PlayerChoice.P_PAPER
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

                    //we do combo text HERE
                    UpdateComboScore();
                }

                else if (attackbuttonPressed == true && playerInstance.p_choice == SPS_Player.PlayerChoice.P_STONE
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

                    //we do combo text HERE
                    UpdateComboScore();
                }
            }

            else if (other.gameObject.tag == "Obstacle") //obstacle
            {
                if (jumpbuttonPressed == true && playerInstance.p_choice == SPS_Player.PlayerChoice.P_JUMP
                    && other.GetComponent<SPS_Obstacles>().obstacle_choice == SPS_Obstacles.ObstacleChoice.OBS_LOG)
                {
                    Debug.Log("Player Jump successful");

                    other.gameObject.tag = "SafeObstacle";
                }
            }
        }
    }

    #endregion

    #region Combo Manager Functions

    public void AddedCombo()
    {
        TweenManager.Instance.AnimateFade(comboText.GetComponent<CanvasGroup>(), 1, 0.25f);
    }

    public void ComboBroken()
    {
        TweenManager.Instance.AnimateShake(comboText.transform, 2, 1f);
        TweenManager.Instance.AnimateFade(comboText.GetComponent<CanvasGroup>(), 0f, 0.5f);
    }

    public void UpdateComboScore()
    {
        comboText.GetComponent<TMP_Text>().text = "Combo " + ComboManager.Instance.GetCurrentCombo() + "x";
    }

    #endregion

}
