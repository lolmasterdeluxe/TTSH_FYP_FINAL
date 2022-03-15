using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyboardMovement : MonoBehaviour
{
    //this script handles player movement in the main menu game scene
    //keyboard method

    #region Variables

    [Tooltip("Reference to the Player's Rigidbody")]
    Rigidbody2D playerRB2D;

    [Tooltip("Reference to the Tutorial Screen Manager script")]
    TutorialScreenManager tutorialScreenmanagerInstance;

    //player speed
    [SerializeField] private float playerSpeed;

    //vector variables
    public Vector2 playerMovement;
    public static Vector3 v3_playerRollbackPosition;

    //bool variables
    public static bool b_doRollbackPosition = false;
    [HideInInspector] public bool b_playerisRight = true;
    private bool playerCollides;

    //AudioSource variables
    [SerializeField] private AudioSource footstepsSFX;

    #endregion

    #region Unity Callbacks

    public void Start()
    {
        if (b_doRollbackPosition)
        {
            //store the rollback position as the current position of the player
            transform.position = v3_playerRollbackPosition;
            b_doRollbackPosition = false;
        }

        //get references HERE
        playerRB2D = GetComponent<Rigidbody2D>();

        //get script references HERE
        tutorialScreenmanagerInstance = FindObjectOfType<TutorialScreenManager>();

        //set the player speed on START
        playerSpeed = 5f;

    }

    private void Update()
    {
        if (tutorialScreenmanagerInstance.b_tutorialScreenOpen == true)
            return;

        //call functions HERE
        PlayerMovement();

        if (playerMovement.x < 0 && b_playerisRight) //set bool to be false
            b_playerisRight = false;
        else if (playerMovement.x > 0 && !b_playerisRight) //set bool to be true
            b_playerisRight = true;

        //we use this to check for sound UPDATES
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) 
            || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            //we activate the audio sound
            footstepsSFX.volume = 0.75f;
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A)
            || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            //we mute the audio sound
            footstepsSFX.volume = 0f;
        }

    }

    private void FixedUpdate()
    {
        //update player movement HERE
        playerRB2D.MovePosition(playerRB2D.position + playerMovement * playerSpeed * Time.fixedDeltaTime);
    }

    #endregion

    #region Functions

    public void PlayerMovement()
    {
        playerMovement.x = Input.GetAxisRaw("Horizontal");
        playerMovement.y = Input.GetAxisRaw("Vertical");
    }

    public void SetRollbackPosition(Vector2 collisionPosition)
    {
        float rollbackBounceOffDistanceMultiplier = 0.5f;
        b_doRollbackPosition = true;
        v3_playerRollbackPosition = (transform.position - new Vector3(collisionPosition.x, collisionPosition.y, transform.position.z)).normalized * rollbackBounceOffDistanceMultiplier + transform.position;
    }

    #endregion

}
