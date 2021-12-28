using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    public GameObject _splashScreenGroup;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitFade());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Fade()
    {
        TweenManager.Instance.AnimateFade(_splashScreenGroup.GetComponent<CanvasGroup>(), 0f, 2f);
    }

    IEnumerator WaitFade()
    {
        yield return new WaitForSeconds(2f);
        Fade();
    }
}
