using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightStartTrigger : MonoBehaviour
{
    public GameObject bossFight;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            bossFight.SetActive(true);
        }
    }
}
