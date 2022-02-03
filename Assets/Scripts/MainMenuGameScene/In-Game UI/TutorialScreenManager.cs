using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialScreenManager : MonoBehaviour
{

    #region Variables

    //reference to the screen gameObjects HERE
    public enum TutorialScreenType
    { 
        SPS, CHAPTEH, FIVESTONES
    };

    [Tooltip("Reference to Tutorial Screen Manager script")]
    public static TutorialScreenManager instance;

    [Tooltip("GameObject that stores the tutorial screens for each game")]
    public GameObject sps_tutorialScreen, fivestones_tutorialScreen, chapteh_tutorialScreen;

    [Tooltip("NPC GameObjects")]
    public GameObject sps_npc, fivestones_npc, chapteh_npc;

    [Tooltip("Sprite Containers for each game tutorial")]
    public List<Sprite> sps_tutorialContainer, fivestones_tutorialContainer, chapteh_tutorialContainer;

    [Tooltip("Tutorial Base Image for the games")]
    public Image sps_tutorialBase, fivestones_tutorialBase, chapteh_tutorialBase;

    [Tooltip("Int value: to keep track of which page of the tutorial is on")]
    public int screenNumber = 0;

    [Tooltip("Boolean variable: to stop player from doing any movements on a tutorial screen")]
    public bool b_tutorialScreenOpen;

    [Tooltip("Int value: to reference to other script")]
    public int gametype_referenceNumber;

    [Tooltip("Sprite List for each pages of 5 Stones tutorial")]
    public Image fivestones_Image;
    public Sprite[] fivestones_Page2, fivestones_Page5;

    [Tooltip("Sprite List for each pages of SPS tutorial")]
    public Image sps_Image;
    public Sprite[] sps_Page3, sps_Page4, sps_Page5;

    [Tooltip("Sprite List for each pages of Chapteh tutorial")]
    public Image chapteh_Image;
    public Sprite[] chapteh_Page3;

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (gametype_referenceNumber == 0) // SPS
        {
            if (screenNumber == 2)
                sps_Image.sprite = sps_Page3[(int)(Time.time * 3) % sps_Page3.Length];
            if (screenNumber == 3)
                sps_Image.sprite = sps_Page4[(int)(Time.time * 3) % sps_Page4.Length];
            if (screenNumber == 4)
                sps_Image.sprite = sps_Page5[(int)(Time.time * 3) % sps_Page5.Length];
        }
        else if (gametype_referenceNumber == 1) // 5 Stones
        {
            if (screenNumber == 1)
                fivestones_Image.sprite = fivestones_Page2[(int)(Time.time * 4) % fivestones_Page2.Length];
            if (screenNumber == 4)
                fivestones_Image.sprite = fivestones_Page5[(int)(Time.time * 2) % fivestones_Page5.Length];
        }
        else if (gametype_referenceNumber == 2) // Chapteh
        {
            if (screenNumber == 2)
                chapteh_Image.sprite = chapteh_Page3[(int)(Time.time * 3) % chapteh_Page3.Length];
        }
    }

    #endregion

    #region Helper Functions

    public void TutorialScreenOpen(TutorialScreenType selectedScreen)
    {
        switch (selectedScreen)
        {
            case TutorialScreenType.SPS:
                sps_tutorialScreen.SetActive(true);
                sps_tutorialBase.sprite = sps_tutorialContainer[0];
                b_tutorialScreenOpen = true;
                break;
            case TutorialScreenType.FIVESTONES:
                fivestones_tutorialScreen.SetActive(true);
                fivestones_tutorialBase.sprite = fivestones_tutorialContainer[0];
                b_tutorialScreenOpen = true;
                break;
            case TutorialScreenType.CHAPTEH:
                chapteh_tutorialScreen.SetActive(true);
                chapteh_tutorialBase.sprite = chapteh_tutorialContainer[0];
                b_tutorialScreenOpen = true;
                break;
        }
    }

    public void TutorialScreenClosed(TutorialScreenType selectedScreen)
    {
        switch (selectedScreen)
        {
            case TutorialScreenType.SPS:
                sps_tutorialScreen.SetActive(false);
                b_tutorialScreenOpen = false;
                break;
            case TutorialScreenType.FIVESTONES:
                fivestones_tutorialScreen.SetActive(false);
                b_tutorialScreenOpen = false;
                break;
            case TutorialScreenType.CHAPTEH:
                chapteh_tutorialScreen.SetActive(false);
                b_tutorialScreenOpen = false;
                break;
        }
    }

    public void StartGameButtonPressed(int gameType)
    {
        switch (gameType)
        {
            case 0:
                Destroy(sps_npc.GetComponent<CapsuleCollider2D>());
                SceneManager.LoadScene("Scissors Paper Stone");
                break;
            case 1:
                Destroy(fivestones_npc.GetComponent<CapsuleCollider2D>());
                SceneManager.LoadScene("FiveStonesFruitNinja");
                break;
            case 2:
                Destroy(chapteh_npc.GetComponent<CapsuleCollider2D>());
                SceneManager.LoadScene("Chapteh");
                break;
        }

    }
    
    //attached to the start and exit button
    public void ButtonHover(GameObject selectedButton)
    {
        selectedButton.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
    }
    
    //attached to the start and exit button
    public void ButtonHoverReset(GameObject selectedButton)
    {
        selectedButton.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    //attached to the exit button
    public void ButtonCloseScreen(GameObject selectedScreen)
    {
        selectedScreen.SetActive(false);
    }

    //scroll between tutorial screens
    public void ScrollToNextScreen(int gameType)
    {
        screenNumber += 1;
        gametype_referenceNumber = gameType;

        //warparound the screenNumber

        if (screenNumber > 4)
            screenNumber = 0;

        switch (gameType)
        {
            case 0: //sps
                switch (screenNumber) 
                {
                    case 0:
                        sps_tutorialBase.sprite = sps_tutorialContainer[0];
                        break;
                    case 1:
                        sps_tutorialBase.sprite = sps_tutorialContainer[1];
                        break;
                    case 2:
                        sps_tutorialBase.sprite = sps_tutorialContainer[2];
                        break;
                    case 3:
                        sps_tutorialBase.sprite = sps_tutorialContainer[3];
                        break;
                    case 4:
                        sps_tutorialBase.sprite = sps_tutorialContainer[4];
                        break;
                }
                break;
            case 1: //five stones
                switch (screenNumber)
                {
                    case 0:
                        fivestones_tutorialBase.sprite = fivestones_tutorialContainer[0];
                        break;
                    case 1:
                        fivestones_tutorialBase.sprite = fivestones_tutorialContainer[1];
                        break;
                    case 2:
                        fivestones_tutorialBase.sprite = fivestones_tutorialContainer[2];
                        break;
                    case 3:
                        fivestones_tutorialBase.sprite = fivestones_tutorialContainer[3];
                        break;
                    case 4:
                        fivestones_tutorialBase.sprite = fivestones_tutorialContainer[4];
                        break;
                }
                break;
            case 2: //chapteh
                switch (screenNumber)
                {
                    case 0:
                        chapteh_tutorialBase.sprite = chapteh_tutorialContainer[0];
                        break;
                    case 1:
                        chapteh_tutorialBase.sprite = chapteh_tutorialContainer[1];
                        break;
                    case 2:
                        chapteh_tutorialBase.sprite = chapteh_tutorialContainer[2];
                        break;
                    case 3:
                        chapteh_tutorialBase.sprite = chapteh_tutorialContainer[3];
                        break;
                    case 4:
                        chapteh_tutorialBase.sprite = chapteh_tutorialContainer[4];
                        break;
                }
                break;
        }

    }

    //scroll between tutorial screens
    public void ScrollToPreviousScreen(int gameType)
    {
        screenNumber -= 1;
        gametype_referenceNumber = gameType;

        //warparound the screenNumber

        if (screenNumber < 0)
            screenNumber = 4;

        switch (gameType)
        {
            case 0: //sps
                switch (screenNumber)
                {
                    case 0:
                        sps_tutorialBase.sprite = sps_tutorialContainer[0];
                        break;
                    case 1:
                        sps_tutorialBase.sprite = sps_tutorialContainer[1];
                        break;
                    case 2:
                        sps_tutorialBase.sprite = sps_tutorialContainer[2];
                        break;
                    case 3:
                        sps_tutorialBase.sprite = sps_tutorialContainer[3];
                        break;
                    case 4:
                        sps_tutorialBase.sprite = sps_tutorialContainer[4];
                        break;
                }
                break;
            case 1: //five stones
                switch (screenNumber)
                {
                    case 0:
                        fivestones_tutorialBase.sprite = fivestones_tutorialContainer[0];
                        break;
                    case 1:
                        fivestones_tutorialBase.sprite = fivestones_tutorialContainer[1];
                        break;
                    case 2:
                        fivestones_tutorialBase.sprite = fivestones_tutorialContainer[2];
                        break;
                    case 3:
                        fivestones_tutorialBase.sprite = fivestones_tutorialContainer[3];
                        break;
                    case 4:
                        fivestones_tutorialBase.sprite = fivestones_tutorialContainer[4];
                        break;
                }
                break;
            case 2: //chapteh
                switch (screenNumber)
                {
                    case 0:
                        chapteh_tutorialBase.sprite = chapteh_tutorialContainer[0];
                        break;
                    case 1:
                        chapteh_tutorialBase.sprite = chapteh_tutorialContainer[1];
                        break;
                    case 2:
                        chapteh_tutorialBase.sprite = chapteh_tutorialContainer[2];
                        break;
                    case 3:
                        chapteh_tutorialBase.sprite = chapteh_tutorialContainer[3];
                        break;
                    case 4:
                        chapteh_tutorialBase.sprite = chapteh_tutorialContainer[4];
                        break;
                }
                break;
        }
    }

    //allow player to move again
    public void ResetPlayerMovement()
    {
        b_tutorialScreenOpen = false;
    }


    #endregion

}
