using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ComboManager : MonoBehaviour
{
    private static ComboManager _instance;
    public static ComboManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject instance = new GameObject("ComboManager");
                instance.AddComponent<ComboManager>();
            }

            return _instance;
        }
    }

    private int m_combo;

    private bool m_doesComboExpire;
    private float m_comboExpireSpeed;
    private float m_comboExpiryDefault;
    private float m_comboExpiryTimer;

    public UnityEvent e_comboStarted = new UnityEvent();
    public UnityEvent e_comboAdded = new UnityEvent();
    public UnityEvent e_comboChanged = new UnityEvent();
    public UnityEvent e_comboBreak = new UnityEvent();
    public UnityEvent e_comboExpired = new UnityEvent();

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (m_comboExpiryTimer > 0 && m_combo > 0 && m_doesComboExpire)
        {
            m_comboExpiryTimer -= Time.deltaTime * m_comboExpireSpeed;

            if (m_comboExpiryTimer <= 0)
            {
                m_comboExpiryTimer = m_comboExpiryDefault;
                m_combo--;

                if (m_combo <= 0)
                {
                    e_comboExpired.Invoke();
                    BreakCombo();
                }
            }
        }

    }

    // This will start the combo expiry
    // Setting expiry to 0 will disallow the combo from expiring
    public void SetComboExpiry(float expiry, float expireSpeed = 1)
    {
        if (expiry <= 0)
            m_doesComboExpire = false;
        else
        {
            m_comboExpiryDefault = expiry;
            m_comboExpiryTimer = m_comboExpiryDefault;
            m_comboExpireSpeed = expireSpeed;
            m_doesComboExpire = true;
        }
    }

    // Adds a combo, this will also invoke combo started event if it just started and will invoke combo added event
    public void AddCombo(int combo = 1)
    {
        if (m_combo <= 1)  
            e_comboStarted.Invoke();

        if (m_doesComboExpire)
            m_comboExpiryTimer = m_comboExpiryDefault;

        m_combo += combo;
        e_comboAdded.Invoke();
        e_comboChanged.Invoke();
    }

    public void SetCombo(int combo = 0)
    {
        m_combo = combo;
        e_comboChanged.Invoke();
    }

    // Breaks the combo, this will also invoke the combo break event
    public void BreakCombo()
    {
        m_combo = 0;
        e_comboBreak.Invoke();
        e_comboChanged.Invoke();
    }

    // Returns the current combo
    public int GetCurrentCombo()
    {
        return m_combo;
    }

    // Returns the current the timer for the combo expiry
    public float GetComboExpiryTimer()
    {
        return m_comboExpiryTimer;
    }

    // Returns the default expiry time also known as the maximum expiry time
    // Can be used to add a progress bar for the combo
    public float GetComboExpiryTimerDefault()
    {
        return m_comboExpiryDefault;
    }

    // Returns the expiry time multiplier
    public float GetComboExpirySpeed()
    {
        return m_comboExpireSpeed;
    }

    // Returns the expiry time format in either 00:00:00 or 00:00, depending on whether there are any minutes
    public string GetComboExpiryTimerFormatted()
    {
        float time = m_comboExpiryTimer;
        string formattedTime = "";

        if (m_comboExpiryTimer >= 60)
        {
            float minutes = Mathf.FloorToInt(time / 60);
            float seconds = Mathf.FloorToInt(time % 60);
            float milliseconds = (time % 1) * 1000;

            formattedTime = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        }
        else
        {
            float seconds = time;
            float milliseconds = (time % 1) * 1000;

            formattedTime = string.Format("{0:00}:{1:00}", seconds, milliseconds);
        }

        return formattedTime;
    }

    // Remove this if you want the combo manager instance to be the same throughout the whole program
    private void OnDestroy()
    {
        if (this == _instance)
            _instance = null;
    }
}
