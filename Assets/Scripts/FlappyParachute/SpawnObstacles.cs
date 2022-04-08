using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    [SerializeField]
    private GameObject Tree, Mynah, Pipes, Balloon, PasirTree, TrumpetTree;
    private bool mynahSpawned = false, treeSpawned = false, balloonSpawned = false, PasirTreespawned = false, TrumpetTreeSpawned = false;
    private float mynahSpawnRate, treeSpawnRate, pipeSpawnRate = 1.2f, balloonSpawnRate, PasirTreeSpawnRate,TrumpetTreeSpawnRate;
    [SerializeField]
    private float mynahMinSpawnRate = 1f, mynahMaxSpawnRate = 3f, treeMinSpawnRate = 1f, treeMaxSpawnRate = 5f, mynahMinheight = 2f, mynahMaxheight = 4f;
    [SerializeField]
    private float PasirTreeMinSpawnRate = 1f, PasirTreeMaxSpawnRate = 5f, TrumpetTreeMinSpawnRate = 1f, TrumpetTreeMaxSpawnRate = 5f;
    [SerializeField]
    private float balloonMinSpawnRate = 1f, balloonMaxSpawnRate = 2f, balloonMinHeight = 1f,balloonMaxHeight = 4f;

    /*private void OnEnable()
    {
        InvokeRepeating(nameof(spawnPipes), pipeSpawnRate, pipeSpawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(spawnPipes));
    }*/

    private void Update()
    {
        mynahSpawnRate -= Time.deltaTime * FlappyGameManager.Instance.SpawnMultiplier;
        treeSpawnRate -= Time.deltaTime * FlappyGameManager.Instance.SpawnMultiplier;
        balloonSpawnRate -= Time.deltaTime * FlappyGameManager.Instance.SpawnMultiplier;
        PasirTreeSpawnRate -= Time.deltaTime * FlappyGameManager.Instance.SpawnMultiplier;
        TrumpetTreeSpawnRate -= Time.deltaTime * FlappyGameManager.Instance.SpawnMultiplier;

        if (mynahSpawned)
        {
            mynahSpawnRate = Random.Range(mynahMinSpawnRate, mynahMaxSpawnRate);
            mynahSpawned = false;
        }
        if (treeSpawned && TrumpetTreeSpawned == false && PasirTreespawned == false)
        {
            treeSpawnRate = Random.Range(treeMinSpawnRate, treeMaxSpawnRate);
            treeSpawned = false;
        }
        if(balloonSpawned)
        {
            balloonSpawnRate = Random.Range(balloonMinSpawnRate, balloonMaxSpawnRate);
            balloonSpawned = false;
        }
        if(PasirTreespawned)
        {
            PasirTreeSpawnRate = Random.Range(PasirTreeMinSpawnRate, PasirTreeMaxSpawnRate);
            PasirTreespawned = false;
        }
        if(TrumpetTreeSpawned)
        {
            TrumpetTreeSpawnRate = Random.Range(TrumpetTreeMinSpawnRate, TrumpetTreeMaxSpawnRate);
            TrumpetTreeSpawned = false;
        }

        if (mynahSpawnRate <= 0f)
            spawnMynah();
        if (treeSpawnRate <= 0f &&TrumpetTreeSpawned==false && PasirTreespawned == false)
            spawnTree();
        if (balloonSpawnRate <= 0f)
            spawnBalloon();
        if (PasirTreeSpawnRate <= 0f && treeSpawned == false && TrumpetTreeSpawned == false)
            spawnPasirTree();
        if (TrumpetTreeSpawnRate <= 0f && treeSpawned == false && PasirTreespawned == false)
            spawnTrumpetTree();
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
        Debug.Log("Spawning Regular Tree");
    }

    private void spawnPasirTree()
    {
        if (!FlappyGameManager.Instance.m_gameStarted || FlappyGameManager.Instance.m_gameEnded)
            return;
        GameObject PasirrTree = Instantiate(PasirTree, transform.position, Quaternion.identity);
        float PasirTreeScale = Random.Range(0.8f, 1.3f);
        PasirrTree.transform.localScale = new Vector3(PasirTreeScale, PasirTreeScale, PasirTreeScale);
        PasirrTree.transform.position = new Vector3(transform.position.x, transform.position.y -2, transform.position.z);
        PasirTreespawned = true;
        Debug.Log("Spawning Pasir Tree");
    }

    private void spawnTrumpetTree()
    {
        if (!FlappyGameManager.Instance.m_gameStarted || FlappyGameManager.Instance.m_gameEnded)
            return;
        GameObject Trumpettree = Instantiate(TrumpetTree, transform.position, Quaternion.identity);
        float Trumpettree_scale = Random.Range(0.75f, 1.25f);
        Trumpettree.transform.localScale = new Vector3(Trumpettree_scale, Trumpettree_scale, Trumpettree_scale);
        Trumpettree.transform.position = new Vector3(transform.position.x, transform.position.y -2, transform.position.z);
        TrumpetTreeSpawned = true;
        Debug.Log("Spawning Trumpet Tree");
    }

    private void spawnBalloon()
    {
        if (!FlappyGameManager.Instance.m_gameStarted || FlappyGameManager.Instance.m_gameEnded)
            return;
        GameObject balloon = Instantiate(Balloon, transform.position, Quaternion.identity);
        balloon.transform.position += Vector3.up * Random.Range(balloonMinHeight, balloonMaxHeight);
        balloonSpawned = true;
    }

   /* private void spawnPipes()
    {
        if (!FlappyGameManager.Instance.m_gameStarted || FlappyGameManager.Instance.m_gameEnded)
            return;
        Vector3 pipePos = new Vector3(transform.position.x, Random.Range(-2f, 2f), transform.position.z);
        GameObject pipes = Instantiate(Pipes, pipePos, Quaternion.identity);
    }*/
}
