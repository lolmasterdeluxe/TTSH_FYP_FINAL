using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SPS_ObjectDespawningManager : MonoBehaviour
{
    //this manager handles the despawning of objects (enemies, obstacles, powerups) off-screen

    #region Variables

    [Tooltip("Reference to Object Manager Script")]
    [SerializeField] private SPS_ObjectManager objectspawningInstance;

    [Tooltip("Reference to UI Manager Script")]
    [SerializeField] private SPS_UIManager uimanagerInstance;

   

    #endregion

    

    #region Unity Callbacks
    private void Update()
    {
        if (uimanagerInstance.b_gameEnded == true)
        {
            foreach (GameObject a in objectspawningInstance.objectWaveList)
            {
                if (a != null)
                {
                    Destroy(a);
                    if (a.gameObject.GetComponent<Rigidbody2D>() != null)
                    {
                        Destroy(a.gameObject.GetComponent<Rigidbody2D>());
                    }
                    //Debug.Log("destroying enemies");
                }
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
        if (!other.CompareTag("EnemyTag")&&!other.CompareTag("Powerup"))
        {
            //remove the gameobject instance from the list

            objectspawningInstance.objectWaveList.Remove(other.gameObject);

            //we now destroy it

            Destroy(other.gameObject);
            //if it has a rigidbody2d, we destroy it as well

            if (other.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                Destroy(other.gameObject.GetComponent<Rigidbody2D>());
            }


        }
        else if (other.CompareTag("EnemyTag"))
        {
            other.gameObject.transform.DOKill(true);
            other.transform.position = objectspawningInstance.objectStartPosition.transform.position;
            objectspawningInstance.objectWaveList.Remove(other.gameObject);
            other.gameObject.SetActive(false);

        }
        //Debug.Log("An object has been destroyed");
        
    }

    #endregion

}
