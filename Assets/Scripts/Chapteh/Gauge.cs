using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    public Transform spawnPoint;

    public Transform fullFillImage;
    public Transform fillImage;

    // Start is called before the first frame update
    void Start()
    {
        // Default to an empty bar
        var newScale = this.fillImage.localScale;
        newScale.y = 0;
        this.fillImage.localScale = newScale;
    }

    // Update is called once per frame
    void Update()
    {
        // Sets position of Chapteh to beside the player head
        transform.position = spawnPoint.position + (new Vector3(5f, 0f));
    }

    public void SetFillBar(float fillAmount)
    {
        // Make sure value is between 0 and 1
        fillAmount = Mathf.Clamp01(fillAmount);
        // Scale the fillImage accordingly
        var newScale = this.fillImage.localScale;
        newScale.y = this.fullFillImage.localScale.y * fillAmount;
        this.fillImage.localScale = newScale;
    }
}
