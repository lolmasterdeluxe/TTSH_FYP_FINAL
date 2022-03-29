using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPipes : MonoBehaviour
{
    public GameObject prefab;
    public float spawnRate = 1f;
    public float minheight = -1f;
    public float maxheight = 1f;

    private void OnEnable()
    {
        InvokeRepeating(nameof(spawnPipes), spawnRate, spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(spawnPipes));
    }

    private void spawnPipes()
    {
        GameObject pipes = Instantiate(prefab, transform.position, Quaternion.identity);
        pipes.transform.position += Vector3.up * Random.Range(minheight, maxheight);
    }
}
