using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    [SerializeField]
    private GameObject Tree, Mynah, Pipes;
    private bool mynahSpawned = false, treeSpawned = false;
    private float mynahSpawnRate, treeSpawnRate, pipeSpawnRate = 1.2f;
    [SerializeField]
    private float mynahMinSpawnRate = 1f, mynahMaxSpawnRate = 3f, treeMinSpawnRate = 1f, treeMaxSpawnRate = 5f, mynahMinheight = 2f, mynahMaxheight = 4f;
/*
    private void OnEnable()
    {
        InvokeRepeating(nameof(spawnPipes), pipeSpawnRate, pipeSpawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(spawnPipes));
    }
*/
    private void Update()
    {
        mynahSpawnRate -= Time.deltaTime;
        treeSpawnRate -= Time.deltaTime;
        if (mynahSpawned)
        {
            mynahSpawnRate = Random.Range(mynahMinSpawnRate, mynahMaxSpawnRate);
            mynahSpawned = false;
        }
        if (treeSpawned)
        {
            treeSpawnRate = Random.Range(treeMinSpawnRate, treeMaxSpawnRate);
            treeSpawned = false;
        }

        if (mynahSpawnRate <= 0f)
            spawnMynah();
        if (treeSpawnRate <= 0f)
            spawnTree();
    }

    private void spawnMynah()
    {
        if (!FlappyGameManager.Instance.m_gameStarted || FlappyGameManager.Instance.m_gameEnded)
            return;
        GameObject mynah = Instantiate(Mynah, transform.position, Quaternion.identity);
        mynah.transform.position += Vector3.up * Random.Range(mynahMinheight, mynahMaxheight);
        mynahSpawned = true;
    }

    private void spawnTree()
    {
        if (!FlappyGameManager.Instance.m_gameStarted || FlappyGameManager.Instance.m_gameEnded)
            return;
        GameObject tree = Instantiate(Tree, transform.position, Quaternion.identity);
        float tree_scale = Random.Range(0.75f, 1.25f);
        tree.transform.localScale = new Vector3(tree_scale, tree_scale, tree_scale);
        tree.transform.position = new Vector3(transform.position.x, transform.position.y - 4, transform.position.z);
        treeSpawned = true;
    }

   /* private void spawnPipes()
    {
        if (!FlappyGameManager.Instance.m_gameStarted || FlappyGameManager.Instance.m_gameEnded)
            return;
        Vector3 pipePos = new Vector3(transform.position.x, Random.Range(-2f, 2f), transform.position.z);
        GameObject pipes = Instantiate(Pipes, pipePos, Quaternion.identity);
    }*/
}
