using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyCheckPoint_Script : MonoBehaviour
{
    public static Transform currentCheckpoint; // Stores the latest checkpoint
    public GameObject thePlayer;
    private bool checkDisable = false;

    private void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            // Update the current checkpoint to this one
            currentCheckpoint = other.transform;
            Debug.Log("Checkpoint updated to: " + currentCheckpoint.name);
        }

        if (other.CompareTag("Zap"))
        {
            thePlayer.transform.position = currentCheckpoint.transform.position;
        }

        if (other.CompareTag("Boss"))
        {
            checkDisable = true;
            Debug.Log("Checkpoints are now disabled.");
        }
    }
}

