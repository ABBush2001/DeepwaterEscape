using UnityEngine;

/*
 * Script created by Wyatt Blackwell, last edited by ___
 * Modified date: 4/10/2024
 * This script handles clamination by recieving state information from ClamWalker and applying the appropriate animation.
 * In other words, ClamWalker is the brain, this is a leech.
 */

public class ClamAnimPlayer : MonoBehaviour
{
    Animator claminatorController;

    private void Start()
    {
        if ((claminatorController = GetComponentInChildren<Animator>()) == null) { // Obtain animator component IN the if statement, log error in body
            Debug.LogAssertion("Clam could not find animator component!",this.gameObject);
        }

    }
    public void SetAnimBool(string m_param, bool m_value) => claminatorController.SetBool(m_param, m_value);
    public void SetAnimTrigger(string m_param) => claminatorController.SetTrigger(m_param);

    /*
     * B_isSleeper
     * B_isAmbusher
     * B_isPatroller
     * t_jumping
     * t_spooked
     * b_hasDeburrowed
     * b_hasSpottedPlayer
     * b_isDead
     */
}
