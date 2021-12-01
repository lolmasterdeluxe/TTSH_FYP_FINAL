using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHoops : MonoBehaviour
{
    public GameObject redHoopPrefab, blueHoopPrefab;
    public float spawnRate = 2f;
    private float randX, randY;
    private float nextSpawn = 0f;
    private Vector2 spawnPos;

    public SpriteRenderer skySpriteWidth, skySpriteHeight;
    private Vector2 redPosition;
    private Vector2 bluePosition;
    private int maxSpawnAttempts = 1;
    private float hoopWidth;

    //private float skyWidth, skyHeight;
    //private Vector2 skyPosition;

    // Start is called before the first frame update
    void Start()
    {
        hoopWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
    }

    private void Update()
    {
        if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            randX = Random.Range(skySpriteWidth.bounds.min.x, skySpriteWidth.bounds.max.x);
            randY = Random.Range(skySpriteHeight.bounds.min.y + 3f, skySpriteHeight.bounds.max.y);
            spawnPos = new Vector2(randX, randY);

            Vector2 redHoopPosition = GetRandomSpawnPositionNear(redPosition);

            Instantiate(redHoopPrefab, redHoopPosition, Quaternion.identity);

            for (int i = 0; i < maxSpawnAttempts; i++)
            {
                Vector3 blueHoopPosition = GetRandomSpawnPositionNear(bluePosition);

                // Check for overlaps near this position.
                // (Assuming your wall has a 2D collider attached)
                if (Physics2D.OverlapCircle(blueHoopPosition, hoopWidth) == null)
                {
                    // We found a non-overlapping spot! Slap it down & call it done. :D
                    Instantiate(blueHoopPrefab, blueHoopPosition, Quaternion.identity);
                    return;
                }
                // Otherwise, loop and try again with a new random point.
            }

            //Instantiate(redHoopPrefab, spawnPos, Quaternion.identity);
            Instantiate(blueHoopPrefab, spawnPos, Quaternion.identity);
        }
    }

    Vector2 GetRandomSpawnPositionNear(Vector2 center)
    {
        Vector2 spawnOffset = spawnPos;

        spawnOffset.x *= Random.Range(-0.5f, 0.5f);
        spawnOffset.y *= Random.Range(-0.5f, 0.5f);

        return center + spawnOffset;
    }
}
