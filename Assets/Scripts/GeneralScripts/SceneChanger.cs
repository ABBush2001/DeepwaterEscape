using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeTheScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
        Debug.Log("I changed scenes!");
    }

    public void ResetLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
