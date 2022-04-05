using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyPlayer : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField]
    private float gravity =- 9.8f;
    [SerializeField]
    private float strength = 5f;
    private float initialStength = 0;

    private void Start()
    {
        initialStength = strength;
    }

    private void Update()
    {
        if (!FlappyGameManager.Instance.m_gameStarted || FlappyGameManager.Instance.m_gameEnded)
            return;
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * strength;
            FlappyGameManager.Instance.audioSources[3].Play();
        }

/*        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                direction = Vector3.up * strength;
            }
        }
*/
        direction.y += gravity * Time.deltaTime;
        transform.position += direction*Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            FlappyGameManager.Instance.GameOver();
        }
        else if (other.gameObject.tag == "SafeObstacle")
        {
            strength = 0;
        }
        else if (other.gameObject.tag =="Scoring")
        {
            FlappyGameManager.Instance.increaseScore();
            FlappyGameManager.Instance.audioSources[2].Play();
        }
        else if(other.gameObject.tag == "Balloon")
        {
            FlappyGameManager.Instance.balloonScore();
            FlappyGameManager.Instance.audioSources[2].Play();
            Destroy(other.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "SafeObstacle")
        {
            strength = initialStength;
        }
    }
}
