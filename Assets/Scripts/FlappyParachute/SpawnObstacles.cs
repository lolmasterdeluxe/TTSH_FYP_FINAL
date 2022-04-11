using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpawnObstacles : MonoBehaviour
{
    [SerializeField]
    private GameObject Tree, Mynah, Pipes, Balloon;
    [SerializeField]
    private Sprite PasirTree, TrumpetTree;
    private bool mynahSpawned = false, treeSpawned = false, balloonSpawned = false;
    private float mynahSpawnRate, treeSpawnRate, pipeSpawnRate = 1.2f, balloonSpawnRate;
    [SerializeField]
    private float mynahMinSpawnRate = 1f, mynahMaxSpawnRate = 3f, treeMinSpawnRate = 1f, treeMaxSpawnRate = 5f, mynahMinheight = 2f, mynahMaxheight = 4f;
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
        if (balloonSpawned)
        {
            balloonSpawnRate = Random.Range(balloonMinSpawnRate, balloonMaxSpawnRate);
            balloonSpawned = false;
        }

        if (mynahSpawnRate <= 0f)
            spawnMynah();
        if (treeSpawnRate <= 0f)
            spawnTree();
        if (balloonSpawnRate <= 0f)
            spawnBalloon();
    }

    private void spawnMynah()
    {
        if (!FlappyGameManager.Instance.m_gameStarted || FlappyGameManager.Instance.m_gameEnded)
            return;
        GameObject mynah = ObjectPooling.SharedInstance.GetPooledObject("Mynah");
        if (mynah != null)
        {
            mynah.transform.position = transform.position;
            mynah.transform.rotation = Quaternion.identity;
            mynah.transform.position += Vector3.up * Random.Range(mynahMinheight, mynahMaxheight);
            mynah.SetActive(true);
            mynahSpawned = true;
        }
        //GameObject mynah = Instantiate(Mynah, transform.position, Quaternion.identity);
        //mynah.transform.position += Vector3.up * Random.Range(mynahMinheight, mynahMaxheight);
        //mynahSpawned = true;
    }

    private void spawnTree()
    {
        if (!FlappyGameManager.Instance.m_gameStarted || FlappyGameManager.Instance.m_gameEnded)
            return;
       // GameObject tree = Instantiate(Tree, transform.position, Quaternion.identity);
        GameObject tree = ObjectPooling.SharedInstance.GetPooledObject("Trees");
        if (tree != null)
        {
            tree.SetActive(true);
        }
        int tree_type = Random.Range(0, 3);
        if (tree_type == 1)
            tree.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = PasirTree;
        else if (tree_type == 2)
            tree.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = TrumpetTree;

        float tree_scale = Random.Range(0.75f, 1.25f);
        tree.transform.localScale = new Vector3(tree_scale, tree_scale, tree_scale);
        tree.transform.position = new Vector3(transform.position.x, transform.position.y - 4, transform.position.z);
        treeSpawned = true;
    }

    private void spawnBalloon()
    {
        if (!FlappyGameManager.Instance.m_gameStarted || FlappyGameManager.Instance.m_gameEnded)
            return;
        GameObject balloon = Instantiate(Balloon, transform.position, Quaternion.identity);

        int balloonTypeRand = Random.Range(0, 6);

        if (balloonTypeRand >= 0 && balloonTypeRand <= 2)
            balloon.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
        else if (balloonTypeRand >= 3 && balloonTypeRand <= 4)
            balloon.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.yellow;
        else if (balloonTypeRand == 5)
            balloon.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.blue;

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
