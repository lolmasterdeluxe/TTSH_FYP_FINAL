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
    SPS_ObjectManager objectmanagerInstance;

    //for score

    [Tooltip("Player Score")]
    public int current_playerScore;

    [Tooltip("Base score that each game session starts on")]
    int i_baseScore;
    
    [SerializeField]
    GameObject g_scoreText;

    //for timer

    [SerializeField]
    GameObject g_timerText;

    //for combo

    public GameObject g_comboGroup, g_comboText, g_comboText_finalPos;

    public bool b_gameStart;

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
        objectmanagerInstance = FindObjectOfType<SPS_ObjectManager>();
    }

    public void StartGame(float time, int score)
    {
        b_gameStart = true;

        //start the coroutine that allows spawning
        objectmanagerInstance.b_allowObjectSpawning = true;

        StartGameTimer(time);

        i_baseScore = score;

        //set combo expiry HERE
        ComboManager.Instance.SetComboExpiry(4f);
        //set up scores HERE
        ScoreManager.Instance.LoadNewGamemode(ScoreManager.Gamemode.SPS);
        //we set the combo manager's alpha to be 0 on start
        TweenManager.Instance.AnimateFade(g_comboGroup.GetComponent<CanvasGroup>(), 0f, 0f);

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
    }

    #endregion

    #region Score Functions

    public void PlayerScores()
    {
        ScoreManager.Instance.AddCurrentGameScore(i_baseScore * ComboManager.Instance.GetCurrentCombo());
    }

    public void UpdatePlayerScore()
    {
        g_scoreText.GetComponent<TMP_Text>().text = "Score: " + ScoreManager.Instance.GetCurrentGameScore();
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
        ScoreManager.Instance.EndCurrentGameScore();
    }

    public void UpdateTimerText()
    {
        g_timerText.GetComponent<TMP_Text>().text = TimerManager.Instance.GetFormattedRemainingTime();
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


}
