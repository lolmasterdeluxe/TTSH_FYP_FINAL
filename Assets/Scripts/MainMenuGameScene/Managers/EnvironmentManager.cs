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

    //containers for different objects in the main menu

    [Tooltip("Container for cloud GameObjects")]
    public List<GameObject> cloudGameObjectList;

    [Tooltip("Container for platform GameObjects")]
    public List<GameObject> platformGameObjectList;

    [Tooltip("Container for Dragon Slide GameObjects")]
    public List<GameObject> dragonslideGameObjectList;

    [Tooltip("Container for Horse GameObjects")]
    public List<GameObject> horseGameObjectList;

    [Tooltip("Container for See-Saw GameObjects")]
    public List<GameObject> seesawGameObjectList;

    [Tooltip("Container for Spinner GameObjects")]
    public List<GameObject> spinnerGameObjectList;

    [Tooltip("Container for Lamp GameObjects")]
    public List<GameObject> lampGameObjectList;

    [Tooltip("Container for Building Background GameObjects")]
    public List<GameObject> buildingGameObjectList;

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

        //for object fading
        DOObjectFading();

        //for time cycle
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
        parallexbackgroundmanagerInstance.SetBackgroundOffsetVector(new Vector2(0.01f, 0f));
        parallexbackgroundmanagerInstance.componentContainer[1].GetComponent<MeshRenderer>().material.mainTextureOffset
        += parallexbackgroundmanagerInstance.GetBackgroundOffsetVector() * Time.deltaTime;

        //noon cloud
        parallexbackgroundmanagerInstance.SetBackgroundOffsetVector(new Vector2(0.01f, 0f));
        parallexbackgroundmanagerInstance.componentContainer[2].GetComponent<MeshRenderer>().material.mainTextureOffset
        += parallexbackgroundmanagerInstance.GetBackgroundOffsetVector() * Time.deltaTime;

        //night cloud
        parallexbackgroundmanagerInstance.SetBackgroundOffsetVector(new Vector2(0.01f, 0f));
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

    public void DOObjectFading()
    {
        //run the cycles base on the time
        if (f_currentTime > 6.5f && f_currentTime <= 12.5f)
        {
            //clouds
            cloudGameObjectList[0].transform.GetComponent<MeshRenderer>().material.DOFade(1f, 4f);
            cloudGameObjectList[1].transform.GetComponent<MeshRenderer>().material.DOFade(0f, 4f);
            cloudGameObjectList[2].transform.GetComponent<MeshRenderer>().material.DOFade(0f, 4f);

            //platforms
            platformGameObjectList[0].transform.GetComponent<SpriteRenderer>().DOFade(1f, 4f);
            platformGameObjectList[1].transform.GetComponent<SpriteRenderer>().DOFade(0f, 4f);
            platformGameObjectList[2].transform.GetComponent<SpriteRenderer>().DOFade(0f, 4f);
            
            //dragon slide
            dragonslideGameObjectList[0].transform.GetComponent<SpriteRenderer>().DOFade(1f, 2f);
            dragonslideGameObjectList[1].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);
            dragonslideGameObjectList[2].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);

            //horse
            horseGameObjectList[0].transform.GetComponent<SpriteRenderer>().DOFade(1f, 2f);
            horseGameObjectList[1].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);
            horseGameObjectList[2].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);

            //see-saw
            seesawGameObjectList[0].transform.GetComponent<SpriteRenderer>().DOFade(1f, 2f);
            seesawGameObjectList[1].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);
            seesawGameObjectList[2].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);

            //spinner
            spinnerGameObjectList[0].transform.GetComponent<SpriteRenderer>().DOFade(1f, 2f);
            spinnerGameObjectList[1].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);
            spinnerGameObjectList[2].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);

            //lamp posts
            lampGameObjectList[0].transform.GetComponent<SpriteRenderer>().material.DOFade(1f, 2f);
            lampGameObjectList[1].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);
            lampGameObjectList[2].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);
            lampGameObjectList[3].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);

            lampGameObjectList[4].transform.GetComponent<SpriteRenderer>().material.DOFade(1f, 2f);
            lampGameObjectList[5].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);
            lampGameObjectList[6].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);
            lampGameObjectList[7].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);

            lampGameObjectList[8].transform.GetComponent<SpriteRenderer>().material.DOFade(1f, 2f);
            lampGameObjectList[9].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);
            lampGameObjectList[10].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);
            lampGameObjectList[11].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);

            //building
            buildingGameObjectList[0].transform.GetComponent<MeshRenderer>().material.DOFade(1f, 4f);
            buildingGameObjectList[1].transform.GetComponent<MeshRenderer>().material.DOFade(0f, 4f);
            buildingGameObjectList[2].transform.GetComponent<MeshRenderer>().material.DOFade(0f, 4f);
        }

        if (f_currentTime > 12.5f && f_currentTime <= 19f)
        {
            //clouds
            cloudGameObjectList[0].transform.GetComponent<MeshRenderer>().material.DOFade(0f, 4f);
            cloudGameObjectList[1].transform.GetComponent<MeshRenderer>().material.DOFade(1f, 4f);
            cloudGameObjectList[2].transform.GetComponent<MeshRenderer>().material.DOFade(0f, 4f);
            
            //platforms
            platformGameObjectList[0].transform.GetComponent<SpriteRenderer>().DOFade(0f, 4f);
            platformGameObjectList[1].transform.GetComponent<SpriteRenderer>().DOFade(1f, 4f);
            platformGameObjectList[2].transform.GetComponent<SpriteRenderer>().DOFade(0f, 4f);
            
            //dragon slide
            dragonslideGameObjectList[0].transform.GetComponent<SpriteRenderer>().DOFade(0f, 2f);
            dragonslideGameObjectList[1].transform.GetComponent<SpriteRenderer>().material.DOFade(1f, 2f);
            dragonslideGameObjectList[2].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);

            //horse
            horseGameObjectList[0].transform.GetComponent<SpriteRenderer>().DOFade(0f, 2f);
            horseGameObjectList[1].transform.GetComponent<SpriteRenderer>().material.DOFade(1f, 2f);
            horseGameObjectList[2].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);

            //see-saw
            seesawGameObjectList[0].transform.GetComponent<SpriteRenderer>().DOFade(0f, 2f);
            seesawGameObjectList[1].transform.GetComponent<SpriteRenderer>().material.DOFade(1f, 2f);
            seesawGameObjectList[2].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);

            //spinner
            spinnerGameObjectList[0].transform.GetComponent<SpriteRenderer>().DOFade(0f, 2f);
            spinnerGameObjectList[1].transform.GetComponent<SpriteRenderer>().material.DOFade(1f, 2f);
            spinnerGameObjectList[2].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);

            //lamp posts
            lampGameObjectList[0].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);
            lampGameObjectList[1].transform.GetComponent<SpriteRenderer>().material.DOFade(1f, 2f);
            lampGameObjectList[2].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);
            lampGameObjectList[3].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);

            lampGameObjectList[4].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);
            lampGameObjectList[5].transform.GetComponent<SpriteRenderer>().material.DOFade(1f, 2f);
            lampGameObjectList[6].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);
            lampGameObjectList[7].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);

            lampGameObjectList[8].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);
            lampGameObjectList[9].transform.GetComponent<SpriteRenderer>().material.DOFade(1f, 2f);
            lampGameObjectList[10].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);
            lampGameObjectList[11].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);

            //building
            buildingGameObjectList[0].transform.GetComponent<MeshRenderer>().material.DOFade(0f, 4f);
            buildingGameObjectList[1].transform.GetComponent<MeshRenderer>().material.DOFade(1f, 4f);
            buildingGameObjectList[2].transform.GetComponent<MeshRenderer>().material.DOFade(0f, 4f);
        }

        if (f_currentTime > 19f && f_currentTime <= 24f)
        {
            //clouds
            cloudGameObjectList[0].transform.GetComponent<MeshRenderer>().material.DOFade(0f, 4f);
            cloudGameObjectList[1].transform.GetComponent<MeshRenderer>().material.DOFade(0f, 4f);
            cloudGameObjectList[2].transform.GetComponent<MeshRenderer>().material.DOFade(1f, 4f);

            //platforms
            platformGameObjectList[0].transform.GetComponent<SpriteRenderer>().DOFade(0f, 4f);
            platformGameObjectList[1].transform.GetComponent<SpriteRenderer>().DOFade(0f, 4f);
            platformGameObjectList[2].transform.GetComponent<SpriteRenderer>().DOFade(1f, 4f);
            
            //dragon slide
            dragonslideGameObjectList[0].transform.GetComponent<SpriteRenderer>().DOFade(0f, 2f);
            dragonslideGameObjectList[1].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);
            dragonslideGameObjectList[2].transform.GetComponent<SpriteRenderer>().material.DOFade(1f, 2f);

            //horse
            horseGameObjectList[0].transform.GetComponent<SpriteRenderer>().DOFade(0f, 2f);
            horseGameObjectList[1].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);
            horseGameObjectList[2].transform.GetComponent<SpriteRenderer>().material.DOFade(1f, 2f);

            //see-saw
            seesawGameObjectList[0].transform.GetComponent<SpriteRenderer>().DOFade(0f, 2f);
            seesawGameObjectList[1].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);
            seesawGameObjectList[2].transform.GetComponent<SpriteRenderer>().material.DOFade(1f, 2f);

            //spinner
            spinnerGameObjectList[0].transform.GetComponent<SpriteRenderer>().DOFade(0f, 2f);
            spinnerGameObjectList[1].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);
            spinnerGameObjectList[2].transform.GetComponent<SpriteRenderer>().material.DOFade(1f, 2f);

            //lamp posts
            lampGameObjectList[0].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);
            lampGameObjectList[1].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);
            lampGameObjectList[2].transform.GetComponent<SpriteRenderer>().material.DOFade(1f, 2f);
            lampGameObjectList[3].transform.GetComponent<SpriteRenderer>().material.DOFade(1f, 2f);

            lampGameObjectList[4].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);
            lampGameObjectList[5].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);
            lampGameObjectList[6].transform.GetComponent<SpriteRenderer>().material.DOFade(1f, 2f);
            lampGameObjectList[7].transform.GetComponent<SpriteRenderer>().material.DOFade(1f, 2f);

            lampGameObjectList[8].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);
            lampGameObjectList[9].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 2f);
            lampGameObjectList[10].transform.GetComponent<SpriteRenderer>().material.DOFade(1f, 2f);
            lampGameObjectList[11].transform.GetComponent<SpriteRenderer>().material.DOFade(1f, 2f);
            
            //building
            buildingGameObjectList[0].transform.GetComponent<MeshRenderer>().material.DOFade(0f, 4f);
            buildingGameObjectList[1].transform.GetComponent<MeshRenderer>().material.DOFade(0f, 4f);
            buildingGameObjectList[2].transform.GetComponent<MeshRenderer>().material.DOFade(1f, 4f);
        }
    }

    #endregion

    public void OnStartRun() //set to morning on START
    {
        //clouds
        cloudGameObjectList[0].transform.GetComponent<MeshRenderer>().material.DOFade(1f, 0f);
        cloudGameObjectList[1].transform.GetComponent<MeshRenderer>().material.DOFade(0f, 0f);
        cloudGameObjectList[2].transform.GetComponent<MeshRenderer>().material.DOFade(0f, 0f);

        //platform
        platformGameObjectList[0].transform.GetComponent<SpriteRenderer>().DOFade(1f, 0f);
        platformGameObjectList[1].transform.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
        platformGameObjectList[2].transform.GetComponent<SpriteRenderer>().DOFade(0f, 0f);

        //dragon
        dragonslideGameObjectList[0].transform.GetComponent<SpriteRenderer>().material.DOFade(1f, 0f);
        dragonslideGameObjectList[1].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 0f);
        dragonslideGameObjectList[2].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 0f);

        //horse
        horseGameObjectList[0].transform.GetComponent<SpriteRenderer>().material.DOFade(1f, 0f);
        horseGameObjectList[1].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 0f);
        horseGameObjectList[2].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 0f);

        //see-saw
        seesawGameObjectList[0].transform.GetComponent<SpriteRenderer>().material.DOFade(1f, 0f);
        seesawGameObjectList[1].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 0f);
        seesawGameObjectList[2].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 0f);

        //spinner
        spinnerGameObjectList[0].transform.GetComponent<SpriteRenderer>().material.DOFade(1f, 0f);
        spinnerGameObjectList[1].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 0f);
        spinnerGameObjectList[2].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 0f);

        //lamp posts
        lampGameObjectList[0].transform.GetComponent<SpriteRenderer>().material.DOFade(1f, 0f);
        lampGameObjectList[1].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 0f);
        lampGameObjectList[2].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 0f);
        lampGameObjectList[3].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 0f);
        lampGameObjectList[4].transform.GetComponent<SpriteRenderer>().material.DOFade(1f, 0f);
        lampGameObjectList[5].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 0f);
        lampGameObjectList[6].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 0f);
        lampGameObjectList[7].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 0f);
        lampGameObjectList[8].transform.GetComponent<SpriteRenderer>().material.DOFade(1f, 0f);
        lampGameObjectList[9].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 0f);
        lampGameObjectList[10].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 0f);
        lampGameObjectList[11].transform.GetComponent<SpriteRenderer>().material.DOFade(0f, 0f);

        //building
        buildingGameObjectList[0].transform.GetComponent<MeshRenderer>().material.DOFade(1f, 0f);
        buildingGameObjectList[1].transform.GetComponent<MeshRenderer>().material.DOFade(0f, 0f);
        buildingGameObjectList[2].transform.GetComponent<MeshRenderer>().material.DOFade(0f, 0f);

    }

}
