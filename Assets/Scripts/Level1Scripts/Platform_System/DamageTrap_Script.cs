using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script handles platform damage for the trap platforms
 * in level 5.
*/
public class DamageTrap_Script : MonoBehaviour
{
    //variables
    [SerializeField] private Animator Trap = null;

    [SerializeField] private bool Damage_Trap = false;

    //if player collides with platform, deal damage to them
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Damage_Trap)
            {
                Trap.Play("Damage_Trap", 0, 0.0f);
                gameObject.SetActive(false);
            }
        }
    }
}
