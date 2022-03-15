using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour
{
    public void AnimateButton(bool animate) 
    {
        RectTransform button = GetComponent<RectTransform>();
        if (animate)
            button.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        else
            button.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        Debug.Log("Animate bool: " + animate);
    }

}
