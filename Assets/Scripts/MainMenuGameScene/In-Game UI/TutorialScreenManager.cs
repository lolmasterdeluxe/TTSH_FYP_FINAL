using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScreenManager : MonoBehaviour
{

    #region Variables

    //reference to the screen gameObjects HERE
    public enum TutorialScreenType
    { 
        SPS, CHAPTEH, FIVESTONES
    };

    public GameObject sps_tutorialScreen, fivestones_tutorialScreen, chapteh_tutorialScreen;
    public GameObject sps_npc, fivestones_npc, chapteh_npc;

    public GameObject testPanel;


    public static TutorialScreenManager instance;
    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            testPanel.SetActive(true);
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            testPanel.SetActive(false);
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
                break;
            case TutorialScreenType.FIVESTONES:
                fivestones_tutorialScreen.SetActive(true);
                break;
            case TutorialScreenType.CHAPTEH:
                chapteh_tutorialScreen.SetActive(true);
                break;
        }

    }

    public void TutorialScreenClosed(TutorialScreenType selectedScreen)
    {
        switch (selectedScreen)
        {
            case TutorialScreenType.SPS:
                sps_tutorialScreen.SetActive(false);
                break;
            case TutorialScreenType.FIVESTONES:
                fivestones_tutorialScreen.SetActive(false);
                break;
            case TutorialScreenType.CHAPTEH:
                chapteh_tutorialScreen.SetActive(false);
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

    //this should setactive only the current val of the screen type
    public void ScrollToNextScreen(List<GameObject> selectedGame, int val)
    {
        val += 1;

        //do wrapping HERE

        foreach(GameObject a in selectedGame)
        {
            if (selectedGame.IndexOf(a) == val)
            {
                a.SetActive(true);
            }
            else
            {
                a.SetActive(false);
            }
        }     
    }

    public void ScrollToPreviousScreen(List<GameObject> selectedGame, int val)
    {
        val -= 1;

        //do wrapping HERE

        foreach (GameObject a in selectedGame)
        {
            if (selectedGame.IndexOf(a) == val)
            {
                a.SetActive(true);
            }
            else
            {
                a.SetActive(false);
            }
        }
    }


    #endregion


}
