using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerTouchMovement : MonoBehaviour
{
    //this script handles the movement of the player in the main menu game scene

    #region Variables

    private Rigidbody2D playerRB;

    [Tooltip("Reference to the Tutorial Screen Manager script")]
    [SerializeField] private TutorialScreenManager tutorialScreenmanagerInstance;

    [SerializeField] private float playerSpeed;
    [SerializeField] private Transform touchFeedback;

    //for mobile touch
    [Tooltip("Touch Position of Finger")]
    public Vector3 touchPosition, whereToMove, location;
    [HideInInspector] public bool isMoving = false;
    private bool CanTouch = true, isEnabled = true;

    float previousDistanceToTouchPos, currentDistanceToTouchPos;

    public bool b_playerisRight = true;

    //AudioSource variables
    [SerializeField] private AudioSource footstepsSFX;

    #endregion

    #region Unity Callbacks

    public void Start()
    {
        //get references HERE
        playerRB = GetComponent<Rigidbody2D>();

        //set values HERE
        playerSpeed = 5f;
    }
    
    private void Update()
    {
        if (tutorialScreenmanagerInstance.b_tutorialScreenOpen)
            return;

        //PlayerMovementFunction();
        TouchAndGo();
    }

    private void FixedUpdate()
    {
        if (isMoving)
            playerRB.velocity = new Vector2(whereToMove.x * playerSpeed, whereToMove.y * playerSpeed);
    }


    #endregion

    #region Functions

    public void SetTouch(bool move)
    {
        CanTouch = move;
    }
    public void SetEnabled(bool isTrue)
    {
        isEnabled = isTrue;
    }

    //these functions for mobile
    private void TouchAndGo()
    {
        touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        touchPosition.z = 1;
        touchPosition.y += 1.5f;
        if ((touchPosition.x < -19.2 || touchPosition.x > 19 || touchPosition.y > 0.15 || touchPosition.y < -4 || !CanTouch || !isEnabled) && !isMoving)
            return;

        if (isMoving)
        {
            currentDistanceToTouchPos = (location - transform.position).magnitude;
            footstepsSFX.volume = 0.75f;
        }

        if (Input.GetMouseButtonDown(0))
        {
            touchFeedback.gameObject.GetComponent<SpriteRenderer>().DORestart(false);
            touchFeedback.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
            previousDistanceToTouchPos = 0;
            currentDistanceToTouchPos = 0;
            isMoving = true;
            whereToMove = (touchPosition - transform.position).normalized;
            location = touchPosition;
            touchFeedback.position = new Vector3(touchPosition.x, touchPosition.y - 1.5f, touchPosition.z);
            touchFeedback.gameObject.GetComponent<SpriteRenderer>().DOFade(0, 1.5f);
            if (touchPosition.x > transform.position.x)
                b_playerisRight = true;
            else
                b_playerisRight = false;
        }

        if (currentDistanceToTouchPos > previousDistanceToTouchPos)
        {
            isMoving = false;
            playerRB.velocity = Vector2.zero;
            footstepsSFX.volume = 0.0f;
        }

        if (isMoving)
            previousDistanceToTouchPos = (location - transform.position).magnitude;
    }
    #endregion
}
