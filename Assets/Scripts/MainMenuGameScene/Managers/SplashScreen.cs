using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] private GameObject _splashScreenGroup;
    [SerializeField] private float WaitTime = 2;

    // Start is called before the first frame update
    void Start()
    {
        //ScoreManager.Instance.LoadWebScoreList();
        ScoreManager.Instance.ResetUser();
        StartCoroutine(WaitFade());
    }
    private void Fade()
    {
        TweenManager.Instance.AnimateFade(_splashScreenGroup.GetComponent<CanvasGroup>(), 0f, WaitTime);
    }

    IEnumerator WaitFade()
    {
        yield return new WaitForSeconds(WaitTime);
        Fade();
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
}
