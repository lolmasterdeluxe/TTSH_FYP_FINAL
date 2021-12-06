﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SPS_Player : MonoBehaviour
{
    //this script handles all things involved with the player

    #region Enumerations
    public enum PlayerChoice
    {
        P_SCISSOR, P_PAPER, P_STONE, P_JUMP, P_SLIDE, P_NONE
    };

    #endregion

    #region Variables

    public PlayerChoice p_choice;

    ComboManager combomanagerInstance;
    SPS_LivesManager livesInstance;
    SPS_ObjectSpawningScript waveCheck;
    
    #endregion

    #region Unity Callbacks

    private void Start()
    {
        p_choice = PlayerChoice.P_NONE;
        livesInstance = FindObjectOfType<SPS_LivesManager>();
        combomanagerInstance = FindObjectOfType<ComboManager>();
        waveCheck = FindObjectOfType<SPS_ObjectSpawningScript>();
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyTag")
        {
            Debug.Log("Player goes OW : trigger enter");
            waveCheck.waveCompleted = false;
            Destroy(other.gameObject.GetComponent<Rigidbody>());
            Destroy(other.gameObject);
            livesInstance.PlayerTakesDamage();
            combomanagerInstance.BreakCombo();  
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

    public void PlayerJumps()
    {
        p_choice = PlayerChoice.P_JUMP;
    }

    public void PlayerSlides()
    {
        p_choice = PlayerChoice.P_SLIDE;
    }

    #endregion
}
