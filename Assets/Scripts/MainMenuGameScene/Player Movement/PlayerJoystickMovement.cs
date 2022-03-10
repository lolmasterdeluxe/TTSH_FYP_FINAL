using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoystickMovement : MonoBehaviour
{
    public JoystickMovement movementJoystick;
    [SerializeField] private float playerSpeed;
    private Rigidbody2D rb;

    //bool variables
    [HideInInspector] public bool b_playerisRight = true;

    [Tooltip("Reference to the Tutorial Screen Manager script")]
    [SerializeField] private TutorialScreenManager tutorialScreenmanagerInstance;

    //AudioSource variables
    [SerializeField] private AudioSource footstepsSFX;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (tutorialScreenmanagerInstance.b_tutorialScreenOpen == true)
            return;
        if (rb.velocity.x < 0 && b_playerisRight) //set bool to be false
            b_playerisRight = false;
        else if (rb.velocity.x > 0 && !b_playerisRight) //set bool to be true
            b_playerisRight = true;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movementJoystick.joystickVec.y != 0)
        {
            rb.velocity = new Vector2(movementJoystick.joystickVec.x * playerSpeed, movementJoystick.joystickVec.y * playerSpeed);
            footstepsSFX.volume = 0.75f;
        }
        else
        {
            rb.velocity = Vector2.zero;
            footstepsSFX.volume = 0f;
        }
    }
}
