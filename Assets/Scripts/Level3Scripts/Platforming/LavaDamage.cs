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
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(reloadScene());
        }
    }

    //reload the level
    IEnumerator reloadScene()
    {
        mainCamera.GetComponent<CameraFadeOut>().fadeOut = true;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
