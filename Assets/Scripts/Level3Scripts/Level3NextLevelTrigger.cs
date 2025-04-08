using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3NextLevelTrigger : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(fadeToNextScene());
        }
    }

    IEnumerator fadeToNextScene()
    {
        mainCamera.GetComponent<CameraFadeOut>().fadeOut = true;
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("4.Arena");
    }
}
