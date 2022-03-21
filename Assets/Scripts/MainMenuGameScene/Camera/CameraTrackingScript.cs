using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrackingScript : MonoBehaviour
{
    //this camera tracks the position of the player in Main Menu Scene

    #region Variables

    [Tooltip("Reference to the Main Character's transform")] [SerializeField]
    Transform playerPosition;

    #endregion

    #region Unity Callbacks

    private void Update()
    {
        #region Player Movement

        transform.position = new Vector3(playerPosition.position.x, 0f, -1f);

        if (transform.position.x <= -10f)
        {
            //clamp the x-position
            transform.position = new Vector3(-10f, 0f, -1f);
        }

        if (transform.position.x >= 9.95f)
        {
            //clamp the x-position
            transform.position = new Vector3(9.95f, 0f, -1f);
        }

        #endregion
    }

    #endregion


}
