using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    private float playerWidth;
    public SpriteRenderer skyWidth;
    private Vector2 playerPosition = new Vector2(0f, 0f);
    public float moveSpeed;

    private Vector3 mousePosition;

    public SpriteRenderer playerSprite;

    Animator playerAnim;

    public ParticleSystem sandDust;
    private PauseMenu pauseMenu;
    private Chapteh chapteh;
    private float kickTime;

    public bool isRunning = false;

    Vector2 playermove;
    //public AudioSource runningSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Gets the size of the player width
        playerWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;

        playerAnim = GetComponent<Animator>();

        pauseMenu = GameObject.Find("Pause Manager").GetComponent<PauseMenu>();
        chapteh = GameObject.Find("Chapteh").GetComponent<Chapteh>();

        //runningSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ChaptehGameManager.Instance.m_gameStarted)
            return;
        else if (ChaptehGameManager.Instance.m_gameEnded)
        {
            // Stops the animation and particle effects when game ends
            playerAnim.enabled = false;
            DisppearSandDust();
            return;
        }

        // Get input from mouse control
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        playermove.x = Input.GetAxisRaw("Horizontal");
        playermove.y = Input.GetAxisRaw("Vertical");
        kickTime -= Time.deltaTime;

        PlayerSpriteAnimation();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(playerPosition);

        // Moves player with deltaTime
        //rb.MovePosition(;
        //playerPosition = Vector2.Lerp(transform.position, mousePosition, moveSpeed * Time.deltaTime);
        // playerPosition.x = x +( moveSpeed * Time.deltaTime *10 );   
        playerPosition += playermove * moveSpeed * Time.fixedDeltaTime;

        // Clamp player within the boundaries of the background
        playerPosition.x = Mathf.Clamp(playerPosition.x, skyWidth.bounds.min.x + playerWidth - 0.6f, skyWidth.bounds.max.x - playerWidth + 0.6f);
    }

    public void SpriteFlip()
    {
        if (!pauseMenu.isPaused)
        {
            // Gets value for mouse position of x
            //float worldPosX = mousePosition.x;
            //if (worldPosX > gameObject.transform.position.x) 
            if (Input.GetKeyDown("d"))
                // Sets the sprite to original position
                playerSprite.flipX = false;
            else if (Input.GetKeyDown("a"))
                // Flips the sprite to be inverted
                playerSprite.flipX = true;
        }
    }

    private void PlayerSpriteAnimation()
    {
        if (!chapteh.inPlay)
        {
            if (Input.GetMouseButtonUp(0))
            {
                playerAnim.SetBool("PlayerRun", false);
                playerAnim.SetTrigger("PlayerKick");
                kickTime = 0.25f;
            }
            else if (playermove.magnitude == 0 && kickTime <= 0) // If mouse input is not detected
            {
                playerAnim.SetBool("PlayerRun", false);
                isRunning = false;
                DisppearSandDust();

                //runningSource.Stop();
            }
            //else if (Input.GetAxis("Mouse X") != 0 && Input.GetAxis("Mouse Y") != 0) // If mouse input is detected
            else if (playermove.magnitude != 0 && kickTime <= 0)
            {
                if (!isRunning)
                {
                    playerAnim.SetBool("PlayerRun", true);
                    isRunning = true;
                    FlipSandDust();
                }

                // Calls func for player sprite to flip
                //runningSource.Play();
            }

        }
        // When chapteh is in the air, do nothing
        if (chapteh.inPlay && kickTime <= 0)
        {
            if (playermove.magnitude != 0)
            {
                playerAnim.SetBool("PlayerRun", true);
                FlipSandDust();
            }
            else
            {
                playerAnim.SetBool("PlayerRun", false);
                DisppearSandDust();
            }
        }

        SpriteFlip();
    }

    private void CreateSandDust()
    {
        sandDust.Play();
    }

    private void DisppearSandDust()
    {
        sandDust.Stop();
    }

    private void FlipSandDust()
    {
        if (playerSprite.flipX)
        {
            sandDust.transform.rotation = Quaternion.Euler(0f, 5f, 0f);
            CreateSandDust();
        }
        else
        {
            sandDust.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            CreateSandDust();
        }
    }
}
