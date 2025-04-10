using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterAudioSources : MonoBehaviour
{
    public AudioSource bossMusic; // Assign this in the Unity Editor
    private List<AudioSource> soundList = new List<AudioSource>();

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SoundSource"))
        {
            AudioSource audioSource = other.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                soundList.Add(audioSource);
            }
        }
        else if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger zone.");
            if (bossMusic != null && !bossMusic.isPlaying) // Play boss music if not already playing
            {
                bossMusic.Play();
            }

            foreach (AudioSource sound in soundList)
            {
                if (!sound.isPlaying)
                {
                    sound.Play();
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SoundSource"))
        {
            AudioSource audioSource = other.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                soundList.Remove(audioSource);
            }
        }
        else if (other.CompareTag("Player"))
        {
            if (bossMusic != null && bossMusic.isPlaying) // Stop boss music when player leaves
            {
                bossMusic.Stop();
            }

            foreach (AudioSource sound in soundList)
            {
                if (sound.isPlaying)
                {
                    sound.Stop();
                }
            }
        }
    }
}
