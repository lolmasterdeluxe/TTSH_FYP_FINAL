using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{
    private Image chargeFillImage;

    // Start is called before the first frame update
    void Start()
    {
        // Default to an empty bar
        chargeFillImage = GetComponent<Image>();
        chargeFillImage.fillAmount = 1f;
        //playerChargeBarPoint = GetComponent<Transform>();

        // For Screen UI
        //charge.SetActive(false);
    }

    public void SetFillBar(float fillAmt)
    {
        // Sets image fill amount to duration of holding
        chargeFillImage.fillAmount = fillAmt;
    }
}
