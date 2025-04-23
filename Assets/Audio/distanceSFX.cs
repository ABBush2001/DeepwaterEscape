using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class distanceSFX : MonoBehaviour
{
    public Transform player;
    public AudioSource audioSource;
    public float maxDistance = 20f;

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // Normalize volume between 0 and 1
        float volume = 1 - Mathf.Clamp01(distance / maxDistance);
        audioSource.volume = volume;
    }
}
