using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHoops : MonoBehaviour
{
    public GameObject redHoopPrefab, blueHoopPrefab;
    public float spawnRate = 2f;
    private float randX, randY;
    private float randOption;
    private float nextSpawn = 0f;
    private Vector2 spawnPos;

    public SpriteRenderer skySpriteWidth, skySpriteHeight;
    private List<Vector2> spawnedPositions;
    private float hoopRadius;

    private int points;

    // Start is called before the first frame update
    void Start()
    {
        //hoopWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        hoopRadius = redHoopPrefab.GetComponent<Collider2D>().bounds.extents.x;
        spawnedPositions = new List<Vector2>();
    }

    private void Update()
    {
        RandomHoopSpawn();
    }

    #region Misc
    /*if (Time.time > nextSpawn)
    {
        nextSpawn = Time.time + spawnRate;
        randX = Random.Range(skySpriteWidth.bounds.min.x, skySpriteWidth.bounds.max.x);
        randY = Random.Range(skySpriteHeight.bounds.min.y + 3f, skySpriteHeight.bounds.max.y);
        spawnPos = new Vector2(randX, randY);
        int tempval = 0;
        for (int i = 0; i < maxSpawnAttempts; i++)
        {
            Vector2 redHoopPosition = GetRandomSpawnPositionNear(redPosition);
            if (tempval < 1)
                Instantiate(redHoopPrefab, redHoopPosition, Quaternion.identity);

            //Vector3 blueHoopPosition = GetRandomSpawnPositionNear(bluePosition);
            // Check for overlaps near this position.
            // (Assuming your wall has a 2D collider attached)
            if (Physics2D.OverlapCircle(redHoopPrefab.transform.position, hoopWidth) == null)
            {
                // We found a non-overlapping spot! Slap it down & call it done. :D
                Instantiate(redHoopPrefab, redHoopPosition, Quaternion.identity);
                tempval += 1;
                return;
            }
            // Otherwise, loop and try again with a new random point.
        }

        for (int i = 0; i < maxSpawnAttempts; i++)
        {
            Vector2 blueHoopPosition = GetRandomSpawnPositionNear(bluePosition);

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
        //Instantiate(blueHoopPrefab, spawnPos, Quaternion.identity);
    }*/
    #endregion

    private void RandomHoopSpawn()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            hoopRadius = redHoopPrefab.GetComponent<Collider2D>().bounds.max.x;

            for (int i = 0; i < 3; i++)
            {
                randX = Random.Range(skySpriteWidth.bounds.min.x, skySpriteWidth.bounds.max.x);
                randY = Random.Range(skySpriteHeight.bounds.min.y + 3f, skySpriteHeight.bounds.max.y);
                randOption = Random.Range(0, 2);
                spawnPos = new Vector2(randX, randY);

                Collider2D colliderWithHoop = Physics2D.OverlapCircle(spawnPos, hoopRadius, LayerMask.GetMask("HoopLayer"));

                if (!colliderWithHoop)
                    spawnedPositions.Add(spawnPos);

                if (spawnedPositions[i].x != randX && spawnedPositions[i].y != randY)
                {
                    if (colliderWithHoop == false)
                    {
                        GameObject temp;
                        if (randOption < 1)
                            temp = Instantiate(redHoopPrefab, spawnPos, Quaternion.identity);
                        else
                            temp = Instantiate(blueHoopPrefab, spawnPos, Quaternion.identity);
                        spawnedPositions.RemoveAt(spawnedPositions.Count - 1);
                        spawnedPositions.Add(temp.transform.position);
                        
                    }
                }
            }
        }
    }

}
