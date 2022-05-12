using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject LoadingScreenObject;
    private CanvasGroup LoadingScreenCanvas;
    [SerializeField]
    private Sprite[] LoadScreenImage;
    [SerializeField]
    private float loadingDuration = 3;
    // Start is called before the first frame update
    void Start()
    {
        LoadingScreenCanvas = LoadingScreenObject.GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        LoadingScreenObject.transform.GetChild(2).GetComponent<Image>().sprite = LoadScreenImage[(int)(Time.time * 2) % LoadScreenImage.Length];
    }

    public void LoadScreen()
    {
        LoadingScreenObject.SetActive(true);
        TweenManager.Instance.AnimateFade(LoadingScreenCanvas, 1, 3); 
        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {
        yield return null;
        float loadingTimer = 0;
        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("MainMenuGameScene");
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                //Wait to you press the space key to activate the Scene
                loadingTimer += Time.deltaTime;
                if (loadingTimer > loadingDuration)
                    //Activate the Scene
                    asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
