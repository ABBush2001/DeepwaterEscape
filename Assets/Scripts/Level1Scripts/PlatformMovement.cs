using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public GameObject Node1;
    public GameObject Node2;

    [SerializeField] private int curNode = 1;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float threshold = 0.1f;

    private Vector3 lastPlatformPosition;


    // Update is called once per frame
    void Update()
    {
        // Move the platform towards the target position
        Vector3 targetPosition = (curNode == 1) ? Node1.transform.position : Node2.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Switch nodes when the platform is close enough
        if (Vector3.Distance(transform.position, targetPosition) < threshold)
        {
            curNode = (curNode == 1) ? 2 : 1;
        }

        // Update the platform's position for the next frame
        lastPlatformPosition = transform.position;
    }
}