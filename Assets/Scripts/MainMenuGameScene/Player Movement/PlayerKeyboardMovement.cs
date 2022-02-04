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

    [Tooltip("Reference to the Animator attached to the Player")]
    Animator playerAC;

    [Tooltip("Reference to the Tutorial Screen Manager script")]
    TutorialScreenManager tutorialscreenmanagerInstance;

    //player speed
    public float f_player2DSpeed;

    //vector variables
    public Vector2 v2_playerMovement, v2_playerPrevMovement;
    public static Vector3 v3_playerRollbackPosition;

    //bool variables
    public static bool b_doRollbackPosition = false;
    public bool b_playerisRight = true;
    bool playerisBehind;

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
        playerAC = GetComponent<Animator>();

        //get script references HERE
        tutorialscreenmanagerInstance = FindObjectOfType<TutorialScreenManager>();

        //set the player speed on START
        f_player2DSpeed = 5f;

    }

    private void Update()
    {
        if (tutorialscreenmanagerInstance.b_tutorialScreenOpen == true)
            return;

        if (v2_playerMovement.sqrMagnitude > 0)
        {
            v2_playerPrevMovement = v2_playerMovement;
        }

        //call functions HERE
        DOPlayerMovement();
        DOPlayerAnimation();

        if (v2_playerMovement.x < 0 && b_playerisRight) //set bool to be false
            b_playerisRight = false;
        else if (v2_playerMovement.x > 0 && !b_playerisRight) //set bool to be true
            b_playerisRight = true;
    }

    private void FixedUpdate()
    {
        //update player movement HERE
        playerRB2D.MovePosition(playerRB2D.position + v2_playerMovement * f_player2DSpeed * Time.fixedDeltaTime);
    }

    #endregion

    #region Functions

    public void DOPlayerMovement()
    {
        v2_playerMovement.x = Input.GetAxisRaw("Horizontal");

        if (playerisBehind == true)
        {
            v2_playerMovement.y = 0f;
        }
        else
        {
            v2_playerMovement.y = Input.GetAxisRaw("Vertical");
        }
    }

    public void DOPlayerAnimation()
    {
        playerAC.SetFloat("XPosition", v2_playerMovement.x);
        playerAC.SetFloat("YPosition", v2_playerMovement.y);
        playerAC.SetFloat("PlayerSpeed", v2_playerMovement.sqrMagnitude);
        playerAC.SetFloat("PreviousXPosition", v2_playerMovement.x);
        playerAC.SetFloat("PreviousYPosition", v2_playerMovement.y);
    }

    public void SetRollbackPosition(Vector2 collisionPosition)
    {
        float rollbackBounceOffDistanceMultiplier = 0.5f;
        b_doRollbackPosition = true;
        v3_playerRollbackPosition = (transform.position - new Vector3(collisionPosition.x, collisionPosition.y, transform.position.z)).normalized * rollbackBounceOffDistanceMultiplier + transform.position;
    }

    #endregion

    #region Trigger Collider Functions

    private void OnTriggerEnter2D(Collider2D other)
    {
        //firstly, we check the tag of the collided gameObject
        if (other.gameObject.tag == "NPC")
        {
            //we do a distance check between the player and the collided gameObject
            float distance = this.gameObject.transform.localPosition.y - other.gameObject.transform.localPosition.y;

            Debug.Log("Distance Enter: " + distance);

            if ((distance >= 0.15f && distance <= 0.25f)) //player is behind
            {
                playerisBehind = true;
            }
            else if (distance <= -0.33f)
            {
                playerisBehind = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //firstly, we check the tag of the collided gameObject
        if (other.gameObject.tag == "NPC")
        {
            //we do a distance check between the player and the collided gameObject
            float distance = this.gameObject.transform.localPosition.y - other.gameObject.transform.localPosition.y;

            //Debug.Log("Distance Stay: " + distance);

            if ((distance >= 0.15f && distance <= 0.25f)) //player is behind
            {
                playerisBehind = true;
            }
            else if (distance <= -0.33f)
            {
                playerisBehind = true;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "NPC")
        {
            playerisBehind = false;
        }
    }


    #endregion

}
