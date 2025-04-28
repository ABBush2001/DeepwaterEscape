using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour
{
    public Camera mainCamera;
    public string sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(fadeToCredits());
        }
    }

    IEnumerator fadeToCredits()
    {
        mainCamera.GetComponent<CameraFadeOut>().fadeOut = true;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneToLoad);
    }
}
