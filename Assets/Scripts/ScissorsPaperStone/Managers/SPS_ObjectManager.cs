using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SPS_ObjectManager : MonoBehaviour
{
    //this script handles everything to do with OBJECTS
    //this includes enemies, obstacles and powerups

    #region Object Enumerations

    public enum WaveFormat 
    { WAVE_SINGLE_RANDOM, WAVE_MULTIPLE_RANDOM, WAVE_MULTIPLE_SCISSORS, 
        WAVE_MULTIPLE_PAPER, WAVE_MULTIPLE_STONE, WAVE_OBSTACLE, WAVE_POWERUP, WAVE_POWERUP_AND_OBSTACLE, 
        WAVE_TOTAL};
    public WaveFormat current_waveFormat;

    #endregion

    #region Variables

    //references to object scripts
    SPS_Enemy enemyInstance;

    [Tooltip("This stores the objects in each wave")]
    public List<GameObject> objectWaveList;

    //base prefabs for objects spawning
    GameObject g_objectInstance;
    public GameObject enemyPrefab, obstaclePrefab, powerupPrefab;

    //these pre-determined positions are to be used with DOTween
    public GameObject objectStartPosition, enemyEndPosition, obstacleEndPosition;

    //spawning speed variables
    [Tooltip("Uptime for each object spawn")]
    float f_objectspawnLifetime;
    [Tooltip("Multiplier for the speed of each object spawn")]
    float f_objectLifetimeMultiplier;
    [Tooltip("Object's travel speed")]
    [SerializeField]
    float f_objectTravelSpeed;
    [Tooltip("Object's travel speed multiplier")]
    [SerializeField]
    float f_objecttravelspeedMultiplier;

    //powerup counter vairables
    [Tooltip("Powerup Counter: ensures the player can get powerup at least ONCE per game session")]
    int i_powerupCount;

    //wave counter variables
    [Tooltip("Wave Counter: checks the in-game current wave")]
    int i_currentwaveCount;
    [Tooltip("Boolean: checks if a wave is completed")]
    public bool b_waveCompleted;

    #endregion

    #region Unity Callbacks

    private void Start()
    {

        enemyInstance = FindObjectOfType<SPS_Enemy>();

        //set these on start
        i_currentwaveCount = 0;
        i_powerupCount = 0;
        f_objectTravelSpeed = 5f;
        f_objecttravelspeedMultiplier = 1f;
    }

    private void Update()
    {
        //we increase the speed every 5 WAVES
        if (i_currentwaveCount % 5 == 0)
        {
            ReduceObjectSpawnTime();
        }

        f_objectspawnLifetime += Time.deltaTime;
        if (f_objectspawnLifetime >= (f_objectspawnLifetime * f_objectLifetimeMultiplier) && b_waveCompleted == false)
        {
            Debug.Log("please");
            //we check to see if the player has received any powerups recently
            if (i_powerupCount >= 3)
            {
                //we run it such that the next set of spawn HAS to be a powerup wave
                int randVal = Random.Range(0, 11);

                switch (randVal % 2)
                {
                    case 0: SpawnPowerupObject(); break;
                    case 1: SpawnPowerupAndObstacleObject(); break;
                }

                f_objectspawnLifetime = 0f;

            }
            else
            {
                //for the first 5 waves we only allow enemies to spawn
                if (i_currentwaveCount <= 5)
                {
                    current_waveFormat = (WaveFormat)Random.Range(1, (int)WaveFormat.WAVE_MULTIPLE_STONE);

                    switch (current_waveFormat)
                    {
                        case WaveFormat.WAVE_SINGLE_RANDOM:
                            SpawnSingleRandomEnemy();
                            break;

                        case WaveFormat.WAVE_MULTIPLE_RANDOM:
                            SpawnMultipleRandomEnemies();
                            break;

                        case WaveFormat.WAVE_MULTIPLE_SCISSORS:
                            SpawnMultipleRandomEnemies();
                            break;

                        case WaveFormat.WAVE_MULTIPLE_PAPER:
                            SpawnMultipleRandomEnemies();
                            break;

                        case WaveFormat.WAVE_MULTIPLE_STONE:
                            SpawnMultipleRandomEnemies();
                            break;
                    }
                }

                else
                {
                    //randomly decide on what kind of wave format it will be
                    current_waveFormat = (WaveFormat)Random.Range(1, (int)WaveFormat.WAVE_TOTAL);

                    switch (current_waveFormat)
                    {
                        case WaveFormat.WAVE_SINGLE_RANDOM:
                            SpawnSingleRandomEnemy();
                            break;

                        case WaveFormat.WAVE_MULTIPLE_RANDOM:
                            SpawnMultipleRandomEnemies();
                            break;

                        case WaveFormat.WAVE_MULTIPLE_SCISSORS:
                            SpawnMultipleRandomEnemies();
                            break;

                        case WaveFormat.WAVE_MULTIPLE_PAPER:
                            SpawnMultipleRandomEnemies();
                            break;

                        case WaveFormat.WAVE_MULTIPLE_STONE:
                            SpawnMultipleRandomEnemies();
                            break;

                        case WaveFormat.WAVE_OBSTACLE:
                            SpawnObstacleObject();
                            break;

                        case WaveFormat.WAVE_POWERUP:
                            SpawnPowerupObject();
                            break;

                        case WaveFormat.WAVE_POWERUP_AND_OBSTACLE:
                            SpawnPowerupAndObstacleObject();
                            break;

                    }
                }

                f_objectspawnLifetime = 0f;

            }
        }
    }

    #endregion

    #region Spawning Functions

    public void SpawnSingleRandomEnemy()
    {
        //instantiate an enemy HERE
        g_objectInstance = Instantiate(enemyPrefab,
         new Vector3(objectStartPosition.transform.position.x, 
         objectStartPosition.transform.position.y + 0.65f,
         objectStartPosition.transform.position.z), objectStartPosition.transform.rotation);
        g_objectInstance.transform.DOMoveX(enemyEndPosition.transform.position.x, f_objectTravelSpeed * f_objecttravelspeedMultiplier);

        //do the movement HERE
        g_objectInstance.GetComponent<Rigidbody>().DOMoveX(enemyEndPosition.transform.position.x, f_objectTravelSpeed * f_objecttravelspeedMultiplier);
        
        //add this object to the list of objects spawned
        objectWaveList.Add(g_objectInstance);

        //do other things HERE
        b_waveCompleted = true;
        i_powerupCount += 1;
        WaveIncrease();

    }

    public void SpawnMultipleRandomEnemies()
    {
        int waveSize = Random.Range(3, 6);

        for (int val = 0; val <= waveSize; val++)
        {
            g_objectInstance = Instantiate(enemyPrefab,
           new Vector3(objectStartPosition.transform.position.x + val * 4.5f, 
           objectStartPosition.transform.position.y + 0.65f, 
           objectStartPosition.transform.position.z), objectStartPosition.transform.rotation);

            //do the movement HERE
            g_objectInstance.transform.DOMoveX(enemyEndPosition.transform.position.x, (f_objectTravelSpeed * f_objectTravelSpeed));
            g_objectInstance.GetComponent<Rigidbody>().DOMoveX(enemyEndPosition.transform.position.x, (f_objectTravelSpeed * f_objectTravelSpeed));

            //add objects to the list of objects spawned
            objectWaveList.Add(g_objectInstance);
        }

        //do other things here
        b_waveCompleted = true;
        i_powerupCount += 1;
        WaveIncrease();

    }

    public void SpawnObstacleObject()
    {
        g_objectInstance = Instantiate(obstaclePrefab,
        objectStartPosition.transform.position, objectStartPosition.transform.rotation);

        //do the movement HERE
        g_objectInstance.transform.DOMoveX(obstacleEndPosition.transform.position.x, f_objectTravelSpeed * f_objecttravelspeedMultiplier);
        g_objectInstance.GetComponent<Rigidbody>().DOMoveX(obstacleEndPosition.transform.position.x, f_objectTravelSpeed * f_objecttravelspeedMultiplier);

        objectWaveList.Add(g_objectInstance);
        b_waveCompleted = true;

        i_powerupCount += 1;
        WaveIncrease();
    }

    public void SpawnPowerupObject()
    {
        g_objectInstance = Instantiate(powerupPrefab,
       objectStartPosition.transform.position, objectStartPosition.transform.rotation);

        //do the movement HERE
        g_objectInstance.transform.DOMoveX(obstacleEndPosition.transform.position.x, f_objectTravelSpeed * f_objecttravelspeedMultiplier);
        g_objectInstance.GetComponent<Rigidbody>().DOMoveX(obstacleEndPosition.transform.position.x, f_objectTravelSpeed * f_objecttravelspeedMultiplier);

        objectWaveList.Add(g_objectInstance);
        b_waveCompleted = true;

        i_powerupCount = 0;
        WaveIncrease();
    }

    public void SpawnPowerupAndObstacleObject()
    {
        //we create the obstacle first
        SpawnObstacleObject();

        //we now create an instance of the powerup ontop of the obstacle
        g_objectInstance = Instantiate(powerupPrefab,
         new Vector3(objectStartPosition.transform.position.x, objectStartPosition.transform.position.y + 2.9f,
         objectStartPosition.transform.position.z), objectStartPosition.transform.rotation);

        //do the movement HERE
        g_objectInstance.transform.DOMoveX(obstacleEndPosition.transform.position.x, f_objectTravelSpeed * f_objecttravelspeedMultiplier);
        g_objectInstance.GetComponent<Rigidbody>().DOMoveX(obstacleEndPosition.transform.position.x, f_objectTravelSpeed * f_objecttravelspeedMultiplier);

        objectWaveList.Add(g_objectInstance);
        b_waveCompleted = true;

        i_powerupCount = 0;
        WaveIncrease();

    }

    #endregion

    #region Progression Functions

    public void ReduceObjectSpawnTime()
    {
        f_objectLifetimeMultiplier -= 0.125f;

        //we limit the amount it can reduce to
        if (f_objectLifetimeMultiplier <= 0.5f)
        {
            f_objectLifetimeMultiplier = 0.5f;
        }
    }

    public void WaveIncrease()
    {
        i_currentwaveCount += 1;
    }

    #endregion


}
