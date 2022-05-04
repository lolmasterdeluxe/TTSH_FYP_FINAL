using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialScreenManager : MonoBehaviour
{

    #region Variables

    //reference to the screen gameObjects HERE
    public enum TutorialScreenType
    { 
        SPS, CHAPTEH, FIVESTONES, FLAPPY, ERASER, HANGMAN
    };

    [Tooltip("Reference to Tutorial Screen Manager script")]
    public static TutorialScreenManager instance;

    [Tooltip("GameObject that stores the tutorial screens for each game")]
    public GameObject sps_tutorialScreen, fivestones_tutorialScreen, chapteh_tutorialScreen, flappy_tutorialScreen, erasers_tutorialScreen, hangman_tutorialScreen;

    [Tooltip("Sprite Containers for each game tutorial")]
    public List<Sprite> sps_tutorialContainer, fivestones_tutorialContainer, chapteh_tutorialContainer, flappy_tutorialContainer, erasers_tutorialContainer, hangman_tutorialContainer;

    [Tooltip("Tutorial Base Image for the games")]
    public Image sps_tutorialBase, fivestones_tutorialBase, chapteh_tutorialBase, flappy_tutorialBase, erasers_tutorialBase, hangman_tutorialBase;

    [Tooltip("Int value: to keep track of which page of the tutorial is on")]
    public int screenNumber = 0;

    [Tooltip("Boolean variable: to stop player from doing any movements on a tutorial screen")]
    public bool b_tutorialScreenOpen;

    [Tooltip("Int value: to reference to other script")]
    public int gametype_referenceNumber;

    [Tooltip("Sprite List for each pages of 5 Stones tutorial")]
    public Sprite[] fivestones_Page2, fivestones_Page5;

    [Tooltip("Sprite List for each pages of SPS tutorial")]
    public Image sps_Page1Run;
    public Sprite[] sps_Page1, sps_Page3, sps_Page4, sps_Page5;

    [Tooltip("Sprite List for each pages of Chapteh tutorial")]
    public Image chapteh_Page1Idle, chapteh_Page2Run, chapteh_Page2Mouse, chapteh_Page2LeftArrow, chapteh_Page2RightArrow;
    public Sprite[] chapteh_Page1, chapteh_Page3;

    [Tooltip("Sprite List for each pages of Country Erasers tutorial")]
    public Sprite[] erasers_Page4;

    [Tooltip("Sprite List for each pages of Country Erasers tutorial")]
    public Sprite[] hangman_Page3, hangman_Page4;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        chapteh_Page2Mouse.gameObject.SetActive(false);
        chapteh_Page2Run.gameObject.SetActive(false);
        chapteh_Page2RightArrow.gameObject.SetActive(false);
        chapteh_Page2LeftArrow.gameObject.SetActive(false);
    }

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        TutorialScreenAnimations();
    }

    #endregion

    #region Helper Functions

    public void TutorialScreenOpen(string selectedScreen)
    {
        TutorialScreenType screenType = 0;

        if (selectedScreen == "SPS")
            screenType = TutorialScreenType.SPS;
        else if (selectedScreen == "FIVESTONES")
            screenType = TutorialScreenType.FIVESTONES;
        else if (selectedScreen == "CHAPTEH")
            screenType = TutorialScreenType.CHAPTEH;
        else if (selectedScreen == "FLAPPY")
            screenType = TutorialScreenType.FLAPPY;
        else if (selectedScreen == "ERASER")
            screenType = TutorialScreenType.ERASER;
        else if (selectedScreen == "HANGMAN")
            screenType = TutorialScreenType.HANGMAN;

        switch (screenType)
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
            case TutorialScreenType.FLAPPY:
                flappy_tutorialScreen.SetActive(true);
                flappy_tutorialBase.sprite = flappy_tutorialContainer[0];
                b_tutorialScreenOpen = true;
                break;
            case TutorialScreenType.ERASER:
                erasers_tutorialScreen.SetActive(true);
                erasers_tutorialBase.sprite = erasers_tutorialContainer[0];
                b_tutorialScreenOpen = true;
                break;
            case TutorialScreenType.HANGMAN:
                hangman_tutorialScreen.SetActive(true);
                hangman_tutorialBase.sprite = hangman_tutorialContainer[0];
                b_tutorialScreenOpen = true;
                break;
            default:
                break;
        }
    }

    public void TutorialScreenClosed(string selectedScreen)
    {
        TutorialScreenType screenType = 0;

        if (selectedScreen == "SPS")
            screenType = TutorialScreenType.SPS;
        else if (selectedScreen == "FIVESTONES")
            screenType = TutorialScreenType.FIVESTONES;
        else if (selectedScreen == "CHAPTEH")
            screenType = TutorialScreenType.CHAPTEH;
        else if (selectedScreen == "FLAPPY")
            screenType = TutorialScreenType.FLAPPY;
        else if (selectedScreen == "ERASER")
            screenType = TutorialScreenType.ERASER;
        else if (selectedScreen == "HANGMAN")
            screenType = TutorialScreenType.HANGMAN;

        switch (screenType)
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
            case TutorialScreenType.FLAPPY:
                flappy_tutorialScreen.SetActive(false);
                b_tutorialScreenOpen = false;
                break;
            case TutorialScreenType.ERASER:
                erasers_tutorialScreen.SetActive(false);
                b_tutorialScreenOpen = false;
                break;
            case TutorialScreenType.HANGMAN:
                hangman_tutorialScreen.SetActive(false);
                b_tutorialScreenOpen = false;
                break;
            default:
                break;
        }
    }

    public void StartGameButtonPressed(int gameType)
    {
        switch (gameType)
        {
            case 0:
                SceneManager.LoadScene("Scissors Paper Stone");
                break;
            case 1:
                SceneManager.LoadScene("FiveStonesFruitNinja");
                break;
            case 2:
                SceneManager.LoadScene("Chapteh");
                break;
            case 3:
                SceneManager.LoadScene("FlappyParachute");
                break;
            case 4:
                SceneManager.LoadScene("CountryErasers");
                break;
            case 5:
                SceneManager.LoadScene("Hangman");
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

    //scroll between tutorial screens
    public void ScrollToNextScreen(int gameType)
    {
        screenNumber += 1;
        gametype_referenceNumber = gameType;

        //warparound the screenNumber

        if (screenNumber > 4)
            screenNumber = 0;

        UpdateTutorialScreen();
    }

    //scroll between tutorial screens
    public void ScrollToPreviousScreen(int gameType)
    {
        screenNumber -= 1;
        gametype_referenceNumber = gameType;

        //warparound the screenNumber

        if (screenNumber < 0)
            screenNumber = 4;

        UpdateTutorialScreen();
    }

    private void UpdateTutorialScreen()
    {
        switch (gametype_referenceNumber)
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
            case 3: //chapteh
                switch (screenNumber)
                {
                    case 0:
                        flappy_tutorialBase.sprite = flappy_tutorialContainer[0];
                        break;
                    case 1:
                        flappy_tutorialBase.sprite = flappy_tutorialContainer[1];
                        break;
                    case 2:
                        flappy_tutorialBase.sprite = flappy_tutorialContainer[2];
                        break;
                    case 3:
                        flappy_tutorialBase.sprite = flappy_tutorialContainer[3];
                        break;
                    case 4:
                        flappy_tutorialBase.sprite = flappy_tutorialContainer[4];
                        break;
                }
                break;
            case 4: //country erasers
                switch (screenNumber)
                {
                    case 0:
                        erasers_tutorialBase.sprite = erasers_tutorialContainer[0];
                        break;
                    case 1:
                        erasers_tutorialBase.sprite = erasers_tutorialContainer[1];
                        break;
                    case 2:
                        erasers_tutorialBase.sprite = erasers_tutorialContainer[2];
                        break;
                    case 3:
                        erasers_tutorialBase.sprite = erasers_tutorialContainer[3];
                        break;
                    case 4:
                        erasers_tutorialBase.sprite = erasers_tutorialContainer[4];
                        break;
                }
                break;
            case 5: //Hangman
                switch (screenNumber)
                {
                    case 0:
                        hangman_tutorialBase.sprite = hangman_tutorialContainer[0];
                        break;
                    case 1:
                        hangman_tutorialBase.sprite = hangman_tutorialContainer[1];
                        break;
                    case 2:
                        hangman_tutorialBase.sprite = hangman_tutorialContainer[2];
                        break;
                    case 3:
                        hangman_tutorialBase.sprite = hangman_tutorialContainer[3];
                        break;
                    case 4:
                        hangman_tutorialBase.sprite = hangman_tutorialContainer[4];
                        break;
                }
                break;
        }
    }

    private void TutorialScreenAnimations()
    {
        if (gametype_referenceNumber == 0) // SPS
        {
            // Page 1
            if (screenNumber == 0)
            {
                // When it's on page 1, set active to true
                sps_Page1Run.gameObject.SetActive(true);
                // Time taken to render next sprite
                sps_Page1Run.sprite = sps_Page1[(int)(Time.time * 10) % sps_Page1.Length];
            }
            else
                // When it's not on page 1, set active to false
                sps_Page1Run.gameObject.SetActive(false);

            // Page 3
            if (screenNumber == 2)
                sps_tutorialBase.sprite = sps_Page3[(int)(Time.time * 3) % sps_Page3.Length];
            // Page 4
            if (screenNumber == 3)
                sps_tutorialBase.sprite = sps_Page4[(int)(Time.time * 3) % sps_Page4.Length];
            // Page 5
            if (screenNumber == 4)
                sps_tutorialBase.sprite = sps_Page5[(int)(Time.time * 3) % sps_Page5.Length];
        }
        else if (gametype_referenceNumber == 1) // 5 Stones
        {
            // Page 2
            if (screenNumber == 1)
                fivestones_tutorialBase.sprite = fivestones_Page2[(int)(Time.time * 4) % fivestones_Page2.Length];
            // Page 5
            if (screenNumber == 4)
                fivestones_tutorialBase.sprite = fivestones_Page5[(int)(Time.time * 2) % fivestones_Page5.Length];
        }
        else if (gametype_referenceNumber == 2) // Chapteh
        {
            // Page 1
            if (screenNumber == 0)
            {
                chapteh_Page1Idle.gameObject.SetActive(true);
                chapteh_Page1Idle.sprite = chapteh_Page1[(int)(Time.time * 10) % chapteh_Page1.Length];
            }
            else
                chapteh_Page1Idle.gameObject.SetActive(false);

            // Page 3
            if (screenNumber == 2)
                chapteh_tutorialBase.sprite = chapteh_Page3[(int)(Time.time * 3) % chapteh_Page3.Length];
        }
        else if (gametype_referenceNumber == 4) // Country Erasers
        {
            // Page 4
            if (screenNumber == 3)
            {
                erasers_tutorialBase.sprite = erasers_Page4[(int)(Time.time) % erasers_Page4.Length];
            }
        }
        else if (gametype_referenceNumber == 5) // Hangman
        {
            // Page 3
            if (screenNumber == 2)
            {
                hangman_tutorialBase.sprite = hangman_Page3[(int)(Time.time) % hangman_Page3.Length];
            }
            // Page 4
            if (screenNumber == 3)
            {
                hangman_tutorialBase.sprite = hangman_Page4[(int)(Time.time) % hangman_Page4.Length];
            }
        }
    }

    //allow player to move again
    public void ResetPlayerMovement()
    {
        b_tutorialScreenOpen = false;
    }


    #endregion

}
