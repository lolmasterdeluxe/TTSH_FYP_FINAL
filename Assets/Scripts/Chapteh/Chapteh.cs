﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapteh : MonoBehaviour
{
    private Rigidbody2D rbChapteh;
    public bool inPlay;
    public Transform spawnPoint;
    public Transform playerSprite;

    public SpriteRenderer skyWidth, skyHeight;
    private float chaptehWidth, chaptehHeight;

    private Quaternion rotPos;

    private Vector2 lookDirection;
    private float lookAngle;

    private Vector2 oldPosition;
    private bool isDecreasing = false;
    private int tempvalue = -1;
    
    [SerializeField] private KickChapteh kickChapteh;
    [SerializeField] private SpawnRings spawnRings;

    // Start is called before the first frame update
    void Start()
    {
        rbChapteh = GetComponent<Rigidbody2D>();

        chaptehWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        chaptehHeight = GetComponent<SpriteRenderer>().bounds.size.y / 2;

        // Set initial rotation to 0
        rotPos = transform.rotation;

        kickChapteh = GameObject.Find("Chapteh Manager").GetComponent<KickChapteh>();
        spawnRings = GameObject.Find("Rings Spawner").GetComponent<SpawnRings>();

        ComboManager.Instance.e_comboBreak.AddListener(ChaptehGameManager.Instance.OnComboBreak);

        oldPosition.y = gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // When the Chapteh is not yet launched
        if (!inPlay)
        {
            // Sets position of Chapteh to above the player head
            transform.position = spawnPoint.position;
            
            LookAtMouseDirection();
        }

        kickChapteh.PowerLaunch();

        FallOnGravity();

        MoveRingBoxCollider();
    }

    // Chapteh rotates at direction of the mouse position
    void LookAtMouseDirection()
    {
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, lookAngle - 90f);
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
            //Debug.Log("Chapteh respawns to Player");
            rbChapteh.velocity = Vector2.zero;
            transform.rotation = rotPos;
            inPlay = false;
        }

        // Chapteh lands on the ground and breaks the combo
        if (other.CompareTag("Ground"))
        {
            Debug.Log("Chapteh lands on ground");
            ComboManager.Instance.BreakCombo();
        }

        if (other.CompareTag("RedRing") || other.CompareTag("YellowRing") || other.CompareTag("GreenRing"))
        {
            ChaptehGameManager.Instance.OnChaptehHit(other.gameObject);
            Destroy(other.gameObject);
            //other.gameObject.SetActive(false);
            spawnRings.spawnedRings.Remove(other.gameObject);
        }
    }

    private void MoveRingBoxCollider()
    {
        if (oldPosition.y > gameObject.transform.position.y)
        {
            oldPosition.y = gameObject.transform.position.y;
            isDecreasing = true;
        }
        if (oldPosition.y < gameObject.transform.position.y)
        {
            oldPosition.y = gameObject.transform.position.y;
            isDecreasing = false;
        }

        if (isDecreasing && tempvalue != spawnRings.spawnedRings.Count && spawnRings.spawnedRings.Count != 0)
        {
            for (int i = 0; i < spawnRings.spawnedRings.Count - 1; i++)
            {
                spawnRings.spawnedRings[i].GetComponent<BoxCollider2D>().offset = new Vector2(0, -1.13f);
            }
        }
        else if (!isDecreasing && tempvalue == spawnRings.spawnedRings.Count && spawnRings.spawnedRings.Count != 0)
        {
            for (int i = 0; i < spawnRings.spawnedPositions.Count - 1; i++)
                spawnRings.spawnedRings[i].GetComponent<BoxCollider2D>().offset = new Vector2(0, 2.13f);
        }
        tempvalue = spawnRings.spawnedRings.Count;
    }
}
