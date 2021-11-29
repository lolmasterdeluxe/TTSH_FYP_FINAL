using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPS_AttackCollision : MonoBehaviour
{
    //this script handles when the player is attacking and has hit an enemy

    #region Variables

    public bool buttonPressed;
    public float rangeUptime;
    SPS_Player playerChoice;
    SPS_Enemy aiChoice;

    #endregion

    private void Start()
    {
        playerChoice = GetComponentInParent<SPS_Player>();
        aiChoice = FindObjectOfType<SPS_Enemy>();
    }

    private void Update()
    {
        Timer();
    }

    public void ButtonPress()
    {
        buttonPressed = true;
    }

    public void Timer()
    {
        if (buttonPressed == true)
        {
            rangeUptime += Time.deltaTime;
            if (rangeUptime >= 1f)
            {
                rangeUptime = 0f;
                buttonPressed = false;
                Debug.Log("finished");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //we check to see if there are any enemies in range
            
        if (buttonPressed == true && other.gameObject.tag == "Enemy")
        {
            //we now check that the matchup is correct to kill the enemy
            if ((playerChoice.p_choice == SPS_Player.PlayerChoice.P_SCISSOR 
                && aiChoice.ai_choice == SPS_Enemy.AIChoice.AI_PAPER) 
                || (playerChoice.p_choice == SPS_Player.PlayerChoice.P_PAPER
                && aiChoice.ai_choice == SPS_Enemy.AIChoice.AI_STONE) 
                || (playerChoice.p_choice == SPS_Player.PlayerChoice.P_STONE
                && aiChoice.ai_choice == SPS_Enemy.AIChoice.AI_SCISSOR))
            {
                Debug.Log("Enemy goes OW: trigger enter");
            }
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (buttonPressed == true && other.gameObject.tag == "Enemy")
        {
            //we now check that the matchup is correct to kill the enemy
            if ((playerChoice.p_choice == SPS_Player.PlayerChoice.P_SCISSOR
                && aiChoice.ai_choice == SPS_Enemy.AIChoice.AI_PAPER)
                || (playerChoice.p_choice == SPS_Player.PlayerChoice.P_PAPER
                && aiChoice.ai_choice == SPS_Enemy.AIChoice.AI_STONE)
                || (playerChoice.p_choice == SPS_Player.PlayerChoice.P_STONE
                && aiChoice.ai_choice == SPS_Enemy.AIChoice.AI_SCISSOR))
            {
                Debug.Log("Enemy goes OW: trigger stay");
            }

        }
    }

}
