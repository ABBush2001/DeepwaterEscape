using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * This script is placed on a collider to check if the player
 * is within range of a dialogue queue. If so, then the appropriate
 * UI prompt is activated. When players press the prompt button,
 * Dialogue is entered.
*/
public class DialogueTrigger : MonoBehaviour
{
    // Variables
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;  // UI visual cue (like a 'Press F to talk' prompt)

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJson;  // Ink file for dialogue

    [SerializeField] private TextMeshProUGUI continueText;

    [Header("Optional Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private string animvar;

    private bool playerInRange;

    // Set playerInRange to false by default
    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);  // Make sure the visual cue starts hidden
    }

    // Checks if the player is in range and dialogue hasn't started yet
    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            // Show the visual cue only if player is in range and dialogue isn't playing
            visualCue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))  // Player presses F to start the dialogue
            {
                continueText.enabled = true;
                DialogueManager.GetInstance().EnterDialogueMode(inkJson);
            }
        }
        else
        {
            visualCue.SetActive(false);  // Hide the visual cue when the player is out of range or dialogue is playing
        }
    }

    // Trigger when player enters collider range
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Check if the object entering is the player
        {
            playerInRange = true;
        }
    }

    // Trigger when player exits collider range
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))  // Check if the object exiting is the player
        {
            playerInRange = false;
        }
    }
}
