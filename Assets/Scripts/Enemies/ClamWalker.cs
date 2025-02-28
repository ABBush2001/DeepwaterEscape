using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/*
 * This script handles the clam 
*/

public class Clam_Walker : MonoBehaviour
{
    private bool isAggro = false;
    public NavMeshAgent clamNavAgent;
    public Transform navTarget;


    // Update is called once per frame
    void Update()
    {
        if (isAggro) {
            clamNavAgent.destination = navTarget.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isAggro = true;
        }
    }
}
