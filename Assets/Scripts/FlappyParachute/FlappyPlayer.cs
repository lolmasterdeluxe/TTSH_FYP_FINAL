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

    public ParticleSystem particleSystem_;

    private void Start()
    {
        initialStength = strength;
        particleSystem_.playOnAwake = true;
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

        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Mynah" )
        {
            FlappyGameManager.Instance.GameOver();
        }
        else if(other.gameObject.tag == "Trees")
        {
            Debug.Log("Trees");
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
        else if (other.gameObject.tag == "Balloon")
        {
            FlappyGameManager.Instance.balloonScore(other.GetComponent<SpriteRenderer>());
            FlappyGameManager.Instance.audioSources[2].Play();
            Destroy(other.gameObject);
            Debug.Log("particle spawn");
            ParticleSystem particleEffect = new ParticleSystem();
            particleEffect = Instantiate(particleSystem_, other.transform.position, other.transform.rotation);
            Destroy(particleEffect, 2.0f);
            
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
