using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour
{
    public void AnimateButton(bool animate) 
    {
        RectTransform button = GetComponent<RectTransform>();
        if (animate)
            button.localScale += new Vector3(0.25f, 0.25f, 0.25f);
        else
            button.localScale -= new Vector3(0.25f, 0.25f, 0.25f);
    }

}
