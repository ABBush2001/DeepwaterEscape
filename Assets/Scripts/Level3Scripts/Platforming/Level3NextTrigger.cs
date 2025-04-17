using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * This script handles the transition from level 3 to the Arena.
*/
public class Level3NextTrigger : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Collided!");

            StartCoroutine(fadeToNextScene());
        }
    }


    public void GoToNextScene()
    {
        StartCoroutine(fadeToNextScene());
    }

    IEnumerator fadeToNextScene()
    {
        mainCamera.GetComponent<CameraFadeOut>().fadeOut = true;
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("4.Arena");
    }
}
