using UnityEngine;

/*
 * This script handles the movement of the camera for the third person controller.
*/
public class CameraController : MonoBehaviour
{
    // This is the movement of the camera
    public float sensitivity = 5f; // how fast the camera move
    public float smoothCam = 10f; // make the camera movement smooth
    public float lookUpMax = 60f; // the max of the camera angle of the angle
    public float lookUpMin = -60f; // the min of the camera angle of the angle

    public float maxCameraDistance = 5f; // Maximum distance of the camera from the player
    public float minCameraDistance = 1f; // Minimum distance of the camera to avoid clipping
    public LayerMask collisionLayer; // Layers that will block the camera

    // what the screen is click it move the camera 
    public bool clickToMoveCamera = false;

    private float mouseX; // Horizontal camera rotation
    private float mouseY; // Vertical camera rotation
    private Quaternion camRotation; // Rotation of the camera
    private Transform player; // Reference to the player's transform
    private Vector3 defaultCamOffset; // Default offset position of the camera
    private Vector3 currentCamOffset; // Adjusted camera offset after collision detection
    private Transform t_cam; // The actual camera transform

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        // main camera move go to t_camera
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

        mouseX += Input.GetAxis("Mouse X") * sensitivity;
        mouseY -= Input.GetAxis("Mouse Y") * sensitivity;

        mouseY = Mathf.Clamp(mouseY, lookUpMin, lookUpMax);

        // Update rotation based on mouse input
        transform.localRotation = Quaternion.Euler(mouseY, mouseX, 0);

        // camera collision detection
        Vector3 desiredCameraPosition = transform.position + transform.rotation * defaultCamOffset;
        RaycastHit hit;

        if (Physics.Linecast(transform.position, desiredCameraPosition, out hit, collisionLayer))
        {
            // Move camera closer if there's an obstacle
            currentCamOffset = Vector3.ClampMagnitude(defaultCamOffset, hit.distance - 0.2f); // Small offset to avoid clipping
        }
        else
        {
            // if no obstacle the camera will return back to normal 
            currentCamOffset = Vector3.Lerp(currentCamOffset, defaultCamOffset, Time.deltaTime * smoothCam);
        }

        // Apply the updated camera position and collision
        t_cam.localPosition = Vector3.Lerp(t_cam.localPosition, currentCamOffset, Time.deltaTime * smoothCam);

        // Keep the camera parent following the player
        transform.position = player.position;

    }
}
