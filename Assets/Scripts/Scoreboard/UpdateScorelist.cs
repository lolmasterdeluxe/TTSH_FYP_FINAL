using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScorelist : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        if (!ScoreManager.Instance.driveRequest.IsDone)
        {
            gameObject.GetComponent<Button>().interactable = false;
            gameObject.GetComponent<ButtonAnimation>().isEnabled = false;
        }
        else
        {
            gameObject.GetComponent<Button>().interactable = true;
            gameObject.GetComponent<ButtonAnimation>().isEnabled = true;
        }
    }
}
