using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rigidBody;
    public Animator animator;
    Vector2 movement;
    Vector2 previousMovement;

    // Start is called before the first frame update
    void Start()
    {
        if (rigidBody == null)
            rigidBody = gameObject.GetComponent<Rigidbody2D>();

        if (animator == null)
            animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // If there are any movements, we store the previous movement vector
        if (movement.sqrMagnitude > 0)
            previousMovement = movement;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        animator.SetFloat("PreviousHorizontal", previousMovement.x);
        animator.SetFloat("PreviousVertical", previousMovement.y);
    }

    void FixedUpdate()
    {
        // Movement
        rigidBody.MovePosition(rigidBody.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
