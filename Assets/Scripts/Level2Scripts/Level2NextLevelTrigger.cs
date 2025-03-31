using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2NextLevelTrigger : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Collided!");

            StartCoroutine(fadeToNextScene());
        }
    }

    IEnumerator fadeToNextScene()
    {
        mainCamera.GetComponent<CameraFadeOut>().fadeOut = true;
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("DEMOOnRails");
    }
}
