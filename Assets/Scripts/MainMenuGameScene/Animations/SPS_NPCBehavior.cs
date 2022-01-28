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

    [Tooltip("Reference to the Animators attached to the NPCs")]
    Animator npc1_ac, npc2_ac;

    [Tooltip("Enum Variable of NPCs Choice")]
    public NPCChoice npc1Choice, npc2Choice;

    [Tooltip("Reference to the NPC gameObjects")] [SerializeField]
    GameObject g_npc1, g_npc2;

    [Tooltip("Float variable: checks execute time for NPC action")]
    float executeTimer;

    bool stop = false;


    #endregion

    #region Unity Callbacks

    private void Start()
    {
        npc1_ac = g_npc1.GetComponent<Animator>();
        npc2_ac = g_npc2.GetComponent<Animator>();
        Debug.Log(npc1_ac);
        Debug.Log(npc2_ac);
    }

    private void Update()
    {
        //we want to run an action only once
        executeTimer += Time.deltaTime;

        if (executeTimer >= 4f && stop == false)
        {
            DO_NPC1Action();
            DO_NPC2Action();
            stop = true;
        }


    }

    #endregion

    #region Helper Functions

    public void DO_NPC1Action()
    {
        npc1Choice = (NPCChoice)(Random.Range(1, (int)NPCChoice.CHOICE_TOTAL));

        Debug.Log(npc1Choice);

        switch (npc1Choice)
        {
            case NPCChoice.CHOICE_SCISSORS:
                npc1_ac.SetBool("DO_SCISSORS", true);
                break;
            case NPCChoice.CHOICE_PAPER:
                npc1_ac.SetBool("DO_PAPER", true);
                break;
            case NPCChoice.CHOICE_STONE:
                npc1_ac.SetBool("DO_STONE", true);
                break;
        }
    }

    public void DO_NPC2Action()
    {
        npc2Choice = (NPCChoice)(Random.Range(1, (int)NPCChoice.CHOICE_TOTAL));

        Debug.Log(npc2Choice);

        switch (npc2Choice)
        {
            case NPCChoice.CHOICE_SCISSORS:
                npc2_ac.SetBool("DO_SCISSORS", true);
                break;
            case NPCChoice.CHOICE_PAPER:
                npc2_ac.SetBool("DO_PAPER", true);
                break;
            case NPCChoice.CHOICE_STONE:
                npc2_ac.SetBool("DO_STONE", true);
                break;
        }
    }


    #endregion



}
