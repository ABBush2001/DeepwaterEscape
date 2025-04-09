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
    public GameObject TextBox;
    public TextMeshProUGUI dialogueText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(openingDialogue());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator openingDialogue()
    {
        yield return new WaitForSeconds(3f);
        TextBox.SetActive(true);
        dialogueText.text = "You: *groans* Everything hurts...";
        yield return new WaitForSeconds(6f);
        dialogueText.text = "You: How long did that explosion have me out for?";
        yield return new WaitForSeconds(6f);
        dialogueText.text = "You: I must find my way back to the surface.";
        yield return new WaitForSeconds(6f);
        dialogueText.text = "";
        TextBox.SetActive(false);
    }
}
