using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jellyfish_Spark_script : MonoBehaviour
{
    public GameObject particlePrefab; 
    private ParticleSystem particleSystemInstance;

    private void Start()
    {
        if (particlePrefab != null)
        {
            
            Vector3 particlePosition = transform.position + new Vector3(0, 10, 0);

            GameObject particleObject = Instantiate(particlePrefab, particlePosition, Quaternion.identity);
            particleSystemInstance = particleObject.GetComponent<ParticleSystem>();

            if (particleSystemInstance != null)
            {
                particleSystemInstance.Play();
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
