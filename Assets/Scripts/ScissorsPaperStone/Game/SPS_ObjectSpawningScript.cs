using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using DG.Tweening;

public class SPS_ObjectSpawningScript : MonoBehaviour
{
    //this handles object spawning in the scene at runtime:
    //this includes enemies and obstacles that the player will interact with

    #region Enumerations

    //this enumeration determines the type of enemies in a particular wave

    public enum EnemyWaveType
    { 
        ENEMY_SINGLE_RANDOM, ENEMY_WAVE_RANDOM, 
        ENEMY_WAVE_SCISSORS, ENEMY_WAVE_PAPER, 
        ENEMY_WAVE_STONE
    };

    public EnemyWaveType enemywaveType;

    #endregion


    #region Variables

    //this list keeps track of objects being spawned
    public List<GameObject> objectwaveList;

    //base prefab: material will be slapped on accordingly
    private GameObject objectInstance;
    public GameObject enemyPrefab, obstaclePrefab, powerupPrefab;

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
            //spawn either a enemy, obstacle or powerup

            int randVal = Random.Range(0, 10);

            switch (randVal)
            {
                case (0 | 1 | 2 | 3 | 4 | 5)://enemy
                    SpawnEnemyObject();
                    break;

                case (6 | 7 | 8): //obstacle
                    SpawnObstacleObject();
                    break;

                case (9 | 10): //powerup
                    SpawnPowerupObject();
                    break;
            }
            objectspawnUptime = 0f;
        }

    }

#endregion

    #region Functions
    
    //these functions spawn in enemies, obstacles and powerups

    public void SpawnEnemyObject()
    {
        //we now roll randomly to determine what kind of enemy wave it is

        int randomVal = Random.Range(0, 3);

        switch (randomVal)
        {
            case 0: //random single
                
                SpawnEnemy();

                break;

            case 1: //random wave

                SpawnEnemyWave();

                break;

            case 2: //uniform wave

                int randomVal2 = Random.Range(0, 3);

                switch (randomVal2)
                {
                    case 0:
                        enemywaveType = EnemyWaveType.ENEMY_WAVE_SCISSORS;
                        break;
                    case 1:
                        enemywaveType = EnemyWaveType.ENEMY_WAVE_PAPER;
                        break;
                    case 2:
                        enemywaveType = EnemyWaveType.ENEMY_WAVE_STONE;
                        break;
                }

                SpawnEnemyWave();

                break;
        }
    }

    public void SpawnEnemy()
    {
        objectInstance = Instantiate(enemyPrefab,
        new Vector3(objectstartPosition.position.x, objectstartPosition.position.y + 0.65f, objectstartPosition.position.z), objectstartPosition.rotation);
        objectInstance.transform.DOMoveX(enemyEndPosition.transform.position.x, objectSpeed * objectSpeedMultiplier);
        objectInstance.GetComponent<Rigidbody>().DOMoveX(enemyEndPosition.transform.position.x, objectSpeed * objectSpeedMultiplier);
        objectwaveList.Add(objectInstance);
        waveCompleted = true;
        enemywaveType = EnemyWaveType.ENEMY_SINGLE_RANDOM;
    }

    public void SpawnEnemyWave()
    {
        //generate a set of enemies equally spaced out

        for (int val = 1; val < 5; val++)
        {
            objectInstance = Instantiate(enemyPrefab,
            new Vector3(objectstartPosition.position.x + val * 4, objectstartPosition.position.y + 0.65f, objectstartPosition.position.z), objectstartPosition.rotation);
            objectInstance.transform.DOMoveX(obstacleEndPosition.transform.position.x, (objectSpeed * objectSpeedMultiplier) + val);
            objectInstance.GetComponent<Rigidbody>().DOMoveX(obstacleEndPosition.transform.position.x, (objectSpeed * objectSpeedMultiplier) + val);
            objectwaveList.Add(objectInstance);
            waveCompleted = true;
        }
    }

    public void SpawnObstacleObject()
    {
        //we now randomly roll to determine the type of obstacle to be spawned

        objectInstance = Instantiate(obstaclePrefab,
        objectstartPosition.position, objectstartPosition.rotation);
        objectInstance.transform.DOMoveX(obstacleEndPosition.transform.position.x, objectSpeed * objectSpeedMultiplier);
        objectInstance.GetComponent<Rigidbody>().DOMoveX(obstacleEndPosition.transform.position.x, objectSpeed * objectSpeedMultiplier);
        objectwaveList.Add(objectInstance);
        waveCompleted = true;
    }

    public void SpawnPowerupObject()
    {
        //we now randomly determine the position of the powerup

        int ranVal = Random.Range(0, 11);

        switch (ranVal % 2)
        {
            case 0: //solo powerup
                objectInstance = Instantiate(powerupPrefab,
                objectstartPosition.position, objectstartPosition.rotation);
                objectInstance.transform.DOMoveX(obstacleEndPosition.transform.position.x, objectSpeed * objectSpeedMultiplier);
                objectInstance.GetComponent<Rigidbody>().DOMoveX(obstacleEndPosition.transform.position.x, objectSpeed * objectSpeedMultiplier);
                objectwaveList.Add(objectInstance);
                waveCompleted = true;
                break;

            case 1: //powerup on top of obstacle

                //we create the obstacle first
                SpawnObstacleObject();

                //we now create an instance of the powerup ontop of the obstacle
                objectInstance = Instantiate(powerupPrefab,
                 new Vector3(objectstartPosition.position.x, objectstartPosition.position.y + 2.9f, 
                 objectstartPosition.position.z), objectstartPosition.rotation);


                //do the movement HERE
                objectInstance.transform.DOMoveX(obstacleEndPosition.transform.position.x, objectSpeed * objectSpeedMultiplier);
                objectInstance.GetComponent<Rigidbody>().DOMoveX(obstacleEndPosition.transform.position.x, objectSpeed * objectSpeedMultiplier);

                objectwaveList.Add(objectInstance);
                waveCompleted = true;
                break;      
        }
    }



    #endregion
}
