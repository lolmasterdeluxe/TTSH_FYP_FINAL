using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoop : MonoBehaviour
{
    private Rigidbody2D rbHoop;
    private Vector2 screenBounds;
    public Vector2 hoopPosition;

    public SpriteRenderer skyWidth, skyHeight;
    private float hoopWidth, hoopHeight;

    // Start is called before the first frame update
    void Start()
    {
        rbHoop = GetComponent<Rigidbody2D>();

        hoopWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        hoopHeight = GetComponent<SpriteRenderer>().bounds.size.y / 2;

        hoopPosition.x = Mathf.Clamp(hoopPosition.x, skyWidth.bounds.min.x + hoopWidth, skyWidth.bounds.max.x - hoopWidth);
        hoopPosition.y = Mathf.Clamp(hoopPosition.y, skyHeight.bounds.min.x + hoopHeight, skyHeight.bounds.max.x - hoopHeight);

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(hoopPosition.x, hoopPosition.y));
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < screenBounds.x * 2)
        {
            Destroy(this.gameObject);
        }
    }
}
