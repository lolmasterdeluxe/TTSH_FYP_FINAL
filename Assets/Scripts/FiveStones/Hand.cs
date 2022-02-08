using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public GameObject handTrailPrefab;
    public GameObject currentHandTrail;

    Rigidbody2D rigidBody;
    CircleCollider2D circleCollider;
    Camera cam;

    public float velocity;
    public float cuttingVelocityThreshold = 0.0001f;
    public Vector2 prevPosition;

    public Sprite closedHand;
    public Sprite openedHand;
    public SpriteRenderer handSpriteRenderer; 

    bool isCatching = false;

    // Start is called before the first frame update
    void Start()
    {
        if (rigidBody == null)
            rigidBody = GetComponent<Rigidbody2D>();

        if (cam == null)
            cam = Camera.main;

        if (circleCollider == null)
            circleCollider = GetComponent<CircleCollider2D>();

        if (handSpriteRenderer == null)
            handSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseMovement();

        if (Input.GetMouseButtonDown(0))
            StartCatching();
        else if (Input.GetMouseButtonUp(0))
            StopCatching();       
    }

    void UpdateMouseMovement()
    {
        Vector2 maxBound = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 minBound = Camera.main.ScreenToWorldPoint(Vector2.zero);

        Vector2 screenToWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);

        screenToWorldPosition.x = Mathf.Clamp(screenToWorldPosition.x, minBound.x, maxBound.x);
        screenToWorldPosition.y = Mathf.Clamp(screenToWorldPosition.y, minBound.y, maxBound.y);

        rigidBody.position = screenToWorldPosition;

        velocity = (screenToWorldPosition - prevPosition).magnitude * Time.deltaTime;

        // bruh why am i doing this here
        // it works here but not in the individual catching functions
        if (isCatching)
        {
            if (velocity > 0)
                circleCollider.enabled = true;
            else
                circleCollider.enabled = false;
        }
        else
            circleCollider.enabled = false;

        prevPosition = screenToWorldPosition;
    }

    void StartCatching()
    {
        FiveStonesGameManager.Instance.audioSources[3].Play();
        isCatching = true;
        currentHandTrail = Instantiate(handTrailPrefab, transform);
        currentHandTrail.GetComponent<TrailRenderer>().Clear();
        handSpriteRenderer.sprite = closedHand;
    }

    void StopCatching()
    {
        isCatching = false;
        handSpriteRenderer.sprite = openedHand;
        Destroy(currentHandTrail);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stone")
        {
            FiveStonesGameManager.Instance.OnStoneCaught(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }

}
