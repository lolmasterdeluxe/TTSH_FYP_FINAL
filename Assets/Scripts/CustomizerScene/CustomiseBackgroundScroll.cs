using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomiseBackgroundScroll : MonoBehaviour
{
    #region Variables

    [Tooltip("Reference to Parallex Background Manager Script")]
    [SerializeField] private ParallexBackgroundManager parallexbackgroundmanagerInstance;

    #endregion

    #region Unity Callbacks

    private void Update()
    {
        parallexbackgroundmanagerInstance.SetBackgroundOffsetVector(new Vector2(0.2f, -0.05f));
        parallexbackgroundmanagerInstance.componentContainer[0].GetComponent<Renderer>().material.mainTextureOffset
        += parallexbackgroundmanagerInstance.GetBackgroundOffsetVector() * Time.deltaTime;
    }



    #endregion
}
