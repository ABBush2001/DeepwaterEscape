using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JellyCheckPoint_Script : MonoBehaviour
{
    public static Transform currentCheckpoint; // Stores the latest checkpoint
    public GameObject thePlayer;
    //private bool checkDisable = false;

    public string sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            currentCheckpoint = other.transform;
            Debug.Log("Checkpoint updated to: " + currentCheckpoint.name);
        }

        if (other.CompareTag("Zap"))
        {
            if (currentCheckpoint != null)
            {
                thePlayer.transform.position = currentCheckpoint.transform.position;
            }
            else
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}
