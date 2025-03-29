using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * !!!INCOMPLETE & NON FUNCTIONAL!!!
 * 
 * This handles moving generic gameobjects between waypoints.
 * Code copied from the clam patrolling logic.
*/

public class ObstacleMover : MonoBehaviour
{
    [Tooltip("Does the object return to first waypoint when reaching the end?")]
    public bool isClosedLoop;
    private bool isReversing;
    [Tooltip("Speed of object")]
    public float moveSpeed;
    [Tooltip("'Waypoints' goes here, but you probably shouldn't touch this.")]
    public Transform waypointList;
    private Transform[] waypoints;
    private int waypointIndex = 0;
    private Vector3 waypointTarget;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = new Transform[waypointList.childCount]; // actually intialize array with size of waypointlist
        foreach (Transform t in waypointList) // Get each waypoint in waypointList automagically
        {
            waypoints[waypointIndex] = t;
            waypointIndex++;
        }
        waypointIndex = 0; // We're using this for later, keep it around at 0
        UpdateWaypointDest();
    }


    private void FixedUpdate()
    {
        if (Vector3.Distance(gameObject.transform.position, waypointTarget) < 2f)
        {
            UpdateWaypointDest();
            IterwateWaypointIndex();
        }
    }

    private void IterwateWaypointIndex()
    {
        
        if (isClosedLoop)
        {
            if (isReversing) 
            {
                waypointIndex--;
                if (waypointIndex == 0) {
                    isReversing = false;
                }
            }

            else {
                waypointIndex++;
                if (waypointIndex == waypoints.Length)
                {
                    isReversing = true;
                }
            }

        }
        
        // NOT closed loop
        else {
            waypointIndex++;
            if (waypointIndex == waypoints.Length) {
                waypointIndex = 0; 
            }
        }
    }

    private void UpdateWaypointDest()
    {
        throw new NotImplementedException();
    }
}
