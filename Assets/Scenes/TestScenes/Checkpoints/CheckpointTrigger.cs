using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>().setCheckpoint(this.gameObject);
        }
    }
}
