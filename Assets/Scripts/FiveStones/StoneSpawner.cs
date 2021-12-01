using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpawner : MonoBehaviour
{
    public GameObject stonePrefab;

    public float minSpawnDelay;
    public float maxSpawnDelay;

    public int minStone;
    public int maxStone;

    public float minForce;
    public float maxForce;

    public List<Transform> spawnPointList;

    // Start is called before the first frame update
    void Start()
    {
        if (stonePrefab == null)
        {
            Debug.LogError("stonePrefab is null");
            return;
        }

        if (spawnPointList == null || spawnPointList.Count == 0)
        {
            Debug.LogError("spawnPointList is null or empty, make sure to initialized some points");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SpawnStoneLoop()
    {
        while (true)
        {
            float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
            SpawnStone(Random.Range(minStone, maxStone));
            yield return new WaitForSeconds(delay);
            // Add condition to break out of loop
        }
    }

    void SpawnStone(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, spawnPointList.Count - 1);
            Transform randomPoint = spawnPointList[randomIndex];

            GameObject spawnedStone = Instantiate(stonePrefab, randomPoint.position, randomPoint.rotation);
            spawnedStone.GetComponent<Rigidbody2D>().AddForce(spawnedStone.transform.up * Random.Range(minForce, maxForce), ForceMode2D.Impulse);
            spawnedStone.GetComponent<Stone>().type = FiveStonesGameManager.GetRandomColouredObjective();

            Destroy(spawnedStone, 5f);
        }
    }
}
