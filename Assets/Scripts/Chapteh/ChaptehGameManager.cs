﻿using System.Collections;
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
        HIT_RED_RINGS,
        HIT_YELLOW_RINGS,
        HIT_GREEN_RINGS,
        HIT_ANY_RINGS,
        TOTAL,
    }

    public GameObject g_timerText;
    public GameObject g_comboGroup;
    public GameObject g_scoreText;
    public GameObject g_comboText;
    public GameObject g_comboExpiryBar;
    public GameObject g_objectiveText;
    public Objective m_currentObjective;

    public int m_score;
    private int redbaseScore = 1;
    private int yellowbaseScore = 3;
    private int greenbaseScore = 5;
    private int anybaseScore = 2;

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
        ScoreManager.Instance.LoadAllScoreList();
        ScoreManager.Instance.EndSessionConcludeScore();
    }

    void StartGame(float time)
    {
        // Setup managers
        TimerManager.Instance.StartCountdown(time);
        ComboManager.Instance.SetComboExpiry(8f);
        ScoreManager.Instance.LoadNewGamemode(ScoreManager.Gamemode.CHAPTEH);

        // Setup game logic
        RandomizeObjective();
        StartCoroutine(ObjectiveCoroutine());

        // Attach events
        TimerManager.Instance.e_TimerFinished.AddListener(OnGameEnd);
        ComboManager.Instance.e_comboAdded.AddListener(OnComboAdd);
        ComboManager.Instance.e_comboBreak.AddListener(OnComboBreak);
    }

    // Update is called once per frame
    void Update()
    {
        UIUpdate();

        EndGame();
    }

    public IEnumerator ObjectiveCoroutine()
    {
        while (true)
        {
            RandomizeObjective();
            yield return new WaitForSeconds(5);
        }
    }

    public void RandomizeObjective()
    {
        m_currentObjective = (Objective)Random.Range(1, (int)Objective.TOTAL);
        //m_currentObjective = Objective.HIT_ANY_RINGS;

        switch (m_currentObjective)
        {
            case Objective.HIT_RED_RINGS:
                g_objectiveText.GetComponent<TMP_Text>().text = "Shoot into <color=red>red</color> Rings!";
                break;
            case Objective.HIT_YELLOW_RINGS:
                g_objectiveText.GetComponent<TMP_Text>().text = "Shoot into <color=yellow>yellow</color> Rings!";
                break;
            case Objective.HIT_GREEN_RINGS:
                g_objectiveText.GetComponent<TMP_Text>().text = "Shoot into <color=green>green</color> Rings!";
                break;
            case Objective.HIT_ANY_RINGS:
                g_objectiveText.GetComponent<TMP_Text>().text = "Shoot into <color=grey>any</color> Rings!";
                break;
        }
    }

    private void UIUpdate()
    {
        g_scoreText.GetComponent<TMP_Text>().text = "Score \n" + ScoreManager.Instance.GetCurrentGameScore();
        g_timerText.GetComponent<TMP_Text>().text = TimerManager.Instance.GetFormattedRemainingTime();
        g_comboText.GetComponent<TMP_Text>().text = "Combo: " + ComboManager.Instance.GetCurrentCombo() + "x";
        g_comboExpiryBar.GetComponent<Slider>().value = ComboManager.Instance.GetComboExpiryTimer() / ComboManager.Instance.GetComboExpiryTimerDefault();

        // Replace this with animation for pop up combo box here or something
        if (ComboManager.Instance.GetCurrentCombo() > 0)
        {
            g_comboText.SetActive(true);
        }
        else
        {
            g_comboText.SetActive(false);
        }
    }

    public void OnChaptehHit(GameObject gameObject)
    {
        if (gameObject.GetComponent<Rings>().type == m_currentObjective && m_currentObjective == Objective.HIT_RED_RINGS)
        {
            ComboManager.Instance.AddCombo();
            ScoreManager.Instance.AddCurrentGameScore(redbaseScore * ComboManager.Instance.GetCurrentCombo());
        }
        else if(gameObject.GetComponent<Rings>().type == m_currentObjective && m_currentObjective == Objective.HIT_YELLOW_RINGS)
        {
            ComboManager.Instance.AddCombo();
            ScoreManager.Instance.AddCurrentGameScore(yellowbaseScore * ComboManager.Instance.GetCurrentCombo());
        }
        else if (gameObject.GetComponent<Rings>().type == m_currentObjective && m_currentObjective == Objective.HIT_GREEN_RINGS)
        {
            ComboManager.Instance.AddCombo();
            ScoreManager.Instance.AddCurrentGameScore(greenbaseScore * ComboManager.Instance.GetCurrentCombo());
        }
        else if (gameObject.GetComponent<Rings>().type == m_currentObjective && m_currentObjective == Objective.HIT_ANY_RINGS)
        {
            ComboManager.Instance.AddCombo();
            ScoreManager.Instance.AddCurrentGameScore(anybaseScore * ComboManager.Instance.GetCurrentCombo());
        }
        else
        {
            switch (gameObject.GetComponent<Rings>().type)
            {
                case Objective.HIT_RED_RINGS:
                    ScoreManager.Instance.AddCurrentGameScore(redbaseScore);
                    break;
                case Objective.HIT_YELLOW_RINGS:
                    ScoreManager.Instance.AddCurrentGameScore(yellowbaseScore);
                    break;
                case Objective.HIT_GREEN_RINGS:
                    ScoreManager.Instance.AddCurrentGameScore(greenbaseScore);
                    break;
                case Objective.HIT_ANY_RINGS:
                    ScoreManager.Instance.AddCurrentGameScore(anybaseScore);
                    break;
            }

            
        }
    }

    public void OnGroundBreakCombo()
    {
        ComboManager.Instance.BreakCombo();
    }

    public void OnComboAdd()
    {
        TweenManager.Instance.AnimateFade(g_comboGroup.GetComponent<CanvasGroup>(), 1f, 0.25f);
        TweenManager.Instance.AnimateEnlargeText(g_comboText.transform, 1f, 0.25f);
    }

    public void OnComboBreak()
    {
        TweenManager.Instance.AnimateShake(g_comboText.transform, 2, 1f);
        TweenManager.Instance.AnimateFade(g_comboGroup.GetComponent<CanvasGroup>(), 0f, 0.5f);
    }

    public void OnGameEnd()
    {
        ScoreManager.Instance.EndCurrentGameScore();
    }

    public static Objective GetRandomColouredObjective()
    {
        return (Objective)Random.Range(1, (int)Objective.TOTAL - 1);
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
