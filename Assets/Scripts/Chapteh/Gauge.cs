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
        transform.position = spawnPoint.position + (new Vector3(3f, 0f));
    }

    public void SetFillBar(float holdTime)
    {
        for (holdTime = 0f; holdTime <= 1f; holdTime += Time.time)
        {
            gaugeImage.fillAmount = holdTime;
        }
    }
}
