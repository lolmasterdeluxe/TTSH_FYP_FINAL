using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnvironmentManager : MonoBehaviour
{
    //environment manager: moves the backgrounds of the game and stuff relative to time

    #region Variables

    [Tooltip("Reference to the Parallex Background Manager")]
    ParallexBackgroundManager parallexbackgroundmanagerInstance;

    //for time system
    [Tooltip("Float time: for day night cycle shift")]
    float f_currentTime;

    [Tooltip("Reference to the cloud GameObjects")]
    [SerializeField]
    GameObject g_morningCloud, g_nightCloud;

    [Tooltip("Reference to the platform GameObjects")]
    [SerializeField]
    GameObject g_morningPlatform, g_nightPlatform;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        parallexbackgroundmanagerInstance = FindObjectOfType<ParallexBackgroundManager>();
        f_currentTime = 2f;
        //set everthing to night FIRST
        OnStartRun();

    }

    private void Update()
    {

        #region Time

        f_currentTime += Time.deltaTime * 1.2f;

        //one day cycle has been completed, reset it
        if (f_currentTime >= 24f)
        {
            f_currentTime = 0f;
            Debug.Log("cycle once");
        }

        //switching to morning
        if (f_currentTime >= 9f)
        {
            ItIsDay();
        }

        //switching to night
        if (f_currentTime >= 19f)
        {
            ItIsNight();
        }



        #endregion

        #region Background Scrolling 

        //for background

        parallexbackgroundmanagerInstance.SetBackgroundOffsetVector(new Vector2(0f, -0.048f));
        parallexbackgroundmanagerInstance.componentContainer[0].GetComponent<MeshRenderer>().material.mainTextureOffset
        += parallexbackgroundmanagerInstance.GetBackgroundOffsetVector() * Time.deltaTime;

        //for clouds

        //morning cloud
        parallexbackgroundmanagerInstance.SetBackgroundOffsetVector(new Vector2(0.08f, 0f));
        parallexbackgroundmanagerInstance.componentContainer[1].GetComponent<MeshRenderer>().material.mainTextureOffset
        += parallexbackgroundmanagerInstance.GetBackgroundOffsetVector() * Time.deltaTime;

        //night cloud
        parallexbackgroundmanagerInstance.SetBackgroundOffsetVector(new Vector2(0.08f, 0f));
        parallexbackgroundmanagerInstance.componentContainer[2].GetComponent<MeshRenderer>().material.mainTextureOffset
        += parallexbackgroundmanagerInstance.GetBackgroundOffsetVector() * Time.deltaTime;

        #endregion

    }

    #endregion

    #region Helper Functions

    public void ItIsDay()
    {
        g_morningPlatform.transform.GetComponent<SpriteRenderer>().DOFade(1f, 7f);
        g_nightPlatform.transform.GetComponent<SpriteRenderer>().DOFade(0f, 7f);
    }

    public void ItIsNight()
    {
        g_morningPlatform.transform.GetComponent<SpriteRenderer>().DOFade(0f, 7f);
        g_nightPlatform.transform.GetComponent<SpriteRenderer>().DOFade(1f, 7f);
    }

    public void OnStartRun()
    {
        g_morningPlatform.transform.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
        g_nightPlatform.transform.GetComponent<SpriteRenderer>().DOFade(1f, 0f);
    }


    #endregion

}
