using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapteh : MonoBehaviour
{
    private Rigidbody2D rbChapteh;
    [HideInInspector] public bool inPlay;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform playerSprite;

    private Vector2 lookDirection, force;
    private float clampedAngle = 0f;

    //[SerializeField] private KickChapteh kickChapteh;
    [SerializeField] private Player player;
    [SerializeField] private PauseMenu pauseMenu;

    private float glowDuration = 1f;
    public AudioSource onRingHitSource;

    // Start is called before the first frame update
    void Start()
    {
        rbChapteh = GetComponent<Rigidbody2D>();
        ComboManager.Instance.e_comboBreak.AddListener(ChaptehGameManager.Instance.OnComboBreak);
        onRingHitSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseMenu.isPaused)
            return;
        // Sets the chapteh to the origin position before game starts and ends
        if (!ChaptehGameManager.Instance.m_gameStarted)
        {
            transform.position = spawnPoint.position;
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            return;
        }
        else if (ChaptehGameManager.Instance.m_gameEnded)
        {
            transform.position = spawnPoint.position;   
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            return;
        }

        // When the Chapteh is not yet launched
        if (!inPlay)
        {
            // Sets position of Chapteh to above the player head
            transform.position = spawnPoint.position;
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        }
        //LookAtMouseDirection();
        FallOnGravity();
    }

    // Chapteh rotates at direction of the mouse position
/*    void LookAtMouseDirection()
    {
        if (!inPlay)
        {
            arrowIndicator.SetActive(true);

            // Look in the direction of the mouse position
            lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - arrowIndicator.transform.position;

            lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

            //clampedAngle = Mathf.Clamp(lookAngle, 45f, 135f);

            // Rotate according the mouse position
            arrowIndicator.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, clampedAngle));
        }
        else
            arrowIndicator.SetActive(false);
    }*/

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
                rbChapteh.AddForce(playerToMouseDir * speed);
            else
                rbChapteh.AddForce(playerToMouseDir * speed);
        }
    }

    public void Kick(Vector2 force)
    {
        if (!inPlay)
        {
            inPlay = true;
            // Force needed to launch the Chapteh
            Debug.Log("Power: " + force);
            rbChapteh.AddForce(force, ForceMode2D.Impulse);
            this.force = force;
        }
    }


    public void FallOnGravity()
    {
        // When the Chapteh is launched
        if (inPlay)
        {
            // Rotates the Chapteh to fall according to gravity
            Vector2 dir = rbChapteh.velocity;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //rbChapteh.rotation += rbChapteh.gravityScale;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Chapteh lands on the ground and breaks the combo
        if (other.CompareTag("Ground"))
        {
            inPlay = false;
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

            onRingHitSource.Play();
        }
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Player picks up and spawns the Chapteh back to the player if lands on the ground
        if (inPlay && (other.gameObject.CompareTag("Player")))
        {
            Vector2 Normal = other.contacts[0].normal;
            Vector3 m_dir = Vector2.Reflect(rbChapteh.velocity, Normal).normalized;
            m_dir.x = Mathf.Clamp(m_dir.x, -0.25f, 0.25f);
            m_dir.y = 1f;
            Debug.Log("Force * m_dir: " + (force * m_dir));
            rbChapteh.velocity = force * m_dir;
        }
    }

    private IEnumerator GlowRingsOnHit(Collider2D col2D)
    {
        col2D.enabled = false;
        yield return new WaitForSeconds(glowDuration);
        col2D.gameObject.SetActive(false);
        //Destroy(col2D.gameObject);
        col2D.GetComponent<Rings>().isTriggered = false;
    }
}
