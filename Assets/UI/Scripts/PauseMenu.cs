using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu2 : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public GameObject otherUI;
    private bool isPaused = false;
    private float timer = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsMenuUI.activeSelf)
            {
                ReturnToPauseMenu(); // Return to pause menu from settings
            }
            else if (isPaused)
            {
                ResumeGame(); // Resume game from pause menu
            }
            else
            {
                PauseGame(); // Pause game from playing state
            }
        }
    }
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Freeze the game
        isPaused = true;

        otherUI.SetActive(false); // Hide the other UI (dialogue box or any other UI)

        // Unlock the cursor and make it visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Unfreeze the game
        isPaused = false;

        otherUI.SetActive(true); // Show the other UI (dialogue box or any other UI) again

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        Time.timeScale = 1f; // Ensure time resumes
        Application.Quit();
    }

    public void GoToHome()
    {
        Time.timeScale = 1f; // Ensure time resumes
        SceneManager.LoadScene("Main"); // Change this to your main menu scene name
    }

    // Method to show the settings menu and hide the pause menu
    public void OpenSettings()
    {
        Debug.Log("TESTING");

        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }

    // Method to return to the pause menu from settings
    public void ReturnToPauseMenu()
    {
        settingsMenuUI.SetActive(false); // Hide settings menu
        pauseMenuUI.SetActive(true); // Show pause menu
        Time.timeScale = 0f; // Pause the game (you might want to adjust this based on your needs)
        isPaused = true; // Indicate that the game is paused
    }
}
