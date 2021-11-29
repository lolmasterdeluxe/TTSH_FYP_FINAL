using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public GameObject handTrailPrefab;
    public GameObject currentHandTrail;
    public Rigidbody2D rigidBody;
    Camera cam;
    bool isCatching = false;

    // Start is called before the first frame update
    void Start()
    {
        if (rigidBody == null)
            rigidBody = GetComponent<Rigidbody2D>();

        if (cam == null)
            cam = Camera.main;
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
    }

    void StartCatching()
    {
        isCatching = true;
        currentHandTrail = Instantiate(handTrailPrefab, transform);
        currentHandTrail.GetComponent<TrailRenderer>().Clear();
    }

    void StopCatching()
    {
        isCatching = false;
        Destroy(currentHandTrail);
    }

}
