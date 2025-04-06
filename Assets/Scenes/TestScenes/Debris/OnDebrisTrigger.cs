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
                other.gameObject.GetComponent<Player_Health>().TakeDamage(5);
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
        damageCircle = false;

        yield return new WaitForSeconds(2f);

        explosion.Stop();
    }
}
