using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script handles the opening cutscene that introduces the anglerfish queen
*/
public class OpeningBossCutscene : MonoBehaviour
{
    public Camera mainCamera;
    public Camera cutsceneCamera;
    public GameObject enemy;
    public GameObject player;

    public float moveSpeed = 1f;

    public GameObject Node1;
    public GameObject bossFightSystem;
    public GameObject DialogueManager;

    [SerializeField] private TextAsset inkJson;

    bool cutsceneStarted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(cutscene());
        }
    }

    private void Update()
    {
        if(cutsceneStarted && DialogueManager.GetComponent<DialogueManager>().dialogueComplete)
        {
            player.GetComponent<CommentedThirdPersonController>().velocity = 10;
            mainCamera.enabled = true;
            cutsceneCamera.enabled = false;
            bossFightSystem.SetActive(true);
            enemy.transform.SetPositionAndRotation(enemy.transform.position, new Quaternion(enemy.transform.rotation.x, enemy.transform.rotation.y * -1, enemy.transform.rotation.z, enemy.transform.rotation.w));
            Destroy(this.gameObject);
        }
    }

    IEnumerator cutscene()
    {
        cutsceneStarted = true;

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
