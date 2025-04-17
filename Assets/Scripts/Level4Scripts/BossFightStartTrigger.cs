using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * (NOW DEFUNCT)
 * This script handles the trigger that begins the boss fight
*/
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
