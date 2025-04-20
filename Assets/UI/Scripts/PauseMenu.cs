using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu2 : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public GameObject otherUI;
    public GameObject camera;
    private bool isPaused = false;

    // Define Events for Other Scripts (like Bullet)
    public static event System.Action OnPause;
    public static event System.Action OnResume;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsMenuUI.activeSelf)
            {
                ReturnToPauseMenu();
            }
            else if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        camera.gameObject.GetComponent<CommentedCameraController>().enabled = false;
        isPaused = true;
        otherUI.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        OnPause?.Invoke(); // Broadcast pause event
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        otherUI.SetActive(true);
        camera.gameObject.GetComponent<CommentedCameraController>().enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        OnResume?.Invoke(); // Broadcast resume event
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSfwdjz4HT0iWeojGLPPhOp7fo7Z4mVy0J8iz__-lf81F_aDhA/viewform?usp=header");
        Application.Quit();
    }

    public void GoToHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main");
    }

    public void OpenSettings()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }

    public void ReturnToPauseMenu()
    {
        settingsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
}
