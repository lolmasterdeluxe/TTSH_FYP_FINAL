using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectUI : MonoBehaviour
{
    //[SerializeField] private GameObject ButtonBase, ChargeBarCanvas;
    [SerializeField] private CanvasGroup dPadPanel, ChargeBarCanvas, JoystickPanel;
    private Transform player;

    void Start()
    {
        player = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x <= -23)
        {
            dPadPanel.alpha = 0.5f;
            JoystickPanel.alpha = 0.5f;
        }
        else
        {
            dPadPanel.alpha = 1f;
            JoystickPanel.alpha = 1f;
        }

        if (player.transform.position.x >= 27)
        {
            ChargeBarCanvas.alpha = 0.5f;
        }
        else
        {
            ChargeBarCanvas.alpha = 1f;
        }
    }
}
