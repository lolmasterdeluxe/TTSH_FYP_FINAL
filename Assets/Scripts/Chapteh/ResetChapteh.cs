using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetChapteh : MonoBehaviour
{
    public Transform chapteh;

    public Transform spawnPoint;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //chapteh.position = new Vector3(chapteh.position.x, 4.43f, chapteh.position.z);

        chapteh.position = spawnPoint.position;
    }
}
