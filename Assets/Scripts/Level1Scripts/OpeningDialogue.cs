using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is called at the beginning of the submarine scene to queue dialogue that sets the premise
 * for the player and guides them on what to do
*/
public class OpeningDialogue : MonoBehaviour
{
    [SerializeField] private TextAsset inkJson;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startDialogue());
    }

    private void Update()
    {
        
    }

    IEnumerator startDialogue()
    {
        yield return new WaitForSeconds(5);
        DialogueManager.GetInstance().EnterDialogueMode(inkJson);
    }

   

}
