using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{
    [SerializeField] private GameObject handle;
    [SerializeField] private Slider _slider;
    public void SetFillBar(float fillAmt)
    {
        // Sets image fill amount to duration of holding
        _slider.value = fillAmt;
    }

    public float GetFillAmt()
    {
        return _slider.value;
    }

    public void SetHandleActive(bool isActive)
    {
        handle.SetActive(isActive);
    }
}
