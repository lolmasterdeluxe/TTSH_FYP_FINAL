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
    private List<Vector2> spawnedPositions;
    private float redhoopRadius, yellowhoopRadius, greenhoopRadius;
    private GameObject gameObjectRings;

    private Vector2 posDisplacement;
    private Vector2 posOrigin, newPos;
    private float timePassed;
    float randomDistance;

    private Vector2 redRingPos;

    // Start is called before the first frame update
    void Start()
    {

        redhoopRadius = redRingPrefab.GetComponent<Collider2D>().bounds.extents.x;
        yellowhoopRadius = yellowRingPrefab.GetComponent<Collider2D>().bounds.extents.x;
        greenhoopRadius = greenRingPrefab.GetComponent<Collider2D>().bounds.extents.x;
        spawnedPositions = new List<Vector2>();

        randomDistance = Random.Range(-6f, 6f);
        posDisplacement = new Vector2(randomDistance, 0);
        //posOrigin = transform.position;
    }

    private void Update()
    {
        LimitSpawnRings();
        //MoveableRings();
        //DestroyRingsAfterTime();
    }

    private void RandomRingSpawn()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            redhoopRadius = redRingPrefab.GetComponent<Collider2D>().bounds.max.x + 0.5f;
            yellowhoopRadius = yellowRingPrefab.GetComponent<Collider2D>().bounds.max.x + 0.5f;
            greenhoopRadius = greenRingPrefab.GetComponent<Collider2D>().bounds.max.x + 0.5f;

            for (int i = 0; i < 2; i++)
            {
                randX = Random.Range(skySpriteWidth.bounds.min.x + 0.5f, skySpriteWidth.bounds.max.x - 0.5f);
                randY = Random.Range(skySpriteHeight.bounds.min.y + 3f, skySpriteHeight.bounds.max.y - 0.5f);
                randOption = Random.Range(0, 3);
                spawnPos = new Vector2(randX, randY);

                Collider2D colliderWithRedRing = Physics2D.OverlapCircle(spawnPos, redhoopRadius, LayerMask.GetMask("RedRingLayer"));
                Collider2D colliderWithYellowRing = Physics2D.OverlapCircle(spawnPos, yellowhoopRadius, LayerMask.GetMask("YellowRingLayer"));
                Collider2D colliderWithGreenRing = Physics2D.OverlapCircle(spawnPos, greenhoopRadius, LayerMask.GetMask("GreenRingLayer"));

                if (!colliderWithRedRing && !colliderWithYellowRing && !colliderWithGreenRing)
                    spawnedPositions.Add(spawnPos);

                if (spawnedPositions[i].x != randX && spawnedPositions[i].y != randY)
                {
                    if (colliderWithRedRing == false && colliderWithYellowRing == false && colliderWithGreenRing == false)
                    {
                        GameObject temp;
                        timePassed += Time.deltaTime;

                        switch (randOption)
                        {
                            case 0:
                                temp = Instantiate(redRingPrefab, spawnPos, Quaternion.identity);
                                
                                // Here shld be moving the rings in the x-axis using the posDisplacement
                                newPos = Vector2.Lerp(spawnPos + posDisplacement, spawnPos, Mathf.PingPong(timePassed, 1));

                                gameObjectRings = temp;
                                posOrigin = gameObjectRings.transform.position;
                                Debug.Log("Red1 " + posOrigin);
                                break;
                            case 1:
                                temp = Instantiate(yellowRingPrefab, spawnPos, Quaternion.identity);

                                // Here shld be moving the rings in the x-axis using the posDisplacement
                                newPos = Vector2.Lerp(spawnPos + posDisplacement, spawnPos, Mathf.PingPong(timePassed, 1));

                                gameObjectRings = temp;
                                posOrigin = gameObjectRings.transform.position;
                                Debug.Log("Yellow1 " + posOrigin);
                                break;
                            case 2:
                                temp = Instantiate(greenRingPrefab, spawnPos, Quaternion.identity);

                                // Here shld be moving the rings in the x-axis using the posDisplacement
                                newPos = Vector2.Lerp(spawnPos + posDisplacement, spawnPos, Mathf.PingPong(timePassed, 1));

                                gameObjectRings = temp;
                                posOrigin = gameObjectRings.transform.position;
                                Debug.Log("Green1 " + posOrigin);
                                break;
                        }

                        spawnedPositions.RemoveAt(spawnedPositions.Count - 1);
                        spawnedPositions.Add(gameObjectRings.transform.position);

                        // Testing code to see if works outside of switch case
                        /*if (gameObjectRings.CompareTag("RedRing"))
                        {
                            newPos = gameObjectRings.transform.position;
                            newPos = Vector2.Lerp(posOrigin + posDisplacement, posOrigin, Mathf.PingPong(timePassed, 1));


                        }
                        else if (gameObjectRings.CompareTag("YellowRing"))
                        {
                            newPos = gameObjectRings.transform.position;
                            newPos = Vector2.Lerp(posOrigin + posDisplacement, posOrigin, Mathf.PingPong(timePassed, 1));

                        }
                        else if (gameObjectRings.CompareTag("GreenRing"))
                        {
                            newPos = gameObjectRings.transform.position;
                            newPos = Vector2.Lerp(posOrigin + posDisplacement, posOrigin, Mathf.PingPong(timePassed, 1));

                        }*/
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

    private void MoveableRings()
    {
        timePassed += Time.deltaTime;

        if(redRingPrefab.CompareTag("RedRing"))
        {
            redRingPos = redRingPrefab.transform.position;
            Debug.Log(redRingPos);
            //posOrigin = Vector2.Lerp(redRingPos + posDisplacement, redRingPos, Mathf.PingPong(timePassed, 1));
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
