using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SPS_EnemySpawner : MonoBehaviour
{
    //this script spawns enemies that the player encounters

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }


    #region Variables

    public List<Pool> poolList;

    public GameObject enemyPrefab;

    [Tooltip("Offset for each enemy spawning")]
    public float enemySpawnOffset;

    [Tooltip("Minimum and maximum offset timing to have random pattern spawning")]
    public float min, max;

    //for object pooling
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    #endregion

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in poolList)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }


    #region Unity Callbacks
    private void Update()
    {
        enemySpawnOffset += Time.deltaTime;
        if (enemySpawnOffset >= Random.Range(min, max))
        {
            print("yes");
            GameObject x = Instantiate(enemyPrefab, transform.position, transform.rotation);
            x.transform.DOMove(new Vector3(0f, 0f, -0.5f), 2);
            enemySpawnOffset = 0f;
        }


    }

    #endregion

}
