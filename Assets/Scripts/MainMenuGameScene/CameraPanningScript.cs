using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanningScript : MonoBehaviour
{
    //this script helps to move the camera based on the player's position
    //it should stop player's movement, pan the camera then continue with animations and movement

    #region Variables

    public Camera currentCamera;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
            PanCameraToLeft();
        if (Input.GetKeyDown(KeyCode.U))
            PanCameraToRight();
    }


    #endregion


    #region Helper Functions
    
    public void PanCameraToLeft()
    {
        currentCamera.transform.position = new Vector3(-12.6f, 0f, -7f);
    }

    public void PanCameraToRight()
    {
        currentCamera.transform.position = new Vector3(12.6f, 0f, -7f);
    }

    #endregion





}
