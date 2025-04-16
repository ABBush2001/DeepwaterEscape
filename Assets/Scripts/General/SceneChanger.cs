using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * This script handles movement between scenes. It can be called
 * with a given scene name
*/
public class SceneChanger : MonoBehaviour
{
    //load a given scene
    public void ChangeTheScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
        Debug.Log("I changed scenes!");
    }

    //reload the current level
    public void ResetLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
