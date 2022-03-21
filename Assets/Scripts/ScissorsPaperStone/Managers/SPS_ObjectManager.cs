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
        WAVE_MULTIPLE_PAPER, WAVE_MULTIPLE_STONE, WAVE_OBSTACLE, WAVE_POWERUP, 
        WAVE_TOTAL};
    public WaveFormat current_waveFormat;

    #endregion

    #region Variables

    //check for game start
    public bool b_allowObjectSpawning = false;

    //references to object scripts
    SPS_UIManager uimanagerInstance;

    [Tooltip("This stores the objects in each wave")]
    public List<GameObject> objectWaveList;

    //base prefabs for objects spawning
    GameObject g_objectInstance;
    public GameObject enemyPrefab, obstaclePrefab, powerupPrefab;

    //these pre-determined positions are to be used with DOTween
    public GameObject objectStartPosition, enemyEndPosition, obstacleEndPosition;

    //tween references HERE
    public Tween enemymovementTween;

    //spawning speed variables
    [Tooltip("Uptime for each object spawn")]
    float f_objectspawnLifetime;
    [Tooltip("Multiplier for the time for each object spawn")]
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
        //set these on start
        i_currentwaveCount = 0;
        i_powerupCount = 0;
        f_objectTravelSpeed = 5f;
        f_objecttravelspeedMultiplier = 1f;

        //reference scripts hERE
        uimanagerInstance = FindObjectOfType<SPS_UIManager>();

    }

    private void Update()
    {
        if (!uimanagerInstance.b_gameStart)
            return;
        if (b_allowObjectSpawning == true)
        {
            //we increase the speed every 5 WAVES
            if (i_currentwaveCount % 5 == 0)
            {
                ReduceObjectSpawnTime();
            }

            f_objectspawnLifetime += Time.deltaTime;
            if (f_objectspawnLifetime >= (f_objectspawnLifetime * f_objectLifetimeMultiplier) && b_waveCompleted == false)
            {
                //Debug.Log("please");
                //we check to see if the player has received any powerups recently
                if (i_powerupCount >= 3)
                {
                    //we run it such that the next set of spawn HAS to be a powerup wave
                    int randVal = Random.Range(0, 11);

                    switch (randVal % 2)
                    {
                        case 0: SpawnPowerupObject(); break;
                        case 1: SpawnPowerupObject(); break;
                    }

                    f_objectspawnLifetime = 0f;

                }
                else
                {
                    //for the first 3 waves we only allow enemies to spawn
                    if (i_currentwaveCount <= 3)
                    {
                        SpawnSingleRandomEnemy();

                        //current_waveFormat = (WaveFormat)Random.Range(1, (int)WaveFormat.WAVE_MULTIPLE_STONE);

                        //switch (current_waveFormat)
                        //{
                        //    case WaveFormat.WAVE_SINGLE_RANDOM:
                        //        SpawnSingleRandomEnemy();
                        //        break;

                        //    case WaveFormat.WAVE_MULTIPLE_RANDOM:
                        //        SpawnMultipleRandomEnemies();
                        //        break;

                        //    case WaveFormat.WAVE_MULTIPLE_SCISSORS:
                        //        SpawnMultipleRandomEnemies();
                        //        break;

                        //    case WaveFormat.WAVE_MULTIPLE_PAPER:
                        //        SpawnMultipleRandomEnemies();
                        //        break;

                        //    case WaveFormat.WAVE_MULTIPLE_STONE:
                        //        SpawnMultipleRandomEnemies();
                        //        break;
                        //}

                        f_objectspawnLifetime = 0f;

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

                            //case WaveFormat.WAVE_POWERUP_AND_OBSTACLE:
                            //    SpawnPowerupAndObstacleObject();
                            //    break;

                        }

                        f_objectspawnLifetime = 0f;

                    }
                }
            }
        }   
    }

    #endregion

    #region Spawning Functions

    public void SpawnSingleRandomEnemy()
    {
        //instantiate an enemy HERE
        //g_objectInstance = Instantiate(enemyPrefab,
        // new Vector3(objectStartPosition.transform.position.x, 
        // objectStartPosition.transform.position.y,
        // objectStartPosition.transform.position.z), objectStartPosition.transform.rotation);
        g_objectInstance = ObjectPooling.SharedInstance.GetPooledObject("EnemyTag");
        if (!g_objectInstance.activeSelf)
        {
            g_objectInstance.transform.position = objectStartPosition.transform.position;
            g_objectInstance.transform.rotation = objectStartPosition.transform.rotation;
            g_objectInstance.GetComponent<SPS_Enemy>().DetermineEnemyType();
            g_objectInstance.SetActive(true);
        }
        //do the movement HERE
        g_objectInstance.transform.DOMoveX(enemyEndPosition.transform.position.x, f_objectTravelSpeed * f_objecttravelspeedMultiplier * 5f);

        //enemymovementTween = g_objectInstance.GetComponent<Rigidbody2D>().DOMoveX(enemyEndPosition.transform.position.x, f_objectTravelSpeed * f_objecttravelspeedMultiplier * 3f);

        //add this object to the list of objects spawned
        objectWaveList.Add(g_objectInstance);

        //do other things HERE
        b_waveCompleted = true;
        i_powerupCount += 1;
        WaveIncrease();

    }

    public void SpawnMultipleRandomEnemies()
    {
        int waveSize = Random.Range(2, 4);

        for (int val = 1; val <= waveSize; val++)
        {
            // g_objectInstance = Instantiate(enemyPrefab,
            //new Vector3(objectStartPosition.transform.position.x + val * 4.5f, 
            //objectStartPosition.transform.position.y, 
            //objectStartPosition.transform.position.z), objectStartPosition.transform.rotation);
            g_objectInstance = ObjectPooling.SharedInstance.GetPooledObject("EnemyTag");
            if (!g_objectInstance.activeSelf)
            {
                //Debug.Log("help");
                g_objectInstance.transform.position = new Vector3(objectStartPosition.transform.position.x + val * 4.5f, objectStartPosition.transform.position.y, objectStartPosition.transform.position.z);
                g_objectInstance.transform.rotation = objectStartPosition.transform.rotation;
                g_objectInstance.GetComponent<SPS_Enemy>().DetermineEnemyType();
                g_objectInstance.SetActive(true);
            }
            //do the movement HERE
            g_objectInstance.transform.DOMoveX(enemyEndPosition.transform.position.x, (f_objectTravelSpeed * f_objecttravelspeedMultiplier * 5f));

            //g_objectInstance.GetComponent<Rigidbody2D>().DOMoveX(enemyEndPosition.transform.position.x, (f_objectTravelSpeed * f_objectTravelSpeed));

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
            new Vector3(objectStartPosition.transform.position.x, 
            -1.1f, objectStartPosition.transform.position.z), objectStartPosition.transform.rotation);

        //do the movement HERE
        g_objectInstance.transform.DOMoveX(obstacleEndPosition.transform.position.x, f_objectTravelSpeed * f_objecttravelspeedMultiplier * 3f);
        g_objectInstance.GetComponent<Rigidbody2D>().DOMoveX(obstacleEndPosition.transform.position.x, f_objectTravelSpeed * f_objecttravelspeedMultiplier * 3f);

        objectWaveList.Add(g_objectInstance);
        b_waveCompleted = true;

        i_powerupCount += 1;
        WaveIncrease();
    }

    public void SpawnPowerupObject()
    {
        g_objectInstance = Instantiate(powerupPrefab,
            new Vector3(objectStartPosition.transform.position.x,
            -1.25f, objectStartPosition.transform.position.z), objectStartPosition.transform.rotation);

        //do the movement HERE
        g_objectInstance.transform.DOMoveX(obstacleEndPosition.transform.position.x, f_objectTravelSpeed * f_objecttravelspeedMultiplier * 3f);
        g_objectInstance.GetComponent<Rigidbody2D>().DOMoveX(obstacleEndPosition.transform.position.x, f_objectTravelSpeed * f_objecttravelspeedMultiplier * 3f);

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
            new Vector3(objectStartPosition.transform.position.x,
            1f, objectStartPosition.transform.position.z), objectStartPosition.transform.rotation);

        //do the movement HERE
        g_objectInstance.transform.DOMoveX(obstacleEndPosition.transform.position.x, f_objectTravelSpeed * f_objecttravelspeedMultiplier * 3f);
        g_objectInstance.GetComponent<Rigidbody2D>().DOMoveX(obstacleEndPosition.transform.position.x, f_objectTravelSpeed * f_objecttravelspeedMultiplier * 3f);

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

    #region Coroutines

    public IEnumerator EndsEnemy(Animator enemyAnimator, GameObject targetedEnemy)
    {
        //end the movement
        DOTween.Kill(enemymovementTween);

        //change the animation
        enemyAnimator.SetBool("e_died", true);

        //fade out the animation
        targetedEnemy.transform.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
        yield return enemymovementTween.WaitForCompletion();
        //destroy the gameObject and its rigidbody
        //Destroy(targetedEnemy);
        //Destroy(targetedEnemy.GetComponent<Rigidbody2D>());
        targetedEnemy.SetActive(false);
        Debug.Log("Completed deletion");
    }



    #endregion


}
