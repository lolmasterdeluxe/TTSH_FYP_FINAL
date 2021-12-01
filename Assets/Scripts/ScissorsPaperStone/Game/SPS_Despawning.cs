using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPS_Despawning : MonoBehaviour
{
    //this helps to despawn anything that did not manage to get despawned:
    //removes it from the game scene altogether

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyTag" || other.gameObject.tag == "Obstacle")
        {
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
            Destroy(other.gameObject);
            //if it has a rigidbody we destroy it
            if (other.gameObject.GetComponent<Rigidbody>() != null)
            {
                Destroy(other.gameObject.GetComponent<Rigidbody>());
            }
            Debug.Log("Obstacle OR Enemy has been destroyed");
        }
    }
}
