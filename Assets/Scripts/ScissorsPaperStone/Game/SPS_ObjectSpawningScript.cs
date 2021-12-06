using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using DG.Tweening;

public class SPS_ObjectSpawningScript : MonoBehaviour
{
    //this handles object spawning in the scene at runtime:
    //this includes enemies and obstacles that the player will interact with

    #region Variables

    //base prefab: material will be slapped on accordingly
    private GameObject objectInstance;
    public GameObject enemyPrefab, obstaclePrefab;

    //these are pre-determined transform areas to be used with DOTween
    public Transform objectstartPosition;
    public Transform enemyEndPosition;
    public Transform obstacleEndPosition;

    //spawning speed variables

    [Tooltip("Uptime before each object spawn: based off minTime and maxTime")]
    public float objectspawnUptime;

    [Tooltip("This makes the uptime smaller as time progresses")]
    public float objectspawnUptimeMultipier;

    [Tooltip("Boolean flag to check if the wave has been completed")]
    public bool waveCompleted;

    //object speed variables

    [Tooltip("Object travel speed")]
    public float objectSpeed;

    [Tooltip("Object travel speed multiplier")]
    public float objectSpeedMultiplier;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
    }

    private void Update()
    {
        objectspawnUptime += Time.deltaTime;
        if (objectspawnUptime >= (objectspawnUptime * objectspawnUptimeMultipier) && waveCompleted == false)
        {
            float averageTime = Random.Range(4f, 6f);
            int randVal = Random.Range(0, 11);
            switch (randVal % 2)
            {
                case 0:
                    SpawnObject();
                    break;
                case 1:
                    SpawnObject();
                    break;

            }
            objectspawnUptime = 0f;
        }
    }

#endregion

#region Functions
    
    //these functions spawn in enemies, obstacles and powerups

    public void SpawnObject()
    {
        //we now roll randomly to determine what kind of enemy wave it is

        int randomVal = Random.Range(0, 1);

        switch (randomVal)
        {
            case 0:
                objectInstance = Instantiate(enemyPrefab,
                objectstartPosition.position, objectstartPosition.rotation);
                objectInstance.transform.DOMoveX(enemyEndPosition.transform.position.x, objectSpeed * objectSpeedMultiplier);
                objectInstance.GetComponent<Rigidbody>().DOMoveX(enemyEndPosition.transform.position.x, objectSpeed * objectSpeedMultiplier);
                waveCompleted = true;
                break;

            case 1:

                for (int i = 0; i < 4; i++)
                {
                    objectInstance = Instantiate(enemyPrefab,
                    objectstartPosition.position, objectstartPosition.rotation);
                    objectInstance.transform.DOMoveX(enemyEndPosition.transform.position.x, objectSpeed * objectSpeedMultiplier);
                    objectInstance.GetComponent<Rigidbody>().DOMoveX(enemyEndPosition.transform.position.x, objectSpeed * objectSpeedMultiplier);
                    waveCompleted = true;
                }
                break;

                //objectInstance = Instantiate(obstaclePrefab,
                //objectstartPosition.position, objectstartPosition.rotation);
                //objectInstance.transform.DOMoveX(obstacleEndPosition.transform.position.x, averageTime);
                //objectInstance.GetComponent<Rigidbody>().DOMoveX(obstacleEndPosition.transform.position.x, averageTime);


        }







    }

    #endregion
}
