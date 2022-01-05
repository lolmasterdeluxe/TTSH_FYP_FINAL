using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRings : MonoBehaviour
{
    public GameObject redRingPrefab, yellowRingPrefab, greenRingPrefab;
    public float spawnRate;
    private float randX, randY;
    private float randOption;
    private float nextSpawn = 0f;
    private Vector2 spawnPos;

    public SpriteRenderer skySpriteWidth, skySpriteHeight;
    public List<Vector2> spawnedPositions;
    public List<GameObject> spawnedRings = new List<GameObject>();
    private float redringRadius, yellowringRadius, greenringRadius;
    private GameObject gameObjectRings;

    // Start is called before the first frame update
    void Start()
    {
        redringRadius = redRingPrefab.GetComponent<Collider2D>().bounds.extents.x;
        yellowringRadius = yellowRingPrefab.GetComponent<Collider2D>().bounds.extents.x;
        greenringRadius = greenRingPrefab.GetComponent<Collider2D>().bounds.extents.x;
        spawnedPositions = new List<Vector2>();
        spawnedRings = new List<GameObject>();
    }

    private void Update()
    {
        if (!ChaptehGameManager.Instance.m_gameStarted)
            return;
        else if (ChaptehGameManager.Instance.m_gameEnded)
            return;

        LimitSpawnRings();
    }

    private void RandomRingSpawn()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;

            // Gets size of the ring colliders
            redringRadius = redRingPrefab.GetComponent<Collider2D>().bounds.max.x + 0.5f;
            yellowringRadius = yellowRingPrefab.GetComponent<Collider2D>().bounds.max.x + 0.5f;
            greenringRadius = greenRingPrefab.GetComponent<Collider2D>().bounds.max.x + 0.5f;

            // Spawns every 2 rings
            for (int i = 0; i < 2; i++)
            {
                randX = Random.Range(skySpriteWidth.bounds.min.x + 0.5f, skySpriteWidth.bounds.max.x - 0.5f);
                randY = Random.Range(skySpriteHeight.bounds.min.y + 3f, skySpriteHeight.bounds.max.y - 0.5f);
                
                // Sets 3 rings to be random
                randOption = Random.Range(0, 3);
                
                // Sets spawn position to be random
                spawnPos = new Vector2(randX, randY);

                Collider2D colliderWithRedRing = Physics2D.OverlapCircle(spawnPos, redringRadius, LayerMask.GetMask("RedRingLayer"));
                Collider2D colliderWithYellowRing = Physics2D.OverlapCircle(spawnPos, yellowringRadius, LayerMask.GetMask("YellowRingLayer"));
                Collider2D colliderWithGreenRing = Physics2D.OverlapCircle(spawnPos, greenringRadius, LayerMask.GetMask("GreenRingLayer"));

                if (!colliderWithRedRing && !colliderWithYellowRing && !colliderWithGreenRing)
                    spawnedPositions.Add(spawnPos);

                if (spawnedPositions[i].x != randX && spawnedPositions[i].y != randY)
                {
                    if (colliderWithRedRing == false && colliderWithYellowRing == false && colliderWithGreenRing == false)
                    {
                        GameObject temp;

                        // Spawns the rings randomly
                        switch (randOption)
                        {
                            case 0:
                                temp = Instantiate(redRingPrefab, spawnPos, Quaternion.identity);
                                gameObjectRings = temp;
                                spawnedRings.Add(temp);
                                break;
                            case 1:
                                temp = Instantiate(yellowRingPrefab, spawnPos, Quaternion.identity);
                                gameObjectRings = temp;
                                spawnedRings.Add(temp);
                                break;
                            case 2:
                                temp = Instantiate(greenRingPrefab, spawnPos, Quaternion.identity);
                                gameObjectRings = temp;
                                spawnedRings.Add(temp);
                                break;
                        }

                        spawnedPositions.RemoveAt(spawnedPositions.Count - 1);
                        spawnedPositions.Add(gameObjectRings.transform.position);
                    }
                }
            }
        }
    }

    private void LimitSpawnRings()
    {
        if (spawnedPositions.Count <= 30)
        {
            RandomRingSpawn();
        }
    }

    private void DestroyRingsAfterTime()
    {
        if(spawnedPositions.Count >= 16)
        {
            //liveTime -= Time.deltaTime;
            //randOption = Random.Range(0, 3);
            //if (liveTime <= 0)
            //{
            //    //Destroy(gameObjectHoops);

            //    switch (randOption)
            //    {
            //        case 0:
            //            Destroy(redHoopPrefab);
            //            break;
            //        case 1:
            //            Destroy(blueHoopPrefab);
            //            break;
            //        case 2:
            //            Destroy(greenHoopPrefab);
            //            break;
            //    }
            //}

            randOption = Random.Range(0, 3);
            spawnedPositions.RemoveRange(0, 6);
            switch (randOption)
            {
                case 0:
                    Destroy(redRingPrefab);
                    break;
                case 1:
                    Destroy(yellowRingPrefab);
                    break;
                case 2:
                    Destroy(greenRingPrefab);
                    break;
            }
        }
    }

}
