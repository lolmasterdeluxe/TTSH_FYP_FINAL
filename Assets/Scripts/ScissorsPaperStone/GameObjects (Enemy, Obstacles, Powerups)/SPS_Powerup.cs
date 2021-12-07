using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPS_Powerup : MonoBehaviour
{
    //this script handles all things involved with powerups

    #region Enumerations
    public enum PowerupChoice
    {
        POWERUP_ONE, POWERUP_NONE
    };

    #endregion

    #region Variables

    public PowerupChoice powerup_choice;
    public Material powerup_one;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        powerup_choice = PowerupChoice.POWERUP_NONE;
        SetPowerupMaterial();
    }

    #endregion

    #region Functions

    public void SetPowerupMaterial()
    {
        powerup_choice = PowerupChoice.POWERUP_ONE;
        GetComponent<Renderer>().material = powerup_one;
    }


    #endregion
}
