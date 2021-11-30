using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerManager : MonoBehaviour
{
    private static TimerManager _instance;
    public static TimerManager Instance { get { return _instance; } }

    #region Countdown Variable
    public UnityEvent e_TimerStarted;
    public UnityEvent e_TimerPassedThreshold;
    public UnityEvent e_TimerFinished;

    private bool m_timerRun;
    private bool m_timerActivated;
    private bool m_thresholdPassed;

    private float m_timeRemaining;
    private float m_timerThreshold;
    #endregion

    #region ElapsedTime Variable
    public UnityEvent e_elapsedTimeStarted;
    public UnityEvent e_elapsedTimeFinished;

    private bool m_elapsedTimeRun;
    private bool m_elapsedTimeActivated;

    private float m_elapsedTime;
    private float m_elapsedTimeThreshold;

    #endregion

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_timerRun)
        {
            m_timeRemaining -= Time.deltaTime;

            if (!m_timerActivated)
            {
                m_timerActivated = true;
                e_TimerStarted.Invoke();
            }

            if (m_timeRemaining <= 0)
            {
                m_timerRun = false;
                m_timeRemaining = 0;
                e_TimerFinished.Invoke();
                return;
            }

            if (m_timeRemaining <= m_timerThreshold && !m_thresholdPassed)
            {
                m_thresholdPassed = true;
                e_TimerPassedThreshold.Invoke();
            }
        }

        if (m_elapsedTimeRun)
        {
            m_elapsedTime += Time.deltaTime;

            if (!m_elapsedTimeActivated)
            {
                m_elapsedTimeActivated = true;
                e_elapsedTimeStarted.Invoke();
            }

            if (m_elapsedTime >= m_elapsedTimeThreshold)
            {
                m_elapsedTimeRun = false;
                e_elapsedTimeFinished.Invoke();
            }
        }
    }

    #region Timer helper functions
    
    // Starts the timer countdown
    // Parameters: time = how long the countdown lasts for
    // thresholdValue = at which point of the timer will invoke the e_TimerPassedThreshold event
    public void StartCountdown(float time, float thresholdValue = 0)
    {
        m_timerRun = true;
        m_thresholdPassed = false;

        m_timeRemaining = time;
        m_timerThreshold = thresholdValue;
    }

    // Adds time to the timer
    public void AddCountdown(float time)
    {
        m_timeRemaining += time;
    }

    // Reduces the time for the current timer
    public void ReduceCountdown(float time)
    {
        m_timeRemaining = Mathf.Clamp(m_timeRemaining - time, 0, float.MaxValue);
    }

    // Pauses the timer countdown
    public void PauseCountdown()
    {
        m_timerRun = false;
    }

    // Resumes the timer countdown
    public void ResumeCountdown()
    {
        m_timerRun = true;
    }

    // Returns the remaining time on the countdown timer in float
    public float GetRemainingTime()
    {
        return m_timeRemaining;
    }

    // Returns a string of the formatted countdown timer 59:59
    public string GetFormattedRemainingTime()
    {
        float currentTime = m_timeRemaining;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Returns a string of the formatted countdown timer with milliseconds 59:59:99
    public string GetFormattedRemainingTimeMS()
    {
        float currentTime = m_timeRemaining;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        float milliseconds = (currentTime % 1) * 1000;

        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }

    #endregion

    #region Elapsed Time Functions

    // Starts the elapsed time counter
    // Parameter: thresholdValue = at the point where the timer will stop
    // If parameter is empty, it the elapsed time will run forever
    public void StartElapsedTime(float thresholdValue = float.MaxValue)
    {
        m_elapsedTimeRun = true;

        m_elapsedTime = 0;
        m_elapsedTimeThreshold = thresholdValue;
    }

    // Adds time into the elapsed time
    public void AddElapsedTime(float time)
    {
        m_elapsedTime += time;
    }

    // Reduces time from the elapsed time
    public void ReduceElapsedTime(float time)
    {
        m_elapsedTime = Mathf.Clamp(m_elapsedTime - time, 0, float.MaxValue);
    }

    // Pauses the elapsed time
    public void PauseElapsedTime()
    {
        m_elapsedTimeRun = false;
    }

    // Resumes the elapsed time
    public void ResumeElapsedTime()
    {
        m_elapsedTimeRun = true;
    }

    // Returns the elapsed time in float
    public float GetElapsedTime()
    {
        return m_elapsedTime;
    }

    // Returns a string of the formatted elapsed time 59:59
    public string GetFormattedElapsedTime()
    {
        float currentTime = m_elapsedTime;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Returns a string of the formatted elapsed time 59:59:99
    public string GetFormattedElapsedTimeMS()
    {
        float currentTime = m_elapsedTime;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        float milliseconds = (currentTime % 1) * 1000;

        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }

    #endregion


    // Remove this if you want the timer manager instance to be the same throughout the whole scene
    private void OnDestroy()
    {
        if (this == _instance)
            _instance = null;
    }
}
