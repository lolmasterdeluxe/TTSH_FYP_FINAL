using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractNPC : MonoBehaviour
{
    // Start is called before the first frame update

    public enum NPC_TYPE
    {
        FIVE_STONES,
        SPS,
        CHAPTEH,
        CUSTOMIZER
    }

    public NPC_TYPE type;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject.Find("MainCharacter").GetComponent<PlayerMovementScript>().SetRollbackPosition(new Vector2(transform.position.x, transform.position.y));

        switch (type)
        {
            case NPC_TYPE.CHAPTEH:
                SceneManager.LoadScene("Chapteh");
                break;
            case NPC_TYPE.FIVE_STONES:
                SceneManager.LoadScene("FiveStonesFruitNinja");
                break;
            case NPC_TYPE.SPS:
                SceneManager.LoadScene("Scissors Paper Stone");
                break;
            case NPC_TYPE.CUSTOMIZER:
                SceneManager.LoadScene("CustomizeScene");
                break;
        }
    }
}