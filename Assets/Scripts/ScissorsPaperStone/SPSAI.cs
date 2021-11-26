using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum playerChoice
{
    P_SCISSOR,
    P_PAPER,
    P_STONE,
    P_NONE
};

public enum aiChoice
{
    AI_SCISSOR,
    AI_PAPER,
    AI_STONE,
    AI_NONE
};

public class SPSAI : MonoBehaviour
{
    //simple AI script for scissor-paper-stone game

    #region Variables

    SPSAnimation iconrising;

    public playerChoice p_choice;
    public aiChoice ai_choice;

    //this is for the text (for now)
    public GameObject win, loss, tie;


    #endregion

    #region Unity Callbacks

    private void Start()
    {
        iconrising = FindObjectOfType<SPSAnimation>();

        p_choice = playerChoice.P_NONE;
        ai_choice = aiChoice.AI_NONE;

        win.SetActive(false);
        loss.SetActive(false);
        tie.SetActive(false);
    }

    private void Update()
    {
        //here, we check to see who has won

        //player and AI tie
        if ((p_choice == playerChoice.P_SCISSOR && ai_choice == aiChoice.AI_SCISSOR)
           || (p_choice == playerChoice.P_PAPER && ai_choice == aiChoice.AI_PAPER)
           || (p_choice == playerChoice.P_STONE && ai_choice == aiChoice.AI_STONE))
        {
            Tie();
        }

        //player wins
        if ((p_choice == playerChoice.P_SCISSOR && ai_choice == aiChoice.AI_PAPER)
            || (p_choice == playerChoice.P_PAPER && ai_choice == aiChoice.AI_STONE)
            || (p_choice == playerChoice.P_STONE && ai_choice == aiChoice.AI_SCISSOR))
        {
            PlayerWins();
        }

        //player loses
        if ((p_choice == playerChoice.P_SCISSOR && ai_choice == aiChoice.AI_STONE)
            || (p_choice == playerChoice.P_PAPER && ai_choice == aiChoice.AI_SCISSOR)
            || (p_choice == playerChoice.P_STONE && ai_choice == aiChoice.AI_PAPER))
        {
            PlayerLoses();
        }

    }

    #endregion

    #region Functions

    public void PlayerChoosesScissors()
    {
        p_choice = playerChoice.P_SCISSOR;
        AIChoice();
    }

    public void PlayerChoosesPaper()
    {
        p_choice = playerChoice.P_PAPER;
        AIChoice();
    }
    public void PlayerChoosesStone()
    {
        p_choice = playerChoice.P_STONE;
        AIChoice();
    }

    public void AIChoice()
    {
        //randomise the choice of the AI
        int ranVal = Random.Range(0, 3);

        switch (ranVal)
        {
            case 0:
                ai_choice = aiChoice.AI_SCISSOR;
                return;
            case 1:
                ai_choice = aiChoice.AI_PAPER;
                return;
            case 2:
                ai_choice = aiChoice.AI_STONE;
                return;
        }
    }

    public void Tie()
    {
        win.SetActive(false);
        loss.SetActive(false);
        tie.SetActive(true);
    }

    public void PlayerWins()
    {
        win.SetActive(true);
        loss.SetActive(false);
        tie.SetActive(false);
    }

    public void PlayerLoses()
    {
        win.SetActive(false);
        loss.SetActive(true);
        tie.SetActive(false);
    }

    #endregion
}
