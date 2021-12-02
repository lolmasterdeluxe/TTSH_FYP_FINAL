using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChaptehGameManager : MonoBehaviour
{
    private static ChaptehGameManager _instance;
    public static ChaptehGameManager Instance { get { return _instance; } }

    public enum Objective
    {
        DEFAULT,
        HIT_RED_HOOPS,
        HIT_BLUE_HOOPS,
        HIT_GREEN_HOOPS,
        TOTAL,
    }

    public GameObject g_timerText;
    public GameObject g_scoreText;
    public Objective m_currentObjective;

    public int m_score;

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
        StartGame(90);
    }

    void StartGame(float time)
    {
        TimerManager.Instance.StartCountdown(time);
    }

    // Update is called once per frame
    void Update()
    {
        g_scoreText.GetComponent<TMP_Text>().text = "Score \n" + m_score.ToString();
        g_timerText.GetComponent<TMP_Text>().text = TimerManager.Instance.GetFormattedRemainingTime();

        EndGame();
    }

    public void OnChaptehHit(GameObject gameObject)
    {
        if (gameObject.GetComponent<Hoops>().type == Objective.HIT_RED_HOOPS)
        {
            m_score += 10;
        }
        else if(gameObject.GetComponent<Hoops>().type == Objective.HIT_BLUE_HOOPS)
        {
            m_score += 30;
        }
        else if (gameObject.GetComponent<Hoops>().type == Objective.HIT_GREEN_HOOPS)
        {
            m_score += 50;
        }
    }

    private void EndGame()
    {
        if(TimerManager.Instance.GetRemainingTime() == 0)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
        }
    }

    private void OnDestroy()
    {
        if (this == _instance)
            _instance = null;
    }
}
