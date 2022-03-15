using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpawner : MonoBehaviour
{
    public enum SpawnPattern
    {
        DEFAULT,
        NORMAL_SNAKE,
        SNAKE,
        CURVE,
        CROSS,
        TOTAL,
    }

    public GameObject stonePrefab;

    public float minSpawnDelay;
    public float maxSpawnDelay;

    public int minStone;
    public int maxStone;

    public float minForce;
    public float maxForce;

    public GameObject spawnPointHolder;
    public List<Transform> spawnPointList = new List<Transform>();

    private GameObject spawnedStone;
    // Start is called before the first frame update
    void Start()
    {
        if (stonePrefab == null)
        {
            Debug.LogError("stonePrefab is null");
            return;
        }

        // Load spawnPointList
        for (int i = 0; i < spawnPointHolder.transform.childCount; i++)
            spawnPointList.Add(spawnPointHolder.transform.GetChild(i));

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

    public void Configure(float minSpawnDelay, float maxSpawnDelay, int minStone, int maxStone, float minForce, float maxForce)
    {
        this.minSpawnDelay = minSpawnDelay;
        this.maxSpawnDelay = maxSpawnDelay;
        this.minStone = minStone;
        this.maxStone = maxStone;
        this.minForce = minForce;
        this.maxForce = maxForce;
    }

    public void RandomizeRotation(Transform transform)
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Random.Range(-180, 180));
    }

    public IEnumerator SpawnStoneLoop()
    {
        while (true)
        {
            if (TimerManager.Instance.GetRemainingTime() <= 0)
                break;

            SpawnPattern randomSpawnType = (SpawnPattern)Random.Range((int)SpawnPattern.DEFAULT, (int)SpawnPattern.TOTAL);
            float delay = Random.Range(minSpawnDelay, maxSpawnDelay);

            switch (randomSpawnType)
            {
                case SpawnPattern.DEFAULT:
                    SpawnStoneDefault(Random.Range(minStone, maxStone));
                    break;
                case SpawnPattern.NORMAL_SNAKE:
                    StartCoroutine(SpawnStoneNormalSnake());
                    break;
                case SpawnPattern.SNAKE:
                    StartCoroutine(SpawnStoneSnake());
                    break;
                case SpawnPattern.CURVE:
                    StartCoroutine(SpawnStoneCurve());
                    break;
                case SpawnPattern.CROSS:
                    StartCoroutine(SpawnStoneCross());
                    break;
            }

            yield return new WaitForSeconds(delay);
        }
    }


    IEnumerator SpawnStoneNormalSnake()
    {
        float randomForce = Random.Range(minForce, maxForce);
        Debug.Log("NormalSnake?");
        for (int i = 0; i < spawnPointList.Count; i++)
        {
            //GameObject spawnedStone = Instantiate(stonePrefab, spawnPointList[i].position, Quaternion.identity);
            spawnedStone = ObjectPooling.SharedInstance.GetPooledObject("Stone");
            if (spawnedStone != null)
            {
                spawnedStone.SetActive(true);
                spawnedStone.transform.position = spawnPointList[i].position;
                spawnedStone.transform.rotation = Quaternion.identity;
                Vector3 force = spawnedStone.transform.up * randomForce;
                force = new Vector3(force.x, force.y, 0);
                spawnedStone.GetComponent<Rigidbody2D>().AddForce(spawnedStone.transform.up * randomForce, ForceMode2D.Impulse);
                spawnedStone.GetComponent<Stone>().type = FiveStonesGameManager.GetRandomColouredObjective();
                RandomizeRotation(spawnedStone.transform);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator SpawnStoneSnake()
    {
        float randomMagnitude = Random.Range(2f, 5f);
        float randomOffset = Random.Range(10f, 12f);
        Debug.Log("SNAKE");
        for (int i = 0; i < spawnPointList.Count; i++)
        {
            //GameObject spawnedStone = Instantiate(stonePrefab, spawnPointList[i].position, Quaternion.identity);
            spawnedStone = ObjectPooling.SharedInstance.GetPooledObject("Stone");
            if (spawnedStone != null)
            {
                spawnedStone.SetActive(true);
                spawnedStone.transform.position = spawnPointList[i].position;
                spawnedStone.transform.rotation = Quaternion.identity;
                Vector3 force = spawnedStone.transform.up * (randomMagnitude * Mathf.Sin(spawnedStone.transform.position.x) + randomOffset);
                force = new Vector3(force.x, force.y, 0);
                spawnedStone.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
                spawnedStone.GetComponent<Stone>().type = FiveStonesGameManager.GetRandomColouredObjective();
                RandomizeRotation(spawnedStone.transform);
            }
            yield return null;
        }
    }

    IEnumerator SpawnStoneCurve()
    {
        float randomMagnitude = Random.Range(2f, 4f);
        float randomOffset = Random.Range(10f, 12f);
        Debug.Log("CURVE");
        for (int i = 0; i < spawnPointList.Count; i++)
        {
            //GameObject spawnedStone = Instantiate(stonePrefab, spawnPointList[i].position, Quaternion.identity);
            spawnedStone = ObjectPooling.SharedInstance.GetPooledObject("Stone");
            if (spawnedStone != null)
            {
                spawnedStone.SetActive(true);
                spawnedStone.transform.position = spawnPointList[i].position;
                spawnedStone.transform.rotation = Quaternion.identity;
                Vector3 force = spawnedStone.transform.up * (randomMagnitude * Mathf.Cos(0.2f * spawnedStone.transform.position.x) + randomOffset);
                force = new Vector3(force.x, force.y, 0);
                spawnedStone.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
                spawnedStone.GetComponent<Stone>().type = FiveStonesGameManager.GetRandomColouredObjective();
                RandomizeRotation(spawnedStone.transform);
            }
            yield return null;
        }
    }

    IEnumerator SpawnStoneCross()
    {
        float randomMagnitude = Random.Range(2f, 5f);
        float randomOffset = Random.Range(10f, 12f);
        Debug.Log("ACROSS");
        for (int i = 0; i < spawnPointList.Count; i++)
        {
            //GameObject spawnedStone = Instantiate(stonePrefab, spawnPointList[i].position, Quaternion.identity);
            spawnedStone = ObjectPooling.SharedInstance.GetPooledObject("Stone");
            if (spawnedStone != null)
            {
                spawnedStone.SetActive(true);
                spawnedStone.transform.position = spawnPointList[i].position;
                spawnedStone.transform.rotation = Quaternion.identity;
                Vector3 force = spawnedStone.transform.up * (randomMagnitude * Mathf.Cos(3f * spawnedStone.transform.position.x) + randomOffset);
                force = new Vector3(force.x, force.y, 0);
                spawnedStone.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
                spawnedStone.GetComponent<Stone>().type = FiveStonesGameManager.GetRandomColouredObjective();
                RandomizeRotation(spawnedStone.transform);
            }
            yield return null;
        }
    }

    void SpawnStoneDefault(int count)
    {
        List<Transform> usedSpawnPointList = new List<Transform>();
        Debug.Log("Default?");
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, spawnPointList.Count - 1);

            // We make sure the spawn points don't overlap if there is more than enough unique spawn points
            if (count <= spawnPointList.Count)
            {
                while (usedSpawnPointList.Contains(spawnPointList[randomIndex]))
                    randomIndex = Random.Range(0, spawnPointList.Count - 1);
            }
            
            Transform randomPoint = spawnPointList[randomIndex];
            usedSpawnPointList.Add(randomPoint);

            //GameObject spawnedStone = Instantiate(stonePrefab, randomPoint.position, randomPoint.rotation);
            spawnedStone = ObjectPooling.SharedInstance.GetPooledObject("Stone");
            if (spawnedStone != null)
            {
                spawnedStone.SetActive(true);
                spawnedStone.transform.position = randomPoint.position;
                spawnedStone.transform.rotation = randomPoint.rotation;
                Vector3 force = spawnedStone.transform.up * Random.Range(minForce, maxForce);
                force = new Vector3(force.x, force.y, 0);
                spawnedStone.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
                spawnedStone.GetComponent<Stone>().type = FiveStonesGameManager.GetRandomColouredObjective();
            }
            //Destroy(spawnedStone, 5f);
        }
    }
}
