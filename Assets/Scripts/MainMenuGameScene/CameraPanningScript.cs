using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraPanningScript : MonoBehaviour
{
    //this script helps to move the camera based on the player's position
    //it should stop player's movement, pan the camera then continue with animations and movement

    #region Variables

    [Tooltip("Reference to the Camera in the Main Menu Scene")]
    public Camera currentCamera;

    [Tooltip("Reference to the Player")] [SerializeField]
    Transform playerPosition;

    [Tooltip("Vector3 Positions: To store camera pan position")]
    Vector3 v_centerCameraPosition, v_leftCameraPosition, v_rightCameraPosition;

    [Tooltip("Bool: To check which side of pan it is")]
    public bool b_pannedToLeft, b_pannedToRight, b_pannedToCenter;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        //we assign the values to the Vector3 position
        v_leftCameraPosition = new Vector3(-12.6f, 0f, -7f);
        v_rightCameraPosition = new Vector3(12.6f, 0f, -7f);
        v_centerCameraPosition = new Vector3(0f, 0f, -7f);

        //set booleans HERE
        b_pannedToLeft = false; 
        b_pannedToRight = false;
        b_pannedToCenter = true;
    }

    public void Update()
    {

        #region Camera Panning

        //we check the player's position to pan the camera HERE
        if (playerPosition.transform.localPosition.x < -8.99f && b_pannedToCenter == true)
        {
            PanCameraToLeft();
            b_pannedToLeft = true;
            b_pannedToRight = false;
            b_pannedToCenter = false;
        }

        if (playerPosition.transform.localPosition.x > 8.99f && b_pannedToCenter == true)
        {
            PanCameraToRight();
            b_pannedToLeft = false;
            b_pannedToRight = true;
            b_pannedToCenter = false;
        }

        if ((b_pannedToLeft == true && playerPosition.transform.localPosition.x > -3.699f) || (b_pannedToRight == true && playerPosition.transform.localPosition.x < 3.699f))
        {
            PanCameraToCenter();
            b_pannedToLeft = false;
            b_pannedToRight = false;
            b_pannedToCenter = true;
        }

        #endregion

    }

    #endregion

    #region Helper Functions

    public void PanCameraToLeft()
    {
        Tween currentTween = currentCamera.transform.DOMoveX(v_leftCameraPosition.x, 2.75f);
    }

    public void PanCameraToRight()
    {
        Tween currentTween = currentCamera.transform.DOMoveX(v_rightCameraPosition.x, 2.75f);
    }

    public void PanCameraToCenter()
    {
        Tween currentTween = currentCamera.transform.DOMoveX(v_centerCameraPosition.x, 1.375f);
    }

    #endregion





}
