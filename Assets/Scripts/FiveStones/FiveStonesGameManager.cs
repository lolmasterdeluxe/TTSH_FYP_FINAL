using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiveStonesGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // First parameter is the countdown time in seconds
        // Second parameter is a time threshold where when it passes the event e_TimerPassedThreshold gets invoked
        // Second parameter is optional and can be empty
        TimerManager.Instance.StartCountdown(10, 3);
        TimerManager.Instance.StartCountdown(10);

        // Attaches an action/method to the event, it will get called when event is invoked
        TimerManager.Instance.e_TimerPassedThreshold.AddListener(AnnounceGameEndingSoon);
        TimerManager.Instance.e_TimerFinished.AddListener(EndGame);

        // Returns raw remaining time in float (seconds)
        TimerManager.Instance.GetRemainingTime();

        // Returns remaining formatted time in string -> 00:00
        TimerManager.Instance.GetFormattedRemainingTime();

        // Returns remaining formatted time in string -> 00:00:00
        TimerManager.Instance.GetFormattedRemainingTimeMS();
    }

    public void AnnounceGameEndingSoon()
    {
        // Indicate that the game is ending soon etc
        // Change timer text to red or smth
    }

    public void EndGame()
    {
        // Do finished game animation / logic etc
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
