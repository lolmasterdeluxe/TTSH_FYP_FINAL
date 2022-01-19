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
    public GameObject g_morningCloud, g_noonCloud, g_nightCloud;

    [Tooltip("Reference to the platform GameObjects")]
    public GameObject g_morningPlatform, g_noonPlatform, g_nightPlatform;

    [Tooltip("Reference to the Dragon Slide GameObjects")]
    public GameObject g_morningDragon, g_noonDragon, g_nightDragon;


    #endregion

    #region Unity Callbacks

    private void Start()
    {
        parallexbackgroundmanagerInstance = FindObjectOfType<ParallexBackgroundManager>();
        f_currentTime = 8f;
        //set everthing to morning FIRST
        OnStartRun();

    }

    private void Update()
    {

        //update time HERE
        f_currentTime += Time.deltaTime * 0.5f;
        Debug.Log("Time:" + f_currentTime);


        //run the cycles base on the time
        if (f_currentTime > 6.5f && f_currentTime <= 12.5f)
        {
            g_morningCloud.transform.GetComponent<MeshRenderer>().material.DOFade(1f, 4f);
            g_morningPlatform.transform.GetComponent<SpriteRenderer>().DOFade(1f, 4f);
            g_morningDragon.transform.GetComponent<SpriteRenderer>().DOFade(1f, 2f);
            g_noonCloud.transform.GetComponent<MeshRenderer>().material.DOFade(0f, 4f);
            g_noonPlatform.transform.GetComponent<SpriteRenderer>().DOFade(0f, 4f);
            g_noonDragon.transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);
            g_nightCloud.transform.GetComponent<MeshRenderer>().material.DOFade(0f, 4f);
            g_nightPlatform.transform.GetComponent<SpriteRenderer>().DOFade(0f, 4f);
            g_nightDragon.transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);
        }
        if (f_currentTime > 12.5f && f_currentTime <= 19f)
        {
            g_morningCloud.transform.GetComponent<MeshRenderer>().material.DOFade(0f, 4f);
            g_morningPlatform.transform.GetComponent<SpriteRenderer>().DOFade(0f, 4f);
            g_morningDragon.transform.GetComponent<SpriteRenderer>().DOFade(0f, 2f);
            g_noonCloud.transform.GetComponent<MeshRenderer>().material.DOFade(1f, 4f);
            g_noonPlatform.transform.GetComponent<SpriteRenderer>().DOFade(1f, 4f);
            g_noonDragon.transform.GetComponent<SpriteRenderer>().material.DOFade(1f, 2f);
            g_nightCloud.transform.GetComponent<MeshRenderer>().material.DOFade(0f, 4f);
            g_nightPlatform.transform.GetComponent<SpriteRenderer>().DOFade(0f, 4f);
            g_nightDragon.transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);
        }
        if (f_currentTime > 19f && f_currentTime <= 24f)
        {
            g_morningCloud.transform.GetComponent<MeshRenderer>().material.DOFade(0f, 4f);
            g_morningPlatform.transform.GetComponent<SpriteRenderer>().DOFade(0f, 4f);
            g_morningDragon.transform.GetComponent<SpriteRenderer>().DOFade(0f, 2f);
            g_noonCloud.transform.GetComponent<MeshRenderer>().material.DOFade(0f, 4f);
            g_noonPlatform.transform.GetComponent<SpriteRenderer>().DOFade(0f, 4f);
            g_noonDragon.transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);
            g_nightCloud.transform.GetComponent<MeshRenderer>().material.DOFade(1f, 4f);
            g_nightPlatform.transform.GetComponent<SpriteRenderer>().DOFade(1f, 4f);
            g_nightDragon.transform.GetComponent<SpriteRenderer>().material.DOFade(1f, 2f);
        }

        DO24Hours();

        //for background
        DOBackgroundScrolling();

    }

    #endregion

    #region Background Scrolling

    public void DOBackgroundScrolling()
    {
        #region Background

        //for sky background
        parallexbackgroundmanagerInstance.SetBackgroundOffsetVector(new Vector2(0f, -0.025f));
        parallexbackgroundmanagerInstance.componentContainer[0].GetComponent<MeshRenderer>().material.mainTextureOffset
        += parallexbackgroundmanagerInstance.GetBackgroundOffsetVector() * Time.deltaTime;

        #endregion

        #region Clouds

        //morning cloud
        parallexbackgroundmanagerInstance.SetBackgroundOffsetVector(new Vector2(0.04f, 0f));
        parallexbackgroundmanagerInstance.componentContainer[1].GetComponent<MeshRenderer>().material.mainTextureOffset
        += parallexbackgroundmanagerInstance.GetBackgroundOffsetVector() * Time.deltaTime;

        //noon cloud
        parallexbackgroundmanagerInstance.SetBackgroundOffsetVector(new Vector2(0.04f, 0f));
        parallexbackgroundmanagerInstance.componentContainer[2].GetComponent<MeshRenderer>().material.mainTextureOffset
        += parallexbackgroundmanagerInstance.GetBackgroundOffsetVector() * Time.deltaTime;

        //night cloud
        parallexbackgroundmanagerInstance.SetBackgroundOffsetVector(new Vector2(0.04f, 0f));
        parallexbackgroundmanagerInstance.componentContainer[3].GetComponent<MeshRenderer>().material.mainTextureOffset
        += parallexbackgroundmanagerInstance.GetBackgroundOffsetVector() * Time.deltaTime;

        #endregion

    }

    #endregion

    #region Time Cycling

    public void DO24Hours()
    {
        //one day cycle has been completed, reset it
        if (f_currentTime >= 24f)
        {
            f_currentTime = 0f;
            Debug.Log("cycle once");
        }
    }

    #endregion

    public void OnStartRun() //set to morning on START
    {

        //platform
        g_morningPlatform.transform.GetComponent<SpriteRenderer>().DOFade(1f, 0f);
        g_noonPlatform.transform.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
        g_nightPlatform.transform.GetComponent<SpriteRenderer>().DOFade(0f, 0f);

        //clouds
        g_morningCloud.transform.GetComponent<MeshRenderer>().material.DOFade(1f, 0f);
        g_noonCloud.transform.GetComponent<MeshRenderer>().material.DOFade(0f, 0f);
        g_nightCloud.transform.GetComponent<MeshRenderer>().material.DOFade(0f, 0f);

        //dragon
        g_morningDragon.transform.GetComponent<SpriteRenderer>().material.DOFade(1f, 0f);
        g_noonDragon.transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 0f);
        g_nightDragon.transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 0f);
    }

}
