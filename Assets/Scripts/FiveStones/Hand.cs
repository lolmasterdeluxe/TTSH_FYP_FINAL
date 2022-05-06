using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public GameObject handTrailPrefab;
    private GameObject currentHandTrail;
    public GameObject particlePrefab;
    //public GameObject particle;
    public ParticleSystem particleSystem_;
    public ParticleSystem bombparticle_;
    private GameObject instantiatedParticle;
    public FiveStonesGameManager.Objective type;

    Rigidbody2D rigidBody;
    CircleCollider2D circleCollider;
    Camera cam;

    public float velocity;
    public float cuttingVelocityThreshold = 0.0001f;
    public Vector2 prevPosition;

    public Sprite closedHand;
    public Sprite openedHand;
    private SpriteRenderer handSpriteRenderer; 

    bool isCatching = false;

    // Start is called before the first frame update
    [System.Obsolete]
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

        currentHandTrail = Instantiate(handTrailPrefab, transform);
        currentHandTrail.GetComponent<TrailRenderer>().Clear();
        particleSystem_.playOnAwake = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseMovement();

        if (Input.GetMouseButtonDown(0))
        {
            StartCatching();
        }   
        else if (Input.GetMouseButtonUp(0))
        {
            StopCatching();
            
        }
               
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
        
        handSpriteRenderer.sprite = closedHand;
        //StopCoroutine(DestroyTrial());
    }

    void StopCatching()
    {
        isCatching = false;
        handSpriteRenderer.sprite = openedHand;
    }

    private IEnumerator DestroyTrial()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(currentHandTrail);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stone" )
        {
            FiveStonesGameManager.Instance.OnStoneCaught(collision.gameObject);
            collision.gameObject.SetActive(false);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            collision.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
            
            collision.gameObject.transform.rotation = Quaternion.identity;
            collision.gameObject.GetComponent<Animator>().runtimeAnimatorController = null;

            //Instantiate(particlePrefab, collision.gameObject.transform, collision.gameObject.transform);
            if(collision.GetComponent<Stone>().type != FiveStonesGameManager.Objective.BOMB_STONES)
            {
                Debug.Log("sparkle");
                ParticleSystem particleEffect = new ParticleSystem();
                particleEffect = Instantiate(particleSystem_, transform.position, transform.rotation);
                Destroy(particleEffect, 2.0f);
            }

            if(collision.GetComponent<Stone>().type == FiveStonesGameManager.Objective.BOMB_STONES)
            {
                Debug.Log("sparkle");
                ParticleSystem particleEffect = new ParticleSystem();
                particleEffect = Instantiate(bombparticle_, transform.position, transform.rotation);
                Destroy(particleEffect, 2.0f);
            }


            // Destroy(collision.gameObject);
        }
        else
            collision.gameObject.GetComponent<Stone>().type = FiveStonesGameManager.Objective.DEFAULT;
        //if(collision.gameObject.tag == "Stone" && collision.GetComponent<Stone>().type != FiveStonesGameManager.Objective.BOMB_STONES)
        //{
        //    
        //}
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Stone")
        {
            //transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
            
            Debug.Log("Stone particle spawn");
        }
    }

}
