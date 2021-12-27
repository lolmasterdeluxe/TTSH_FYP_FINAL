using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{
    public GameObject charge;
    public Transform playerChargeBarPoint;
    private Image chargeFillImage;

    // Start is called before the first frame update
    void Start()
    {
        chargeFillImage = GameObject.Find("Fill Image").GetComponent<Image>();

        // Default to an empty bar
        chargeFillImage.fillAmount = 0f;

        // For Screen UI
        //charge.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        charge.transform.position = playerChargeBarPoint.transform.position + new Vector3(-1f, 0f, 0f);

        //transform.localScale = new Vector3(0.1f, 0.1f, 0f);
    }

    public void SetFillBar(float holdTime)
    {
        // Sets image fill amount to duration of holding
        chargeFillImage.fillAmount = holdTime;
    }
}
