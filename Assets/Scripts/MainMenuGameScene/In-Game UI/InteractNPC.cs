using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class InteractNPC : MonoBehaviour
{
    TutorialScreenManager tutorialscreenmanagerInstance;

    //for interact HUD
    public GameObject interactButton;

    //audioSource variables
    public AudioSource hoverSFX;
    public AudioSource BGM;

    private bool InRange = false;

    void Start()
    {
        tutorialscreenmanagerInstance = TutorialScreenManager.instance;
        interactButton.transform.GetComponent<Image>().DOFade(0f, 0f);
        interactButton.transform.GetChild(0).GetComponent<Image>().DOFade(0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //close all the screens
            tutorialscreenmanagerInstance.TutorialScreenClosed("SPS");
            tutorialscreenmanagerInstance.TutorialScreenClosed("FIVESTONES");
            tutorialscreenmanagerInstance.TutorialScreenClosed("CHAPTEH");

            //reset the screen number to be 0
            tutorialscreenmanagerInstance.screenNumber = 0;

            //reset audio volume
            BGM.volume = 0.2f;
        }

        if (InRange)
            interactButton.SetActive(true);
        else if (interactButton.transform.GetComponent<Image>().color.a <= 0)
            interactButton.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        InRange = true;
        interactButton.transform.GetComponent<Image>().DOFade(1f, 1.25f);
        interactButton.transform.GetChild(0).GetComponent<Image>().DOFade(1f, 1.25f);
        hoverSFX.Play();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        InRange = false;
        interactButton.transform.GetComponent<Image>().DOFade(0f, .75f);
        interactButton.transform.GetChild(0).GetComponent<Image>().DOFade(0f, .75f);
    }

}