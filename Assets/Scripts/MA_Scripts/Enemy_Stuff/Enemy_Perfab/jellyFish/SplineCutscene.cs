using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineCutscene : MonoBehaviour
{
    public GameObject thePlayer;
    public GameObject cutsceneJF;
    public GameObject cutsceneCamera;

    private Animator jellyFishAnimator;


    private void Start()
    {
        // Get the Animator component from the JellyFish object
        if (cutsceneJF != null)
        {
            jellyFishAnimator = cutsceneJF.GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure only the player triggers this
        {
            cutsceneCamera.SetActive(true);
            cutsceneJF.SetActive(true);
            thePlayer.SetActive(false);

            // Play the JellyFish animation
            if (jellyFishAnimator != null)
            {
                jellyFishAnimator.Play("JellyFish_Animation");
            }
            else
            {
                Debug.LogWarning("No Animator found on cutsceneJF!");
            }
        }
    }
}
