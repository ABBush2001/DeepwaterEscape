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
    public Animator bossAnimator;
    public Animator deathCameraAnimator;


    public GameObject cutscene2;

    public void TriggerBossDeath()
    {
        mainCamera.enabled = false;
        deathCamera.enabled = true;

        bossAnimator.SetTrigger("Death");
        deathCameraAnimator.SetTrigger("Activate");

        healthBarUI.SetActive(false);
        bossTitle.SetActive(false);

        StartCoroutine(ActivateAnimation());
    }

    private IEnumerator ActivateAnimation()
    {
        yield return new WaitForSeconds(bossAnimator.GetCurrentAnimatorStateInfo(0).length);

        deathCamera.enabled = false;
        mainCamera.enabled = true;

        cutscene2.GetComponent<ClosingBossCutscene>().BeginCutscene();
    }
}
