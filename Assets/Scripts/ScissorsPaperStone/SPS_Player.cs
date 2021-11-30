using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    ComboManager combomanagerInstance;
    SPS_LivesManager livesInstance;
    
    #endregion

    #region Unity Callbacks

    private void Start()
    {
        p_choice = PlayerChoice.P_NONE;
        livesInstance = FindObjectOfType<SPS_LivesManager>();
        combomanagerInstance = FindObjectOfType<ComboManager>();
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyTag")
        {
            Debug.Log("Player goes OW : trigger enter");
            Destroy(other.gameObject);
            Destroy(other.gameObject.GetComponent<Rigidbody>());
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

    #endregion
}
