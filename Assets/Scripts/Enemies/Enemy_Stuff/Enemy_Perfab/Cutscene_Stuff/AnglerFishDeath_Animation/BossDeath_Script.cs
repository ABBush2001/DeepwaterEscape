using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath_Script : MonoBehaviour
{
    // Main Camera and the death camera
    public Camera mainCamera;
    public Camera deathCamera;

    // Disable health and title
    public GameObject healthBarUI;
    public GameObject bossTitle;

    // Animator components
    public Animator bossAnimator;       // For the boss's death animation
    public Animator deathCameraAnimator; // For the death camera animation

    // Next cutscene object
    public GameObject cutscene2;

    // Animation triggers
    private const string BossDeathTrigger = "AFDeath_AMN";
    private const string CameraPlayTrigger = "Play";

    void Start()
    {
        // Ensure proper initialization
        if (mainCamera == null || deathCamera == null || healthBarUI == null || bossTitle == null || bossAnimator == null || deathCameraAnimator == null || cutscene2 == null)
        {
            Debug.LogError("Please assign all references in the inspector.");
        }
    }

    public void BeginCutscene()
    {
        // Disable UI elements
        healthBarUI.SetActive(false);
        bossTitle.SetActive(false);

        // Switch cameras
        mainCamera.enabled = false;
        deathCamera.enabled = true;

        // Trigger the animations
        bossAnimator.SetTrigger(BossDeathTrigger); // Boss death animation
        deathCameraAnimator.SetTrigger(CameraPlayTrigger); // Camera animation

        // Start coroutine to handle the transition to the next cutscene
        StartCoroutine(WaitForAnimationsAndStartCutscene2());
    }

    private IEnumerator WaitForAnimationsAndStartCutscene2()
    {
        // Wait for the boss death animation to finish
        while (bossAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 || bossAnimator.IsInTransition(0))
        {
            yield return null;
        }

        // Wait for the camera animation to finish
        while (deathCameraAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 || deathCameraAnimator.IsInTransition(0))
        {
            yield return null;
        }

        // Start the next cutscene
        cutscene2.GetComponent<ClosingBossCutscene>().BeginCutscene();
    }
}
