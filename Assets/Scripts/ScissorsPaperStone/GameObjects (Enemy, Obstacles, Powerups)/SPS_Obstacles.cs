using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPS_Obstacles : MonoBehaviour
{
    //this script handles all things involved with obstacles

    #region Enumerations
    public enum ObstacleChoice
    {   
        OBS_LOG, OBS_NONE
    };

    #endregion

    #region Variables

    public ObstacleChoice obstacle_choice;
    public Material log;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        obstacle_choice = ObstacleChoice.OBS_NONE;
        SetObstacleMaterial();
    }

    #endregion

    #region Functions

    public void SetObstacleMaterial()
    {
        obstacle_choice = ObstacleChoice.OBS_LOG;
        GetComponent<Renderer>().material = log;
    }

    #endregion
}
