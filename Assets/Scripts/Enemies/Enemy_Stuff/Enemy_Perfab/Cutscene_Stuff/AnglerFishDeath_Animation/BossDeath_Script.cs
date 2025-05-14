using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath_Script : MonoBehaviour
{
    public Animator bossAnimator;
    public GameObject cutscene2;

    public void TriggerBossDeath()
    {
        if (bossAnimator != null)
        {
            bossAnimator.SetTrigger("Death"); // Trigger the death animation
        }

        // Optionally start a coroutine if additional timing logic is needed
        StartCoroutine(HandleDeathAnimation());
    }

    private IEnumerator HandleDeathAnimation()
    {
        // Wait until the death animation completes
        if (bossAnimator != null)
        {
            AnimatorStateInfo stateInfo = bossAnimator.GetCurrentAnimatorStateInfo(0);

            // Wait until the death animation finishes
            while (stateInfo.IsName("AFDeath_AMN") && stateInfo.normalizedTime < 1.0f)
            {
                yield return null;
                stateInfo = bossAnimator.GetCurrentAnimatorStateInfo(0);
            }
        }

        // Trigger the next cutscene or action
        if (cutscene2 != null)
        {
            cutscene2.GetComponent<ClosingBossCutscene>().BeginCutscene();
        }
    }
}
