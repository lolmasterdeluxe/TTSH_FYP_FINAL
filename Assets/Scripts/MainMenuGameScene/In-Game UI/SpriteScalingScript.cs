using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScalingScript : MonoBehaviour
{
    //this script handles the scaling of the player's sprite

    #region Variables 

    [Tooltip("Smallest Scale that the player can be in the scene")]
    Vector3 smallestPlayerScale = new Vector3(1.4f, 1.4f, 1.4f);
    [Tooltip("Largest Scale that the player can be in the scene")]
    Vector3 originalPlayerScale;

    [Tooltip("Reference to the Animator attached to the Player")]
    private Animator playerAnim;
    [SerializeField] private SpriteRenderer HatSprite;
    [SerializeField] private SpriteRenderer FaceSprite;

    [Tooltip("Float values that hold the player scale value")]
    float f_originalPlayerScale, f_smallestPlayerScale;

    [Tooltip("GameObject positions that determine the highest and lowest position a player can be")]
    public GameObject furthestPosition, frontPosition;

    private GameObject NPC;

    [Tooltip("To find normalised distance")]
    float _currentY;

    private bool playerCollidesWithEnvironment, playerCollidesWithNPC;

    PlayerKeyboardMovement playerMovement;
    PlayerJoystickMovement playerMovement2;
    PlayerDpadMovement playerMovement3;
    PlayerTouchMovement playerMovement4;

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        originalPlayerScale = transform.localScale;
        playerAnim = GetComponent<Animator>();
    }

    private void Start()
    {
        //set the values HERE
        f_originalPlayerScale = frontPosition.transform.position.y;
        f_smallestPlayerScale = furthestPosition.transform.position.y;
        playerMovement = GetComponent<PlayerKeyboardMovement>();
        playerMovement2 = GetComponent<PlayerJoystickMovement>();
        playerMovement3 = GetComponent<PlayerDpadMovement>();
        playerMovement4 = GetComponent<PlayerTouchMovement>();
    }

    private void Update()
    {
        PlayerAnimationFunction();

        _currentY = transform.position.y;
  
        float normalizedDistance = Mathf.InverseLerp(f_smallestPlayerScale, f_originalPlayerScale, _currentY);

        //this adjusts the sprite size to be scalable between two bounded sizes

        Vector3 newScale = Vector3.Lerp(smallestPlayerScale, originalPlayerScale, normalizedDistance);

        if (playerMovement.enabled)
        {
            if ((playerMovement.b_playerisRight && newScale.x < 0) || (!playerMovement.b_playerisRight && newScale.x > 0))
                newScale.x *= -1;
        }
        if (playerMovement2.enabled)
        {
            if ((playerMovement2.b_playerisRight && newScale.x < 0) || (!playerMovement2.b_playerisRight && newScale.x > 0))
                newScale.x *= -1;
        }
        if (playerMovement3.enabled)
        {
            if ((playerMovement3.b_playerisRight && newScale.x < 0) || (!playerMovement3.b_playerisRight && newScale.x > 0))
                newScale.x *= -1;
        }
        if (playerMovement4.enabled)
        {
            if ((playerMovement4.b_playerisRight && newScale.x < 0) || (!playerMovement4.b_playerisRight && newScale.x > 0))
                newScale.x *= -1;
        }
        else
            playerMovement4.isMoving = false;
        transform.localScale = newScale;
        SortOrderZ();

    }

    private void SortOrderZ()
    {
        if (playerCollidesWithEnvironment)
        {
            GetComponent<SpriteRenderer>().sortingLayerName = "Environment";
            GetComponent<SpriteRenderer>().sortingOrder = 3;
            HatSprite.sortingLayerName = "Environment";
            HatSprite.sortingOrder = 4;
            FaceSprite.sortingLayerName = "Environment";
            FaceSprite.sortingOrder = 4;
        }
        else if (playerCollidesWithNPC)
        {
            if (transform.position.y > NPC.transform.position.y)
            {
                GetComponent<SpriteRenderer>().sortingLayerName = "NPC";
                GetComponent<SpriteRenderer>().sortingOrder = 1;
                HatSprite.sortingLayerName = "NPC";
                HatSprite.sortingOrder = 2;
                FaceSprite.sortingLayerName = "NPC";
                FaceSprite.sortingOrder = 2;
            }
            else
            {
                GetComponent<SpriteRenderer>().sortingLayerName = "Player";
                GetComponent<SpriteRenderer>().sortingOrder = 3;
                HatSprite.sortingLayerName = "Player";
                HatSprite.sortingOrder = 4;
                FaceSprite.sortingLayerName = "Player";
                FaceSprite.sortingOrder = 4;
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().sortingLayerName = "Player";
            GetComponent<SpriteRenderer>().sortingOrder = 3;
            HatSprite.sortingLayerName = "Player";
            HatSprite.sortingOrder = 4;
            FaceSprite.sortingLayerName = "Player";
            FaceSprite.sortingOrder = 4;
        }
    }

    public void PlayerAnimationFunction()
    {
        if (playerMovement2.movementJoystick.joystickVec.y != 0 || playerMovement3.movement.sqrMagnitude != 0 || playerMovement4.isMoving)
            playerAnim.SetBool("isRunning", true);
        else
            playerAnim.SetBool("isRunning", false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //firstly, we check the tag of the collided gameObject
        if (other.gameObject.tag == "EnvironmentObjects")
            playerCollidesWithEnvironment = true;
        else if (other.gameObject.tag == "NPC")
        {
            playerCollidesWithNPC = true;
            NPC = other.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnvironmentObjects")
            playerCollidesWithEnvironment = false;
        else if (other.gameObject.tag == "NPC")
            playerCollidesWithNPC = false;
    }
    #endregion
}
