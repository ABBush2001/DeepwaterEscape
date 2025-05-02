using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JellyCheckpointManager : MonoBehaviour
{
    public string currentCheckpoint;

    public JellyCheckpointManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        // Subscribe to sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (currentCheckpoint != "")
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                GameObject temp = GameObject.FindWithTag(currentCheckpoint);

                player.transform.SetPositionAndRotation(new Vector3(temp.transform.position.x, temp.transform.position.y + 5, temp.transform.position.z), temp.transform.rotation);
            }
        }
    }

    public void setCheckpoint(GameObject newCheckpoint)
    {
        currentCheckpoint = newCheckpoint.gameObject.tag;
    }
}
