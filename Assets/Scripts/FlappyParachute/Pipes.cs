using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    public float speed = 5f;
    private float leftEdgeofscreen;

    private void Start()
    {
        leftEdgeofscreen = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 5f;
    }

    private void Update()
    {
        if (!FlappyGameManager.Instance.m_gameStarted || FlappyGameManager.Instance.m_gameEnded)
            return;
        transform.position += Vector3.left * speed * Time.deltaTime;

        if(transform.position.x < leftEdgeofscreen)
        {
            Destroy(gameObject);
        }
    }


}
