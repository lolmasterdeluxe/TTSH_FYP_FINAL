using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPS_NPCBehavior : MonoBehaviour
{
    //this script handles npc behavior: SPS NPC

    #region Enumerations

    public enum NPCChoice
    { SCISSORS, PAPER, STONE, TOTAL };

    #endregion

    #region Variables

    Animator ac;

    [SerializeField]
    RuntimeAnimatorController npcScissors, npcPaper, npcStone;

    NPCChoice npc_choice;

    float executeTimer;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        ac = GetComponent<Animator>();
    }

    private void Update()
    {
        if (ac.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
        {
            DO_NPCChoice();
        }
    }

    #endregion

    public void DO_NPCChoice()
    {
        npc_choice = (NPCChoice)Random.Range(0, (int)NPCChoice.TOTAL);

        switch (npc_choice)
        {
            case NPCChoice.SCISSORS:
                ac.runtimeAnimatorController = npcScissors;
                break;

            case NPCChoice.PAPER:
                ac.runtimeAnimatorController = npcPaper;
                break;

            case NPCChoice.STONE:
                ac.runtimeAnimatorController = npcStone;
                break;

        }
    }



}
