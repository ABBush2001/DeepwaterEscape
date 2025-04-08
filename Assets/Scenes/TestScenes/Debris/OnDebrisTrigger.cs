using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDebrisTrigger : MonoBehaviour
{
    public ParticleSystem explosion;
    public GameObject fallSystem;

    private bool damageCircle = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (damageCircle)
            {
                other.gameObject.GetComponent<Player_Health>().TakeDamage(10);
                damageCircle = false;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Debris"))
        {
            damageCircle = true;
            fallSystem.GetComponent<FallingDebris>().isFalling = false;
            Destroy(other.gameObject);
            StartCoroutine(playExplosion());
        }
    }

    IEnumerator playExplosion()
    {
        explosion.Play();
        
        yield return new WaitForSeconds(2f);

        damageCircle = false;

        explosion.Stop();
    }
}
