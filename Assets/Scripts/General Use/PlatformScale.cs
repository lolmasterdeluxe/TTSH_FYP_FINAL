using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformScale : MonoBehaviour
{
    private CanvasScaler canvas;
    [SerializeField] private int UnityEditorCanvasMatchValue = 0;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<CanvasScaler>();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        canvas.matchWidthOrHeight = UnityEditorCanvasMatchValue;
#elif UNITY_ANDROID
        canvas.matchWidthOrHeight = 1;
#elif UNITY_IPHONE
        canvas.matchWidthOrHeight = 0;
#else
        canvas.matchWidthOrHeight = 1;
#endif
    }
}
