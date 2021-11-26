using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowButton : MonoBehaviour
{
    public Button rotateButtonLeft, rotateButtonRight;
    public new Transform camera;
    
    float rotateZ = 180;

    private void Start()
    {
        Button btnL = rotateButtonLeft.GetComponent<Button>();
        btnL.onClick.AddListener(onArrowTurn);

        Button btnR = rotateButtonRight.GetComponent<Button>();
        btnR.onClick.AddListener(onArrowTurn);
    }

    public void onArrowTurn()
    {
       camera.transform.Rotate(0, 0, rotateZ);
    }
}
