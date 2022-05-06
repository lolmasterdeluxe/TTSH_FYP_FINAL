using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPS_ScrollingBackground : MonoBehaviour
{
    //this script handles the scrolling background

    #region Variables

    [Tooltip("Reference to the Parallex Background Manager")]
    [SerializeField] 
    private ParallexBackgroundManager parallexbackgroundmanagerInstance;
    [SerializeField]
    private SPS_ObjectManager ObjectManager;
    [SerializeField]
    private float groundSpeed, cloudSpeed, fogSpeed, buildingSpeed, playgroundSpeed;

    #endregion

    #region Unity Callbacks

    private void Update()
    {
        if (SPS_UIManager.Instance.b_gameEnded)
            return;

        //ground
        parallexbackgroundmanagerInstance.SetBackgroundOffsetVector(new Vector2(groundSpeed, 0f));
        parallexbackgroundmanagerInstance.componentContainer[0].GetComponent<Renderer>().material.mainTextureOffset
        += parallexbackgroundmanagerInstance.GetBackgroundOffsetVector() * ObjectManager.f_objectTravelspeedMultiplier * Time.deltaTime ;

        //ground 2
        parallexbackgroundmanagerInstance.SetBackgroundOffsetVector(new Vector2(-groundSpeed, 0f));
        parallexbackgroundmanagerInstance.componentContainer[1].GetComponent<Renderer>().material.mainTextureOffset
        += parallexbackgroundmanagerInstance.GetBackgroundOffsetVector() * ObjectManager.f_objectTravelspeedMultiplier * Time.deltaTime;

        //ground 3
        parallexbackgroundmanagerInstance.SetBackgroundOffsetVector(new Vector2(groundSpeed, 0f));
        parallexbackgroundmanagerInstance.componentContainer[2].GetComponent<Renderer>().material.mainTextureOffset
        += parallexbackgroundmanagerInstance.GetBackgroundOffsetVector() * ObjectManager.f_objectTravelspeedMultiplier * Time.deltaTime;

        //ground 4
        parallexbackgroundmanagerInstance.SetBackgroundOffsetVector(new Vector2(-groundSpeed, 0f));
        parallexbackgroundmanagerInstance.componentContainer[3].GetComponent<Renderer>().material.mainTextureOffset
        += parallexbackgroundmanagerInstance.GetBackgroundOffsetVector() * ObjectManager.f_objectTravelspeedMultiplier * Time.deltaTime;

        //clouds
        parallexbackgroundmanagerInstance.SetBackgroundOffsetVector(new Vector2(cloudSpeed, 0f));
        parallexbackgroundmanagerInstance.componentContainer[4].GetComponent<Renderer>().material.mainTextureOffset
        += parallexbackgroundmanagerInstance.GetBackgroundOffsetVector() * ObjectManager.f_objectTravelspeedMultiplier * Time.deltaTime;

        //fog
        parallexbackgroundmanagerInstance.SetBackgroundOffsetVector(new Vector2(fogSpeed, 0f));
        parallexbackgroundmanagerInstance.componentContainer[5].GetComponent<Renderer>().material.mainTextureOffset
        += parallexbackgroundmanagerInstance.GetBackgroundOffsetVector() * ObjectManager.f_objectTravelspeedMultiplier * Time.deltaTime;

        //building
        parallexbackgroundmanagerInstance.SetBackgroundOffsetVector(new Vector2(buildingSpeed, 0f));
        parallexbackgroundmanagerInstance.componentContainer[6].GetComponent<Renderer>().material.mainTextureOffset
        += parallexbackgroundmanagerInstance.GetBackgroundOffsetVector() * ObjectManager.f_objectTravelspeedMultiplier * Time.deltaTime;

        //playground
        parallexbackgroundmanagerInstance.SetBackgroundOffsetVector(new Vector2(playgroundSpeed, 0f));
        parallexbackgroundmanagerInstance.componentContainer[7].GetComponent<Renderer>().material.mainTextureOffset
        += parallexbackgroundmanagerInstance.GetBackgroundOffsetVector() * ObjectManager.f_objectTravelspeedMultiplier * Time.deltaTime;

    }

    #endregion

}
