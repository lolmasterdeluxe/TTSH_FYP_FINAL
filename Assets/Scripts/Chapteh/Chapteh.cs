using UnityEngine;

public class Chapteh : MonoBehaviour
{
    private Rigidbody2D rbChapteh;
    public bool inPlay;
    public Transform spawnPoint;
    public Transform playerSprite;
    public float speed;

    public SpriteRenderer skyWidth, skyHeight;
    private float chaptehWidth, chaptehHeight;

    private Vector3 gravityDirection;
    private Quaternion rotPos;

    private Vector2 lookDirection;
    private float lookAngle;

    // Start is called before the first frame update
    void Start()
    {
        rbChapteh = GetComponent<Rigidbody2D>();

        chaptehWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        chaptehHeight = GetComponent<SpriteRenderer>().bounds.size.y / 2;

        rotPos = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inPlay)
        {
            transform.position = spawnPoint.position;
        }

        //LookAtMouseDirection();

        Kick();

        if (inPlay)
        {
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, skyWidth.bounds.min.x + chaptehWidth, skyWidth.bounds.max.x - chaptehWidth), 
                                             Mathf.Clamp(transform.position.y, skyHeight.bounds.min.y + chaptehHeight, skyHeight.bounds.max.y - chaptehHeight));

            rbChapteh.rotation += rbChapteh.gravityScale;
        } 
    }

    void LookAtMouseDirection()
    {
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, lookAngle - 90f);
    }

    void Kick()
    {
        if (Input.GetMouseButtonDown(0) && !inPlay)
        {
            inPlay = true;

            Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePositionInWorld = new Vector2(screenToWorld.x, screenToWorld.y);

            Vector2 playerToMouseDir = (mousePositionInWorld - new Vector2(playerSprite.position.x, playerSprite.position.y)).normalized;

            rbChapteh.AddForce(playerToMouseDir * speed);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Ball hit the Player of the screen");
            rbChapteh.velocity = Vector2.zero;
            transform.rotation = rotPos;
            inPlay = false;
        }
    }
}
