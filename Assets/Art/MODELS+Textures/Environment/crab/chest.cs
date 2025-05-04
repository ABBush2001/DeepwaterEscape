using System.Collections;
using UnityEngine;

public class TreasureChestTrigger : MonoBehaviour
{
    public Animator chestAnimator; // assign in Inspector
    public ParticleSystem bubbleEffect; // assign your bubble particle here
    public float detectionRadius = 3f;
    public Transform player;

    //public AudioSource openSound; // optional sound

    private bool isPlayerInside = false;
    private bool isOpened = false;

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        // Check if player is inside the detection radius
        if (distance <= detectionRadius)
        {
            if (!isPlayerInside && !isOpened)
            {
                isPlayerInside = true;
                OpenChest();
            }
        }
        else
        {
            if (isPlayerInside)
            {
                isPlayerInside = false;
                // Chest stays open
            }
        }
    }

    void OpenChest()
    {
        isOpened = true;

        // Play bubble effect
        if (bubbleEffect && !bubbleEffect.isPlaying)
        {
            bubbleEffect.Play();
        }

        // Trigger the chest opening animation
        chestAnimator.SetTrigger("Open");

        /* // Optional sound
        if (openSound)
        {
            openSound.Play();
        }
        */
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
