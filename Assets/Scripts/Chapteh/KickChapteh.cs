using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickChapteh : MonoBehaviour
{
    [SerializeField]
    private Transform playerDir;

    [SerializeField]
    private GameObject chapteh;

    private Vector2 lookDirection;
    private float lookAngle;

    // Update is called once per frame
    void Update()
    {
        ChaptehDirection();
    }

    public void ChaptehDirection()
    {
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, lookAngle - 90f);

        if(Input.GetMouseButtonDown(0))
        {
            Kick();
        }
    }

    public void Kick()
    {
        GameObject kickedChapteh = Instantiate(chapteh, playerDir.position, playerDir.rotation);
        kickedChapteh.GetComponent<Rigidbody2D>().velocity = playerDir.up * 10f;
    }
}
