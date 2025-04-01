using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script handles the trigger for the urchin to shoot his spikes
*/
public class SpikeTrigger : MonoBehaviour
{
    public ParticleSystem projectiles;

    Vector3 startPoint;

    public float radius, moveSpeed;

    private void Start()
    {
        startPoint = transform.position;

        radius = 5f;
        moveSpeed = 5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SpawnProjectiles(other.gameObject);
            other.gameObject.GetComponent<Player_Health>().TakeDamage(5);
        }
    }

    private void SpawnProjectiles(GameObject player)
    {
        projectiles.Play();
        //StartCoroutine(pushPlayerBack(player));
    }

    IEnumerator pushPlayerBack(GameObject player)
    {
        player.GetComponent<CommentedThirdPersonController>().velocity = 0;

        for(int i = 0; i < 10; i++)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, new Vector3(player.transform.forward.x * -1, player.transform.forward.y, player.transform.forward.z * -1), Time.deltaTime * 30);
            yield return new WaitForSeconds(0.1f);
        }

        player.GetComponent<CommentedThirdPersonController>().velocity = 10;
    }
}

