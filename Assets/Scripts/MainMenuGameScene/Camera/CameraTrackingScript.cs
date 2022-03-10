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

        this.transform.position = new Vector3(playerPosition.position.x, -0.45f, -1f);

        if (this.transform.position.x <= -15.95f)
        {
            //clamp the x-position
            this.transform.position = new Vector3(-15.95f, -0.45f, -1f);
        }

        if (this.transform.position.x >= 9.95f)
        {
            //clamp the x-position
            this.transform.position = new Vector3(9.95f, -0.45f, -1f);
        }

        #endregion
    }

    #endregion


}
