using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FiveStonesGameManager : MonoBehaviour
{
    private static FiveStonesGameManager _instance;
    public static FiveStonesGameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject instance = new GameObject("GameManager");
                instance.AddComponent<FiveStonesGameManager>();
            }

            return _instance;
        }
    }

    public enum Objective
    { 
        DEFAULT,
        CATCH_RED_STONES,
        CATCH_YELLOW_STONES,
        CATCH_BLUE_STONES,
        BOMB_STONES,
        CATCH_ANY_STONES,
        TOTAL,
    }

    public GameObject g_scoreText;
    public GameObject g_comboGroup;
    public GameObject g_gameTimeUp;
    public GameObject g_comboText;
    public GameObject g_comboExpiryBar;
    public GameObject g_timerText;
    public GameObject g_objectiveText;
    public Objective m_currentObjective;

    public float difficultyMultiplier;
    public int baseScore = 1;
    public float minObjectiveReset;
    public float maxObjectiveReset;
    public bool m_gameStarted;
    public bool m_gameEnded = false;

    public int m_totalCaught = 0;
    public int m_totalRainbowCaught = 0;

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

    // Difficulty can be any numnber from 0 - 4f
    // Default value would be 1
    public void StartGame(float time, float difficultyMultiplier)
    {
        // Setup managers
        m_gameStarted = true;
        TimerManager.Instance.StartCountdown(time);
        ComboManager.Instance.SetComboExpiry(4f - difficultyMultiplier);
        ScoreManager.Instance.LoadNewGamemode(ScoreManager.Gamemode.FIVESTONES);

        // Setup game logic
        minObjectiveReset = 3;
        maxObjectiveReset = 5;
        this.difficultyMultiplier = difficultyMultiplier;
        TweenManager.Instance.AnimateFade(g_comboGroup.GetComponent<CanvasGroup>(), 0f, 0f);
        TweenManager.Instance.AnimateFade(g_gameTimeUp.GetComponent<CanvasGroup>(), 0f, 0f);
        GetComponent<StoneSpawner>().Configure(3, 5, 3, 5, 10, 15);
        StartCoroutine(GetComponent<StoneSpawner>().SpawnStoneLoop());
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
        if (!m_gameStarted)
            return;

        DifficultyProgression();
        UpdateUI();
    }

    public void DifficultyProgression()
    {
        float completionPercentage = (TimerManager.Instance.GetDefaultCountdownTime() - TimerManager.Instance.GetRemainingTime()) / TimerManager.Instance.GetDefaultCountdownTime();

        minObjectiveReset = 3 - (completionPercentage * difficultyMultiplier);
        maxObjectiveReset = 5 - (completionPercentage * difficultyMultiplier);
        GetComponent<StoneSpawner>().Configure(3 - (completionPercentage * difficultyMultiplier), 5 - (completionPercentage * difficultyMultiplier), 3 + (int)(completionPercentage * 5f), 5 + (int)(completionPercentage * 5f), 10, 15);
    }

    public IEnumerator ObjectiveCoroutine()
    {
        while (true)
        {
            if (TimerManager.Instance.GetRemainingTime() <= 0)
                break;

            RandomizeObjective();
            yield return new WaitForSeconds(Random.Range(minObjectiveReset, maxObjectiveReset));
        }
    }

    public void RandomizeObjective()
    {
        m_currentObjective = (Objective)Random.Range(1, (int)Objective.TOTAL);

        switch (m_currentObjective)
        {
            case Objective.CATCH_RED_STONES:
                g_objectiveText.GetComponent<TMP_Text>().text = "Catch <sprite=2> stones!";
                break;
            case Objective.CATCH_YELLOW_STONES:
                g_objectiveText.GetComponent<TMP_Text>().text = "Catch <sprite=0> stones!";
                break;
            case Objective.CATCH_BLUE_STONES:
                g_objectiveText.GetComponent<TMP_Text>().text = "Catch <sprite=1> stones!";
                break;
            case Objective.CATCH_ANY_STONES:
                g_objectiveText.GetComponent<TMP_Text>().text = "Catch <color=grey>any</color> stones!";
                break;
        }
    }

    void UpdateUI()
    {
        g_scoreText.GetComponent<TMP_Text>().text = "Score: " + ScoreManager.Instance.GetCurrentGameScore();
        g_timerText.GetComponent<TMP_Text>().text = TimerManager.Instance.GetFormattedRemainingTime();
        g_comboText.GetComponent<TMP_Text>().text = "Combo " + ComboManager.Instance.GetCurrentCombo() + "x";
        g_comboExpiryBar.GetComponent<Slider>().value = ComboManager.Instance.GetComboExpiryTimer() / ComboManager.Instance.GetComboExpiryTimerDefault();
    }
    public void OnStoneCaught(GameObject gameObject)
    {
        m_totalCaught++;

        if (gameObject.GetComponent<Stone>().type == Objective.BOMB_STONES)
        {
            ComboManager.Instance.BreakCombo();
            ScoreManager.Instance.ReduceCurrentGameScore(baseScore);
        }
        else if (gameObject.GetComponent<Stone>().type == Objective.CATCH_ANY_STONES)
        {
            m_totalRainbowCaught++;
            ComboManager.Instance.AddCombo();
            ScoreManager.Instance.AddCurrentGameScore(baseScore * ComboManager.Instance.GetCurrentCombo() * 2);
        }
        else if (gameObject.GetComponent<Stone>().type == m_currentObjective || m_currentObjective == Objective.CATCH_ANY_STONES)
        {
            ComboManager.Instance.AddCombo();
            ScoreManager.Instance.AddCurrentGameScore(baseScore * ComboManager.Instance.GetCurrentCombo());
        }
        else
        {
            ComboManager.Instance.BreakCombo();
            ScoreManager.Instance.AddCurrentGameScore(baseScore);
        }
    }
    
    public void OnGameEnd()
    {
        m_gameEnded = true;
        TweenManager.Instance.AnimateFade(g_gameTimeUp.GetComponent<CanvasGroup>(), 1f, 0.25f);
        ScoreManager.Instance.EndCurrentGameScore();
        StartCoroutine(OnLeaderboardLoad());
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

    public static Objective GetRandomColouredObjective()
    {
        return (Objective)Random.Range(1, (int)Objective.TOTAL);
    }

    public IEnumerator OnLeaderboardLoad()
    {
        if (m_gameEnded)
        {
            yield return new WaitForSeconds(3);

            Resources.FindObjectsOfTypeAll<LeaderboardManager>()[0].gameObject.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        if (this == _instance)
            _instance = null;
    }
}
