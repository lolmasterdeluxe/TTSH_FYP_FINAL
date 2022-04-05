using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    private float initialSpeed;
    private float leftEdgeofscreen;
    private bool AddSpeed = false;

    private void Start()
    {
        leftEdgeofscreen = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 5f;
        initialSpeed = speed;
    }

    private void Update()
    {
        if (!FlappyGameManager.Instance.m_gameStarted || FlappyGameManager.Instance.m_gameEnded)
            return;

        speed = initialSpeed + FlappyGameManager.Instance.SpeedMultiplier;
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < leftEdgeofscreen)
        {
            Destroy(gameObject);
        }
    }

    private void PointsScale()
    {
        if (ScoreManager.Instance.GetCurrentGameScore() % 10 == 0 && AddSpeed)
        {
            speed += ScoreManager.Instance.GetCurrentGameScore() / 10;
            AddSpeed = false;
        }
        else if (ScoreManager.Instance.GetCurrentGameScore() % 10 != 0)
            AddSpeed = true;
    }


}
