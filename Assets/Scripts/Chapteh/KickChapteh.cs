using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PowerLaunch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            holdDownStartTime = Time.time;

            gauge.SetFillBar(holdDownStartTime);
        }

        if (Input.GetMouseButtonUp(0))
        {
            float holdDownTime = Time.time - holdDownStartTime;
            chapteh.Kick(CalculateHoldDownForce(holdDownTime));

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
