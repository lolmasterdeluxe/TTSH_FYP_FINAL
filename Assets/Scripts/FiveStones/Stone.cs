using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public FiveStonesGameManager.Objective type;
    public Sprite grey_sprite;
    public Sprite blue_sprite;
    public Sprite yellow_sprite;
    public Sprite red_sprite;


    // Start is called before the first frame update
    void Start()
    {
        if (type == FiveStonesGameManager.Objective.DEFAULT)
            type = FiveStonesGameManager.GetRandomColouredObjective();

        switch (type)
        {
            case FiveStonesGameManager.Objective.CATCH_RED_STONES:
                GetComponent<SpriteRenderer>().sprite = red_sprite;
                break;
            case FiveStonesGameManager.Objective.CATCH_YELLOW_STONES:
                GetComponent<SpriteRenderer>().sprite = yellow_sprite;
                break;
            case FiveStonesGameManager.Objective.CATCH_BLUE_STONES:
                GetComponent<SpriteRenderer>().sprite = blue_sprite;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
