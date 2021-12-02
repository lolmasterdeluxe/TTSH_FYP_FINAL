using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SPS_Timer : MonoBehaviour
{
    //this handles game time, referencing from time manager

    #region Variables

    TimerManager timermanagerInstance;
    public GameObject timertext;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        timermanagerInstance = FindObjectOfType<TimerManager>();
        StartGame(60f);
    }

    private void Update()
    {
        timertext.GetComponent<TMP_Text>().text 
            = timermanagerInstance.GetFormattedRemainingTime();
    }


    #endregion

    #region Functions

    public void StartGame(float gameTime)
    {
        timermanagerInstance.StartCountdown(gameTime);
    }

    #endregion
}
