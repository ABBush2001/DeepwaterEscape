using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    public GameObject spawner;

    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "Player")
        {
            spawner.GetComponent<EnemySpawning>().startEnemySpawn();
        }
    }
}
