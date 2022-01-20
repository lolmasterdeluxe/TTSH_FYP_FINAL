using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRings : MonoBehaviour
{
    public GameObject redRingPrefab, yellowRingPrefab, greenRingPrefab;
    public float spawnRate;
    private float randX, randY;
    private float randOption;
    private float nextSpawn;
    private Vector2 spawnPos;
    public bool spawnRingsStart = true;

    public SpriteRenderer skySpriteWidth, skySpriteHeight;
    public List<Vector2> spawnedPositions;
    public List<GameObject> spawnedRings = new List<GameObject>();
    private float redringRadiusX, yellowringRadiusX, greenringRadiusX;
    private float redringRadiusY, yellowringRadiusY, greenringRadiusY;
    private GameObject gameObjectRings;

    // Start is called before the first frame update
    void Start()
    {
        redringRadiusX = redRingPrefab.GetComponent<Collider2D>().bounds.extents.x;
        yellowringRadiusX = yellowRingPrefab.GetComponent<Collider2D>().bounds.extents.x;
        greenringRadiusX = greenRingPrefab.GetComponent<Collider2D>().bounds.extents.x;

        redringRadiusY = redRingPrefab.GetComponent<Collider2D>().bounds.extents.y;
        yellowringRadiusY = yellowRingPrefab.GetComponent<Collider2D>().bounds.extents.y;
        greenringRadiusY = greenRingPrefab.GetComponent<Collider2D>().bounds.extents.y;

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

    public void RingSpawn(int ringNum)
    {
        // Gets size of the ring colliders of x-axis
        redringRadiusX = redRingPrefab.GetComponent<Collider2D>().bounds.max.x + 0.5f;
        yellowringRadiusX = yellowRingPrefab.GetComponent<Collider2D>().bounds.max.x + 0.5f;
        greenringRadiusX = greenRingPrefab.GetComponent<Collider2D>().bounds.max.x + 0.5f;

        // Gets size of the ring colliders of y-axis
        redringRadiusY = redRingPrefab.GetComponent<Collider2D>().bounds.max.y + 0.5f;
        yellowringRadiusY = yellowRingPrefab.GetComponent<Collider2D>().bounds.max.y + 0.5f;
        greenringRadiusY = greenRingPrefab.GetComponent<Collider2D>().bounds.max.y + 0.5f;

        // Spawns number rings
        for (int i = 0; i < ringNum; i++)
        {
            randX = Random.Range(skySpriteWidth.bounds.min.x + 0.5f, skySpriteWidth.bounds.max.x - 0.5f);
            randY = Random.Range(skySpriteHeight.bounds.min.y + 3f, skySpriteHeight.bounds.max.y - 0.5f);

            // Sets 3 rings to be random
            randOption = Random.Range(0, 3);

            // Sets spawn position to be random
            spawnPos = new Vector2(randX, randY);

            Collider2D colliderWithRedRing = Physics2D.OverlapCircle(spawnPos, redringRadiusX + redringRadiusY, LayerMask.GetMask("RedRingLayer"));
            Collider2D colliderWithYellowRing = Physics2D.OverlapCircle(spawnPos, yellowringRadiusX + yellowringRadiusY, LayerMask.GetMask("YellowRingLayer"));
            Collider2D colliderWithGreenRing = Physics2D.OverlapCircle(spawnPos, greenringRadiusX + greenringRadiusY, LayerMask.GetMask("GreenRingLayer"));

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

    private void RandomRingSpawn()
    {
        if (spawnRingsStart)
        {
            RingSpawn(8);
            spawnRingsStart = false;
        }
        else
        {
            if (Time.time > nextSpawn)
            {
                nextSpawn = Time.time + spawnRate;

                RingSpawn(4);
            }
        }
    }

    private void LimitSpawnRings()
    {
        if (spawnedPositions.Count <= 32)
        {
            RandomRingSpawn();
        }
    }
}
