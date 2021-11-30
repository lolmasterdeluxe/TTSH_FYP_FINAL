using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SPS_EnemySpawner : MonoBehaviour
{
    //this script spawns enemies that the player encounters

    #region Variables

    public GameObject enemyPrefab;

    public Transform enemystartPos;
    public Transform enemyfinalPos;

    public float averageTime;

    public bool deleteEnemy;

    GameObject enemy;

    [Tooltip("Offset for each enemy spawning")]
    public float enemySpawnOffset;

    [Tooltip("Minimum and maximum offset timing to have random pattern spawning")]
    public float minTime, maxTime;



    #endregion

    #region Unity Callbacks

    private void Start()
    {
    }

    private void Update()
    {
        enemySpawnOffset += Time.deltaTime;
        if (enemySpawnOffset >= Random.Range(minTime, maxTime))
        {
            for (int i = 0; i < 1; i++)
            {
                enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
                averageTime = Random.Range(minTime, maxTime);
                enemy.transform.DOMoveX(enemyfinalPos.transform.position.x, averageTime);
                enemy.GetComponent<Rigidbody>().DOMoveX(enemyfinalPos.transform.position.x, averageTime);
                
                enemySpawnOffset = 0f;
            }
        }
    }

    #endregion

    #region Functions

    #endregion

}
