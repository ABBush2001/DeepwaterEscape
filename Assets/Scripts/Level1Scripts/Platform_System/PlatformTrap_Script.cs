using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is called for trap platforms in level 5.
*/
public class PlatformTrap_Script : MonoBehaviour
{
    //variables
    [SerializeField] private Animator Trap = null;

    [SerializeField] private bool Platform_Trap = false;

    //If player collides with trap platform, play the trap animation
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (Platform_Trap)
            {
                Trap.Play("Platform_Trap", 0, 0.0f);
                gameObject.SetActive(false);
            }
        }
    }
}
