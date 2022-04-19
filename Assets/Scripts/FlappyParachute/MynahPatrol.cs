using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MynahPatrol : MonoBehaviour
{
    [SerializeField]
    private float minSpeed, maxSpeed, minHeightRand, maxHeightRand;
    private float speed, initialSpeed, minHeight, maxHeight;
    private bool AddSpeed = false, rise = false;

    private void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        initialSpeed = speed;
        minHeight = Random.Range(minHeightRand, 0);
        maxHeight = Random.Range(0, maxHeightRand);
        minHeight = transform.position.y + minHeight;
        maxHeight = transform.position.y + maxHeight;
    }

    private void Update()
    {
        if (!FlappyGameManager.Instance.m_gameStarted || FlappyGameManager.Instance.m_gameEnded)
            return;

        speed = initialSpeed + FlappyGameManager.Instance.SpeedMultiplier;

        if (transform.position.y >= maxHeight)
            rise = false;
        else if (transform.position.y <= minHeight)
            rise = true;

        if (rise)
            transform.position += Vector3.up * speed * Time.deltaTime;
        else
            transform.position -= Vector3.up * speed * Time.deltaTime;
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
