using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAnimation : MonoBehaviour
{
    public GameObject jellyfish;
    public Animator jellyAnim;

    public TextAsset inkJson;
    public DialogueManager dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(switchAnim());
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    IEnumerator switchAnim()
    {
        yield return new WaitForSeconds(2f);
        jellyAnim.SetBool("IntroDone", true);
        dialogueManager.EnterDialogueMode(inkJson);
    }
}
