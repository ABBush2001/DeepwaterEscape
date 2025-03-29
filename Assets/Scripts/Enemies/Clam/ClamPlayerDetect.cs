using UnityEngine;

/*
 * This script calls the Alert() function in ClamWalker when it detects either a player or gunshot noise.
 */

public class ClamPlayerDetect : MonoBehaviour
{
    [SerializeField] [Tooltip("ClamWalker goes here. Why are you touching this.")]
    private ClamWalker walkScript;
    public bool detectPlayer;
    public bool detectPlayerNoise;

    private void OnTriggerEnter(Collider other)
    {
        if (detectPlayer) {
            if (other.CompareTag("Player"))
            {
                walkScript.Alert(other);
            }
        }
        if (detectPlayerNoise)
        {
            if (other.CompareTag("PlayerNoise"))
            {
                walkScript.Alert(other);
            }
        }

    }
}
