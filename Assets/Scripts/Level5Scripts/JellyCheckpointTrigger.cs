using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyCheckpointTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.Find("JellyCheckpointManager").GetComponent<JellyCheckpointManager>().setCheckpoint(this.gameObject);
        }
    }
}
