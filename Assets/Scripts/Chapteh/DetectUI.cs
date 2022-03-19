using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectUI : MonoBehaviour
{
    //[SerializeField] private GameObject ButtonBase, ChargeBarCanvas;
    [SerializeField] private Image ButtonBase, ButtonLeft, ButtonRight, ChargeBarCanvas, ChargeBarBorder, ChargeBarHandle;
    private Color ButtonBaseOriginal, ButtonLeftOriginal, ButtonRightOriginal, ChargeBarCanvasOriginal, ChargeBarBorderOriginal, ChargeBarHandleOriginal;
    private Transform player;

    void Start()
    {
        player = GetComponent<Transform>();
        ButtonBaseOriginal = ButtonBase.color;
        ButtonLeftOriginal = ButtonLeft.color;
        ButtonRightOriginal = ButtonRight.color;
        ChargeBarCanvasOriginal = ChargeBarCanvas.color;
        ChargeBarBorderOriginal = ChargeBarBorder.color;
        ChargeBarHandleOriginal = ChargeBarHandle.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x <= -23)
        {
            ButtonBase.color = new Color(255, 255, 255, 0.5f);
            ButtonLeft.color = new Color(255, 255, 255, 0.5f);
            ButtonRight.color = new Color(255, 255, 255, 0.5f);
        }
        else
        {
            ButtonBase.color = ButtonBaseOriginal;
            ButtonLeft.color = ButtonLeftOriginal;
            ButtonRight.color = ButtonRightOriginal;
        }

        if (player.transform.position.x >= 27)
        {
            ChargeBarCanvas.color = new Color(255, 255, 255, 0.5f);
            ChargeBarBorder.color = new Color(ChargeBarBorderOriginal.r, ChargeBarBorderOriginal.g, ChargeBarBorderOriginal.b, 0.5f);
            ChargeBarHandle.color = new Color(255, 255, 255, 0.5f);
        }
        else
        {
            ChargeBarCanvas.color = ChargeBarCanvasOriginal;
            ChargeBarBorder.color = ChargeBarBorderOriginal;
            ChargeBarHandle.color = ChargeBarHandleOriginal;
        }
    }
}
