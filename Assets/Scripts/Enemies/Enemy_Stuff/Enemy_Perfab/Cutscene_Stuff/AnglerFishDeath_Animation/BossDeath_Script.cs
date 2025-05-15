using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BossDeath_Script : MonoBehaviour
{
    // Main Camera and the death camera
    public Camera mainCamera;
    //public Camera deathCamera;

    // Disable health and title
    public GameObject healthBarUI;
    public GameObject bossTitle;

    // Animator components
    private Animator bossAnimator;
    //public Animator deathCameraAnimator;
    public GameObject cutscene2;

    public Boss_health boss_HealthScript;
    public GameObject queenDeathModel;

    private Vector3 queenPos = new Vector3(2236.3f, 124f, 1426.3f); // directly from the anim file
    private Quaternion queenRot = new Quaternion(0, 0.987537742f, 0, 0.157382563f); // directly from the anim file

    public void TriggerBossDeath()
    {
        //mainCamera.enabled = false;
        //deathCamera.enabled = true;
        Instantiate(queenDeathModel, queenPos, queenDeathModel.transform.rotation);

        bossAnimator.SetTrigger("Death");
        //deathCameraAnimator.SetTrigger("Activate");

        healthBarUI.SetActive(false);
        bossTitle.SetActive(false);

        StartCoroutine(ActivateAnimation());
    }

    private IEnumerator ActivateAnimation()
    {
        Debug.Log(bossAnimator.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(10f);

        //deathCamera.enabled = false;
        //mainCamera.enabled = true;

        boss_HealthScript.Defeat();
        cutscene2.GetComponent<ClosingBossCutscene>().BeginCutscene();
    }
}
