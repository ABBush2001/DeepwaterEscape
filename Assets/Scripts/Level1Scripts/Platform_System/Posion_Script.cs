using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script handles damage dealt from poison in level 5.
*/
public class Posion_Script : MonoBehaviour
{
    public int poiDmg = 1; // Poison damage
    public float damageOverTime = 0.5f;
    public float poisonLast = 15f;

    //If a player collides with poison objects, deal damage to them   
    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Player_Health playerHealth = other.GetComponent<Player_Health>();
            if (playerHealth != null)
            {
                StartCoroutine(applyPosion(playerHealth));
            }
        }
    }

    //Deal poison damage over time
    private IEnumerator applyPosion(Player_Health playerH)
    {
        float elapsed = 0f;

        while (elapsed < poisonLast)
        {
            playerH.TakeDamage(poiDmg);
            elapsed += damageOverTime;
            yield return new WaitForSeconds(damageOverTime);
        }

    }
}
