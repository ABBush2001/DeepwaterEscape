using System.Collections;
using UnityEngine;

public class BossDeath_Script : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject healthBarUI;
    public GameObject bossTitle;
    public Camera otherCamera;

    private Animator bossAnimator;
    public GameObject cutscene2;
    public Boss_health boss_HealthScript;
    public GameObject queenDeathModel;

    private Vector3 queenPos = new Vector3(2236.3f, 124f, 1426.3f);
    private Quaternion queenRot = new Quaternion(0, 0.987537742f, 0, 0.157382563f);

    public void TriggerBossDeath()
    {
        mainCamera.enabled = false;

        if (otherCamera != null)
        {
            otherCamera.gameObject.SetActive(true);
            otherCamera.enabled = true; // Ensure the Camera component is enabled
        }


        Debug.Log("TriggerBossDeath called!");

        if (queenDeathModel != null)
        {
            Instantiate(queenDeathModel, queenPos, queenDeathModel.transform.rotation);
        }
        else
        {
            Debug.LogError("queenDeathModel is not assigned!");
        }

        if (bossAnimator != null)
        {
            bossAnimator.SetTrigger("Death");
            Debug.Log("Death trigger set on bossAnimator.");
        }
        else
        {
            Debug.LogError("bossAnimator is not assigned!");
        }

        healthBarUI.SetActive(false);
        bossTitle.SetActive(false);

        StartCoroutine(ActivateAnimation());
    }

    private IEnumerator ActivateAnimation()
    {
        //Debug.Log("ActivateAnimation started.");
        float animLength = bossAnimator != null ? bossAnimator.GetCurrentAnimatorStateInfo(0).length : 10f;
        //Debug.Log($"Waiting for {animLength} seconds.");
        yield return new WaitForSeconds(animLength);

        //Debug.Log("Activating cutscene.");
        if (cutscene2 != null && cutscene2.TryGetComponent<ClosingBossCutscene>(out var cutsceneComponent))
        {
            cutsceneComponent.BeginCutscene();
        }
        else
        {
            Debug.LogError("cutscene2 or ClosingBossCutscene is missing!");
        }
    }
}

