using UnityEngine;

/*
 * This script can be used to move fish back and forth from a certain starting point
 * to a given distance
*/
public class Purplefish: MonoBehaviour
{
    //variables
    public float moveDistance = 20f; 
    public float moveSpeed = 2f; // Adjust speed as needed

    private Vector3 startPos;

    //set starting position
    void Start()
    {
        startPos = transform.position;
    }

    //ping pong the position back and forth
    void Update()
    {
        float offset = Mathf.PingPong(Time.time * moveSpeed, moveDistance * 2) - moveDistance;
        transform.position = startPos + new Vector3(offset, 0, 0);
    }
}
