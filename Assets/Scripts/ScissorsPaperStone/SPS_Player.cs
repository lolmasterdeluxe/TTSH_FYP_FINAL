using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPS_Player : MonoBehaviour
{

    #region Enumerations
    public enum PlayerChoice
    {
        P_SCISSOR, P_PAPER, P_STONE, P_NONE
    };

    #endregion

    //this script handles all things involved with the player
    #region Variables

    public PlayerChoice p_choice;
    SPS_AttackCollision attackInstance;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        p_choice = PlayerChoice.P_NONE;
        attackInstance = GetComponentInChildren<SPS_AttackCollision>();
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Player goes OW");
        }
    }

    #endregion

    #region Functions

    public void PlayerChoosesScissors()
    {
        p_choice = PlayerChoice.P_SCISSOR;
    }
    public void PlayerChoosesPaper()
    {
        p_choice = PlayerChoice.P_PAPER;
    }
    public void PlayerChoosesStone()
    {
        p_choice = PlayerChoice.P_STONE;
    }

    #endregion
}
