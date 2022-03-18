using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FS_Despawner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.SetActive(false);
        other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0,0,0);
        other.gameObject.GetComponent<Stone>().type = FiveStonesGameManager.Objective.DEFAULT;
        //other.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
        other.gameObject.transform.rotation = Quaternion.identity;
        other.gameObject.GetComponent<Animator>().runtimeAnimatorController = null;
    }
}
