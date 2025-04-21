using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * This script handles the movement from level 3 to the arena.
*/
public class Level3NextLevelTrigger : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //StartCoroutine(fadeToNextScene());
        }
    }

    public void goToNextScene()
    {
        StartCoroutine(fadeToNextScene());
    }

    IEnumerator fadeToNextScene()
    {
        mainCamera.GetComponent<CameraFadeOut>().fadeOut = true;
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("4.Arena");
    }
}
