using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * This script handles movement from the ocean floor scene
 * to level 3.
*/
public class Level2NextLevelTrigger : MonoBehaviour
{
    //variable
    [SerializeField] GameObject mainCamera;

    public GameObject loading;

    //check to see if player has triggered the collider
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Collided!");

            StartCoroutine(fadeToNextScene());
        }
    }

    //move to next scene
    public void GoToNextScene()
    {
        StartCoroutine(fadeToNextScene());
    }

    //call scene manager to load the next scene
    IEnumerator fadeToNextScene()
    {
        //mainCamera.GetComponent<CameraFadeOut>().fadeOut = true;
        //yield return new WaitForSeconds(4);
        //SceneManager.LoadScene("Level3Test");
        loading.GetComponent<loading>().LoadNextScene(15);
        yield return null;
    }
}
