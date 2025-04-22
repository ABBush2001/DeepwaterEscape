using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script handles the closing cutscene for the arena
*/

public class ClosingBossCutscene : MonoBehaviour
{
    public GameObject anglerfish;
    public GameObject dialogueManager;

    public Camera mainCamera;
    public Camera otherCamera;

    public GameObject Node1;

    public float moveSpeed = 1f;

    [SerializeField] private TextAsset inkJson;

    public GameObject healthBarUI;
    public GameObject bossTitle;

    public void BeginCutscene()
    {
        anglerfish.SetActive(true);
        healthBarUI.SetActive(false);
        bossTitle.SetActive(false);
        mainCamera.enabled = false;
        otherCamera.enabled = true;
        StartCoroutine(playCutscene());
    }

    IEnumerator playCutscene()
    {
        while (anglerfish.transform.position != Node1.transform.position)
        {
            anglerfish.gameObject.transform.position = Vector3.MoveTowards(anglerfish.gameObject.transform.position, Node1.transform.position, Time.deltaTime * moveSpeed);
            yield return new WaitForSeconds(0.01f);
        }

        dialogueManager.GetComponent<DialogueManager>().EnterDialogueMode(inkJson);
    }
}
