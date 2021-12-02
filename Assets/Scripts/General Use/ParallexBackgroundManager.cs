using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallexBackgroundManager : MonoBehaviour
{
    //this script holds the helper functions to move background

    #region Variables

    [Tooltip("Container that stores all types of background")]
    public GameObject[] componentContainer;

    [Tooltip("This is the Vector2 offset that 'scrolls' the material UV")]
    Vector2 backgroundOffset;

    #endregion

    #region Helper Functions

    public void SetBackgroundOffsetVector(Vector2 offset)
    {
        backgroundOffset = offset;
    }

    public Vector2 GetBackgroundOffsetVector()
    {
        return backgroundOffset;
    }

    #endregion



}
