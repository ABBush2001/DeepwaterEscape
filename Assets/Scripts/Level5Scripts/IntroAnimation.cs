using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAnimation : MonoBehaviour
{
    public GameObject jellyfish;
    public GameObject env;
    public Animator jellyAnim;

    public TextAsset inkJson;
    public DialogueManager dialogueManager;

    private bool isAscending = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(switchAnim());
    }

    public void callAscending()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isAscending)
        {
        }
    }

    IEnumerator switchAnim()
    {
        yield return new WaitForSeconds(2f);
        jellyAnim.SetBool("IntroDone", true);
        dialogueManager.EnterDialogueMode(inkJson);
    }

}
