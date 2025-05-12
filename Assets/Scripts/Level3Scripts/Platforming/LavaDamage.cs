using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * This script handles player collision with the lava in level 3. If a player collides with the lava,
 * the level resets.
*/
public class LavaDamage : MonoBehaviour
{
    //variable
    public Camera mainCamera;

    //check for collision with player and lava
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(reloadScene());
            Debug.Log("Lava Fall!!");
        }
    }

    //reload the level
    IEnumerator reloadScene()
    {
        // Temporarily disable camera collision adjustments
        CommentedCameraController cameraController = mainCamera.GetComponentInParent<CommentedCameraController>();
        if (cameraController != null)
        {
            cameraController.enabled = false; // Disable camera movement and collision logic
        }

        mainCamera.transform.SetParent(null, true); // Detach the camera from the player

        mainCamera.GetComponent<CameraFadeOut>().fadeOut = true; // Trigger the fade-out
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the scene

        if (cameraController != null)
        {
            cameraController.enabled = true; // Re-enable camera movement and collision logic
        }
    }

}
