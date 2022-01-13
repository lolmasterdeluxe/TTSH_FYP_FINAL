﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPS_ObjectDespawningManager : MonoBehaviour
{
    //this manager handles the despawning of objects (enemies, obstacles, powerups) off-screen

    #region Variables

    [Tooltip("Reference to Object Manager Script")]
    SPS_ObjectManager objectspawningInstance;

    [Tooltip("Reference to UI Manager Script")]
    SPS_UIManager uimanagerInstance;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        //add instance of object manager HERE
        objectspawningInstance = FindObjectOfType<SPS_ObjectManager>();

        //add instance of UI manager HERE
        uimanagerInstance = FindObjectOfType<SPS_UIManager>();
    }

    private void Update()
    {
        if (uimanagerInstance.b_gameEnded == true)
        {
            foreach (GameObject a in objectspawningInstance.objectWaveList)
            {
                Destroy(a);
                if (a.gameObject.GetComponent<Rigidbody2D>() != null)
                {
                    Destroy(a.gameObject.GetComponent<Rigidbody2D>());
                }
                Debug.Log("destroying enemies");
            }
        }

        if (uimanagerInstance.b_gameEnded)
            return;

        //we set the wave completed to be false when the list is empty
        //this starts the NEXT wave
        if (objectspawningInstance.objectWaveList.Count == 0)
        {
            objectspawningInstance.b_waveCompleted = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //remove the gameobject instance from the list
        objectspawningInstance.objectWaveList.Remove(other.gameObject);

        Debug.Log(other.gameObject);

        //we now destroy it
        Destroy(other.gameObject);
        //if it has a rigidbody2d, we destroy it as well
        if (other.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            Destroy(other.gameObject.GetComponent<Rigidbody2D>());
        }
        Debug.Log("An object has been destroyed");
    }

    #endregion

}
