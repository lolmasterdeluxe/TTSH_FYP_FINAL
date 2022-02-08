﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

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

    //for interact HUD
    public GameObject interactPrefab;

    //audioSource variables
    public AudioSource interactSFX, hoverSFX;
    public AudioSource BGM;

    void Start()
    {
        tutorialscreenmanagerInstance = TutorialScreenManager.instance;
        interactPrefab.transform.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
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

            //reset the screen number to be 0
            tutorialscreenmanagerInstance.screenNumber = 0;

            //reset audio volume
            BGM.volume = 0.2f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject.Find("MainCharacter").GetComponent<PlayerKeyboardMovement>().SetRollbackPosition(new Vector2(transform.position.x, transform.position.y));

        switch (type)
        {
            case NPC_TYPE.SPS:
                interactPrefab.transform.GetComponent<SpriteRenderer>().DOFade(1f, 1.25f);
                hoverSFX.Play();
                if (Input.GetKeyDown(KeyCode.F))
                    tutorialscreenmanagerInstance.TutorialScreenOpen(TutorialScreenManager.TutorialScreenType.SPS);
                break;
            case NPC_TYPE.FIVE_STONES:
                interactPrefab.transform.GetComponent<SpriteRenderer>().DOFade(1f, 1.25f);
                hoverSFX.Play();
                if (Input.GetKeyDown(KeyCode.F))
                tutorialscreenmanagerInstance.TutorialScreenOpen(TutorialScreenManager.TutorialScreenType.FIVESTONES);
                break;
            case NPC_TYPE.CHAPTEH:
                interactPrefab.transform.GetComponent<SpriteRenderer>().DOFade(1f, 1.25f);
                hoverSFX.Play();
                if (Input.GetKeyDown(KeyCode.F))
                tutorialscreenmanagerInstance.TutorialScreenOpen(TutorialScreenManager.TutorialScreenType.CHAPTEH);
                break;
            case NPC_TYPE.CUSTOMIZER:
                interactPrefab.transform.GetComponent<SpriteRenderer>().DOFade(1f, 1.25f);
                hoverSFX.Play();
                if (Input.GetKeyDown(KeyCode.F))
                SceneManager.LoadScene("CustomizeScene");
                break;
            case NPC_TYPE.LEADERBOARD:
                // Recode this later on
                interactPrefab.transform.GetComponent<SpriteRenderer>().DOFade(1f, 1.25f);
                hoverSFX.Play();
                if (Input.GetKeyDown(KeyCode.F))
                Resources.FindObjectsOfTypeAll<LeaderboardManager>()[0].gameObject.SetActive(true);
                break;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject.Find("MainCharacter").GetComponent<PlayerKeyboardMovement>().SetRollbackPosition(new Vector2(transform.position.x, transform.position.y));

        switch (type)
        {
            case NPC_TYPE.SPS:
                if (Input.GetKeyDown(KeyCode.F))
                {
                    interactSFX.Play();
                    BGM.volume = 0.05f;
                    tutorialscreenmanagerInstance.TutorialScreenOpen(TutorialScreenManager.TutorialScreenType.SPS);
                }
                    break;
            case NPC_TYPE.FIVE_STONES:
                if (Input.GetKeyDown(KeyCode.F))
                {
                    interactSFX.Play();
                    BGM.volume = 0.05f;
                    tutorialscreenmanagerInstance.TutorialScreenOpen(TutorialScreenManager.TutorialScreenType.FIVESTONES);
                }
                    break;
            case NPC_TYPE.CHAPTEH:
                if (Input.GetKeyDown(KeyCode.F))
                {
                    interactSFX.Play();
                    BGM.volume = 0.05f;
                    tutorialscreenmanagerInstance.TutorialScreenOpen(TutorialScreenManager.TutorialScreenType.CHAPTEH);
                }   
                    break;
            case NPC_TYPE.CUSTOMIZER:
                if (Input.GetKeyDown(KeyCode.F))
                {
                    interactSFX.Play();
                    BGM.volume = 0.05f;
                    SceneManager.LoadScene("CustomizeScene");
                }
                break;
            case NPC_TYPE.LEADERBOARD:
                // Recode this later on
                if (Input.GetKeyDown(KeyCode.F))
                {
                    interactSFX.Play();
                    BGM.volume = 0.05f;
                    Resources.FindObjectsOfTypeAll<LeaderboardManager>()[0].gameObject.SetActive(true);
                }
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (type)
        {
            case NPC_TYPE.SPS:
                interactPrefab.transform.GetComponent<SpriteRenderer>().DOFade(0f, .75f);
                break;
            case NPC_TYPE.FIVE_STONES:
                interactPrefab.transform.GetComponent<SpriteRenderer>().DOFade(0f, .75f);
                break;
            case NPC_TYPE.CHAPTEH:
                interactPrefab.transform.GetComponent<SpriteRenderer>().DOFade(0f, .75f);
                break;
            case NPC_TYPE.CUSTOMIZER:
                interactPrefab.transform.GetComponent<SpriteRenderer>().DOFade(0f, .75f);
                break;
            case NPC_TYPE.LEADERBOARD:
                interactPrefab.transform.GetComponent<SpriteRenderer>().DOFade(0f, .75f);
                break;
        }
    }


}