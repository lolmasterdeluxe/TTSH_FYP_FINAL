using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    public Transform spawnPoint;

    private Image gaugeImage;

    // Start is called before the first frame update
    void Start()
    {
        gaugeImage = GameObject.Find("Gauge Image").GetComponent<Image>();

        // Default to an empty bar
        gaugeImage.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Sets position of Chapteh to beside the player head
        transform.position = spawnPoint.position;

        // Scale down the power charge
        transform.localScale = new Vector3(0.1f, 0.1f, 0f);
    }

    public void SetFillBar(float holdTime)
    {
        // Sets image fill amount to duration of holding
        gaugeImage.fillAmount = holdTime;
    }
}
