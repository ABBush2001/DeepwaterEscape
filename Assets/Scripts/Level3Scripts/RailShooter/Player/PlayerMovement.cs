using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using KBCore.Refs;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputReader input;

    public float movementSpeed = 2f;

    [SerializeField] private GameObject targetPos;
    private Vector3 localPos;

    // Start is called before the first frame update
    void Start()
    {
        transform.SetPositionAndRotation(targetPos.transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        localPos.z += input.GetMove.x * movementSpeed * Time.deltaTime;
        localPos.y += input.GetMove.y * movementSpeed * Time.deltaTime;

        transform.SetPositionAndRotation(new Vector3(targetPos.transform.position.x, localPos.y, localPos.z), transform.rotation);
        
    }
}
