using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script handles dealing damage to the player with the Octopus arm
*/
public class OctopusDamage : MonoBehaviour
{
    private bool tookDamage = true;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (tookDamage)
            {
                collision.gameObject.GetComponent<Player_Health>().TakeDamage(10);
                StartCoroutine(waitToDamage());
            }

        }

        IEnumerator waitToDamage()
        {
            tookDamage = false;
            yield return new WaitForSeconds(3f);
            tookDamage = true;
        }
    }
}
