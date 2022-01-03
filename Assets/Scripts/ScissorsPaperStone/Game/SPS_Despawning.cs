using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPS_Despawning : MonoBehaviour
{
    //this helps to despawn anything that did not manage to get despawned:
    //removes it from the game scene altogether

    #region Variables

    SPS_ObjectManager objectspawningInstance;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        objectspawningInstance = FindObjectOfType<SPS_ObjectManager>();
    }

    private void Update()
    {
        if (objectspawningInstance.objectWaveList.Count == 0)
        {
            objectspawningInstance.b_waveCompleted = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        //remove the gameobject instance in the list
        objectspawningInstance.objectWaveList.Remove(other.gameObject);

        Destroy(other.gameObject);
        //if it has a rigidbody we destroy it
        if (other.gameObject.GetComponent<Rigidbody>() != null)
        {
            Destroy(other.gameObject.GetComponent<Rigidbody>());
        }
        Debug.Log("Obstacle OR Enemy has been destroyed");
    }

    #endregion

}
