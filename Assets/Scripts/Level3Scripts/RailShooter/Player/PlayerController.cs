using KBCore.Refs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //[SerializeField, Self] private InputReader input;

    [Tooltip("The target spline path which the object will move along")]
    public Transform followTarget;
    [Tooltip("How far the object lags behind it's Follow Target")]
    public float followDistance = 2f;
    [Tooltip("Use to clamp players horizontal/vertical movement with different values")]
    public Vector2 movementLimit = new Vector2(2f, 2f);
    [Tooltip("How fast (and far) the player can adjust the object vertically/horizontally")]
    public float movementSpeed = 10f;
    [Tooltip("The total time that all of the object's movement is smoothed over")]
    public float smoothTime = 0.2f;

    [Tooltip("The maximum allowed roll of the object when player is moving vertically/horizontally")]
    public float maxRoll = 15f;
    [Tooltip("The speed at which the object approaches maxRoll")]
    public float rollSpeed = 2f;

    private Vector3 velocity;
    private float roll;

    private Vector3 targetPos;
    private Vector3 smoothedPos;
    private Vector3 localPos;

    // Update is called once per frame
    private void Update()
    {
        // Calculate target position based on follow distance and target's position
        targetPos = followTarget.position + (followTarget.forward * -followDistance);
        transform.SetPositionAndRotation(targetPos, followTarget.rotation);

        /*
        localPos.z += input.GetMove.x * movementSpeed * Time.deltaTime;
        localPos.y += input.GetMove.y * movementSpeed * Time.deltaTime;

        transform.SetPositionAndRotation(new Vector3(targetPos.x, localPos.y, localPos.z), followTarget.rotation);
        */
        /*
        // Apply smooth damp to the players position
        smoothedPos = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);

        // Calculate the new local position from world position, AKA convert from worldspace to localspace
        localPos = transform.InverseTransformPoint(smoothedPos);
        localPos.x += input.GetMove.x * movementSpeed * Time.deltaTime * movementLimit.x;
        localPos.y += input.GetMove.y * movementSpeed * Time.deltaTime * movementLimit.y;

        // Clamp the local position between negative and positive movement range
        localPos.x = Mathf.Clamp(localPos.x, -movementLimit.x, movementLimit.x);
        localPos.y = Mathf.Clamp(localPos.y, -movementLimit.y, movementLimit.y);

        // Update the player's position and match player's rotation to that of the follow target
        // (so you actually look at where you're going)
        transform.SetPositionAndRotation(transform.TransformPoint(localPos), followTarget.rotation);

        // Match the roll based on player input
        roll = Mathf.Lerp(roll, maxRoll * input.GetMove.x, rollSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, roll);
        */
    }
}
