using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KickChapteh : MonoBehaviour
{
    [SerializeField] private Chapteh chapteh;
    [SerializeField] private ChargeBar chargeBar;

    private const float MAX_FORCE = 1100f;

    private float holdDownStartTime;

    private void Start()
    {
        chapteh = GameObject.Find("Chapteh").GetComponent<Chapteh>();
        chargeBar = GameObject.Find("Fill Image").GetComponent<ChargeBar>();

        // For Player UI in World Space
        chargeBar.charge.SetActive(false);
    }

    public void PowerLaunch()
    {
        if (Input.GetMouseButton(0))
        {
            // Fill amount increases to the hold down mouse key
            if (chargeBar.GetComponent<Image>().fillAmount != 1)
                holdDownStartTime += 0.5f * Time.deltaTime;

            chargeBar.SetFillBar(holdDownStartTime);

            chargeBar.charge.SetActive(true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            float holdDownTime = holdDownStartTime - Time.deltaTime;
            chapteh.Kick(CalculateHoldDownForce(holdDownTime));

            holdDownStartTime = 0f;
            chargeBar.SetFillBar(0);

            chargeBar.charge.SetActive(false);
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
