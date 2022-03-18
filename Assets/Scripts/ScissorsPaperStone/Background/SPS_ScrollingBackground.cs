using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPS_ScrollingBackground : MonoBehaviour
{
    //this script handles the scrolling background

    #region Variables

    [Tooltip("Reference to the Parallex Background Manager")]
    [SerializeField] private ParallexBackgroundManager parallexbackgroundmanagerInstance;


    #endregion

    #region Unity Callbacks

    private void Update()
    {
        if (SPS_UIManager.Instance.b_gameEnded)
            return;

        //ground
        parallexbackgroundmanagerInstance.SetBackgroundOffsetVector(new Vector2(0.08f, 0f));
        parallexbackgroundmanagerInstance.componentContainer[0].GetComponent<Renderer>().material.mainTextureOffset
        += parallexbackgroundmanagerInstance.GetBackgroundOffsetVector() * Time.deltaTime;

        //ground 2
        parallexbackgroundmanagerInstance.SetBackgroundOffsetVector(new Vector2(0.08f, 0f));
        parallexbackgroundmanagerInstance.componentContainer[1].GetComponent<Renderer>().material.mainTextureOffset
        += parallexbackgroundmanagerInstance.GetBackgroundOffsetVector() * Time.deltaTime;

        //clouds
        parallexbackgroundmanagerInstance.SetBackgroundOffsetVector(new Vector2(0.02f, 0f));
        parallexbackgroundmanagerInstance.componentContainer[2].GetComponent<Renderer>().material.mainTextureOffset
        += parallexbackgroundmanagerInstance.GetBackgroundOffsetVector() * Time.deltaTime;

        //fog
        parallexbackgroundmanagerInstance.SetBackgroundOffsetVector(new Vector2(0.035f, 0f));
        parallexbackgroundmanagerInstance.componentContainer[3].GetComponent<Renderer>().material.mainTextureOffset
        += parallexbackgroundmanagerInstance.GetBackgroundOffsetVector() * Time.deltaTime;

        //building
        parallexbackgroundmanagerInstance.SetBackgroundOffsetVector(new Vector2(0.035f, 0f));
        parallexbackgroundmanagerInstance.componentContainer[4].GetComponent<Renderer>().material.mainTextureOffset
        += parallexbackgroundmanagerInstance.GetBackgroundOffsetVector() * Time.deltaTime;

        //playground
        parallexbackgroundmanagerInstance.SetBackgroundOffsetVector(new Vector2(0.035f, 0f));
        parallexbackgroundmanagerInstance.componentContainer[5].GetComponent<Renderer>().material.mainTextureOffset
        += parallexbackgroundmanagerInstance.GetBackgroundOffsetVector() * Time.deltaTime;

    }

    #endregion

}
