using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SPS_EnemySpawner : MonoBehaviour
{
    //this script spawns enemies that the player encounters

    #region Variables

    public GameObject enemyPrefab;

    [Tooltip("Offset for each enemy spawning")]
    public float enemySpawnOffset;

    [Tooltip("Minimum and maximum offset timing to have random pattern spawning")]
    public float min, max;

    #endregion

    private void Start()
    {
    }


    #region Unity Callbacks
    private void Update()
    {
        enemySpawnOffset += Time.deltaTime;
        if (enemySpawnOffset >= Random.Range(min, max))
        {
            print("yes");
            GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
            enemy.transform.DOMove(new Vector3(0f, 0f, -0.5f), 2);
            enemySpawnOffset = 0f;
        }


    }

    #endregion

}
