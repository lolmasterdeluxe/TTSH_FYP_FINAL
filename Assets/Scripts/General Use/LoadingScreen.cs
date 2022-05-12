using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject LoadingScreenObject;
    private CanvasGroup LoadingScreenCanvas;
    [SerializeField]
    private Sprite[] LoadScreenImage;
    // Start is called before the first frame update
    void Start()
    {
        LoadingScreenCanvas = LoadingScreenObject.GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        LoadingScreenObject.transform.GetChild(1).GetComponent<Image>().sprite = LoadScreenImage[(int)(Time.time * 2) % LoadScreenImage.Length];
    }

    public void LoadScreen()
    {
        LoadingScreenObject.SetActive(true);
        TweenManager.Instance.AnimateFade(LoadingScreenCanvas, 1, 3);
    }
}
