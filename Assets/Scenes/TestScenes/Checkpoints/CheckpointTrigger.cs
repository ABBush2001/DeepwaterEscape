using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public GameObject checkPointText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>().setCheckpoint(this.gameObject);
        }
    }

    IEnumerator flashText()
    {
        checkPointText.SetActive(true);
        yield return new WaitForSeconds(2f);
        checkPointText.SetActive(false);

    }
}
