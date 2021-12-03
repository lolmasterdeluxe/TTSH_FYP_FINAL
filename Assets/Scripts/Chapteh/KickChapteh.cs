using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickChapteh : MonoBehaviour
{
    [SerializeField] private Chapteh chapteh;
    //[SerializeField] private Gauge guage;

    private float holdDownStartTime;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            holdDownStartTime = Time.time;

            //guage.SetFillBar(holdDownStartTime);
        }

        if(Input.GetMouseButtonUp(0))
        {
            float holdDownTime = Time.time - holdDownStartTime;
            chapteh.Kick(CalculateHoldDownForce(holdDownTime));

            //guage.SetFillBar(0);
        }
    }

    private float CalculateHoldDownForce(float holdTime)
    {
        float maxForceHoldDownTime = 1f;
        float holdTimeNormalized = Mathf.Clamp01(holdTime / maxForceHoldDownTime);
        float force = holdTimeNormalized * Chapteh.MAX_FORCE;

        return force;
    }
}
