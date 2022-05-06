using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ScaleCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        Camera.main.orthographicSize = (20.0f / Screen.width * Screen.height / 2.0f);
    }
}
