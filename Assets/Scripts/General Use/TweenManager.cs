using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AnimateEnlargeText(Transform goTransform, float strength, float duration)
    {
        goTransform.gameObject.SetActive(true);
        goTransform.DORewind();
        goTransform.DOPunchScale(new Vector3(strength, strength, strength), duration);
    }

    public void AnimateShakeAndFade(CanvasGroup goCanvasGroup, float duration)
    {
        goCanvasGroup.DOFade(0, duration).OnComplete(
            () => goCanvasGroup.gameObject.SetActive(false)    
        );
    }

    public void KillTween(GameObject gameObject)
    {
        
    }

    // Remove this if you want the TweenManager instance to be the same throughout the whole program
    private void OnDestroy()
    {
        if (this == _instance)
            _instance = null;
    }
}
