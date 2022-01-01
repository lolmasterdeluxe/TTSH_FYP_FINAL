using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    //this script handles the movement of the player in the main menu game scene

    #region Variables

    Rigidbody2D playerRB;
    Animator playerAC;

    public float playerSpeed;
    public static bool doRollbackPosition = false;
    public static Vector3 rollbackPosition;
    public Vector2 movement, prevMovement;

    public bool m_isRight = true;
    #endregion

    #region Unity Callbacks

    private void Start()
    {
        if (doRollbackPosition)
        {
            transform.position = rollbackPosition;
            doRollbackPosition = false;
        }

        //get references HERE
        playerRB = GetComponent<Rigidbody2D>();
        playerAC = GetComponent<Animator>();

        //set values HERE
        playerSpeed = 5f;
    }
    
    private void Update()
    {
        if (movement.sqrMagnitude > 0)
        {
            prevMovement = movement;
        }

        PlayerMovementFunction();
        PlayerAnimationFunction();

        if (movement.x < 0 && m_isRight)
            m_isRight = false;
        else if (movement.x > 0 && !m_isRight)
            m_isRight = true;
    }

    private void FixedUpdate()
    {
        //we do the movement of the player HERE
        playerRB.MovePosition(playerRB.position + movement * playerSpeed * Time.fixedDeltaTime);
    }


    #endregion

    #region Functions

    public void PlayerMovementFunction()
    {
        //add whatever movement-based code HERE

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    public void PlayerAnimationFunction()
    {
        //add whatever animation-based code HERE
        playerAC.SetFloat("XPosition", movement.x);
        playerAC.SetFloat("YPosition", movement.y);
        playerAC.SetFloat("PlayerSpeed", movement.sqrMagnitude);
        playerAC.SetFloat("PreviousXPosition", prevMovement.x);
        playerAC.SetFloat("PreviousYPosition", prevMovement.y);
    }

    public void SetRollbackPosition(Vector2 collisionPosition)
    {
        float rollbackBounceOffDistanceMultiplier = 1.05f;
        doRollbackPosition = true;
        rollbackPosition = (transform.position - new Vector3(collisionPosition.x, collisionPosition.y, transform.position.z)).normalized * rollbackBounceOffDistanceMultiplier + transform.position;
    }

    #endregion

}
