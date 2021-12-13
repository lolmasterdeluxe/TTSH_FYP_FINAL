using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SPS_ButtonBehaviour : MonoBehaviour
{
    //this script holds helper functions that can be referenced to change button appearance

    public void ResizeButton(GameObject button, Vector3 size)
    {
        button.transform.localScale = new Vector3(size.x, size.y, size.z);
    }

    public void ResetButtonSize(GameObject button, Vector3 size)
    {
        button.transform.localScale = new Vector3(size.x, size.y, size.z);
    }

    public void DimButton(GameObject button)
    {

    }
}
