using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    #region Variables

    [Tooltip("Reference to Parallex Background Manager Script")]
    ParallexBackgroundManager parallexbackgroundmanagerInstance;

    [Tooltip("Reference to Object Manager Script")]
    SPS_UIManager uimanagerInstance;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        //get reference to other scripts HERE
        parallexbackgroundmanagerInstance = FindObjectOfType<ParallexBackgroundManager>();
        uimanagerInstance = FindObjectOfType<SPS_UIManager>();

    }

    private void Update()
    {
        if (uimanagerInstance.b_gameEnded == true)
            return;

        parallexbackgroundmanagerInstance.SetBackgroundOffsetVector(new Vector2(0.2f, 0f));
        parallexbackgroundmanagerInstance.componentContainer[0].GetComponent<Renderer>().material.mainTextureOffset
        += parallexbackgroundmanagerInstance.GetBackgroundOffsetVector() * Time.deltaTime;

        parallexbackgroundmanagerInstance.SetBackgroundOffsetVector(new Vector2(0.05f, 0f));
        parallexbackgroundmanagerInstance.componentContainer[1].GetComponent<Renderer>().material.mainTextureOffset
        += parallexbackgroundmanagerInstance.GetBackgroundOffsetVector() * Time.deltaTime;

    }


    #endregion

}
