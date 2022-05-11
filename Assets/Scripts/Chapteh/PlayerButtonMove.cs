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

    [SerializeField] private float playerSpeed = 5;
    [Tooltip("Reference to the Dpad buttons")]
    [SerializeField] private GameObject dPad, Joystick, Left, Right;
    [HideInInspector] public Vector2 movement;
    [HideInInspector] public bool b_playerisRight = true;
    public Vector2 Position;

    [SerializeField] private ParticleSystem sandDust;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private Chapteh chapteh;

    private CustomizerManager customizer;
    private Animator playerAnim;
    private float kickTime = 0;

    public bool isRunning = false;

    public JoystickMovement movementJoystick;
    #endregion

    #region Unity Callbacks

    public void Start()
    {
        //get references HERE
        rb = GetComponent<Rigidbody2D>();
        //set values HERE
        kickTime = 0;
        playerAnim = GetComponent<Animator>();
        customizer = FindObjectOfType<CustomizerManager>();

        if (customizer.ControlPreference == 0)
        {
            dPad.SetActive(true);
            Joystick.SetActive(false);
        }
        else if (customizer.ControlPreference == 1)
        {
            dPad.SetActive(true);
            Joystick.SetActive(false);
        }
        else if (customizer.ControlPreference == 2)
        {
            dPad.SetActive(false);
            Joystick.SetActive(true);
        }
    }

    private void Update()
    {
        Position = rb.position;
        if (!ChaptehGameManager.Instance.m_gameStarted)
            return;
        else if (ChaptehGameManager.Instance.m_gameEnded)
        {
            // Stops the animation and particle effects when game ends
            Left.transform.localScale = new Vector3(1, 1, 1);
            Right.transform.localScale = new Vector3(1, 1, 1);
            movement.x = 0;
            Joystick.SetActive(false);
            playerAnim.enabled = false;
            DisppearSandDust();
            return;
        }
        kickTime -= Time.deltaTime;
        PlayerSpriteAnimation();
    }

    private void FixedUpdate()
    {
        //we do the movement of the player HERE
        if (dPad.activeInHierarchy)
            rb.MovePosition(rb.position + movement * playerSpeed * Time.fixedDeltaTime);
        else if (Joystick.activeInHierarchy)
        {
            if (movementJoystick.joystickVec.x != 0)
            {
                rb.velocity = new Vector2(movementJoystick.joystickVec.x * playerSpeed, 0);
                footstepsSFX.Play();
            }
            else
            {
                rb.velocity = Vector2.zero;
                footstepsSFX.Stop();
            }
        }
    }


    #endregion

    #region Functions
    public void PlayerMovementFunction(string dir)
    {
        //add whatever movement-based code HERE
        if (dir == "Left")
        {
            footstepsSFX.Play();
            movement.x = -1;
            Left.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
        }
        if (dir == "Right")
        {
            footstepsSFX.Play();
            movement.x = 1;
            Right.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
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
            if (movement.x > 0 || rb.velocity.x > 0)
            {
                // Sets the sprite to original position
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                // Flips the sprite to be inverted
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
    }

    private void PlayerSpriteAnimation()
    {
        if (movement.magnitude == 0 && rb.velocity.x == 0) // If input is not detected
        {
            playerAnim.SetBool("PlayerRun", false);

            isRunning = false;
            DisppearSandDust();

            footstepsSFX.Stop();
        }
        else
        {
            if (!isRunning)
            {
                playerAnim.SetBool("PlayerRun", true);
                isRunning = true;
            }
            kickTime = 0.1f;
            // Calls func for player sprite to flip
            SpriteFlip();
            FlipSandDust();
        }

        // When chapteh is in the air, do nothing
        if (chapteh.inPlay && isRunning)
        {
            if (!playerAnim.GetCurrentAnimatorStateInfo(0).IsName("player_kick_new"))
                playerAnim.SetBool("PlayerRun", true);
            return;
        }
        else
        {
            // When chapteh is at the player, play kick animation
            if (Input.GetMouseButtonUp(0) && kickTime < 0)
            {
                playerAnim.SetBool("PlayerRun", false);
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
        if (transform.rotation == Quaternion.Euler(0, 180, 0))
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
