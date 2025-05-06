using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class EndCreditsController : MonoBehaviour
{
    public float creditsDuration = 20f;
    public float fadeDuration = 2f;
    public string mainMenuSceneName = "Main";

    [Header("UI")]
    public Image fadeImage; 

    void Start()
    {
        StartCoroutine(HandleCreditsSequence());
    }

    IEnumerator HandleCreditsSequence()
    {
        //if checkpoint manager exists, destroy it
        if(GameObject.Find("CheckpointManager") != null)
        {
            Destroy(GameObject.Find("CheckpointManager"));
        }

        // Wait for the duration of the credits
        yield return new WaitForSeconds(creditsDuration);

        // Start fading to black
        yield return StartCoroutine(FadeToBlack());

        // Load the main menu
        SceneManager.LoadScene(mainMenuSceneName);
    }

    IEnumerator FadeToBlack()
    {
        float elapsed = 0f;
        Color color = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            float t = elapsed / fadeDuration;
            fadeImage.color = new Color(color.r, color.g, color.b, Mathf.Lerp(0f, 1f, t));
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure it's fully opaque
        fadeImage.color = new Color(color.r, color.g, color.b, 1f);
    }
}
