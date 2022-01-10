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

    public static TutorialScreenManager instance;
    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        instance = this;
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
                SceneManager.LoadScene("Scissors Paper Stone");
                Destroy(sps_npc.GetComponent<CapsuleCollider2D>());
                break;
            case 1:
                SceneManager.LoadScene("FiveStonesFruitNinja");
                Destroy(fivestones_npc.GetComponent<CapsuleCollider2D>());
                break;
            case 2:
                SceneManager.LoadScene("Chapteh");
                Destroy(chapteh_npc.GetComponent<CapsuleCollider2D>());
                break;
        }

    }

    public void ButtonHover(GameObject selectedButton)
    {
        selectedButton.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
    }

    public void ButtonHoverReset(GameObject selectedButton)
    {
        selectedButton.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void ButtonCloseScreen(GameObject selectedScreen)
    {
        selectedScreen.SetActive(false);
    }


    #endregion


}
