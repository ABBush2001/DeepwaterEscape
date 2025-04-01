using UnityEngine;
using TMPro;
using Ink.Runtime;  // Make sure to include this for the Story class

public class DialogueManager : MonoBehaviour
{
    // Variables
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI pressEText;  // "Press E to Continue" Text

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }
    public bool dialogueComplete = false;

    private TextEffect textEffect;

    private static DialogueManager instance;

    // Singleton pattern - get the instance of DialogueManager
    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        // Make sure there's only one instance of DialogueManager
        if (instance != null)
        {
            Debug.LogWarning("Found more than one DialogueManager in the scene!");
        }
        instance = this;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        // Get the TextEffect component from the dialogueText object
        textEffect = dialogueText.GetComponent<TextEffect>();

        // Set up the "Press E" text to be hidden initially
        if (pressEText != null)
        {
            pressEText.alpha = 0;  // Hide it at the start
        }
    }

    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            dialogueComplete = false;
            return;
        }

        // Flicker the "Press E" prompt using LeanTween
        if (pressEText != null && dialogueIsPlaying)
        {
            LeanTween.alphaText(pressEText.rectTransform, pressEText.alpha == 0 ? 1 : 0, 0.5f)
                .setLoopPingPong();  // Ping-pong loop to fade in and out
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);  // Use the Story class from Ink
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        // Stop flickering when dialogue ends
        if (pressEText != null)
        {
            LeanTween.cancel(pressEText.rectTransform);  // Cancel any ongoing LeanTween animations
            pressEText.alpha = 0;  // Hide the "Press E" text
        }

        dialoguePanel.SetActive(false);
        dialogueIsPlaying = false;
        dialogueText.text = "";
        dialogueComplete = true;
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            string dialogue = currentStory.Continue();

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
