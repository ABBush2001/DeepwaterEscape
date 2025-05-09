using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu2 : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public GameObject otherUI;
    private bool isPaused = false;
    public Shooting shootScript;

    // Define Events for Other Scripts (like Bullet)
    public static event System.Action OnPause;
    public static event System.Action OnResume;

    private void Start()
    {
        if (GameObject.Find("Gun") != null)
        {
            shootScript = GameObject.Find("Gun").GetComponent<Shooting>();
        }
    }

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
        isPaused = true;
        otherUI.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        shootScript.FreezeShoot(true);

        OnPause?.Invoke(); // Broadcast pause event
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        otherUI.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        shootScript.FreezeShoot(false);

        OnResume?.Invoke(); // Broadcast resume event
    }

    public void QuitGame()
    {
        Debug.Log("Quitting!");
        Time.timeScale = 1f;
        Application.Quit();
    }

    public void GoToHome()
    {
        Time.timeScale = 1f;

        //check if a checkpoint manager exists and destroy it if so
        GameObject checkpointManager = null;

        try
        {
            checkpointManager = GameObject.Find("CheckpointManager");
        }catch(Exception e)
        {
            Debug.Log("No checkpoint manager in scene!");
            Debug.LogException(e);
        }

        if(checkpointManager != null)
        {
            Destroy(checkpointManager);
        }

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
