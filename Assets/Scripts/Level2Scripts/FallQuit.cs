using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * This script handles resetting the Ocean floor scene
 * when the player falls to the bottom of the canyon
*/
public class FallQuit : MonoBehaviour
{
    //variables
    [SerializeField] GameObject mainCamera;

    //if player collides with exit trigger, begin reload sequence
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(fallTransition());
        }
    }

    //begin fade out then reload level
    IEnumerator fallTransition()
    {
        mainCamera.GetComponent<CameraFadeOut>().fadeOut = true;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("UpdatedOceanfloor");
    }
}
