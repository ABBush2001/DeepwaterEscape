using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDebrisTrigger : MonoBehaviour
{
    public ParticleSystem explosion;
    public GameObject fallSystem;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Debris"))
        {
            fallSystem.GetComponent<FallingDebris>().isFalling = false;
            Destroy(other.gameObject);
            StartCoroutine(playExplosion());
        }
    }

    IEnumerator playExplosion()
    {
        explosion.Play();

        yield return new WaitForSeconds(2f);

        explosion.Stop();
    }
}
