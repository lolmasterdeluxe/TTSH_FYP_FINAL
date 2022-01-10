using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScrolling : MonoBehaviour
{
    #region Variables

    [Tooltip("Reference to Object Manager Script")]
    SPS_UIManager uimanagerInstance;

    public float speed;
    public GameObject secondobject;
    #endregion

    #region Unity Callbacks

    private void Start()
    {
        secondobject.transform.localPosition = new Vector3(Screen.currentResolution.width, transform.localPosition.y, transform.localPosition.z);
        //get reference to other scripts HERE
        uimanagerInstance = FindObjectOfType<SPS_UIManager>();

    }

    private void Update()
    {
        if (uimanagerInstance.b_gameEnded == true)
            return;
        gameObject.transform.localPosition += new Vector3(-speed, 0, 0);
        secondobject.transform.localPosition += new Vector3(-speed, 0, 0);
        if (gameObject.transform.localPosition.x <= -Screen.currentResolution.width)
            gameObject.transform.localPosition = new Vector3(Screen.currentResolution.width, transform.localPosition.y, transform.localPosition.z);
        else if (secondobject.transform.localPosition.x <= -Screen.currentResolution.width)
            secondobject.transform.localPosition = new Vector3(Screen.currentResolution.width, transform.localPosition.y, transform.localPosition.z);
    }
   
    #endregion

}

public static class TextureExtension
{
    public static void ShiftPixelsHorizontally(this RawImage original, int pixels)
    {
        Texture2D backgroundImage = (Texture2D)original.mainTexture;
        
        var originalPixels = backgroundImage.GetPixels();

        Color[] newPixels = new Color[originalPixels.Length];

        int width = backgroundImage.width;
        int rows = backgroundImage.height;
        Texture2D newtex = new Texture2D(width, rows);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                if (x + pixels < width && x + pixels >= 0)
                    newPixels[x + y * width] = originalPixels[x + pixels + y * width];
                else if (x + pixels < 0)
                    newPixels[x + y * width] = originalPixels[rows + pixels + y * width];
                else
                    newPixels[x + y * width] = originalPixels[0 + pixels + y * width];
            }
        }

        newtex.SetPixels(newPixels);
        newtex.Apply();
        original.texture = newtex;
    }
}