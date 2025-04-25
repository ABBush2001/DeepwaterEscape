using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceSFX : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public AudioSource audioSource;

    [Header("Audio Settings")]
    public float maxDistance = 20f;
    public float smoothSpeed = 2f;

    private float currentVolume = 0f;
    private Transform myTransform;

    void Awake()
    {
        myTransform = transform;

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (player == null || audioSource == null) return;

        float distance = Vector3.Distance(myTransform.position, player.position);

        // Normalize the target volume
        float targetVolume = 1 - Mathf.Clamp01(distance / maxDistance);

        // Smooth the volume transition
        currentVolume = Mathf.Lerp(currentVolume, targetVolume, Time.deltaTime * smoothSpeed);

        audioSource.volume = currentVolume;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
}
