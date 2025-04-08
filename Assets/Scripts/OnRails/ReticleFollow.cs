using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleFollow : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get the target object's position

        Vector3 targetPosition = player.transform.position;

        // Set the UI element's position based on the target

        transform.position = targetPosition + new Vector3(0, 2, 0); // Add an offset if needed

        Quaternion targetRotation = player.transform.rotation;

        transform.rotation = targetRotation;
    }
}
