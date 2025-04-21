using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public GameObject checkpointText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>().setCheckpoint(this.gameObject);
            StartCoroutine(flashCheckpointText());
        }
    }

    IEnumerator flashCheckpointText()
    {
        checkpointText.SetActive(true);
        yield return new WaitForSeconds(2f);
        checkpointText.SetActive(false);
    }
}
