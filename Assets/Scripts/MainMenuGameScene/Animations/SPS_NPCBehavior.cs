using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPS_NPCBehavior : MonoBehaviour
{
    //this script handles the SPS NPC Behavior in Main Menu
    //we attach this script to only one of the two NPCs
    //based on the choice of this NPC, we use this script to handle the animations of the OTHER gameobject

    #region Enumerations

    public enum NPCChoice
    { 
        CHOICE_IDLE, CHOICE_SCISSORS, CHOICE_PAPER, CHOICE_STONE, CHOICE_TOTAL
    };

    #endregion

    #region Variables

    Animator ac;
    public NPCChoice npcChoice;
    public GameObject otherNPC;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        ac = GetComponent<Animator>();

        //we want to set the animation state to be idle at the start





    }

    private void Update()
    {
        
    }

    #endregion

    #region Helper Functions

    public void UpdateNPCAnimation()
    {
        if (npcChoice == NPCChoice.CHOICE_IDLE)
        {

        }
    }

    #endregion



}
