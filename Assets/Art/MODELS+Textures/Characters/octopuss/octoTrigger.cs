using UnityEngine;

public class OctopusTrigger : MonoBehaviour
{
    public Animator octopusAnimator; // assign in Inspector
    public ParticleSystem sandParticle; // assign in Inspector
    public float detectionRadius = 5f;
    public Transform player;

    private bool isPlayerInside = false;

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        // Check if player is inside the detection radius
        if (distance <= detectionRadius)
        {
            if (!isPlayerInside)
            {
                // Player has entered the radius, start attack
                isPlayerInside = true;
                StartAttack();
            }
        }
        else
        {
            if (isPlayerInside)
            {
                // Player has left the radius, stop attack
                isPlayerInside = false;
                StopAttack();
            }
        }
    }

    void StartAttack()
    {
        // Set the isAttacking flag to true to trigger the animation in Animator
        octopusAnimator.SetBool("isAttacking", true);

        // Play particle effect ONCE when the attack begins
        if (sandParticle && !sandParticle.isPlaying)
        {
            sandParticle.Play();
        }
    }

    void StopAttack()
    {
        // Set the isAttacking flag to false to stop the attack animation
        octopusAnimator.SetBool("isAttacking", false);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRadius); // Show detection radius in editor
    }
}
