using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KickChapteh : MonoBehaviour
{
    [SerializeField] private Chapteh chapteh;
    [SerializeField] private Gauge gauge;

    private const float MAX_FORCE = 1100f;

    private float holdDownStartTime;

    private void Start()
    {
        chapteh = GameObject.Find("Dumb Chapteh").GetComponent<Chapteh>();
        gauge = GameObject.Find("Gauge Image").GetComponent<Gauge>();
    }

    public void PowerLaunch()
    {
        if (Input.GetMouseButton(0))
        {
            // Fill amount increases to the hold down mouse key
            if (gauge.GetComponent<Image>().fillAmount != 1)
                holdDownStartTime += 0.5f * Time.deltaTime;

            gauge.SetFillBar(holdDownStartTime);
        }

        if (Input.GetMouseButtonUp(0))
        {
            float holdDownTime = holdDownStartTime - Time.deltaTime;
            chapteh.Kick(CalculateHoldDownForce(holdDownTime));

            holdDownStartTime = 0f;
            gauge.SetFillBar(0);
        }
    }

    private float CalculateHoldDownForce(float holdTime)
    {
        float maxForceHoldDownTime = 1f;
        float holdTimeNormalized = Mathf.Clamp01(holdTime / maxForceHoldDownTime);
        float force = holdTimeNormalized * MAX_FORCE;

        return force;
    }
}
