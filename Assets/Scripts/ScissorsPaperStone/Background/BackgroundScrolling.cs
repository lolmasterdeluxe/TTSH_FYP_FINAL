using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    #region Variables

    ParallexBackgroundManager managerInstance;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        managerInstance = FindObjectOfType<ParallexBackgroundManager>();
    }

    private void Update()
    {
        managerInstance.SetBackgroundOffsetVector(new Vector2(0.4f, 0f));
        managerInstance.componentContainer[0].GetComponent<Renderer>().material.mainTextureOffset
        += managerInstance.GetBackgroundOffsetVector() * Time.deltaTime;

        managerInstance.SetBackgroundOffsetVector(new Vector2(0.1f, 0f));
        managerInstance.componentContainer[1].GetComponent<Renderer>().material.mainTextureOffset
        += managerInstance.GetBackgroundOffsetVector() * Time.deltaTime;

    }


    #endregion

}
