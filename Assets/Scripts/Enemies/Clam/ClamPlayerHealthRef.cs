using UnityEngine;
using System.Collections;

/*
 * This script exists purely for the clams to reference the health variable of Player_Health.
 * Doing it here saves each clam from individually searching for the script.
 * Yes it's jank and I should probably rewrite the health system from scratch.
 */

public class ClamPlayerHealthRef : MonoBehaviour
{
    public Player_Health playerHealth;
    public GameObject player;
    const string MethodName = "GetPlayerHealthScript"; // silence compiler suggestion about string literal
    private void Start()
    {
        //playerHealth = GameObject.FindGameObjectWithTag("UI").GetComponent<Player_Health>();
        Invoke(MethodName, .1f); // Invoke w/ .1f delay to make sure the UI gets loaded before referencing it.
    }

    public Player_Health GetPlayerHealth()
    {
        return playerHealth;
    }

    public GameObject GetPlayer()
    {
        return player;
    }
    
    private void GetPlayerHealthScript()
    {
        //playerHealth = GameObject.FindGameObjectWithTag("UI").GetComponent<Player_Health>();
    }
}
