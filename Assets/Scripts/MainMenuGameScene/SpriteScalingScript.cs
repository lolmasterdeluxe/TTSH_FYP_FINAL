using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScalingScript : MonoBehaviour
{
    //this script handles the scaling of the player's sprite

    #region Variables 

    [Tooltip("Smallest Scale that the player can be in the scene")]
    Vector3 smallestPlayerScale = new Vector3(0.75f, 0.75f, 0.75f);
    [Tooltip("Largest Scale that the player can be in the scene")]
    Vector3 originalPlayerScale;

    [Tooltip("Float values that hold the player scale value")]
    float f_originalPlayerScale, f_smallestPlayerScale;

    [Tooltip("GameObject positions that determine the highest and lowest position a player can be")]
    public GameObject furthestPosition, frontPosition;

    [Tooltip("To find normalised distance")]
    float _currentY;

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        originalPlayerScale = transform.localScale;
    }

    private void Start()
    {

        //set the values HERE
        f_originalPlayerScale = frontPosition.transform.position.y;
        f_smallestPlayerScale = furthestPosition.transform.position.y;

    }

    private void Update()
    {
        _currentY = transform.position.y;
  
        float normalizedDistance = Mathf.InverseLerp(f_smallestPlayerScale, f_originalPlayerScale, _currentY);

        //this adjusts the sprite size to be scalable between two bounded sizes
        transform.localScale = Vector3.Lerp(smallestPlayerScale, originalPlayerScale, normalizedDistance);

    }
    #endregion
}
