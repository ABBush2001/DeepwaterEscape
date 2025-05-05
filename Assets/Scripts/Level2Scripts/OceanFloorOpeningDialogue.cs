using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * This script plays dialogue during the opening of the ocean floor
 * scene.
*/
public class OceanFloorOpeningDialogue : MonoBehaviour
{
    //variables
    public GameObject TextBox;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI dialogueName;

    private CheckpointManager checkpointManager;

    //call the dialogue coroutine
    void Start()
    {
        checkpointManager = GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>();

        if (checkpointManager.currentCheckpoint == "")
        {
            StartCoroutine(openingDialogue());
        }
    }

    //play dialogue
    IEnumerator openingDialogue()
    {
        yield return new WaitForSeconds(3f);
        TextBox.SetActive(true);
        dialogueName.text = "You";
        dialogueText.text = "*groans* Everything hurts...";
        yield return new WaitForSeconds(6f);
        dialogueText.text = "How long did that explosion have me out for...?";
        yield return new WaitForSeconds(6f);
        dialogueText.text = "I need to find a way back to the surface.";
        yield return new WaitForSeconds(6f);
        dialogueText.text = "";
        TextBox.SetActive(false);
    }
}
