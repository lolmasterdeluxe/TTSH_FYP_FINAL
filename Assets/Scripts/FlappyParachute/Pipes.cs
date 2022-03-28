using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    public float speed = 5f;
    private float leftEdgeofscreen;

    private void Start()
    {
        leftEdgeofscreen = Camera.main.ScreenToWorldPoint(Vector3.zero).x-5f;

    }

    private void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if(transform.position.x<leftEdgeofscreen)
        {
            Destroy(gameObject);
        }
    }


}
