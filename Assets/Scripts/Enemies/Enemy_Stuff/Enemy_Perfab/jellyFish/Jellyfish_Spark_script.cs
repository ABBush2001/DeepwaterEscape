using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jellyfish_Spark_script : MonoBehaviour
{
    public GameObject particlePrefab; // Assign your particle prefab in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the Player has the "Player" tag
        {
            Instantiate(particlePrefab, transform.position, Quaternion.identity);
        }
    }
}

