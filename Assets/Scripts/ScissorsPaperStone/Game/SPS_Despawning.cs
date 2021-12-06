﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPS_Despawning : MonoBehaviour
{
    //this helps to despawn anything that did not manage to get despawned:
    //removes it from the game scene altogether

    #region Variables

    SPS_ObjectSpawningScript objectspawningInstance;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        objectspawningInstance = FindObjectOfType<SPS_ObjectSpawningScript>();
    }

    private void Update()
    {
        if (objectspawningInstance.objectwaveList.Count == 0)
        {
            objectspawningInstance.waveCompleted = false;
            //Debug.Log("List is empty");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyTag" || other.gameObject.tag == "Obstacle" || other.gameObject.tag == "SafeObstacle")
        {
            //remove the gameobject instance in the list
            objectspawningInstance.objectwaveList.Remove(other.gameObject);

            Destroy(other.gameObject);
            //if it has a rigidbody we destroy it
            if (other.gameObject.GetComponent<Rigidbody>() != null)
            {
                Destroy(other.gameObject.GetComponent<Rigidbody>());
            }
            Debug.Log("Obstacle OR Enemy has been destroyed");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "EnemyTag" || other.gameObject.tag == "Obstacle")
        {
            //remove the gameobject instance in the list
            objectspawningInstance.objectwaveList.Remove(other.gameObject);

            Destroy(other.gameObject);
            //if it has a rigidbody we destroy it
            if (other.gameObject.GetComponent<Rigidbody>() != null)
            {
                Destroy(other.gameObject.GetComponent<Rigidbody>());
            }
            Debug.Log("Obstacle OR Enemy has been destroyed");
        }
    }

    #endregion


}
