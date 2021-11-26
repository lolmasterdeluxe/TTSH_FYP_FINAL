using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAnimationScript : MonoBehaviour
{
    //this script handles the movement of background animation

    #region Variables

    Material material;
    Vector2 imgOffset;

    public float xVel, yVel;

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    private void Start()
    {
        imgOffset = new Vector2(xVel, yVel);
    }

    private void Update()
    {
        material.mainTextureOffset += imgOffset * Time.deltaTime;
    }
    #endregion

}
