using UnityEngine;

/*
 * This script exists purely for the clams to reference the health variable of Player_Health.
 * Doing it here saves each clam from individually searching for the script.
 * Yes it's jank and I should probably rewrite the health system from scratch.
 */

public class ClamPlayerHealthRef : MonoBehaviour
{
    public Player_Health playerHealth;
    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("UI").GetComponent<Player_Health>();
    }

    public Player_Health GetPlayerHealth()
    {
        return playerHealth;
    }
}
