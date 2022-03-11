using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDpadMovement : MonoBehaviour
{
    //this script handles the movement of the player in the main menu game scene

    #region Variables

    private Rigidbody2D rb;

    [Tooltip("Reference to the Tutorial Screen Manager script")]
    [SerializeField] private TutorialScreenManager tutorialScreenmanagerInstance;

    //AudioSource variables
    [SerializeField] private AudioSource footstepsSFX;

    [SerializeField] private float playerSpeed;
    [Tooltip("Reference to the Dpad buttons")]
    [SerializeField] private GameObject Up, Down, Left, Right;
    [HideInInspector] public Vector2 movement;


    [HideInInspector] public bool b_playerisRight = true;
    #endregion

    #region Unity Callbacks

    public void Start()
    {
        //get references HERE
        rb = GetComponent<Rigidbody2D>();


        //set values HERE
        playerSpeed = 5f;
    }

    private void Update()
    {
        if (tutorialScreenmanagerInstance.b_tutorialScreenOpen == true)
            return;
        if (movement.sqrMagnitude > 0)
            footstepsSFX.volume = 0.75f;
        else
            footstepsSFX.volume = 0;

        if (movement.x < 0 && b_playerisRight)
            b_playerisRight = false;
        else if (movement.x > 0 && !b_playerisRight)
            b_playerisRight = true;
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
        if (dir == "Up")
        {
            movement.y = 1;
            Up.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        }
        if (dir == "Down")
        {
            movement.y = -1;
            Down.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        }
    }

    public void OnPointerDown(string dir)
    {
        PlayerMovementFunction(dir);
    }
    public void OnPointerUp()
    {
        movement.Set(0, 0);
        Up.transform.localScale = new Vector3(1, 1, 1);
        Down.transform.localScale = new Vector3(1, 1, 1);
        Left.transform.localScale = new Vector3(1, 1, 1);
        Right.transform.localScale = new Vector3(1, 1, 1);
    }

    #endregion
}
