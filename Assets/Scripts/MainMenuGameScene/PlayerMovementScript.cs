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
    Vector2 movement, prevMovement;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        //get references HERE
        playerRB = GetComponent<Rigidbody2D>();
        playerAC = GetComponent<Animator>();

        //set values HERE
        playerSpeed = 5f;
    }

    private void Update()
    {
        if (prevMovement.sqrMagnitude > 0)
        {
            prevMovement = movement;
        }

        PlayerMovementFunction();
        PlayerAnimationFunction();

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
    }



    #endregion

}
