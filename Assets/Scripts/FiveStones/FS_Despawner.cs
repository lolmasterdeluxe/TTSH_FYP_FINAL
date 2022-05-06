using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FS_Despawner : MonoBehaviour
{
    private void Start()
    {
        if (Screen.height > 1080)
            gameObject.transform.position = new Vector3(transform.position.x, ((transform.position.y / 1080) * Screen.height), transform.position.z);
    }
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
