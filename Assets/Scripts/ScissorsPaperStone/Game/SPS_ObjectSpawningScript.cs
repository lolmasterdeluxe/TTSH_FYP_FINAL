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

    public float averageTime;

    [Tooltip("Uptime before each object spawn: based off minTime and maxTime")]
    public float objectspawnUptime;
    
    [Tooltip("Minimum and Maximum time offset: used for random object pattern spawning")]
    public float minTime, maxTime;


    #endregion

    #region Unity Callbacks

    private void Start()
    {
        
    }

    private void Update()
    {
        objectspawnUptime += Time.deltaTime;
        if (objectspawnUptime >= Random.Range(minTime, maxTime))
        {
            averageTime = Random.Range(minTime, maxTime);
            int randVal = Random.Range(0, 11);
            switch (randVal % 2)
            {
                case 0:
                    objectInstance = Instantiate(enemyPrefab,
                        objectstartPosition.position, objectstartPosition.rotation);
                    objectInstance.transform.DOMoveX(enemyEndPosition.transform.position.x, averageTime);
                    objectInstance.GetComponent<Rigidbody>().DOMoveX(enemyEndPosition.transform.position.x, averageTime);
                    break;
                case 1:
                    objectInstance = Instantiate(obstaclePrefab,
                        objectstartPosition.position, objectstartPosition.rotation);
                    objectInstance.transform.DOMoveX(obstacleEndPosition.transform.position.x, averageTime);
                    objectInstance.GetComponent<Rigidbody>().DOMoveX(obstacleEndPosition.transform.position.x, averageTime);
                    break;

            }
            objectspawnUptime = 0f;
        }
    }

    #endregion


}
