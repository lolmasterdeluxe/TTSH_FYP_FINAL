using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TweenManager : MonoBehaviour
{
    private static TweenManager _instance;
    public static TweenManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject instance = new GameObject("TweenManager");
                instance.AddComponent<TweenManager>();
            }

            return _instance;
        }
    }
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
        DOTween.defaultAutoPlay = AutoPlay.All;
        DOTween.Init();
    }

    // Update is called once per frame
    void Update()
    {
        DOTween.PlayAll();
    }

    public void AnimateEnlargeText(Transform goTransform, float strength, float duration)
    {
        goTransform.DORewind();
        goTransform.DOPunchScale(new Vector3(strength, strength, strength), duration);
    }

    public void AnimateFade(CanvasGroup goCanvasGroup, float alpha, float duration)
    {
        goCanvasGroup.DOFade(alpha, duration);
    }

    public void AnimateShake(Transform goTransform, float strength, float duration)
    {
        goTransform.DOShakePosition(duration, strength);
    }

    public void AnimateScale(Transform goTransform, float strength, float duration)
    {
        goTransform.DOScale(new Vector3(strength, strength, strength), duration);
    }

    public void AnimateFloat(Transform goTransform, float timeTaken, Vector3 finalPos)
    {
        goTransform.DOMoveY(finalPos.y, timeTaken);
    }

    public void AnimateSpriteFade(SpriteRenderer goSprite, float alpha, float duration)
    {
        goSprite.DOFade(alpha, duration);
    }

    public void AnimateImageFade(Image goImage, float alpha, float duration)
    {
        goImage.DOFade(alpha, duration);
    }

    public void KillTween(GameObject gameObject)
    {
        gameObject.transform.DOKill(false);
    }

    public void KillCanvasGroupTween(CanvasGroup gameObject)
    {
        gameObject.DOKill(false);
    }

    // Remove this if you want the TweenManager instance to be the same throughout the whole program
    private void OnDestroy()
    {
        if (this == _instance)
            _instance = null;
    }
}
