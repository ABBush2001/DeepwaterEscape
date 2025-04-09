using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public AudioSource audioSource;  // Assign in Inspector
    public AudioClip bossMusic;      // Assign in Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure your player has the "Player" tag
        {
            if (audioSource.clip != bossMusic) // Only change if it's a different clip
            {
                audioSource.clip = bossMusic;
                audioSource.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Stop(); // Optional: Stop music when leaving the trigger
        }
    }
}
