using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SPS_ButtonHover : MonoBehaviour
{
    //this script handles the button scaling

    #region Unity Callbacks

    #endregion

    #region Functions

    public void ButtonPressEffect()
    {
        transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
    }

    public void ResetButtonSize()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    #endregion

}
