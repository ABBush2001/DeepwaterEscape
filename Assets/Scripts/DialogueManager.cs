using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

/*
 * This script represents a dialogue manager system. It manages active dialogue
 * queues, allowing them to continue and end
*/

public class DialogueManager : MonoBehaviour
{
    //variables
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }

    public bool dialogueComplete = false;

    private static DialogueManager instance;

    // Reference to the TextEffect component
    private TextEffect textEffect;

    // Awake checks if a dialogue manager already exists in scene
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue manager in the scene!");
        }
        instance = this;

        //audioSource = this.gameObject.AddComponent<AudioSource>();
    }

    // Returns if there is an active instance of the DialogueManager
    public static DialogueManager GetInstance()
    {
        return instance;
    }

    // Sets dialogue active to false on start
    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        // Get the TextEffect component from the dialogueText object
        textEffect = dialogueText.GetComponent<TextEffect>();
    }

    // Checks if dialogue is playing. If player presses appropriate
    // dialogue button, it continues the dialogue
    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            dialogueComplete = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ContinueStory();
        }
    }

    // Enters the dialogue queue
    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();
    }

    // Exits the dialogue queue
    private void ExitDialogueMode()
    {
        Debug.Log("Running");

        dialoguePanel.SetActive(false);
        dialogueIsPlaying = false;
        dialogueText.text = "";
        dialogueComplete = true;
    }

    // Continues dialogue and uses typewriter effect to display text
    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            string dialogue = currentStory.Continue();

            // Trigger the typewriter effect with the dialogue text
            if (textEffect != null)
            {
                textEffect.SetText(dialogue);  // Use the SetText method to update the text and start the effect
            }
        }
        else
        {
            ExitDialogueMode();
        }
    }
}

