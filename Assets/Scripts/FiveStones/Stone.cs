using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public FiveStonesGameManager.Objective type;

    // Start is called before the first frame update
    void Start()
    {
        if (type == FiveStonesGameManager.Objective.DEFAULT)
            type = FiveStonesGameManager.GetRandomColouredObjective();

        switch (type)
        {
            case FiveStonesGameManager.Objective.CATCH_RED_STONES:
                GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case FiveStonesGameManager.Objective.CATCH_YELLOW_STONES:
                GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
            case FiveStonesGameManager.Objective.CATCH_BLUE_STONES:
                GetComponent<SpriteRenderer>().color = Color.cyan;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
