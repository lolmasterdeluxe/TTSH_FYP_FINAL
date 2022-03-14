using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtonMove : MonoBehaviour
{
    //this script handles the movement of the player in the main menu game scene

    #region Variables

    private Rigidbody2D rb;

    //AudioSource variables
    [SerializeField] private AudioSource footstepsSFX;

    [SerializeField] private float playerSpeed;
    [Tooltip("Reference to the Dpad buttons")]
    [SerializeField] private GameObject Left, Right;
    [HideInInspector] public Vector2 movement;
    [HideInInspector] public bool b_playerisRight = true;

    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private ParticleSystem sandDust;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private Chapteh chapteh;

    private Animator playerAnim;
    private float kickTime;

    public bool isRunning = false;
    #endregion

    #region Unity Callbacks

    public void Start()
    {
        //get references HERE
        rb = GetComponent<Rigidbody2D>();
        //set values HERE
        playerSpeed = 5f;

        playerAnim = GetComponent<Animator>();
    }

    private void Update()
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

        PlayerSpriteAnimation();
    }

    private void FixedUpdate()
    {
        //we do the movement of the player HERE
        rb.MovePosition(rb.position + movement * playerSpeed * Time.fixedDeltaTime);
    }


    #endregion

    #region Functions
    public void PlayerMovementFunction(string dir)
    {
        //add whatever movement-based code HERE
        if (dir == "Left")
        {
            movement.x = -1;
            Left.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        }
        if (dir == "Right")
        {
            movement.x = 1;
            Right.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        }
    }

    public void OnPointerDown(string dir)
    {
        PlayerMovementFunction(dir);
    }
    public void OnPointerUp()
    {
        movement.Set(0, 0);
        Left.transform.localScale = new Vector3(1, 1, 1);
        Right.transform.localScale = new Vector3(1, 1, 1);
    }

    public void SpriteFlip()
    {
        if (!pauseMenu.isPaused)
        {
            if (movement.x > 0)
                // Sets the sprite to original position
                playerSprite.flipX = false;
            else
                // Flips the sprite to be inverted
                playerSprite.flipX = true;
        }
    }

    private void PlayerSpriteAnimation()
    {
        if (movement.magnitude == 0) // If mouse input is not detected
        {
            playerAnim.SetBool("PlayerRun", false);

            isRunning = false;
            DisppearSandDust();

            //runningSource.Stop();
        }
        else
        {
            if (!isRunning)
            {
                playerAnim.SetBool("PlayerRun", true);
                isRunning = true;
            }

            // Calls func for player sprite to flip
            SpriteFlip();
            FlipSandDust();
        }

        // When chapteh is in the air, do nothing
        if (chapteh.inPlay)
        {
            if (!playerAnim.GetCurrentAnimatorStateInfo(0).IsName("player_kick_new") && kickTime < 0)
                playerAnim.SetBool("PlayerRun", true);
            return;
        }
        else
        {
            // When chapteh is at the player, play kick animation
            if (Input.GetMouseButtonUp(0))
            {
                playerAnim.SetBool("PlayerRun", false);
                playerAnim.SetTrigger("PlayerKick");
                kickTime = 0.25f;
            }
        }

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
        if (playerSprite.flipX == true)
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


    #endregion
}
