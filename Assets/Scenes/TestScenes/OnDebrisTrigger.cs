using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDebrisTrigger : MonoBehaviour
{
    public ParticleSystem explosion;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Debris"))
        {
            Destroy(other.gameObject);
            explosion.Play();
        }
    }
}
