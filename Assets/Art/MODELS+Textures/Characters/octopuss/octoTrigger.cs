using UnityEngine;
using System.Collections;

public class OctopusTrigger : MonoBehaviour
{
    public Animator octopusAnimator; // assign in Inspector
    public ParticleSystem sandParticle; // assign in Inspector
    public float detectionRadius = 5f;
    public Transform player;

    //public AudioSource attackAudio; //audio

    private bool isPlayerInside = false;
    private Coroutine attackCoroutine = null;

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        // Check if player is inside the detection radius
        if (distance <= detectionRadius)
        {
            if (!isPlayerInside)
            {
                isPlayerInside = true;
                StartAttackSequence();
            }
        }
        else
        {
            if (isPlayerInside)
            {
                isPlayerInside = false;
                StopAttack();
            }
        }
    }

    void StartAttackSequence()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }

        attackCoroutine = StartCoroutine(DelayedAttack());

       /* if (attackAudio && !attackAudio.isPlaying)
        {
            attackAudio.loop = true;
            attackAudio.Play();
        }*/
    }

    IEnumerator DelayedAttack()
    {
        // Play particle first
        if (sandParticle && !sandParticle.isPlaying)
        {
            sandParticle.Play();
        }

        // Wait 1 second before starting the animation
        yield return new WaitForSeconds(1f);

        // Trigger the animation
        octopusAnimator.SetBool("isAttacking", true);
    }

    void StopAttack()
    {
        // Stop the coroutine if it's running
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }

        // Stop the attack animation
        octopusAnimator.SetBool("isAttacking", false);

       /* if (attackAudio && attackAudio.isPlaying)
        {
            attackAudio.Stop();
        } */
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRadius); // Show detection radius in editor
    }
}
