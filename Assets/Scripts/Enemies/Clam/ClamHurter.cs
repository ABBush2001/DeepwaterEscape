using UnityEngine;

/*
 * We have to check for hurt collisions here to not screw with triggers on the brain, but -
 * defer hurt logic to ClamWalker to keep logic concentrated in one place.
 */

public class ClamHurter : MonoBehaviour
{
    private ClamWalker walkerScript;
    
    private void Awake() {
        walkerScript = GetComponentInParent<ClamWalker>(); // Get clamwalker script component
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) {
            walkerScript.AttemptHurt();
        }
    }
}
