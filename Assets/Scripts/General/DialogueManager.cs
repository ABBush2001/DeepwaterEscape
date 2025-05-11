 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.SceneManagement;

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
    [SerializeField] private TextMeshProUGUI dialogueName;

    private Story currentStory;
    public bool DialogueIsPlaying { get; private set; }

    public bool dialogueComplete = false;

    public AudioSource audioDialogue;

    private bool isAnimating = false;
    private string animTriggerString;

    private static DialogueManager instance;

    private string dialogue;

    // Reference to the TextEffect component
    private TextEffect textEffect;

    public GameObject nextLevelTrigger;

    private Animator animator;

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
        DialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        // Get the TextEffect component from the dialogueText object
        textEffect = dialogueText.GetComponent<TextEffect>();
    }

    // Checks if dialogue is playing. If player presses appropriate
    // dialogue button, it continues the dialogue
    private void Update()
    {
        if (!DialogueIsPlaying)
        {
            dialogueComplete = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ContinueStory();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // if animating something, animate it.
            if (isAnimating)
            {
                animator.SetTrigger(animTriggerString);
            }
            // Trigger the typewriter effect with the dialogue text
            if (textEffect != null)
            {
                // Set the name of the character currently speaking to the current tag above the current line of dialogue
                List<string> tags = currentStory.currentTags;
                if (tags.Count > 0)
                {
                    dialogueName.text = tags[0];
                }
                textEffect.SetText(dialogue);  // Use the SetText method to update the text and start the effect

            }
        }
    }

    // Enters the dialogue queue
    public void EnterDialogueMode(TextAsset inkJSON)
    {
        if(audioDialogue != null)
        {
            audioDialogue.Play();
        }

        currentStory = new Story(inkJSON.text);
        DialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();
        if (isAnimating)
        {
            animator.SetTrigger(animTriggerString);
        }
    }

    // Exits the dialogue queue
    private void ExitDialogueMode()
    {
        if (audioDialogue != null)
        {
            audioDialogue.Stop();
        }

        Debug.Log("Running");

        dialoguePanel.SetActive(false);
        DialogueIsPlaying = false;
        dialogueText.text = "";
        dialogueComplete = true;

        if(nextLevelTrigger != null)
        {
            nextLevelTrigger.SetActive(true);
        }

        if(SceneManager.GetActiveScene().name == "UpdatedOceanFloor")
        {
            CheckpointManager temp = GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>();
            temp.currentCheckpoint = "";
            GameObject.Find("loading").GetComponent<loading>().LoadNextScene("Level3Test");
        }

        if(SceneManager.GetActiveScene().name == "Level3Test")
        {
            GameObject.Find("loading").GetComponent<loading>().LoadNextScene("4.Arena");
        }

        if (SceneManager.GetActiveScene().name == "4.Arena")
        {
            if (GameObject.Find("BossManager").GetComponent<BossManager>().bossDefeated)
            {
                GameObject.Find("loading").GetComponent<loading>().LoadNextScene("DemoJellyfishCutscene");
            }
        }
        if(SceneManager.GetActiveScene().name == "DemoJellyfishCutscene")
        {
            GameObject.Find("loading").GetComponent<loading>().LoadNextScene("5. JellyfishJump");
            //GameObject.Find("EventSystem").GetComponent<IntroAnimation>().callAscending();
        }

        isAnimating = false;
        animator = null;
        animTriggerString = null;
    }

    // Continues dialogue and uses typewriter effect to display text
    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogue = currentStory.Continue();
            
            // if animating something, animate it.
            if (isAnimating)
            {
                animator.SetTrigger(animTriggerString);
            }
            // Trigger the typewriter effect with the dialogue text
            if (textEffect != null)
            {
                // Set the name of the character currently speaking to the current tag above the current line of dialogue
                List<string> tags = currentStory.currentTags;
                if (tags.Count > 0)
                {
                    dialogueName.text = tags[0];
                }
                textEffect.SetText(dialogue);  // Use the SetText method to update the text and start the effect
                
            }
        }
        else
        {
            ExitDialogueMode();
        }
    }

    // Set animation to play each time dialogue is advanced
    public void SetAnim(Animator p_animator, string triggerString)
    {
        animator = p_animator;
        animTriggerString = triggerString;
        isAnimating = true;
    }
}
