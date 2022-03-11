using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerTouchMovement : MonoBehaviour
{
    //this script handles the movement of the player in the main menu game scene

    #region Variables

    Rigidbody2D playerRB;
    Animator playerAC;

    [Tooltip("Reference to the Tutorial Screen Manager script")]
    [SerializeField] private TutorialScreenManager tutorialScreenmanagerInstance;

    [SerializeField] private float playerSpeed;

    //for mobile touch
    [Tooltip("Touch Position of Finger")]
    public Vector3 touchPosition, whereToMove;
    private Touch touch;
    [HideInInspector] public bool isMoving = false;

    float previousDistanceToTouchPos, currentDistanceToTouchPos;

    public bool b_playerisRight = true;

    #endregion

    #region Unity Callbacks

    public void Start()
    {
        //get references HERE
        playerRB = GetComponent<Rigidbody2D>();
        playerAC = GetComponent<Animator>();

        //set values HERE
        playerSpeed = 5f;
    }
    
    private void Update()
    {
        if (tutorialScreenmanagerInstance.b_tutorialScreenOpen == true)
            return;

        //PlayerMovementFunction();
        TouchAndGo();
        PlayerAnimationFunction();
    }

    private void FixedUpdate()
    {
        if (isMoving)
            playerRB.velocity = new Vector2(whereToMove.x * playerSpeed, whereToMove.y * playerSpeed);
    }


    #endregion

    #region Functions


    public void PlayerAnimationFunction()
    {
        //add whatever animation-based code HERE
        playerAC.SetFloat("PlayerSpeed", playerRB.velocity.sqrMagnitude);
    }

    //these functions for mobile
    private void TouchAndGo()
    {
        if (isMoving)
            currentDistanceToTouchPos = (touchPosition - transform.position).magnitude;

       /* if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                previousDistanceToTouchPos = 0;
                currentDistanceToTouchPos = 0;
                isMoving = true;
                touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0;
                whereToMove = (touchPosition - transform.position).normalized;
                playerRB.velocity = new Vector2(whereToMove.x * playerSpeed, whereToMove.y * playerSpeed);
            }
        }*/

        if (Input.GetMouseButtonDown(0))
        {
            previousDistanceToTouchPos = 0;
            currentDistanceToTouchPos = 0;
            isMoving = true;
            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchPosition.z = 1;
            touchPosition.y += 1.5f;
            whereToMove = (touchPosition - transform.position).normalized;
            if (touchPosition.x > transform.position.x)
                b_playerisRight = true;
            else
                b_playerisRight = false;
        }

        if (currentDistanceToTouchPos > previousDistanceToTouchPos)
        {
            isMoving = false;
            playerRB.velocity = Vector2.zero;
        }

        if (isMoving)
            previousDistanceToTouchPos = (touchPosition - transform.position).magnitude;
    }
    #endregion
}
