using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script handles the trigger for the urchin to shoot his spikes
*/
public class SpikeTrigger : MonoBehaviour
{
    public GameObject spikes;
    public ParticleSystem projectiles;

    Vector3 startPoint;

    public float radius;
    public float duration = 0.001f;
    public int damage = 5;

    public GameObject circleMat;
    private Color tempColor;

    private bool inDamageZone = false;

    private void Start()
    {
        tempColor = circleMat.GetComponent<Renderer>().material.color;

        startPoint = transform.position;

        radius = 5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inDamageZone = true;
            SpawnProjectiles(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inDamageZone = false;
        }
    }

    private void SpawnProjectiles(GameObject player)
    {
        
        //projectiles.Play();
        StartCoroutine(flashAnimation(player));
        //StartCoroutine(pushPlayerBack(player));
    }

    IEnumerator flashAnimation(GameObject playerObj)
    {
        Renderer rend = circleMat.GetComponent<Renderer>();
        Material mat = rend.material;

        Color color = mat.color;

        for (int cycle = 0; cycle < 2; cycle++)
        {
            // Fade out
            for (float alpha = color.a; alpha > 0; alpha -= 0.07f)
            {
                color.a = alpha;
                mat.color = color;
                yield return new WaitForSeconds(duration);
            }

            // Fade in
            for (float alpha = 0; alpha < 0.5f; alpha += 0.07f)
            {
                color.a = alpha;
                mat.color = color;
                yield return new WaitForSeconds(duration);
            }

            yield return new WaitForSeconds(0.5f);
        }

        // Final fade out
        for (float alpha = 0.7f; alpha > 0; alpha -= 0.07f)
        {
            color.a = alpha;
            mat.color = color;
            yield return new WaitForSeconds(duration);
        }

        //projectiles.Play();
        //spikes.GetComponent<SpikeExplosion>().Explode();
        GameObject spike = Instantiate(spikes, this.gameObject.transform);
        spike.GetComponent<SpikeExplosion>().Explode();

        if (inDamageZone)
        {
            playerObj.gameObject.GetComponent<Player_Health>().TakeDamage(damage);
        }

        yield return new WaitForSeconds(2f);
        //projectiles.Stop();
    }

}

