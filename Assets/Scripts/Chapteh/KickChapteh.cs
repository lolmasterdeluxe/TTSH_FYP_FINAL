using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KickChapteh : MonoBehaviour
{
    [SerializeField] private Chapteh chapteh;
    [SerializeField] private ChargeBar chargeBar;
    [SerializeField] private float MinimumForce = 100;

    private const float MAX_FORCE = 1200f;

    private float holdDownStartTime;

    public bool m_isIncrease = true;

    public AudioSource[] audioSources;

    private void Start()
    {
        chapteh = GameObject.Find("Chapteh").GetComponent<Chapteh>();
        chargeBar = GameObject.Find("Fill Image").GetComponent<ChargeBar>();
        holdDownStartTime = 0f;
    }

    public void PowerLaunch()
    {
        // When chapteh is in the air, do nothing
        if (chapteh.inPlay)
            return;
        else
        {
            // When chapteh is at the player, charge up to launch it.
            if (Input.GetMouseButton(0))
            { 
                if (holdDownStartTime == 0)
                {
                    // Charge bar sound
                    audioSources[1].Play();
                }

                if (m_isIncrease)
                {
                    holdDownStartTime += Time.deltaTime;

                    if (holdDownStartTime >= 1)
                    {
                        holdDownStartTime = 1;
                        m_isIncrease = false;
                    }
                }
                else
                {
                    holdDownStartTime -= Time.deltaTime;

                    if (holdDownStartTime <= 0)
                    {
                        holdDownStartTime = 0;
                        m_isIncrease = true;

                        // Charge bar sound
                        audioSources[1].Play();
                    }
                }

                chargeBar.SetFillBar(holdDownStartTime);
            }

            if (Input.GetMouseButtonUp(0))
            {
                chapteh.Kick(CalculateHoldDownForce(holdDownStartTime));

                // Resets the values to 0
                holdDownStartTime = 0f;
                chargeBar.SetFillBar(0);

                // Kick chapteh sound
                audioSources[0].Play();
            }
        }
    }

    private float CalculateHoldDownForce(float holdTime)
    {
        float maxForceHoldDownTime = 1f;
        float holdTimeNormalized = Mathf.Clamp01(holdTime / maxForceHoldDownTime);
        float force = holdTimeNormalized * MAX_FORCE;

        if (force < MinimumForce)
            force = MinimumForce;
        return force;
    }
}
