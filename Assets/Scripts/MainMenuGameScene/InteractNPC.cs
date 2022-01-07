using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractNPC : MonoBehaviour
{
    // Start is called before the first frame update

    public enum NPC_TYPE
    {
        FIVE_STONES,
        SPS,
        CHAPTEH,
        CUSTOMIZER,
        LEADERBOARD
    }

    public NPC_TYPE type;
    TutorialScreenManager tutorialscreenmanagerInstance;

    void Start()
    {
        tutorialscreenmanagerInstance = TutorialScreenManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //close all the screens
            tutorialscreenmanagerInstance.TutorialScreenClosed(TutorialScreenManager.TutorialScreenType.SPS);
            tutorialscreenmanagerInstance.TutorialScreenClosed(TutorialScreenManager.TutorialScreenType.FIVESTONES);
            tutorialscreenmanagerInstance.TutorialScreenClosed(TutorialScreenManager.TutorialScreenType.CHAPTEH);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject.Find("MainCharacter").GetComponent<PlayerMovementScript>().SetRollbackPosition(new Vector2(transform.position.x, transform.position.y));

        switch (type)
        {
            case NPC_TYPE.SPS:
                tutorialscreenmanagerInstance.TutorialScreenOpen(TutorialScreenManager.TutorialScreenType.SPS);
                //SceneManager.LoadScene("Scissors Paper Stone");
                break;
            case NPC_TYPE.FIVE_STONES:
                tutorialscreenmanagerInstance.TutorialScreenOpen(TutorialScreenManager.TutorialScreenType.FIVESTONES);
                //SceneManager.LoadScene("FiveStonesFruitNinja");
                break;
            case NPC_TYPE.CHAPTEH:
                tutorialscreenmanagerInstance.TutorialScreenOpen(TutorialScreenManager.TutorialScreenType.CHAPTEH);
                //SceneManager.LoadScene("Chapteh");
                break;
            case NPC_TYPE.CUSTOMIZER:
                SceneManager.LoadScene("CustomizeScene");
                break;
            case NPC_TYPE.LEADERBOARD:
                // Recode this later on
                Resources.FindObjectsOfTypeAll<LeaderboardManager>()[0].gameObject.SetActive(true);
                break;
        }
    }
}