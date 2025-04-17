using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimStates : MonoBehaviour
{
    // Camera movement settings
    public float sensitivity = 5f; // How fast the camera moves
    public float smoothCam = 10f; // Smoothing for camera movement
    public float lookUpMax = 60f; // Max upward camera angle
    public float lookUpMin = -60f; // Max downward camera angle

    public float maxCameraDistance = 5f; // Maximum distance from the player
    public float minCameraDistance = 1f; // Minimum distance to avoid clipping
    public LayerMask collisionLayer; // Layers that block the camera

    public bool clickToMoveCamera = false; // Whether camera moves on click

    public Cinemachine.AxisState xAxis, yAxis; // Cinemachine axis states

    [SerializeField] private Transform camFollowPos; // Camera follow position

    private float mouseX; // Horizontal rotation
    private float mouseY; // Vertical rotation
    private Quaternion camRotation; // Camera rotation
    private Transform player; // Reference to the player's transform
    private Vector3 defaultCamOffset; // Default camera offset
    private Vector3 currentCamOffset; // Adjusted offset after collision
    private Transform t_cam; // Actual camera transform

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        // Reference to the main camera
        t_cam = Camera.main.transform;

        defaultCamOffset = t_cam.localPosition;
        currentCamOffset = defaultCamOffset;
        camRotation = transform.localRotation;

        if (!clickToMoveCamera)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Update()
    {
        if (clickToMoveCamera && Input.GetAxisRaw("Fire2") == 0)
        {
            return;
        }

        // Update Cinemachine axis states
        xAxis.Update(Time.deltaTime);
        yAxis.Update(Time.deltaTime);

        // Update mouse input
        mouseX += Input.GetAxis("Mouse X") * sensitivity;
        mouseY -= Input.GetAxis("Mouse Y") * sensitivity;

        mouseY = Mathf.Clamp(mouseY, lookUpMin, lookUpMax);

        // Update rotation based on input
        transform.localRotation = Quaternion.Euler(mouseY, mouseX, 0);

        // Update camera collision detection
        Vector3 desiredCameraPosition = transform.position + transform.rotation * defaultCamOffset;
        RaycastHit hit;

        if (Physics.Linecast(transform.position, desiredCameraPosition, out hit, collisionLayer))
        {
            // Move camera closer to avoid obstacles
            currentCamOffset = Vector3.ClampMagnitude(defaultCamOffset, hit.distance - 0.2f);
        }
        else
        {
            // Return to default offset if no obstacles
            currentCamOffset = Vector3.Lerp(currentCamOffset, defaultCamOffset, Time.deltaTime * smoothCam);
        }

        // Apply updated camera position
        t_cam.localPosition = Vector3.Lerp(t_cam.localPosition, currentCamOffset, Time.deltaTime * smoothCam);

        // Keep the camera parent following the player
        transform.position = player.position;
    }

    private void LateUpdate()
    {
        // Synchronize Cinemachine's axis states with camera follow position
        camFollowPos.localEulerAngles = new Vector3(yAxis.Value, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.localEulerAngles.x, xAxis.Value, transform.localEulerAngles.z);
    }
}

