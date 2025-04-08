using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LavaDamage : MonoBehaviour
{
    public Camera mainCamera;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(reloadScene());
        }
    }

    IEnumerator reloadScene()
    {
        mainCamera.GetComponent<CameraFadeOut>().fadeOut = true;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
