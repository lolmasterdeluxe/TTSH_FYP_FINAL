using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Rings : MonoBehaviour
{
    public Light2D ringGlow;
    public bool isTriggered = false;

    public ChaptehGameManager.Objective type;

    // Start is called before the first frame update
    void Start()
    {
        if (type == ChaptehGameManager.Objective.DEFAULT)
            type = ChaptehGameManager.GetRandomColouredObjective();

        switch (type)
        {
            case ChaptehGameManager.Objective.HIT_RED_RINGS:
                GetComponentInChildren<SpriteRenderer>().CompareTag("RedRing");
                break;
            case ChaptehGameManager.Objective.HIT_YELLOW_RINGS:
                GetComponentInChildren<SpriteRenderer>().CompareTag("YellowRing");
                break;
            case ChaptehGameManager.Objective.HIT_GREEN_RINGS:
                GetComponentInChildren<SpriteRenderer>().CompareTag("GreenRing");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("RedRing") || gameObject.CompareTag("YellowRing") || gameObject.CompareTag("GreenRing"))
        {
            if (isTriggered == true)
            {
                ringGlow.intensity += 5.5f * Time.deltaTime;
            }
            else
                ringGlow.intensity = 0f;
        }
    }
}
