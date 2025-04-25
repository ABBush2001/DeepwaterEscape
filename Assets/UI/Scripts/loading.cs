using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loading : MonoBehaviour
{

    public GameObject LoadingScreen;
    public Slider slider;

    public GameObject otherUI;

    public void LoadNextScene(string sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
        
    }

    IEnumerator LoadAsynchronously(string sceneIndex)
    {

        otherUI.SetActive(false);
        LoadingScreen.SetActive(true);

        yield return new WaitForSeconds(6f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;

            yield return null;
        }

    }
}
