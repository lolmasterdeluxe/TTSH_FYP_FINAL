using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractGame : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject TutorialPanel;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TutorialPanel == null)
            return;

        TutorialPanel.SetActive(true);
    }
}
