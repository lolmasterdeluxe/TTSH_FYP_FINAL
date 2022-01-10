using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPS_ScrollingBackground : MonoBehaviour
{
    //this script handles the scrolling background

    #region Variables

    [Tooltip("Reference to the Parallex Background Manager")]
    ParallexBackgroundManager parallexbackgroundmanagerInstance;


    #endregion

    #region Unity Callbacks

    private void Start()
    {
        parallexbackgroundmanagerInstance = FindObjectOfType<ParallexBackgroundManager>();
    }


    private void Update()
    {
        if (SPS_UIManager.Instance.b_gameEnded == true)
            return;

        //ground
        parallexbackgroundmanagerInstance.SetBackgroundOffsetVector(new Vector2(0.2f, 0f));
        parallexbackgroundmanagerInstance.componentContainer[0].GetComponent<Renderer>().material.mainTextureOffset
        += parallexbackgroundmanagerInstance.GetBackgroundOffsetVector() * Time.deltaTime;

        //sky
        parallexbackgroundmanagerInstance.SetBackgroundOffsetVector(new Vector2(0.05f, 0f));
        parallexbackgroundmanagerInstance.componentContainer[1].GetComponent<Renderer>().material.mainTextureOffset
        += parallexbackgroundmanagerInstance.GetBackgroundOffsetVector() * Time.deltaTime;
    }

    #endregion

}
