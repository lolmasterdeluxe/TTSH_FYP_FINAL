using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SPS_Timer : MonoBehaviour
{
    //this handles game time, referencing from time manager

    #region Variables
    public GameObject timertext;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        StartGame(90f);
    }

    private void Update()
    {
        timertext.GetComponent<TMP_Text>().text 
            = TimerManager.Instance.GetFormattedRemainingTime();
    }

    #endregion

    #region Functions

    public void StartGame(float gameTime)
    {
        TimerManager.Instance.StartCountdown(gameTime);
    }

    #endregion
}
