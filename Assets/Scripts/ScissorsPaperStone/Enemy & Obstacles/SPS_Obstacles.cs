using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPS_Obstacles : MonoBehaviour
{
    //this script handles all things involved with obstacles

    public enum ObstacleChoice
    { 
        OBS_MOUNTAIN, OBS_LOG, OBS_NONE
    };

    #region Variables

    public ObstacleChoice obstacle_choice;
    public Material mountain, log;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        obstacle_choice = ObstacleChoice.OBS_NONE;
        RandomizeObstacle();
    }

    #endregion

    #region Functions

    public void RandomizeObstacle()
    {
        int randVal = Random.Range(0, 2);
        switch (randVal)
        {
            case 0:
                obstacle_choice = ObstacleChoice.OBS_MOUNTAIN;
                GetComponent<Renderer>().material = mountain;
                break;
            case 1:
                obstacle_choice = ObstacleChoice.OBS_LOG;
                GetComponent<Renderer>().material = log;
                break;
        }

    }

    #endregion
}
