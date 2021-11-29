using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChapteh : MonoBehaviour
{
    public GameObject chapteh;
    public MouseMovement playerPoint;

    private void Start()
    {
        chapteh.transform.position = new Vector2(playerPoint.transform.position.x, playerPoint.transform.position.y + 0.5f);

        //Instantiate(chapteh, new Vector2(playerPoint.transform.position.x, playerPoint.transform.position.y + 0.5f), chapteh.transform.rotation);
    }

    private void Update()
    {
        chapteh.transform.position = new Vector2(playerPoint.transform.position.x, playerPoint.transform.position.y + 0.5f);

        FaceMouseDirection();
    }

    void FaceMouseDirection()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - chapteh.transform.position.x, mousePosition.y - chapteh.transform.position.y);

        chapteh.transform.up = direction;
    }
}
