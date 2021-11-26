using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public GameObject leftStep, rightStep;
    private bool isleftStepOn, isrightStepOn;

    private void Start()
    {
        leftStep.SetActive(false);
        isleftStepOn = false;
        
        rightStep.SetActive(false);
        isrightStepOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        onLeftMouseClick();
        onRightMouseClick();
    }

    public void onLeftMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isleftStepOn == false)
            {
                leftStep.SetActive(true);
                isleftStepOn = true;

                Debug.Log("Taken left step");

                //Vector3 tempVal = new Vector3(leftStep.transform.position.x, leftStep.transform.position.y, leftStep.transform.position.z);
                //tempVal.y += 253;
                //leftStep.transform.Translate(0, tempVal.y, 0);

                leftStep.transform.Translate(0, 253, 0);

                Debug.Log(leftStep.transform.position);
            }

            if (isrightStepOn == true)
            {
                rightStep.SetActive(false);
                isrightStepOn = false;

                Debug.Log("Taken right step off");
            }
        }
    }

    public void onRightMouseClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (isrightStepOn == false)
            {
                rightStep.SetActive(true);
                isrightStepOn = true;

                Debug.Log("Taken right step");

                rightStep.transform.Translate(0, 253, 0);

                Debug.Log(rightStep.transform.position);
            }

            if (isleftStepOn == true)
            {
                leftStep.SetActive(false);
                isleftStepOn = false;

                Debug.Log("Taken left step off");
            }
        }
    }

    //public void onLeftandRightClick()
    //{
    //    if (Input.GetMouseButtonDown(0) && Input.GetMouseButtonDown(1))
    //    {
    //        if (isleftStepOn == false || isrightStepOn == false)
    //        {
    //            leftStep.SetActive(true);
    //            isrightStepOn = true;
    //            rightStep.SetActive(true);
    //            isrightStepOn = true;

    //            Debug.Log("Taken both step");

    //            leftStep.transform.Translate(0, 253, 0);
    //            rightStep.transform.Translate(0, 253, 0);

    //            Debug.Log(rightStep.transform.position);
    //        }
    //    }
    //}
}
