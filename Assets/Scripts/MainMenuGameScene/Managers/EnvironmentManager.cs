using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnvironmentManager : MonoBehaviour
{
    //environment manager: moves the backgrounds of the game and stuff relative to time

    [Tooltip("Reference to the Parallex Background Manager")]
    [SerializeField]
    private ParallexBackgroundManager parallexbackgroundmanagerInstance;

    private void Update()
    {
        //for background
        DOBackgroundScrolling();
    }

    public void DOBackgroundScrolling()
    {
        parallexbackgroundmanagerInstance.componentContainer[1].transform.position = new Vector3(parallexbackgroundmanagerInstance.componentContainer[1].transform.position.x - Time.deltaTime, parallexbackgroundmanagerInstance.componentContainer[1].transform.position.y, parallexbackgroundmanagerInstance.componentContainer[1].transform.position.z);

        parallexbackgroundmanagerInstance.componentContainer[2].transform.position = new Vector3(parallexbackgroundmanagerInstance.componentContainer[2].transform.position.x - Time.deltaTime, parallexbackgroundmanagerInstance.componentContainer[2].transform.position.y, parallexbackgroundmanagerInstance.componentContainer[2].transform.position.z);

        parallexbackgroundmanagerInstance.componentContainer[3].transform.position = new Vector3(parallexbackgroundmanagerInstance.componentContainer[3].transform.position.x - Time.deltaTime, parallexbackgroundmanagerInstance.componentContainer[3].transform.position.y, parallexbackgroundmanagerInstance.componentContainer[3].transform.position.z);

        if (parallexbackgroundmanagerInstance.componentContainer[1].transform.position.x < -25)
        {
            parallexbackgroundmanagerInstance.componentContainer[1].transform.position = new Vector3(25, parallexbackgroundmanagerInstance.componentContainer[1].transform.position.y, parallexbackgroundmanagerInstance.componentContainer[1].transform.position.z);
        }
        if (parallexbackgroundmanagerInstance.componentContainer[2].transform.position.x < -25)
        {
            parallexbackgroundmanagerInstance.componentContainer[2].transform.position = new Vector3(23, parallexbackgroundmanagerInstance.componentContainer[2].transform.position.y, parallexbackgroundmanagerInstance.componentContainer[2].transform.position.z);
        }
        if (parallexbackgroundmanagerInstance.componentContainer[3].transform.position.x < -25)
        {
            parallexbackgroundmanagerInstance.componentContainer[3].transform.position = new Vector3(24, parallexbackgroundmanagerInstance.componentContainer[3].transform.position.y, parallexbackgroundmanagerInstance.componentContainer[3].transform.position.z);
        }
    }
}
