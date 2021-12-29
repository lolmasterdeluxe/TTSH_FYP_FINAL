using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pregame : MonoBehaviour
{
    public SpriteRenderer backgroundSpriteRenderer;
    public CanvasGroup panelCanvasGroup;
    public CanvasGroup mainUICanvasGroup;
    public TMP_Text countdownText;

    // Start is called before the first frame update
    void Start()
    {
        TweenManager.Instance.AnimateFade(mainUICanvasGroup, 0f, 0f);
        TweenManager.Instance.AnimateFade(panelCanvasGroup, 0f, 0f);
        TimerManager.Instance.StartCountdown(4);
        TimerManager.Instance.e_TimerTick.AddListener(CountdownTick);
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
    }

    void GameStart()
    {
        TweenManager.Instance.AnimateScale(backgroundSpriteRenderer.transform, 1.5f, 1f);
        TweenManager.Instance.AnimateFade(mainUICanvasGroup, 1f, 0.5f);
        TweenManager.Instance.AnimateFade(panelCanvasGroup, 1f, 0.5f);
        countdownText.gameObject.SetActive(false);
        FiveStonesGameManager.Instance.StartGame(60, 1);
    }
}
