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

    [SerializeField] private GameObject g_scoreText;
    [SerializeField] private GameObject g_comboGroup;
    [SerializeField] private GameObject g_popupTextGroup;
    [SerializeField] private GameObject g_popupImageGroup;
    [SerializeField] private GameObject g_gameTimeUp;
    [SerializeField] private GameObject g_comboText;
    [SerializeField] private GameObject g_popupText;
    [SerializeField] private GameObject g_comboExpiryBar;
    [SerializeField] private GameObject g_PopupExpiry;
    [SerializeField] private GameObject g_timerText;
    [SerializeField] private GameObject g_objectiveText;
    [SerializeField] private Objective m_currentObjective;
    [SerializeField] private GameObject AudioObject;

    private float difficultyMultiplier;
    private int baseScore = 1;
    private float minObjectiveReset;
    private float maxObjectiveReset;
    private bool m_gameStarted;
    private bool m_gameEnded = false;

    [HideInInspector] public int m_totalCaught = 0;
    [HideInInspector] public int m_totalRainbowCaught = 0;

    public AudioSource[] audioSources;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
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
        TweenManager.Instance.AnimateFade(g_popupTextGroup.GetComponent<CanvasGroup>(), 0f, 0f);
        TweenManager.Instance.AnimateFade(g_popupImageGroup.GetComponent<CanvasGroup>(), 0f, 0f);
        TweenManager.Instance.AnimateFade(g_gameTimeUp.GetComponent<CanvasGroup>(), 0f, 0f);
        GetComponent<StoneSpawner>().Configure(3, 5, 4, 6, 10, 15);
        StartCoroutine(GetComponent<StoneSpawner>().SpawnStoneLoop());
        RandomizeObjective();
        StartCoroutine(ObjectiveCoroutine());

        audioSources[0].Play();
        // Attach events
        TimerManager.Instance.e_TimerFinished.AddListener(OnGameEnd);
        ComboManager.Instance.e_comboAdded.AddListener(OnComboAdd);
        ComboManager.Instance.e_comboBreak.AddListener(OnComboBreak);
        ComboManager.Instance.e_comboAdded.AddListener(OnPopUp);
        //ComboManager.Instance.e_comboAdded.AddListener(OnPopUpImage);
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
                g_objectiveText.GetComponent<TMP_Text>().text = "<sprite=2>";
                break;
            case Objective.CATCH_YELLOW_STONES:
                g_objectiveText.GetComponent<TMP_Text>().text = "<sprite=1>";
                break;
            case Objective.CATCH_BLUE_STONES:
                g_objectiveText.GetComponent<TMP_Text>().text = "<sprite=0>";
                break;
            case Objective.CATCH_ANY_STONES:
                g_objectiveText.GetComponent<TMP_Text>().text = "ANY";
                break;
        }
    }

    public void changePopups()
    {
        int currcom = ComboManager.Instance.GetCurrentCombo();
        if (currcom == 1)
        {
            g_popupText.GetComponent<TMP_Text>().text = "Nice";
        }
        else if (currcom == 2)
        {
            g_popupText.GetComponent<TMP_Text>().text = "Good";
        }
        else if (currcom == 3)
        {
            g_popupText.GetComponent<TMP_Text>().text = "Cool";
        }
        else if (currcom == 4)
        {
            g_popupText.GetComponent<TMP_Text>().text = "Awesome";
        }
        else if (currcom >= 5)
        {
            g_popupText.GetComponent<TMP_Text>().text = "Amazing!";

        }
        
    }

    public void changepopUpImage()
    {
        int currcom = ComboManager.Instance.GetCurrentCombo();

        if(currcom >=5)
        {
            OnPopUpImage();
        }
    }

    void UpdateUI()
    {
        g_scoreText.GetComponent<TMP_Text>().text = ScoreManager.Instance.GetCurrentGameScore().ToString();
        g_timerText.GetComponent<TMP_Text>().text = TimerManager.Instance.GetFormattedRemainingTime();
        g_comboText.GetComponent<TMP_Text>().text = "Combo " + ComboManager.Instance.GetCurrentCombo() + "x";
        changePopups();
        changepopUpImage();
        g_comboExpiryBar.GetComponent<Slider>().value = ComboManager.Instance.GetComboExpiryTimer() / ComboManager.Instance.GetComboExpiryTimerDefault();
        g_PopupExpiry.GetComponent<Slider>().value = ComboManager.Instance.GetComboExpiryTimer() / ComboManager.Instance.GetComboExpiryTimerDefault();
    }
    public void OnStoneCaught(GameObject gameObject)
    {
        m_totalCaught++;

        if (gameObject.GetComponent<Stone>().type == Objective.BOMB_STONES)
        {
            audioSources[1].Play();
            ScoreManager.Instance.ReduceCurrentGameScore(baseScore * ComboManager.Instance.GetCurrentCombo());
            ComboManager.Instance.BreakCombo();
        }
        else if (gameObject.GetComponent<Stone>().type == Objective.CATCH_ANY_STONES)
        {
            audioSources[4].Play();
            m_totalRainbowCaught++;
            ComboManager.Instance.AddCombo();
            ScoreManager.Instance.AddCurrentGameScore(baseScore * ComboManager.Instance.GetCurrentCombo() * 2);
        }
        else if (gameObject.GetComponent<Stone>().type == m_currentObjective || m_currentObjective == Objective.CATCH_ANY_STONES)
        {
            audioSources[5].Play();
            ComboManager.Instance.AddCombo();
            ScoreManager.Instance.AddCurrentGameScore(baseScore * ComboManager.Instance.GetCurrentCombo());
        }
        else
        {
            audioSources[5].Play();
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

    public void OnPopUp()
    {
        TweenManager.Instance.AnimateFade(g_popupTextGroup.GetComponent<CanvasGroup>(), 1f, 0.25f);
        //TweenManager.Instance.AnimateEnlargeText(g_popupText.transform,1f,0.25f);
    }

    public void OnPopUpImage()
    {
        TweenManager.Instance.AnimateFade(g_popupImageGroup.GetComponent<CanvasGroup>(), 1f, 0.25f);
        TweenManager.Instance.AnimateShake(g_comboText.transform, 2, 1f);
    }

    public void OnComboBreak()
    {
        TweenManager.Instance.AnimateShake(g_comboText.transform, 2, 1f);
        TweenManager.Instance.AnimateFade(g_comboGroup.GetComponent<CanvasGroup>(), 0f, 0.5f);
        TweenManager.Instance.AnimateFade(g_popupTextGroup.GetComponent<CanvasGroup>(), 0, 0.5f);
        TweenManager.Instance.AnimateFade(g_popupImageGroup.GetComponent<CanvasGroup>(), 0, 0.5f);
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
            AudioObject.SetActive(false);
            Resources.FindObjectsOfTypeAll<LeaderboardManager>()[0].gameObject.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        if (this == _instance)
            _instance = null;
    }
}
