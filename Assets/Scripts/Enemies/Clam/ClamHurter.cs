using UnityEngine;

/*
 * This script does not hurt the clam, but it does trigger a messa
 * Remind me to rename variables and tooltips to make sense, I'm writing this while delusional.
 * We have to check for hurt collisions here to not screw with triggers on the brain.
 */

public class ClamHurter : MonoBehaviour
{
    private ClamWalker walkerScript;
    
    private void Awake() {
        walkerScript = GetComponentInParent<ClamWalker>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) {
            walkerScript.AttemptHurt();
        }
    }
}
