using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script handles the movement of the door in the submarine scene.
 * It includes the playing of the animation for the door swinging open
*/
public class DoorScript : MonoBehaviour
{
    //variables
    [SerializeField] private Animator myDoor = null;
    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;

    [SerializeField] private GameObject doorHandle;

    //When the bullet shoots the door
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (openTrigger)
            {
                myDoor.Play("DoorOpen", 0, 0.0f);
                gameObject.SetActive(false);
                doorHandle.SetActive(false);
            }

            else if (closeTrigger)
            {
                myDoor.Play("DoorClose", 0, 0.0f);
                gameObject.SetActive(false);
                doorHandle.SetActive(true);
            }
        }
    }
}
