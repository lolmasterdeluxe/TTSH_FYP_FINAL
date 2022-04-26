using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour
{
    public bool isEnabled = true;
    public void AnimateButton(bool animate) 
    {
        if (isEnabled)
        {
            RectTransform button = GetComponent<RectTransform>();
            if (animate)
                button.localScale += new Vector3(0.25f, 0.25f, 0.25f);
            else
                button.localScale -= new Vector3(0.25f, 0.25f, 0.25f);
        }
    }

}
