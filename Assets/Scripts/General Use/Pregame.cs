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
    public SpriteRenderer backgroundSpriteRenderer;

    // Panel Canvas for the black background
    public CanvasGroup panelCanvasGroup;

    // Main UI Canvas
    public CanvasGroup mainUICanvasGroup;

    // TMP of count down text
    public TMP_Text countdownText;

    public GameMode currentGamemode;

    public bool m_countdownOver = false;

    public AudioSource[] countdownSource;

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
                SPS_UIManager.Instance.StartGame(60, 1);
                break;
            case GameMode.FIVE_STONES:
                m_countdownOver = false;
                TweenManager.Instance.AnimateScale(backgroundSpriteRenderer.transform, 1.5f, 1f);
                TweenManager.Instance.AnimateFade(mainUICanvasGroup, 1f, 0.5f);
                TweenManager.Instance.AnimateFade(panelCanvasGroup, 1f, 0.5f);
                countdownText.gameObject.SetActive(false);
                FiveStonesGameManager.Instance.StartGame(60, 1);
                break;
            case GameMode.CHAPTEH:
                m_countdownOver = false;
                TweenManager.Instance.AnimateFade(mainUICanvasGroup, 1f, 1f);
                TweenManager.Instance.AnimateFade(panelCanvasGroup, 0f, 0f);
                countdownText.gameObject.SetActive(false);
                ChaptehGameManager.Instance.StartGame(60);
                break;
        }
    }
}
