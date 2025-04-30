using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * This script handles the opening cutscene that introduces the anglerfish queen
*/

public class OpeningBossCutscene : MonoBehaviour
{
    //variables
    public Camera mainCamera;
    public Camera cutsceneCamera;
    public GameObject enemy;
    public GameObject player;
    public GameObject canvas;

    public float moveSpeed = 1f;

    public GameObject Node1;
    public GameObject bossFightSystem;
    public GameObject bossManager;
    public GameObject DialogueManager;

    public TextMeshProUGUI instructionsText;
    public GameObject instructionsBorder;
    public TextMeshProUGUI bossTitle;
    public GameObject bossHealthSlider;


    public AudioSource mainAudio;
    public AudioSource cutsceneAudio;

    [SerializeField] private TextAsset inkJson;

    bool canTrigger = true;
    bool cutsceneStarted = false;

    //start cutscene when the player enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && canTrigger)
        {
            canTrigger = false;
            StartCoroutine(cutscene());
        }
    }

    //Check to see if boss movement is complete to begin dialogue
    private void Update()
    {
        if(cutsceneStarted && DialogueManager.GetComponent<DialogueManager>().dialogueComplete)
        {
            canvas.SetActive(true);
            player.GetComponent<CommentedThirdPersonController>().velocity = 10;
            enemy.transform.Rotate(0, 180, 0);
            mainCamera.enabled = true;
            cutsceneCamera.enabled = false;
            bossFightSystem.SetActive(true);
            enemy.transform.SetPositionAndRotation(enemy.transform.position, new Quaternion(enemy.transform.rotation.x, enemy.transform.rotation.y * -1, enemy.transform.rotation.z, enemy.transform.rotation.w));
            instructionsText.enabled = true;
            instructionsBorder.SetActive(true);
            Time.timeScale = 0.1f;
        }

        //update dialogue if started
        if(instructionsText.enabled && Input.GetKeyDown(KeyCode.Space))
        {
            bossHealthSlider.SetActive(true);
            bossTitle.enabled = true;
            instructionsText.enabled = false;
            instructionsBorder.SetActive(false);
            Time.timeScale = 1f;
            Destroy(this.gameObject);
        }
    }

    //move boss into position
    IEnumerator cutscene()
    {
        cutsceneStarted = true;

        canvas.SetActive(false);

        // Ensure only cutscene audio is playing
        if (mainAudio != null) mainAudio.Stop();
        if (cutsceneAudio != null)
        {
            cutsceneAudio.Stop(); // force reset if looping
            cutsceneAudio.Play();
        }


        //player.GetComponent<CommentedThirdPersonController>().velocity = 0;

        //mainCamera.gameObject.GetComponent<CameraFadeOut>().fadeOut = true;
        //yield return new WaitForSeconds(3f);

        mainCamera.enabled = false;
        cutsceneCamera.enabled = true;

        //cutsceneCamera.gameObject.GetComponent<CameraFadeIn>().fadein = true;

        while(enemy.transform.position.y > Node1.transform.position.y)
        {
            enemy.gameObject.transform.position = Vector3.MoveTowards(enemy.gameObject.transform.position, Node1.transform.position, Time.deltaTime * moveSpeed);
            yield return new WaitForSeconds(0.01f);
        }

        DialogueManager.GetComponent<DialogueManager>().EnterDialogueMode(inkJson);
    }
}
