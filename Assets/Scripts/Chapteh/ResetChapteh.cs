using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetChapteh : MonoBehaviour
{
    public Transform chapteh;

    public MouseMovement playerPoint;

    private Rigidbody2D rb;
    public float downScaler;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //chapteh.position = new Vector3(chapteh.position.x, 4.43f, chapteh.position.z);

        //chapteh.position = playerPoint.playerPosition;

        rb.gravityScale = downScaler;
    }

    private void Update()
    {
        chapteh.position = playerPoint.playerPosition;
    }
}
