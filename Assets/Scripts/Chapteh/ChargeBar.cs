using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{
    private Image chargeBarImage;

    // Start is called before the first frame update
    void Start()
    {
        chargeBarImage = GameObject.Find("Fill Image").GetComponent<Image>();

        // Default to an empty bar
        chargeBarImage.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetFillBar(float holdTime)
    {
        // Sets image fill amount to duration of holding
        chargeBarImage.fillAmount = holdTime;
    }
}
