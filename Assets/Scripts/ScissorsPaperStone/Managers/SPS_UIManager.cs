using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class SPS_UIManager : MonoBehaviour
{
    //this manager handles all the UI in the game

    private static SPS_UIManager _instance;

    public static SPS_UIManager Instance { get { return _instance; } }

    #region Variables

    //reference to other scripts
    [SerializeField]
    private SPS_ObjectManager objectmanagerInstance;

    //for score

    [Tooltip("Base score that each game session starts on")]
    int i_baseScore;
    
    [SerializeField]
    private GameObject g_scoreText;

    //for timer

    [SerializeField]
    private GameObject g_timerText;

    //for combo
    [SerializeField]
    private GameObject g_comboGroup, g_comboText, g_comboText_finalPos;

    //for objective (formely powerups)

    [Tooltip("Bonus objective value that adds at end of game")]
    int i_objectiveValue;

    [SerializeField]
    private GameObject g_objectiveText;

    //for UI buttons

    [Tooltip("Reference to the UI buttons")]
    public GameObject g_scissorsButton, g_paperButton, g_stoneButton;

    // for game start and end
    [SerializeField]
    private GameObject g_gameTimeUp;

    [HideInInspector] 
    public bool b_gameStart, b_gameEnded;

    //variables for data HERE
    [HideInInspector]
    public int enemyCount, sweetCount;

    //for audio source
    public AudioSource bgmSource;


    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    private void Start()
    {
        //pause audio here: for countdown
        bgmSource.Stop();
    }

    public void StartGame(float time, int score)
    {
        //start audio HERE
        bgmSource.Play();

        b_gameStart = true;
        b_gameEnded = false;

        //start the coroutine that allows spawning
        objectmanagerInstance.b_allowObjectSpawning = true;

        StartGameTimer(time);

        i_baseScore = score;

        i_objectiveValue = 0;

        //set combo expiry HERE
        ComboManager.Instance.SetComboExpiry(4f);
        //set up scores HERE
        ScoreManager.Instance.LoadNewGamemode(ScoreManager.Gamemode.SPS);
        //we set the combo manager's alpha to be 0 on start
        TweenManager.Instance.AnimateFade(g_comboGroup.GetComponent<CanvasGroup>(), 0f, 0f);
        TweenManager.Instance.AnimateFade(g_gameTimeUp.GetComponent<CanvasGroup>(), 0f, 0f);

        //attach events HERE
        ComboManager.Instance.e_comboAdded.AddListener(ComboAdded);
        ComboManager.Instance.e_comboBreak.AddListener(ComboBroken);
        TimerManager.Instance.e_TimerFinished.AddListener(EndGame);

    }


    private void Update()
    {
        if (!b_gameStart)
            return;

        UpdatePlayerScore();
        UpdateTimerText();
        UpdatePlayerObjectiveValue();

        StartCoroutine(OnLeaderboardLoad());
    }

    #endregion

    #region Score Functions

    public void PlayerScores()
    {
        enemyCount++;
        if (ComboManager.Instance.GetCurrentCombo() != 0)
            ScoreManager.Instance.AddCurrentGameScore(i_baseScore * ComboManager.Instance.GetCurrentCombo());
        else
            ScoreManager.Instance.AddCurrentGameScore(i_baseScore);
    }

    public void UpdatePlayerScore()
    {
        g_scoreText.GetComponent<TMP_Text>().text = ScoreManager.Instance.GetCurrentGameScore().ToString();
    }

    #endregion

    #region Combo Manager Functions

    public void UpdateComboScore(GameObject text)
    {
        text.GetComponent<TMP_Text>().text = "Combo " + ComboManager.Instance.GetCurrentCombo() + "x";
    }

    #endregion

    #region Timer Functions

    public void StartGameTimer(float gameTime)
    {
        TimerManager.Instance.StartCountdown(gameTime);
    }

    public void EndGame()
    {
        b_gameEnded = true;
        TweenManager.Instance.AnimateFade(g_gameTimeUp.GetComponent<CanvasGroup>(), 1f, 0.5f);
        ScoreManager.Instance.EndCurrentGameScore();
    }

    public void UpdateTimerText()
    {
        g_timerText.GetComponent<TMP_Text>().text = TimerManager.Instance.GetFormattedRemainingTime();
    }

    #endregion

    #region Objective Text Functions

    public void AddObjectiveValue()
    {
        i_objectiveValue += 1;
        ScoreManager.Instance.AddCurrentGameScore(10);
        sweetCount++;
    }

    public void UpdatePlayerObjectiveValue()
    {
        g_objectiveText.GetComponent<TMP_Text>().text = i_objectiveValue.ToString();
    }

    #endregion


    #region Combo Manager Functions

    public void ComboAdded()
    {
        TweenManager.Instance.AnimateFade(g_comboGroup.GetComponent<CanvasGroup>(), 1, 0.25f);
        TweenManager.Instance.AnimateEnlargeText(g_comboText.transform,
            1f + (0.5f * ComboManager.Instance.GetCurrentCombo()), 0.25f);
        TweenManager.Instance.AnimateFloat(g_comboText.transform, 5f, g_comboText_finalPos.transform.position);
    }

    public void ComboBroken()
    {
        TweenManager.Instance.AnimateShake(g_comboText.transform, 3, 1f);
        TweenManager.Instance.AnimateFade(g_comboGroup.GetComponent<CanvasGroup>(), 0f, 0.5f);
    }

    #endregion

    #region LeaderBoard Functions

    public IEnumerator OnLeaderboardLoad()
    {
        if (b_gameEnded)
        {
            yield return new WaitForSeconds(3);
            bgmSource.Stop();
            Resources.FindObjectsOfTypeAll<LeaderboardManager>()[0].gameObject.SetActive(true);
        }
    }

    #endregion
}
