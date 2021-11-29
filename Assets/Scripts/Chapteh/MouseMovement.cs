using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    private Vector3 mousePosition;
    public Vector2 playerPosition = new Vector2(0f, 0f);
    public float moveSpeed = 0.1f;
    Rigidbody2D rb;

    private float playerWidth;
    public SpriteRenderer skyWidth;

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
    }

    private void FixedUpdate()
    {
        rb.MovePosition(playerPosition);

        playerPosition = Vector2.Lerp(transform.position, mousePosition, moveSpeed * Time.deltaTime);
        playerPosition.x = Mathf.Clamp(playerPosition.x, skyWidth.bounds.min.x + playerWidth, skyWidth.bounds.max.x - playerWidth);
    }
}
