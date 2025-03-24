using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Ths script is placed on a collider to check if the player
 * is within range of a dialogue queue. If so, then the appropriate
 * UI prompt is activated. When players press the prompt button,
 * Dialogue is entered
*/
public class DialogueTrigger : MonoBehaviour
{
    //variables
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJson;

    private bool playerInRange;

    //sets player in range to false by default
    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(true);

    }

    //checks if the player is in range and dialogue hasn't started yet
    private void Update()
    {
        if(playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            if(Input.GetKeyDown(KeyCode.F))
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJson);
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    //scripts to activate/deactivate prompt for dialogue 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
