using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHoops : MonoBehaviour
{
    public GameObject redHoopPrefab, blueHoopPrefab, greenHoopPrefab;
    public float spawnRate = 2f;
    private float randX, randY;
    private float randOption;
    private float nextSpawn = 0f;
    private Vector2 spawnPos;

    public SpriteRenderer skySpriteWidth, skySpriteHeight;
    private List<Vector2> spawnedPositions;
    private float redhoopRadius, bluehoopRadius, greenhoopRadius;

    private GameObject gameObjectHoops;

    // Start is called before the first frame update
    void Start()
    {
        //hoopWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        redhoopRadius = redHoopPrefab.GetComponent<Collider2D>().bounds.extents.x;
        bluehoopRadius = blueHoopPrefab.GetComponent<Collider2D>().bounds.extents.x;
        greenhoopRadius = greenHoopPrefab.GetComponent<Collider2D>().bounds.extents.x;
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
            redhoopRadius = redHoopPrefab.GetComponent<Collider2D>().bounds.max.x;
            bluehoopRadius = redHoopPrefab.GetComponent<Collider2D>().bounds.max.x;
            greenhoopRadius = redHoopPrefab.GetComponent<Collider2D>().bounds.max.x;

            for (int i = 0; i < 2; i++)
            {
                randX = Random.Range(skySpriteWidth.bounds.min.x + 0.5f, skySpriteWidth.bounds.max.x - 0.5f);
                randY = Random.Range(skySpriteHeight.bounds.min.y + 3f, skySpriteHeight.bounds.max.y - 0.5f);
                randOption = Random.Range(0, 3);
                spawnPos = new Vector2(randX, randY);

                Collider2D colliderWithRedHoop = Physics2D.OverlapCircle(spawnPos, redhoopRadius, LayerMask.GetMask("RedHoopLayer"));
                Collider2D colliderWithBlueHoop = Physics2D.OverlapCircle(spawnPos, bluehoopRadius, LayerMask.GetMask("BlueHoopLayer"));
                Collider2D colliderWithGreenHoop = Physics2D.OverlapCircle(spawnPos, greenhoopRadius, LayerMask.GetMask("GreenHoopLayer"));

                if (!colliderWithRedHoop && !colliderWithBlueHoop && !colliderWithGreenHoop)
                    spawnedPositions.Add(spawnPos);

                if (spawnedPositions[i].x != randX && spawnedPositions[i].y != randY)
                {
                    if (colliderWithRedHoop == false && colliderWithBlueHoop == false && colliderWithGreenHoop == false)
                    {
                        GameObject temp;
                        
                        switch(randOption)
                        {
                            case 0:
                                temp = Instantiate(redHoopPrefab, spawnPos, Quaternion.identity);
                                gameObjectHoops = temp;
                                break;
                            case 1:
                                temp = Instantiate(blueHoopPrefab, spawnPos, Quaternion.identity);
                                gameObjectHoops = temp;
                                break;
                            case 2:
                                temp = Instantiate(greenHoopPrefab, spawnPos, Quaternion.identity);
                                gameObjectHoops = temp;
                                break;
                        }

                        //if (randOption < 1)
                        //    temp = Instantiate(redHoopPrefab, spawnPos, Quaternion.identity);
                        //else if (randOption < 0)
                        //    temp = Instantiate(blueHoopPrefab, spawnPos, Quaternion.identity);
                        //else
                        //    temp = Instantiate(greenHoopPrefab, spawnPos, Quaternion.identity);

                        spawnedPositions.RemoveAt(spawnedPositions.Count - 1);
                        spawnedPositions.Add(gameObjectHoops.transform.position);
                    }
                }

            }
        }
    }

}
