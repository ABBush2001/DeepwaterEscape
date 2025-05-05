using System;
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
        else if(currentCheckpoint == "Checkpoint1" || currentCheckpoint == "Checkpoint2" || currentCheckpoint == "Checkpoint3")
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {

                GameObject curNode = null;

                try
                {
                    curNode = GameObject.FindWithTag(currentCheckpoint);
                }
                catch (Exception e)
                {
                    Debug.Log("Current Checkpoint not set!");
                }

                player.transform.SetPositionAndRotation(new Vector3(curNode.transform.position.x, curNode.transform.position.y + 5, curNode.transform.position.z), curNode.transform.rotation);
            }
        }
    }

    public void SetCheckpoint(GameObject newCheckpoint)
    {
        currentCheckpoint = newCheckpoint.tag;
    }
}