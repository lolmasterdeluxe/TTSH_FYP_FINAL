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

    public bool m_timerRun;
    public bool m_timerActivated;
    public bool m_thresholdPassed;

    public float m_timeRemaining;
    public float m_timerThreshold;
    #endregion

    #region ElapsedTime Variable
    public UnityEvent e_elapsedTimeStarted;
    public UnityEvent e_elapsedTimeFinished;

    public bool m_elapsedTimeRun;
    public bool m_elapsedTimeActivated;

    public float m_elapsedTime;
    public float m_elapsedTimeThreshold;

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
    public void StartCountdown(float time, float thresholdValue = 0)
    {
        m_timerRun = true;
        m_thresholdPassed = false;

        m_timeRemaining = time;
        m_timerThreshold = thresholdValue;
    }

    public void AddCountdown(float time)
    {
        m_timeRemaining += time;
    }

    public void ReduceCountdown(float time)
    {
        m_timeRemaining = Mathf.Clamp(m_timeRemaining - time, 0, float.MaxValue);
    }

    public void PauseCountdown()
    {
        m_timerRun = false;
    }

    public void ResumeCountdown()
    {
        m_timerRun = true;
    }

    public float GetRemainingTime()
    {
        return m_timeRemaining;
    }
    public string GetFormattedRemainingTime()
    {
        float currentTime = m_timeRemaining;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

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

    public void StartElapsedTime(float thresholdValue = float.MaxValue)
    {
        m_elapsedTimeRun = true;

        m_elapsedTime = 0;
        m_elapsedTimeThreshold = thresholdValue;
    }

    public void AddElapsedTime(float time)
    {
        m_elapsedTime += time;
    }

    public void ReduceElapsedTime(float time)
    {
        m_elapsedTime = Mathf.Clamp(m_elapsedTime - time, 0, float.MaxValue);
    }

    public void PauseElapsedTime()
    {
        m_elapsedTimeRun = false;
    }

    public void ResumeElapsedTime()
    {
        m_elapsedTimeRun = true;
    }

    public float GetElapsedTime()
    {
        return m_elapsedTime;
    }
    public string GetFormattedElapsedTime()
    {
        float currentTime = m_elapsedTime;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public string GetFormattedElapsedTimeMS()
    {
        float currentTime = m_elapsedTime;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        float milliseconds = (currentTime % 1) * 1000;

        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }

    #endregion


    // This can be removed if all scenes decide to use the same instance, but I doubt so
    private void OnDestroy()
    {
        if (this == _instance)
            _instance = null;
    }
}
