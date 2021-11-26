using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SPSAnimation : MonoBehaviour
{
    //this is for the animation of the choice by AI

    #region Variables

    SPSAI checkAIChoice;
    public Sprite scissorImg, paperImg, stoneImg;
    public Image currImg;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        checkAIChoice = FindObjectOfType<SPSAI>();


    }

    private void Update()
    {
        ChangeIconImage();
    }
    #endregion

    #region Functions

    public void ChangeIconImage()
    {
        if (checkAIChoice.ai_choice == aiChoice.AI_SCISSOR)
        {
            currImg.sprite = scissorImg;
        }
        if (checkAIChoice.ai_choice == aiChoice.AI_PAPER)
        {
            currImg.sprite = paperImg;
        }
        if (checkAIChoice.ai_choice == aiChoice.AI_STONE)
        {
            currImg.sprite = stoneImg;
        }
    }

    #endregion


}
