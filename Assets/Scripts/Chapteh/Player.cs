using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    private float playerWidth;
    public SpriteRenderer skyWidth;
    public Vector2 playerPosition = new Vector2(0f, 0f);
    public float moveSpeed = 0.1f;

    private Vector3 mousePosition;

    public SpriteRenderer playerSprite;

    Animator playerAnim;
    private Chapteh chapteh;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Gets the size of the player width
        playerWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        chapteh = GameObject.Find("Chapteh").GetComponent<Chapteh>();

        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from mouse control
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Calls func for player sprite to flip
        //SpriteFlip();

        PlayerSpriteAnimation();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(playerPosition);

        // Moves player with deltaTime
        playerPosition = Vector2.Lerp(transform.position, mousePosition, moveSpeed * Time.deltaTime);
        // Clamp player within the boundaries of the background
        playerPosition.x = Mathf.Clamp(playerPosition.x, skyWidth.bounds.min.x + playerWidth, skyWidth.bounds.max.x - playerWidth);
    }

    private void SpriteFlip()
    {
        // Gets value for mouse position of x
        float worldPosX = mousePosition.x;
        if (worldPosX > gameObject.transform.position.x)
            // Sets the sprite to original position
            playerSprite.flipX = false;
        else
            // Flips the sprite to be inverted
            playerSprite.flipX = true;
    }

    private void PlayerSpriteAnimation()
    {
        if (Input.GetAxis("Mouse X") == 0 && Input.GetAxis("Mouse Y") == 0)
        {
            playerAnim.SetBool("PlayerIdle", true);
            playerAnim.SetBool("PlayerRun", false);
        }
        else if (Input.GetAxis("Mouse X") != 0 && Input.GetAxis("Mouse Y") != 0)
        {
            playerAnim.SetBool("PlayerRun", true);
            playerAnim.SetBool("PlayerIdle", false);
            SpriteFlip();
        }

        if (chapteh.isKicked == true)
        {
            playerAnim.SetTrigger("PlayerKick");
        }
    }
}
