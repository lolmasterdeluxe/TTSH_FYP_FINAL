using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rings : MonoBehaviour
{
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
        
    }
}
