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

        this.transform.position = new Vector3(playerPosition.position.x, 0, -1f);

        if (this.transform.position.x <= -9f)
        {
            //clamp the x-position
            this.transform.position = new Vector3(-9f, 0, -1f);
        }

        if (this.transform.position.x >= 9f)
        {
            //clamp the x-position
            this.transform.position = new Vector3(9f, 0, -1f);
        }

        #endregion
    }

    #endregion


}
