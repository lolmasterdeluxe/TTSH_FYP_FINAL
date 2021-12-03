using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoops : MonoBehaviour
{
    public ChaptehGameManager.Objective type;

    // Start is called before the first frame update
    void Start()
    {
        if (type == ChaptehGameManager.Objective.DEFAULT)
            type = ChaptehGameManager.GetRandomColouredObjective();

        switch (type)
        {
            case ChaptehGameManager.Objective.HIT_RED_HOOPS:
                GetComponent<SpriteRenderer>().CompareTag("RedHoop");
                break;
            case ChaptehGameManager.Objective.HIT_BLUE_HOOPS:
                GetComponent<SpriteRenderer>().CompareTag("BlueHoop");
                break;
            case ChaptehGameManager.Objective.HIT_GREEN_HOOPS:
                GetComponent<SpriteRenderer>().CompareTag("GreenHoop");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
