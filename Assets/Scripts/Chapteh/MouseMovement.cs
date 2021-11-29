using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public Vector3 mousePosition;
    public Vector2 playerPosition = new Vector2(0f, 0f);
    public float moveSpeed = 0.1f;
    Rigidbody2D rb;

    private float playerWidth;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        playerWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        playerPosition = Vector2.Lerp(transform.position, mousePosition, moveSpeed);

        ClampMovement();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(playerPosition);
    }

    private void ClampMovement()
    {
        Vector3 position = transform.position;

        float distance = transform.position.z - Camera.main.transform.position.z;

        float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + playerWidth;
        float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance)).x - playerWidth;

        position.x = Mathf.Clamp(position.x, leftBorder, rightBorder);
        transform.position = position;
    }
}
