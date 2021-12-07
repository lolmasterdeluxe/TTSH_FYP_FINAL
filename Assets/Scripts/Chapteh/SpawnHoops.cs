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

    private float liveTime = 5f;

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

        //DestroyHoopsAfterTime();
    }

    private void RandomHoopSpawn()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            redhoopRadius = redHoopPrefab.GetComponent<Collider2D>().bounds.max.x + 0.5f;
            bluehoopRadius = redHoopPrefab.GetComponent<Collider2D>().bounds.max.x + 0.5f;
            greenhoopRadius = redHoopPrefab.GetComponent<Collider2D>().bounds.max.x + 0.5f;

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

                        spawnedPositions.RemoveAt(spawnedPositions.Count - 1);
                        spawnedPositions.Add(gameObjectHoops.transform.position);

                        Debug.Log(spawnedPositions.Count);
                    }
                }
            }
        }
    }

    private void DestroyHoopsAfterTime()
    {
        if(spawnedPositions.Count > 10)
        {
            liveTime -= Time.deltaTime;
            randOption = Random.Range(0, 3);
            if (liveTime <= 0)
            {
                //Destroy(gameObjectHoops);

                switch (randOption)
                {
                    case 0:
                        Destroy(redHoopPrefab);
                        break;
                    case 1:
                        Destroy(blueHoopPrefab);
                        break;
                    case 2:
                        Destroy(greenHoopPrefab);
                        break;
                }
            }
        }
    }

}
