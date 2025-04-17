using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jellyfish_Spark_script : MonoBehaviour
{
    public GameObject particlePrefab; // Assign your particle prefab in the Inspector
    private ParticleSystem particleSystemInstance; // Reference to the instantiated Particle System

    private void Start()
    {
        if (particlePrefab != null)
        {
            // Instantiate the particle prefab and get its ParticleSystem component
            GameObject particleObject = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            particleSystemInstance = particleObject.GetComponent<ParticleSystem>();

            if (particleSystemInstance != null)
            {
                particleSystemInstance.Play(); // Start the particle system
            }
            else
            {
                Debug.LogError("The particlePrefab does not contain a ParticleSystem component!");
            }
        }
        else
        {
            Debug.LogError("Particle prefab is not assigned in the Inspector!");
        }
    }
}
