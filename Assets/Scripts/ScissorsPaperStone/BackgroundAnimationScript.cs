using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAnimationScript : MonoBehaviour
{
    //this script handles the movement of background animation

    #region Variables

    //reference the parallex background manager HERE
    ParallexBackgroundManager managerInstance;

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
    }

    private void Start()
    {
        managerInstance = FindObjectOfType<ParallexBackgroundManager>();
    }

    private void Update()
    {


        //material.mainTextureOffset += imgOffset * Time.deltaTime;
        //managerInstance.materialList[managerInstance.GetMaterialNum()].mainTextureOffset 
        //    += managerInstance.GetImageOffsetVector() * Time.deltaTime;

    }
    #endregion

}
