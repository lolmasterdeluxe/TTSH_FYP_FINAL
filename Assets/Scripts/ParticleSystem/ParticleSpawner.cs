using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    [SerializeField] private Transform Spawnpoint;
    public GameObject prefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(prefab, Spawnpoint.position, Spawnpoint.rotation);
        
    }
}
