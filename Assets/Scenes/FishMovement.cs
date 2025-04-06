using UnityEngine;

public class Purplefish: MonoBehaviour
{
    public float moveDistance = 20f; 
    public float moveSpeed = 2f; // Adjust speed as needed

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float offset = Mathf.PingPong(Time.time * moveSpeed, moveDistance * 2) - moveDistance;
        transform.position = startPos + new Vector3(offset, 0, 0);
    }
}
