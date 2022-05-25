using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] private GameObject _splashScreenGroup;
    [SerializeField] private float WaitTime = 2;
    [SerializeField] private AudioSource BGM;
    private bool Init = false;

    // Start is called before the first frame update
    void Update()
    {
        if (ScoreManager.Instance.driveResult == null)
            return;
        else if (!Init)
        {
            BGM.Play();
            ScoreManager.Instance.ResetUser();
            StartCoroutine(WaitFade());
            Init = true;
        }
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
