using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pregame : MonoBehaviour
{
    public enum GameMode
    {
        SPS,
        FIVE_STONES,
        CHAPTEH,
    }

    // Background sprite
    [SerializeField] private SpriteRenderer backgroundSpriteRenderer;

    // Panel Canvas for the black background
    [SerializeField] private CanvasGroup panelCanvasGroup;

    // Main UI Canvas
    [SerializeField] private CanvasGroup mainUICanvasGroup;

    // TMP of count down text
    [SerializeField] private TMP_Text countdownText;

    [SerializeField] private GameMode currentGamemode;

    public bool m_countdownOver = false;

    [SerializeField] private AudioSource[] countdownSource;

    [SerializeField] private float SPSTimer = 60, FiveStoneTimer = 60, ChaptehTimer = 60;

    [SerializeField] private int SPSDifficulty = 1, FiveStoneDifficulty = 1;

    // Start is called before the first frame update
    void Start()
    {
        switch (currentGamemode)
        {
            case GameMode.SPS:
                m_countdownOver = true;
                TweenManager.Instance.AnimateFade(mainUICanvasGroup, 0f, 0f);
                TweenManager.Instance.AnimateFade(panelCanvasGroup, 1f, 0f);
                TimerManager.Instance.StartCountdown(4);
                TimerManager.Instance.e_TimerTick.AddListener(CountdownTick);
                break;
            case GameMode.FIVE_STONES:
                m_countdownOver = true;
                TweenManager.Instance.AnimateFade(mainUICanvasGroup, 0f, 0f);
                TweenManager.Instance.AnimateFade(panelCanvasGroup, 0f, 0f);
                TimerManager.Instance.StartCountdown(4);
                TimerManager.Instance.e_TimerTick.AddListener(CountdownTick);
                break;
            case GameMode.CHAPTEH:
                m_countdownOver = true;
                TweenManager.Instance.AnimateFade(mainUICanvasGroup, 0f, 0f);
                TweenManager.Instance.AnimateFade(panelCanvasGroup, 1f, 0f);
                TimerManager.Instance.StartCountdown(4);
                TimerManager.Instance.e_TimerTick.AddListener(CountdownTick);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerManager.Instance.GetRemainingTime() <= 1)
        {
            GameStart();
            gameObject.SetActive(false);
        }
    }

    void CountdownTick()
    {
        countdownText.text = ((int)TimerManager.Instance.GetRemainingTime()).ToString();
        TweenManager.Instance.AnimateEnlargeText(countdownText.transform, 1f, 0.25f);

        if (m_countdownOver)
        {
            if ((int)TimerManager.Instance.GetRemainingTime() == 3)
                // Countdown sound
                countdownSource[0].Play();
            else if ((int)TimerManager.Instance.GetRemainingTime() == 2)
                // Countdown sound
                countdownSource[0].Play();
            else if ((int)TimerManager.Instance.GetRemainingTime() == 1)
                // Countdown sound
                countdownSource[0].Play();
            else if ((int)TimerManager.Instance.GetRemainingTime() == 0)
                // Countdown when at 0 sound
                countdownSource[1].Play();
        }
    }

    void GameStart()
    {
        switch (currentGamemode)
        {
            case GameMode.SPS:
                m_countdownOver = false;
                TweenManager.Instance.AnimateFade(mainUICanvasGroup, 1f, 1f);
                TweenManager.Instance.AnimateFade(panelCanvasGroup, 0f, 0f);
                countdownText.gameObject.SetActive(false);
                SPS_UIManager.Instance.StartGame(SPSTimer, SPSDifficulty);
                break;
            case GameMode.FIVE_STONES:
                m_countdownOver = false;
                TweenManager.Instance.AnimateScale(backgroundSpriteRenderer.transform, 1.5f, 1f);
                TweenManager.Instance.AnimateFade(mainUICanvasGroup, 1f, 0.5f);
                TweenManager.Instance.AnimateFade(panelCanvasGroup, 1f, 0.5f);
                countdownText.gameObject.SetActive(false);
                FiveStonesGameManager.Instance.StartGame(FiveStoneTimer, FiveStoneDifficulty);
                break;
            case GameMode.CHAPTEH:
                m_countdownOver = false;
                TweenManager.Instance.AnimateFade(mainUICanvasGroup, 1f, 1f);
                TweenManager.Instance.AnimateFade(panelCanvasGroup, 0f, 0f);
                countdownText.gameObject.SetActive(false);
                ChaptehGameManager.Instance.StartGame(ChaptehTimer);
                break;
        }
    }
}
