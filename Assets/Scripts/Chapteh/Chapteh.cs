using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapteh : MonoBehaviour
{
    private Rigidbody2D rbChapteh;
    public bool inPlay;
    public Transform spawnPoint;
    public Transform playerSprite;
    //public Transform arrowPoint;
    public GameObject arrowIndicator;

    public SpriteRenderer skyWidth, skyHeight;
    private float chaptehWidth, chaptehHeight;

    private Quaternion rotPos;

    private Vector2 lookDirection;
    private float lookAngle = 0f;
    private float clampedAngle = 0f;

    [SerializeField] private KickChapteh kickChapteh;
    [SerializeField] private Player player;

    private float glowDuration = 1f;

    private PauseMenu pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        rbChapteh = GetComponent<Rigidbody2D>();

        chaptehWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        chaptehHeight = GetComponent<SpriteRenderer>().bounds.size.y / 2;

        // Set initial rotation to 0
        rotPos = transform.rotation;

        player = GameObject.Find("Player Sprite").GetComponent<Player>();
        kickChapteh = GameObject.Find("Chapteh Manager").GetComponent<KickChapteh>();
        pauseMenu = GameObject.Find("Pause Manager").GetComponent<PauseMenu>();

        ComboManager.Instance.e_comboBreak.AddListener(ChaptehGameManager.Instance.OnComboBreak);
    }

    // Update is called once per frame
    void Update()
    {
        // Sets the chapteh to the origin position before game starts and ends
        if (!ChaptehGameManager.Instance.m_gameStarted)
        {
            transform.position = spawnPoint.position;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            return;
        }
        else if (ChaptehGameManager.Instance.m_gameEnded)
        {
            transform.position = spawnPoint.position;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            return;
        }

        // When the Chapteh is not yet launched
        if (!inPlay)
        {
            // Sets position of Chapteh to above the player head
            transform.position = spawnPoint.position;
        }

        //arrowIndicator.transform.position = arrowPoint.transform.position; 

        LookAtMouseDirection();

        // Prevents the chapteh to be charged when in Pause Menu
        if(!pauseMenu.isPaused)
            kickChapteh.PowerLaunch();

        FallOnGravity();
    }

    // Chapteh rotates at direction of the mouse position
    void LookAtMouseDirection()
    {
        // Look in the direction of the mouse position
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - arrowIndicator.transform.position;
        
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        clampedAngle = Mathf.Clamp(lookAngle, 45f, 135f);

        // Rotate according the mouse position
        arrowIndicator.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, clampedAngle - 90f));
    }

    public void Kick(float speed)
    {
        if (!inPlay)
        {
            inPlay = true;

            // Gets the mouse input from Screen to World Point in Vector3
            Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Set the mouse position in Vector2
            Vector2 mousePositionInWorld = new Vector2(screenToWorld.x, screenToWorld.y);
            // Gets the player position with the mouse position
            Vector2 playerToMouseDir = (mousePositionInWorld - new Vector2(playerSprite.position.x, playerSprite.position.y)).normalized;

            // Force needed to launch the Chapteh
            if (player.isRunning == true)
                rbChapteh.AddForce(playerToMouseDir * (speed * 0.5f));
            else
                rbChapteh.AddForce(playerToMouseDir * speed);
        }
    }

    public void FallOnGravity()
    {
        // When the Chapteh is launched
        if (inPlay)
        {
            // Clamps the Chapteh within the boundaries of the background
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, (skyWidth.bounds.min.x + 0.2f) + chaptehWidth, (skyWidth.bounds.max.x - 0.2f) - chaptehWidth),
                                             Mathf.Clamp(transform.position.y, skyHeight.bounds.min.y + chaptehHeight, (skyHeight.bounds.max.y + 2f) - chaptehHeight));

            // Rotates the Chapteh to fall according to gravity
            rbChapteh.rotation += rbChapteh.gravityScale;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Player picks up and spawns the Chapteh back to the player if lands on the ground
        if (other.CompareTag("Player"))
        {
            rbChapteh.velocity = Vector2.zero;
            transform.rotation = rotPos;
            inPlay = false;
        }

        // Chapteh lands on the ground and breaks the combo
        if (other.CompareTag("Ground"))
        {
            ComboManager.Instance.BreakCombo();
        }

        // Chapteh hits the respective ring
        if (other.CompareTag("RedRing") || other.CompareTag("YellowRing") || other.CompareTag("GreenRing"))
        {
            // Adds the score to respective ring color
            ChaptehGameManager.Instance.OnChaptehHit(other.gameObject);
            other.GetComponent<Rings>().isTriggered = true;
            StartCoroutine(GlowRingsOnHit(other));

            // Plays the star particle effect on trigger
            other.GetComponentInChildren<ParticleSystem>().Play();
        }
    }

    private IEnumerator GlowRingsOnHit(Collider2D col2D)
    {
        yield return new WaitForSeconds(glowDuration);
        col2D.gameObject.SetActive(false);
        //Destroy(col2D.gameObject);
        col2D.GetComponent<Rings>().isTriggered = false;
    }
}
